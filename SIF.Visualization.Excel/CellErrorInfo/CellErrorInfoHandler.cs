using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIF.Visualization.Excel.Core
{
    class CellErrorInfoHandler
    {
        private List<CellErrorInfoModel> models;

        #region Singleton
        private static CellErrorInfoHandler instance;

        private CellErrorInfoHandler()
        {
            this.models = new List<CellErrorInfoModel>();
        }

        public static CellErrorInfoHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CellErrorInfoHandler();
                }
                return instance;
            }
        }
        #endregion

        public void AddIcon(Violation violation)
        {
            if (models.Count(mod => mod.Type.Equals(violation.ViolationState) && mod.Cell.Equals(violation.Cell)) == 0)
            {
                this.models.Add(new CellErrorInfoModel(violation.ViolationState, violation.Cell));
            }
        }

        public void RemoveIcon(Violation violation)
        {
            CellErrorInfoModel model = models.Select(mod => mod).Where(mod => mod.Type.Equals(violation.ViolationState) && mod.Cell.Equals(violation.Cell)).ToList().ElementAt(0);
            if (model.Violations.Count == 0)
            {
                model.RemoveIcon();
                this.models.Remove(model);
            }
        }
    }
}
