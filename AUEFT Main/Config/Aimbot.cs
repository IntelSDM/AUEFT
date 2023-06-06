using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace AuftEftMain.Config
{
    public class Aimbot
    {
        public bool PlayerAimbot = true;
        public float PlayerHitChance = 100;
        public bool PlayerRageAimbot = false;
        public float PlayerHeadChance = 60;
        public float PlayerBodyChance = 30;
        public float PlayerLegChance = 10;
        public bool PlayerAutoShoot = false;
        public int PlayerMaxDistance = 300;
        public int PlayerMaxAutoDistance = 100;

        public bool ScavAimbot = true;
        public float ScavHitChance = 100;
        public bool ScavRageAimbot = true;
        public float ScavHeadChance = 70;
        public float ScavBodyChance = 20;
        public float ScavLegChance = 10;
        public bool ScavAutoShoot = false;
        public float ScavMaxDistance = 500;
        public float ScavMaxAutoDistance = 100;

        public bool DrawFov = true;
        public float Fov = 200;
        public bool Prediction = false;
    }
}
