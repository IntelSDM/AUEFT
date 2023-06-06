using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace AuftEftMain.Config
{
   public class Item
   {
        public bool Enable = true;
        public bool Distance = true;
        public bool Value = true;
        public bool DrawAll = false;
        public bool IgnoreMinValue = false;
        public int MinValue = 10000;
        public int MaxDistance = 150;
        public bool DrawQuest = false;
        public bool DrawSuperRare = true;
        public bool DrawWhitelisted = true;
   }
}
