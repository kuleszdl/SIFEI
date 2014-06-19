using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIF.Visualization.Excel.ScenarioCore
{
    enum CellDefinitionType
    {
        Input,
        Intermediate,
        Output,
        SanityValue,
        SanityConstraint,
        SanityExplanation,
        SanityChecking
    }
}
