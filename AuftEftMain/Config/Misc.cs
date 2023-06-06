using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AuftEftMain.Config
{
   public class Misc
   {
        public bool BunnyHop = false;
        public bool WaitForInput = true;
        public float TimeBetweenHop = 0.66f;
        public KeyCode HopKey = KeyCode.V;

        public bool FarJump = false;
        public float FarJumpAmount = 0.6f;
        public KeyCode JumpKey = KeyCode.N;

        public bool SlideJump = false;
        public float SlideJumpTime = 2f;
        public KeyCode SlideKey = KeyCode.Comma;

        public bool UnlimitedStamina = true;
        public bool Speedhack = true;
        public float Speed = 2.2f;
        public KeyCode Key = KeyCode.B;

        public bool NoVisor = true;
        public bool NightVision = false;
        public bool ThermalVision = false;
        public bool NightMode = false;
        public bool LootThroughWalls = true;
        public bool CustomTime = false;
        public float Time = 12;
        public bool NoSway = true;
        public bool NoRecoil = false;
        public bool FullAuto = true;
        public bool NoCollide = false;
        public bool UnbreakableBallistics = false;
    }
}
