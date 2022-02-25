using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;

namespace SimulationSystem.Systems
{
    public class InputSystem : Dalak.Ecs.System
    {
        public static BitSet keyDownMask;
        public static BitSet keyUpMask;
        public static BitSet mouseKeyUpMask;
        public static BitSet mouseKeyDownMask;

        public override void Awake()
        {
            keyDownMask = new BitSet();
            keyUpMask = new BitSet();
            mouseKeyDownMask = new BitSet();
            mouseKeyUpMask = new BitSet();
        }

        public override void LateUpdate()
        {
            keyDownMask.ClearAll();
            keyUpMask.ClearAll();
            mouseKeyDownMask.ClearAll();
            mouseKeyUpMask.ClearAll();
        }
    }
}
