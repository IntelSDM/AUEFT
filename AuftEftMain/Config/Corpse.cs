using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AuftEftMain.Config
{
    public class Corpse
    {
        public bool Enable = true;
        public bool Distance = true;
        public int MaxDistance = 300;
        public bool Value = true;
        public int MinValue = 10000;
        public KeyCode ContentsKey = KeyCode.LeftAlt;
        public int ContentsMinValue = 1000;
    }
}
