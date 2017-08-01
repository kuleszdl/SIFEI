using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using SIF.Visualization.Excel.Properties;
using MSExcel = Microsoft.Office.Interop.Excel;

namespace SIF.Visualization.Excel.Core
{
    /// <summary>
    ///     The cell manager provides useful cell selection methods.
    /// </summary>
    public class CellManager
    {
        #region Singleton

        private static volatile CellManager instance;
        private static readonly object syncRoot = new object();

        private CellManager()
        {
        }

        /// <summary>
        ///     Gets the current cell manager instance.
        /// </summary>
        public static CellManager Instance
        {
            get
            {
                if (instance == null)
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new CellManager();
                    }

                return instance;
            }
        }

        #endregion

        #region cell selection

        public void SelectCell(string location)
        {
            var wb = DataModel.Instance.CurrentWorkbook;
            var cell = wb.GetCell(location);
            cell.IsSelected = true;
            var sheet = (MSExcel.Worksheet) wb.Workbook.Sheets[cell.WorksheetKey];
            sheet.Activate();
            sheet.get_Range(cell.ShortLocation, Type.Missing).Select();
        }

        /// <summary>
        ///     Gets the selected cells in the current workbook
        /// </summary>
        /// <param name="wb">workbook model</param>
        /// <returns>List of Cell</returns>
        public List<Cell> GetSelectedCells()
        {
            var wb = DataModel.Instance.CurrentWorkbook;
            var cellList = new List<Cell>();
            var selectedCells = (wb.Workbook.Application.Selection as MSExcel.Range).Cells;
            if (selectedCells.Count < 10000)
            {
                Debug.WriteLine("SELECTED CELLS: Creating List ...");
                var start = DateTime.Now;
                cellList = GetCellsFromRange(selectedCells);
                Debug.WriteLine("SELECTED CELLS: List created! Time: " + (DateTime.Now - start) + ", Items: " +
                                cellList.Count);
                return cellList;
            }
            MessageBox.Show(Resources.tl_CellPicker_ToManyCells);
            return cellList;
        }

        /// <summary>
        ///     Gets the first selected cells in the current workbook
        /// </summary>
        /// <param name="wb">workbook model</param>
        /// <returns>List of Cell or null if no cell is selected</returns>
        public Cell GetFirstSelectedCell(WorkbookModel wb)
        {
            var selectedCell = (wb.Workbook.Application.Selection as MSExcel.Range).Cells.Cells[1] as MSExcel.Range;
            var currentLocation = (selectedCell.Parent as MSExcel.Worksheet).Name + "!" + selectedCell.Address;
            return wb.GetCell(currentLocation);
        }

        public Cell GetCellFromRange(MSExcel.Range range)
        {
            var wb = DataModel.Instance.CurrentWorkbook;
            var currentCell = range.Cells.Cells[1] as MSExcel.Range;
            var currentLocation = (currentCell.Parent as MSExcel.Worksheet).Name + "!" + currentCell.Address;
            return wb.GetCell(currentLocation);
        }

        public List<Cell> GetCellsFromRange(MSExcel.Range range)
        {
            var cellList = new List<Cell>();
            var wb = DataModel.Instance.CurrentWorkbook;
            if (range.Count < 15000)
            {
                foreach (var c in range.Cells)
                {
                    var currentCell = c as MSExcel.Range;
                    var currentLocation = (currentCell.Parent as MSExcel.Worksheet).Name + "!" + currentCell.Address;
                    var selectedCell = wb.GetCell(currentLocation);
                    cellList.Add(selectedCell);
                }
                return cellList;
            }
            MessageBox.Show(Resources.tl_ToManyCells);
            return cellList;
        }

        #endregion
    }
}