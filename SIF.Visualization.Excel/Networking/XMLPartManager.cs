using SIF.Visualization.Excel.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace SIF.Visualization.Excel.Networking
{
    public class XMLPartManager
    {
        #region Singleton

        private static volatile XMLPartManager instance;
        private static object syncRoot = new Object();

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
            var part = this.GetCustomXLPart(workbook, id);

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
            var oldPart = this.GetCustomXLPart(workbook, id);
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

        public void ValidationCallback(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                Debug.Write("WARNING ValidationCallback: ");
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                Debug.Write("ERROR ValidationCallback: ");
            }

            Debug.WriteLine(e.Message);
        }

        #endregion
    }
}
