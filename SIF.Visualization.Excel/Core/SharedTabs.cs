using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIF.Visualization.Excel.Core
{
    public enum SharedTabs
    {
        /// <summary>
        /// Register Violations
        /// </summary>
        Violations,
        /// <summary>
        /// Register Later
        /// </summary>
        Later,
        /// <summary>
        /// Register FalsePositive
        /// </summary>
        Ignore,
        /// <summary>
        /// Register Solved
        /// </summary>
        Solved,
        /// <summary>
        /// Register Cells
        /// </summary>
        Cells,
        /// <summary>
        /// Register Scenarios
        /// </summary>
        Scenarios,
    }
}
