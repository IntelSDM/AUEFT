using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuftEftMain.Config
{
    public class BaseConfig
    {
        public Colours Colour = new Colours();
        public Players Player = new Players();
        public Scav Scav = new Scav();
        public Aimbot Aimbot = new Aimbot();
        public Weapon Weapon = new Weapon();
        public Menu Menu = new Menu();
        public Item Item = new Item();
        public Exfil Exfil = new Exfil();
        public Corpse Corpse = new Corpse();
        public Container Container = new Container();
        public Misc Misc = new Misc();
        public Grenade Grenade = new Grenade();
    }
}
