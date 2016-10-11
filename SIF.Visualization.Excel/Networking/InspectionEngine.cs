using System.Runtime.InteropServices;
using SIF.Visualization.Excel.Core;
using SIF.Visualization.Excel.Properties;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using SIF.Visualization.Excel.Helper;

namespace SIF.Visualization.Excel.Networking
{
    /// <summary>
    /// This class provides a socket server that is able to communicate with the Spreadsheet Inspection Framework.
    /// </summary>
    public class InspectionEngine
    {
        /// <summary>
        /// Saves for a short time if an MemoryRestriction Exception got raised
        /// </summary>
        private bool _hadMemoryRestriction = false;

        #region Singleton

        private static volatile InspectionEngine instance;
        private static object syncRoot = new Object();

        private InspectionEngine()
        {
            // Initialize the default port
            Port = Settings.Default.DefaultPort;

            // Create the inspection queue
            InspectionQueue = new BlockingCollection<InspectionJob>();
        }

        /// <summary>
        /// Gets the current server instance.
        /// </summary>
        public static InspectionEngine Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new InspectionEngine();
                    }
                }

                return instance;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the state of the inspection engine.
        /// </summary>
        public InspectionEngineState State { get; private set; }

        /// <summary>
        /// Gets the inspection queue.
        /// </summary>
        public BlockingCollection<InspectionJob> InspectionQueue { get; private set; }

        /// <summary>
        /// Gets or sets the server thread.
        /// </summary>
        protected Thread ServerThread { get; private set; }

        /// <summary>
        /// Gets or sets the server socket.
        /// </summary>
        protected TcpListener TcpServer { get; private set; }

        /// <summary>
        /// Gets or sets the local port.
        /// </summary>
        public ushort Port { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Setup and start the socket server.
        /// </summary>
        public void Start()
        {
            // Will be true, if the socket server could start up successfully.
            var isStarted = false;

            // Try to connect, repeat until a free port is found.
            while (!isStarted)
            {
                try
                {
                    // Try to bind the socket to the standard or the incremented port
                    TcpServer = new TcpListener(new IPEndPoint(IPAddress.Loopback, Port));

                    // Try and start the socket server.
                    TcpServer.Start();

                    // If there was no exception, the socket server is now connected.
                    isStarted = true;

                    // Create and start the server thread.
                    ServerThread = new Thread(new ThreadStart(ServerRunLoop));
                    ServerThread.IsBackground = true;
                    ServerThread.Start();

                    // Notify that the server is now up and running.
                    State = InspectionEngineState.Waiting;
                }
                catch (Exception)
                {
                    // Increment the port number by one.
                    Port++;
                }
            }
        }

        /// <summary>
        /// Stops the socket server.
        /// </summary>
        public void Stop()
        {
            if (TcpServer != null)
            {
                TcpServer.Stop();
                TcpServer = null;
            }

            State = InspectionEngineState.NotRunning;

            ServerThread.Abort();
        }

        /// <summary>
        /// Returns a value that indicates, whether the specified socket is connected or not.
        /// </summary>
        public static bool IsSocketConnected(Socket socket)
        {
            try
            {
                bool firstCondition = socket.Poll(1000, SelectMode.SelectRead);
                bool secondCondition = (socket.Available == 0);
                return !(firstCondition && secondCondition);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// This method is the server run loop.
        /// </summary>
        protected void ServerRunLoop()
        {
            TcpListener currentTcpListener = TcpServer;
            try
            {
                // The server will keep running until its thread is aborted.
                while (true)
                {
                    if (!File.Exists(Settings.Default.FrameworkPath + Path.DirectorySeparatorChar + "sif.jar"))
                    {
                        // Sif has not been installed correctly.
                        MessageBox.Show(Resources.tl_Path_missing
                                        +
                                        Settings.Default.FrameworkPath + Path.DirectorySeparatorChar +
                                        "sif.jar\n\n" + Resources.tl_Path_install, Resources.tl_MessageBox_Error);
                    }

                    // Launch a new instance of the Spreadsheet Inspection Framework
                    var startInfo = new ProcessStartInfo("cmd",
                        "/q /c java -jar \"" + Settings.Default.FrameworkPath + Path.DirectorySeparatorChar +
                        "sif.jar\" "
                        + Settings.Default.SifOptions + " " + Instance.Port);
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    Process.Start(startInfo);

                    // Wait for the client to connect.
                    var clientSocket = currentTcpListener.AcceptSocket();

                    #region Functionality

                    while (IsSocketConnected(clientSocket))
                    {
                        // Get the next inspection job from the inspection queue
                        var currentJob = InspectionQueue.Take();

                        try
                        {
                            // At first, send the policy as generated Xml
                            var writer = new StringWriter();
                            currentJob.PolicyXML.Save(writer);
                            clientSocket.SendString(writer.GetStringBuilder().ToString());
                            // Read the report from the socket connection

                            var report = clientSocket.ReadString();

                            // Let the inspection job know about the report
                            currentJob.Finalize(report);
                        }
                        catch (OutOfMemoryException)
                        {
                            // Try to release as many resources as possible#
                            ScanHelper.ScanUnsuccessful(Resources.tl_MemoryRestrictions + "\n" + Resources.tl_StartNewScan);
                            _hadMemoryRestriction = true;
                            currentJob.DeleteWorkbookFile();
                            foreach (InspectionJob job in InspectionQueue)
                            {
                                job.DeleteWorkbookFile();
                            }
                            InspectionQueue.Dispose();
                            InspectionQueue = new BlockingCollection<InspectionJob>();
                            
                            // restart the server loop
                            throw new Exception();
                        }
                        catch (Exception e)
                        {
                            if (!_hadMemoryRestriction) ScanHelper.ScanUnsuccessful();
                            Start();
                        }
                    }

                    #endregion
                }
            }
            catch (ExternalException) // Java is not on the path
            {
                if (Globals.Ribbons.Ribbon.scanButton != null && Globals.Ribbons.Ribbon != null &&
                    Globals.Ribbons != null)
                {
                    ScanHelper.ScanUnsuccessful(Resources.tl_No_Java_Enviroment);
                }
                else
                {
                    MessageBox.Show(
                    Resources.tl_No_Java_Enviroment,
                    Resources.tl_MessageBox_Error);
                }
            }
            catch (Exception e)
            {
                
                try
                {
                    ScanHelper.ScanUnsuccessful();
                }
                    //Catch if Ribbon was never instantiated 
                catch (NullReferenceException ex)
                {
                    // Quietly swallow exception
                }
                // start will fork into a new thread
                Start();
                // we will die in a short while, nothing to be done
            }
            finally
            {
                currentTcpListener.Stop();
            }
        }

        #endregion
    }
}
