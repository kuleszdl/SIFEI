using System;
using System.Windows.Forms;
using SIF.Visualization.Excel.Core;

namespace SIF.Visualization.Excel.View
{
    public partial class RuleDataFieldContainer : UserControl
    {
    //    public RuleDataField RuleDataField
    //    {
    //        get
    //        {
    //            if (ruleDataFieldHost != null && RuleDataFieldHost.Child != null)
    //            {
    //                return RuleDataFieldHost.Child as RuleDataField;
    //            }
    //            else
    //            {
    //                return null;
    //            }
    //        }
    //    }

        public RuleDataFieldContainer()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
        }

    }
}
