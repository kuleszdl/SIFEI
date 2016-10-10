using Microsoft.Office.Interop.Excel;
using SIF.Visualization.Excel.Core;
using SIF.Visualization.Excel.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SIF.Visualization.Excel.Cells
{
    /// <summary>
    /// The cell manager provides useful cell selection methods.
    /// </summary>
    public class CellManager
    {
        #region Singleton

        private static volatile CellManager instance;
        private static object syncRoot = new Object();

        private CellManager()
        {
        }

        /// <summary>
        /// Gets the current cell manager instance.
        /// </summary>
        public static CellManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new CellManager();
                    }
                }

                return instance;
            }
        }

        #endregion

        #region cell names

        /// <summary>
        /// Gets the sif cell name of a cell location in a1style
        /// </summary>
        /// <param name="wb">workbook model</param>
        /// <param name="a1Adress">cell location in a1 style</param>
        /// <returns>sif cell name</returns>
        public String GetSIFCellName(WorkbookModel wb, String a1Adress)
        {
            // Important: There might be more than just one name for this cell!
            var name = new CellLocation(wb.Workbook, a1Adress).ScenarioNames.FirstOrDefault();

            if (name != null) return name.Name;
            else return string.Empty;

        }

        /// <summary>
        /// Gets all false positive cell names for the specified location.
        /// </summary>
        /// <param name="workbook">The workbook containing the cell.</param>
        /// <param name="address">The address inside the workbook in A1 notation (e.g. "Rechner!A34").</param>
        /// <returns>A list containing false positive names referencing the given address.</returns>
        public IEnumerable<Name> GetFalsePositiveCellNames(Workbook workbook, string address)
        {
            return new CellLocation(workbook, address).FalsePositiveNames;
        }

        /// <summary>
        /// Gets the user name of a cell location in a1style
        /// </summary>
        /// <param name="wb">workbook model</param>
        /// <param name="a1Adress">cell location in a1 style</param>
        /// <returns>if a user name is defined: user name, else cell location</returns>
        public String GetUserCellName(WorkbookModel wb, String a1Adress)
        {
            // Without FalsePositive.* and SIF.Visualization.*
            var name = GetUserCellNames(wb.Workbook, a1Adress).FirstOrDefault();

            if (name != null) return name.Name;
            return a1Adress;

        }

        /// <summary>
        /// Returns the cell names of a certain address that were neither assigned as being false positives nor as being scenario cells.
        /// </summary>
        /// <param name="workbook">The workbook with the cell inside.</param>
        /// <param name="address">The address to a cell in a somewhat A1-like notation (e.g. "Rechner!A36").</param>
        /// <returns>The user cell names.</returns>
        public IEnumerable<Name> GetUserCellNames(Workbook workbook, string address)
        {
            return new CellLocation(workbook, address).UserNames;
        }

        /// <summary>
        /// Gets the user name of a cell location in sif name style
        /// </summary>
        /// <param name="wb">workbook model</param>
        /// <param name="sifName">sif cell name</param>
        /// <returns>if a user name is defined: user name, else cell location</returns>
        public String GetUserCellNameWithSIFName(WorkbookModel wb, String sifName)
        {
            var a1Adress = GetA1Adress(wb, sifName);

            return GetUserCellName(wb, a1Adress);
        }

        /// <summary>
        /// Get the a1Adress of a cell name
        /// </summary>
        /// <param name="wb">workbook model</param>
        /// <param name="name">cell location name</param>
        /// <returns>cell location in a1 style</returns>
        public String GetA1Adress(WorkbookModel wb, String name)
        {
            foreach (Name n in wb.Workbook.Application.Names)
            {
                if (n.Name == name)
                {
                    return n.RefersTo as String;
                }
            }

            return name;
        }

        /// <summary>
        /// Creates a invisible sif cell name with the properties cell name tag and a guid (without '-')
        /// ex. SIF.Visualisation.0f8fad5bd9cb469fa16570867728950e
        /// </summary>
        /// <returns></returns>
        public String CreateSIFCellName(WorkbookModel wb, String a1Adress)
        {
            return new CellLocation(wb.Workbook, a1Adress).AddName(Settings.Default.CellNameTag, false).Name;

        }

        public String ParseWorksheetName(String a1Adress)
        {
            var startIndex = a1Adress.IndexOf("=") + 1;
            var endIndex = a1Adress.IndexOf("!") - 1;

            return a1Adress.Substring(startIndex, endIndex - startIndex + 1);
        }

        
        public String ParseCellLocation(String a1Adress)
        {
            return a1Adress.Substring(a1Adress.IndexOf("!") + 1);
        }
        #endregion

        #region cell selection

        /// <summary>
        /// Selects a cell in a workbook
        /// </summary>
        /// <param name="wb">workbook model</param>
        /// <param name="location">location of the cell to select</param>
        public void SelectCell(WorkbookModel wb, string location)
        {
            new CellLocation(wb.Workbook, location).Select();
        }

        /// <summary>
        /// Gets the selected cells in the current workbook
        /// </summary>
        /// <param name="wb">workbook model</param>
        /// <returns>List of Cell</returns>
        public List<Cell> GetSelectedCells(WorkbookModel wb)
        {
            var cellList = new List<Cell>();

            Range selectedCells = (wb.Workbook.Application.Selection as Range).Cells;

            Debug.WriteLine("SELECTED CELLS: Creating List ...");
            DateTime start = DateTime.Now;

            foreach (var c in selectedCells.Cells)
            {
                var currentCell = c as Range;
                String currentLocation = "=" + (currentCell.Parent as Worksheet).Name as String + "!" + currentCell.Address as String;
                var selectedCell = new Cell()
                {
                    Id = Convert.ToInt32(currentCell.ID),
                    Location = GetUserCellName(wb, currentLocation),
                    SifLocation = GetSIFCellName(wb, currentLocation),
                    Content = currentCell.Formula as String
                };

                cellList.Add(selectedCell);
            }

            Debug.WriteLine("SELECTED CELLS: List created! Time: " + (DateTime.Now - start).ToString() + ", Items: " + cellList.Count);
            return cellList;
        }

        /// <summary>
        /// Gets the first selected cells in the current workbook
        /// </summary>
        /// <param name="wb">workbook model</param>
        /// <returns>List of Cell or null if no cell is selected</returns>
        public Cell GetFirstSelectedCell(WorkbookModel wb)
        {
            Range selectedCell = (wb.Workbook.Application.Selection as Range).Cells.Cells[1] as Range;
            String currentLocation = "=" + (selectedCell.Parent as Worksheet).Name as String + "!" + selectedCell.Address as String;


            var resultCell = new Cell()
            {
                Id = Convert.ToInt32(selectedCell.ID),
                Location = GetUserCellName(wb, currentLocation),
                SifLocation = GetSIFCellName(wb, currentLocation),
                Content = selectedCell.Formula as String
            };
            // Take the first cell and return it.
            return resultCell;

        }

        #endregion
    }
}
