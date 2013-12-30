using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIF.Visualization.Excel.ScenarioCore.StaticScenarios
{
    public class SpecializedCollection<T> : ObservableCollection<T>
    {
        public new void Add(T item)
        {
            bool result = true;

            foreach (var i in this)
            {
                if (i.GetType() == item.GetType())
                {
                    result = false;
                }
            }

            if (result == true)
            {
                base.Add(item);
            }
        }
    }
}
