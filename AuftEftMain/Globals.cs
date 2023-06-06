using EFT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using AuftEftMain.Helpers;
using System.Reflection;
using System.Threading;
using EFT.Interactive;
using System.Windows.Forms;
using AuftEftMain.CustomObjects;
using EFT.InventoryLogic;

namespace AuftEftMain
{
    public class Globals : MonoBehaviour
    {
        public static Weapon LocalPlayerWeapon;
        public static Camera MainCamera = new Camera();
        public static GameWorld GameWorld;
        public static Player LocalPlayer;
        public static Form FormInstance = null;
        public static AuftEftMain.Config.BaseConfig Config = new Config.BaseConfig();
        
        public static bool IsInYourGroup(Player player)
        {
            return Globals.LocalPlayer.Profile.Info.GroupId == player.Profile.Info.GroupId && player.Profile.Info.GroupId != "0" && player.Profile.Info.GroupId != "" && player.Profile.Info.GroupId != null;
        }

        [ObfuscationAttribute(Exclude = true)]
        private void Start()
        {
            ShaderHelper.GetShader();
            ColourHelper.AddColours();
            ConfigHelper.CreateEnvironment();
            Thread thread = new Thread(new ThreadStart(Program.Main));
            thread.Start();
            // Thread thread2 = new Thread(Overlay.LoopValidate);
            //  thread2.Start();
        

        }
        public static List<PlayerObject> PlayerList = new List<PlayerObject>();
        public static List<ExfilObject> ExfilList = new List<ExfilObject>();
        public static List<ItemObject> LootList = new List<ItemObject>();
        public static List<CorpseObject> CorpseList = new List<CorpseObject>();
        public static List<ContainerObject> ContainerList = new List<ContainerObject>();
        public static List<GrenadeObject> GrenadeList = new List<GrenadeObject>();
        public static List<Door> DoorList = new List<Door>();
    }
}
