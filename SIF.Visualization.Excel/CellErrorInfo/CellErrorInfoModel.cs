using Microsoft.Office.Tools.Excel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIF.Visualization.Excel.Core
{
    class CellErrorInfoModel : BindableBase
    {
        private ViolationType type;
        private CellLocation cell;
        private ControlSite control;
        private string controlName;

        public CellErrorInfoModel(ViolationType type, CellLocation cell)
        {
            this.type = type;
            this.cell = cell;
            drawIcon();
        }

        public ViolationType Type
        {
            get { return this.type; }
        }

        public CellLocation Cell
        {
            get { return this.cell; }
        }

        public ObservableCollection<Violation> Violations
        {
            get
            {
                List<Violation> list = null;
                switch (type)
                {
                    default:
                        list = DataModel.Instance.CurrentWorkbook.Violations.ToList();
                        break;
                    case ViolationType.LATER:
                        list = DataModel.Instance.CurrentWorkbook.LaterViolations.ToList();
                        break;
                    case ViolationType.IGNORE:
                        list = DataModel.Instance.CurrentWorkbook.IgnoredViolations.ToList();
                        break;
                    case ViolationType.SOLVED:
                        list = DataModel.Instance.CurrentWorkbook.SolvedViolations.ToList();
                        break;
                }
                ObservableCollection<Violation> col = new ObservableCollection<Violation>();
                (from violation in list
                 where violation.ViolationState.Equals(type) && violation.Cell.Equals(cell)
                 select violation).ToList().ForEach(vi => col.Add(vi));
                return col;
            }
        }

        public decimal MaxSeverity
        {
            get
            {
                if (Violations.Count == 0)
                {
                    return 0;
                }
                return Violations.Max(vi => vi.Severity);
            }
        }

        public decimal MinSeverity
        {
            get
            {
                if (Violations.Count == 0)
                {
                    return 0;
                }
                return Violations.Min(vi => vi.Severity);
            }
        }

        public bool ItemSelected
        {
            get
            {
                foreach (Violation vi in Violations)
                {
                    if (vi.IsSelected)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public Violation SelectedItem
        {
            get
            {
                foreach (Violation vi in Violations)
                {
                    if (vi.IsSelected)
                    {
                        return vi;
                    }
                }
                return null;
            }
        }

        private void drawIcon()
        {
            var container = new CellErrorInfoContainer();
            container.ElementHost.Child = new CellErrorInfo() { DataContext = this };

            var vsto = Globals.Factory.GetVstoObject(this.cell.Worksheet);

            this.controlName = Guid.NewGuid().ToString();

            this.control = vsto.Controls.AddControl(container, this.cell.Worksheet.Range[this.cell.ShortLocation], this.controlName);
            this.control.Width = this.control.Height + 4;
            this.control.Placement = Microsoft.Office.Interop.Excel.XlPlacement.xlMove;
        }

        public void RemoveIcon()
        {
            if (!string.IsNullOrWhiteSpace(this.controlName))
            {
                var vsto = Globals.Factory.GetVstoObject(this.cell.Worksheet);
                vsto.Controls.Remove(this.controlName);
                this.controlName = null;
            }
        }
    }
}
