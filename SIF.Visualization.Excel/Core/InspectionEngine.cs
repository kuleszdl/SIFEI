using System;
using System.IO;
using System.Net.Http;
using SIF.Visualization.Excel.Helper;
using SIF.Visualization.Excel.Properties;

namespace SIF.Visualization.Excel.Core
{
    public class InspectionEngine
    {
        #region Methods

        /// <summary>
        ///     This async method is called by the workbook model. it will be a silent running request
        /// </summary>
        internal async void doInspection(WorkbookModel workbook, string policyFile, string spreadsheetFile)
        {
            // initalize response string
            string responseString = null;

            // open policy and spreadsheet files save temporarily
            var policyStream = File.Open(policyFile, FileMode.Open);
            HttpContent policyContent = new StreamContent(policyStream);
            var spreadsheetStream = File.Open(spreadsheetFile, FileMode.Open);
            HttpContent spreadsheetContent = new StreamContent(spreadsheetStream);

            // Submit the form using HttpClient and 
            // create form data as Multipart (enctype="multipart/form-data")
            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                // Add the HttpContent objects to the form data
                // <input type="text" name="filename" />
                formData.Add(policyContent, "policy", policyFile);
                formData.Add(spreadsheetContent, "spreadsheet", spreadsheetFile);

                // Actually invoke the request to the server
                // equivalent to (action="{url}" method="post")
                try
                {
                    var response = client.PostAsync(Settings.Default.SifServerUrl + "/ooxml", formData).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        // get the responding xml as string
                        responseString = await response.Content.ReadAsStringAsync();
                        var fileName = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                       Path.DirectorySeparatorChar + "inspectionResponse.xml";
                        File.WriteAllText(fileName, responseString);
                    }
                }
                catch (Exception)
                {
                    ScanHelper.ScanUnsuccessful(Resources.Error_NoConnectionToServer);
                    return;
                }
            }

            workbook.Load(responseString);
        }

        #endregion

        #region Singleton

        private static volatile InspectionEngine instance;
        private static readonly object syncRoot = new object();

        private InspectionEngine()
        {
        }

        /// <summary>
        ///     Gets the current server instance.
        /// </summary>
        public static InspectionEngine Instance
        {
            get
            {
                if (instance == null)
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new InspectionEngine();
                    }

                return instance;
            }
        }

        #endregion
    }
}