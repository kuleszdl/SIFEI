using SIF.Visualization.Excel.Core;
using System;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Windows;
using System.IO;
using SIF.Visualization.Excel.Properties;

namespace SIF.Visualization.Excel.Networking
{
    public class XMLPartManager
    {
        #region Singleton

        private static volatile XMLPartManager instance;
        private static object syncRoot = new Object();
        private XmlSchemaSet report, request;

        private XMLPartManager()
        {
        }

        /// <summary>
        /// Gets the current XML part manager instance.
        /// </summary>
        public static XMLPartManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new XMLPartManager();
                    }
                }

                return instance;
            }
        }

        #endregion

        #region Methods
        public XElement LoadXMLPart(WorkbookModel workbook, string id)
        {
            var part = GetCustomXLPart(workbook, id);

            if (part != null)
            {
                var result = XElement.Parse(part.XML).Elements().First();
                Debug.WriteLine(result.ToString());
                return result;
            }
            else
            {
                return null;
            }
        }

        public void SaveXMLPart(WorkbookModel workbook, XElement root, string id)
        {
            if (root == null) return;

            var masterRoot = new XElement(id);
            masterRoot.Add(new XAttribute("company", "University of Stuttgart, ISTE"));
            masterRoot.Add(new XAttribute("product", "Spreadsheet Inspection Framework (SIF"));
            masterRoot.Add(root);

            //clear old
            var oldPart = GetCustomXLPart(workbook, id);
            if (oldPart != null)
            {
                oldPart.Delete();
            }

            //save
            var scenarioXMLPart = workbook.Workbook.CustomXMLParts.Add(masterRoot.ToString());
            Debug.WriteLine(masterRoot.ToString());
        }

        private Microsoft.Office.Core.CustomXMLPart GetCustomXLPart(WorkbookModel workbook, string id)
        {
            Microsoft.Office.Core.CustomXMLPart resultPart = null;
            foreach (Microsoft.Office.Core.CustomXMLPart part in workbook.Workbook.CustomXMLParts)
            {
                try
                {
                    var xml = XElement.Parse(part.XML);
                    if (xml.Name == XName.Get(id))
                    {
                        resultPart = part;
                        break;
                    }
                }
                catch (Exception e)
                { Console.WriteLine(e.Message); }
            }

            return resultPart;
        }

        public XmlSchema ReadXMLSchemaFromFile(string filename)
        {
            try
            {
                XmlTextReader reader = new XmlTextReader(filename);
                XmlSchema myschema = XmlSchema.Read(reader, ValidationCallback);

                return myschema;
            }
            catch (Exception)
            {
                return null;
            }

        }

        /// <summary>
        /// Creates and returns the XML Schema definition for the SpRuDeL requests
        /// with a ValidationCallback which reports errors to the UI.
        /// </summary>
        /// <returns>The corresponding XmlSchemaSet</returns>
        public XmlSchemaSet GetRequestSchema()
        {
            if (request == null)
            {
                var sprudel = XmlReader.Create(new StringReader(SchemaStrings.getRequestXSD()));
                request = new XmlSchemaSet();
                request.Add(string.Empty, sprudel);
                request.ValidationEventHandler += ValidationCallback;
            }
            return request;
        }

        /// <summary>
        /// Creates and returns the XML Schema definition for the SpRuDeL reports
        /// with a ValidationCallback which reports errors to the UI.
        /// </summary>
        /// <returns>The corresponding XmlSchemaSet</returns>
        public XmlSchemaSet getReportSchema(){
            if (report == null)
            {
                var sprudel = XmlReader.Create(new StringReader(SchemaStrings.getReportXSD()));
                report = new XmlSchemaSet();
                report.Add(string.Empty, sprudel);
                report.ValidationEventHandler += ValidationCallback;
            }
            return report;
        } 

        public void ValidationCallback(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                Debug.Write("WARNING ValidationCallback: ");
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                Debug.Write("ERROR ValidationCallback: ");
                MessageBox.Show(Resources.tl_ValidationError + e.Message, Resources.tl_MessageBox_Error);
            }

            Debug.WriteLine(e.Message);
        }

        #endregion
    }
}
