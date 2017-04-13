using MSExcel = Microsoft.Office.Interop.Excel;
using SIF.Visualization.Excel.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Office.Interop.Excel;

namespace SIF.Visualization.Excel.Core {
    /// <summary>
    /// The cell manager provides useful cell selection methods.
    /// </summary>
    public class CellManager {
        #region Singleton

        private static volatile CellManager instance;
        private static object syncRoot = new Object();

        private CellManager() {
        }

        /// <summary>
        /// Gets the current cell manager instance.
        /// </summary>
        public static CellManager Instance {
            get {
                if (instance == null) {
                    lock (syncRoot) {
                        if (instance == null)
                            instance = new CellManager();
                    }
                }

                return instance;
            }
        }

        #endregion

        #region cell selection

        public void SelectCell(string location) {
            WorkbookModel wb = DataModel.Instance.CurrentWorkbook;
            Cell cell = wb.GetCell(location);
            cell.IsSelected = true;
            MSExcel.Worksheet sheet = (MSExcel.Worksheet)wb.Workbook.Sheets[cell.WorksheetKey];
            ((_Worksheet) sheet).Activate();
            sheet.get_Range(cell.ShortLocation, Type.Missing).Select();
        }

        /// <summary>
        /// Gets the selected cells in the current workbook
        /// </summary>
        /// <param name="wb">workbook model</param>
        /// <returns>List of Cell</returns>
        public List<Cell> GetSelectedCells() {
            WorkbookModel wb = DataModel.Instance.CurrentWorkbook;
            var cellList = new List<Cell>();
            MSExcel.Range selectedCells = (wb.Workbook.Application.Selection as MSExcel.Range).Cells;
            Debug.WriteLine("SELECTED CELLS: Creating List ...");
            DateTime start = DateTime.Now;
            cellList = GetCellsFromRange(selectedCells);          
            Debug.WriteLine("SELECTED CELLS: List created! Time: " + (DateTime.Now - start).ToString() + ", Items: " + cellList.Count);
            return cellList;
        }

        /// <summary>
        /// Gets the first selected cells in the current workbook
        /// </summary>
        /// <param name="wb">workbook model</param>
        /// <returns>List of Cell or null if no cell is selected</returns>
        public Cell GetFirstSelectedCell(WorkbookModel wb) {
            MSExcel.Range selectedCell = (wb.Workbook.Application.Selection as MSExcel.Range).Cells.Cells[1] as MSExcel.Range;
            String currentLocation = (selectedCell.Parent as MSExcel.Worksheet).Name as String + "!" + selectedCell.Address as String;
            return wb.GetCell(currentLocation);
        }

        public Cell GetCellFromRange(MSExcel.Range range) {
            WorkbookModel wb = DataModel.Instance.CurrentWorkbook;
           var currentCell = range.Cells.Cells[1] as MSExcel.Range;
            String currentLocation = (currentCell.Parent as MSExcel.Worksheet).Name as String + "!" + currentCell.Address as String;
            return wb.GetCell(currentLocation);
        }

        public List<Cell> GetCellsFromRange(MSExcel.Range range) {
            List<Cell> cellList = new List<Cell>();
            WorkbookModel wb = DataModel.Instance.CurrentWorkbook;
            foreach (var c in range.Cells) {
                var currentCell = c as MSExcel.Range;
                String currentLocation = (currentCell.Parent as MSExcel.Worksheet).Name as String + "!" + currentCell.Address as String;
                var selectedCell = wb.GetCell(currentLocation);
                cellList.Add(selectedCell);
            }
            return cellList;
        }

        #endregion
    }
}
