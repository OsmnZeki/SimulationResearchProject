using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimulationSystem;

namespace SimulationWFA
{
    public class HierarchySimButton : Button
    {
        public SimObject SimObject;
        public int Id;
        public ComponentPanel componentPanel = new ComponentPanel();
        public List<string> SerializedComponentList = new List<string>();
    }
}
