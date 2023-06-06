using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EFT;
using System.Threading;
using Comfort.Common;
using System.Reflection;
using AuftEftMain.CustomObjects;
using EFT.Interactive;
using EFT.CameraControl;
using EFT.InventoryLogic;
using System.IO;
using System.Runtime.InteropServices;

namespace AuftEftMain
{


    class Caching : MonoBehaviour
    {
        
        public static float PlayerCacheTime;
        public static readonly float PlayerCacheIntival = 3f;
        public static float ExfilCacheTime;
        public static readonly float ExfilCacheIntival = 10;
        public static float ItemCacheTime;
        public static readonly float ItemCacheIntival = 5;
        public static float ContainerCacheTime;
        public static readonly float ContainerCacheIntival = 5;
        public static float GrenadeCacheTime;
        public static readonly float GrenadeCacheIntival = 1;
        public static float DoorCacheTime;
        public static readonly float DoorCacheIntival = 10;

        static private double LastTime;

        public static string GetUser()
        {

            return Auth.Username;
        }

        public static string GetHwid()
        {

            return Auth.Hwid;
        }
        public static string GetPass()
        {

            return Auth.Password;
        }

        public static List<PlayerObject> SortClosestToCrosshair(List<PlayerObject> p)
        {
            return (from tempPlayer in p
                    orderby Vector2.Distance(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), Camera.main.WorldToScreenPoint(tempPlayer.player.Transform.position))
                    select tempPlayer).ToList<PlayerObject>();
        }
        public static List<CorpseObject> SortClosestToCrosshair(List<CorpseObject> p)
        {
            return (from tempPlayer in p
                    orderby Vector2.Distance(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), Camera.main.WorldToScreenPoint(tempPlayer.corpse.transform.position))
                    select tempPlayer).ToList<CorpseObject>();
        }
        public static List<ItemObject> SortClosestToCrosshair(List<ItemObject> p)
        {
            return (from tempPlayer in p
                    orderby Vector2.Distance(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), Camera.main.WorldToScreenPoint(tempPlayer.item.transform.position))
                    select tempPlayer).ToList<ItemObject>();
        }
        public static List<ExfilObject> SortClosestToCrosshair(List<ExfilObject> p)
        {
            return (from tempPlayer in p
                    orderby Vector2.Distance(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), Camera.main.WorldToScreenPoint(tempPlayer.exfilpoint.transform.position))
                    select tempPlayer).ToList<ExfilObject>();
        }
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        IntPtr selectedWindow = GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string IpClassName, string IpWindowName);
        public static IntPtr pshandle = FindWindow(null, "escapefromtarkov");

        [ObfuscationAttribute(Exclude = true)]
    
        void Update()
        {

            //   Globals.PlayerList = SortClosestToCrosshair(Globals.PlayerList);
            //       Globals.ExfilList = SortClosestToCrosshair(Globals.ExfilList);
            //     Globals.CorpseList = SortClosestToCrosshair(Globals.CorpseList);
            //   Globals.LootList = SortClosestToCrosshair(Globals.LootList);
            try
            {
                Cache();
            }
            catch { }
            try
            {
                Caching.SetObjectVars();
            }
            catch { }
            HideWindow();
 



        }
        public static Vector3 WorldPointToScreenPoint(Vector3 worldPoint)
        {
            Vector3 vector = new Vector3();
          
                vector = Camera.main.WorldToScreenPoint(worldPoint, Camera.MonoOrStereoscopicEye.Mono);
                vector.y = (float)UnityEngine.Screen.height - vector.y;
            

            return vector;
        }

        public static void SetObjectVars()
        {
            // so basically we cant loop through objects and access the camera in our drawing loop because it fucks up the game.
            // we create a custom object which stores our entity and the w2s postion to use in the drawing loop instead.

            try
            {
                if (Globals.GameWorld != null)
                {


                    foreach (PlayerObject player in Globals.PlayerList)
                    {

                        if (player != null)
                        {

                            player.w2s = WorldPointToScreenPoint(player.player.Transform.position);
                            player.w2shead = WorldPointToScreenPoint(player.player.PlayerBones.Head.position);

                        }
                    }
                    foreach (ExfilObject exfil in Globals.ExfilList)
                    {
                        if (exfil != null)
                        {

                            exfil.w2s = WorldPointToScreenPoint(exfil.exfilpoint.transform.position);


                        }
                    }
                    foreach (ItemObject loot in Globals.LootList)
                    {
                        if (loot != null)
                        {

                            loot.w2s = WorldPointToScreenPoint(loot.item.transform.position);

                        }
                    }
                    foreach (ContainerObject container in Globals.ContainerList)
                    {
                        if (container != null)
                        {

                            container.w2s = WorldPointToScreenPoint(container.container.transform.position);

                        }
                    }
                    foreach (CorpseObject corpse in Globals.CorpseList)
                    {
                        if (corpse != null)
                        {

                            corpse.w2s = WorldPointToScreenPoint(corpse.corpse.transform.position);

                        }
                    }
                    foreach (GrenadeObject grenade in Globals.GrenadeList)
                    {
                        if (grenade != null)
                        {
                            grenade.w2s = WorldPointToScreenPoint(grenade.Grenade.transform.position);
                        }
                    
                    }
                }
            }
            catch { }
        
        }
        private static void HideWindow()
        {
            #region windowshit
            if (Globals.FormInstance != null)
            {
                if (GetForegroundWindow() != pshandle)
                    Globals.FormInstance.Hide();
                if (GetForegroundWindow() == pshandle)
                    Globals.FormInstance.Show();

                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.Tab))
                    Globals.FormInstance.Hide();
            }
            #endregion


        }
        private static void Cache()
        {
            try
            {
                Globals.MainCamera = Camera.main;
                Globals.GameWorld = Singleton<GameWorld>.Instance;

                if (Globals.GameWorld != null)
                {
                    #region PlayerCahce
                    if (Time.time > PlayerCacheTime)
                    {
                        Globals.PlayerList.Clear();
                        foreach (Player player in Globals.GameWorld.RegisteredPlayers)
                        {
                            // remove invalids
                            if (player != null)
                            {
                                // check if player is us
                                if (player.IsYourPlayer)
                                {
                                    Globals.LocalPlayer = player;
                                    // controlflow say GO TO NEXT PLAYER
                                    continue;
                                }
                                if (player.HealthController.IsAlive)
                                {
                                    // add none dead players to our list we loop every frame
                                    PlayerObject playerobject = new PlayerObject();
                                    playerobject.player = player;
                                    Globals.PlayerList.Add(playerobject);

                                }
                            }
                        }


                        PlayerCacheTime = Time.time + PlayerCacheIntival;
                    }
                    #endregion

                    #region ContainerCache
                    if (Time.time >= ContainerCacheTime)
                    {
                        Globals.ContainerList.Clear();
                        foreach (LootableContainer container in LocationScene.GetAllObjects<LootableContainer>(false))
                        {
                            if (container != null)
                            {
                                ContainerObject containerobject = new ContainerObject();
                                containerobject.container = container;
                                Globals.ContainerList.Add(containerobject);
                            }
                        }

                        ContainerCacheTime = Time.time + ContainerCacheIntival;
                    }
                    #endregion

                    #region DoorCache

                    if (Time.time >= DoorCacheTime)
                    {
                        Globals.DoorList.Clear();
                        foreach (Door door in LocationScene.GetAllObjects<Door>(false))
                        {
                            if (door != null)
                            {
                                Globals.DoorList.Add(door);
                            }
                        }
                        DoorCacheTime = Time.time + DoorCacheIntival;
                    }
                    #endregion

                    #region ExfilCache

                    if (Time.time >= ExfilCacheTime)
                    {
                        Globals.ExfilList.Clear();
                        if (Globals.GameWorld.ExfiltrationController.ExfiltrationPoints != null)
                        {
                            //  foreach(ExfiltrationRequirement exfil in Globals.GameWorld.ExfiltrationController.ExfiltrationPoints)
                            foreach (ExfiltrationPoint exfilpoint in Globals.GameWorld.ExfiltrationController.ExfiltrationPoints)
                            {


                                if (exfilpoint != null)
                                {
                                    ExfilObject exfilobject = new ExfilObject();
                                    exfilobject.exfilpoint = exfilpoint;
                                    Globals.ExfilList.Add(exfilobject);

                                }
                            }
                            ExfilCacheTime = Time.time + ExfilCacheIntival;

                        }



                    }


                    #endregion

                    #region ItemCache
                    if (Time.time > ItemCacheTime)
                    {
                        Globals.CorpseList.Clear();
                        Globals.LootList.Clear();
                        for (int i = 0; i < Globals.GameWorld.LootItems.Count; i++)
                        {
                            LootItem item = Globals.GameWorld.LootItems.GetByIndex(i);
                            if (!(item.Item.ShortName.Localized() == "Default Inventory"))
                            {
                                if (item != null)
                                {

                                    ItemObject itemobject = new ItemObject();
                                    itemobject.item = item;
                                    Globals.LootList.Add(itemobject);
                                }

                            }
                            if (item.Item.ShortName.Localized() == "Default Inventory")
                            {
                                if (item != null)
                                {
                                    CorpseObject corpseobject = new CorpseObject();
                                    corpseobject.corpse = item;
                                    Globals.CorpseList.Add(corpseobject);
                                }

                            }

                        }
                        ItemCacheTime = Time.time + ItemCacheIntival;


                    }
                    #endregion

                    #region GrenadeCache
                    if (Time.time > GrenadeCacheTime)
                    {
                        Globals.GrenadeList.Clear();
                        var e = Globals.GameWorld.Grenades.GetValuesEnumerator().GetEnumerator();
                        while (e.MoveNext())
                        {
                            var grenade = e.Current;

                            if (grenade == null)
                                continue;

                            GrenadeObject obj = new GrenadeObject();
                            obj.Grenade = grenade;
                            Globals.GrenadeList.Add(obj);
                        }
                        GrenadeCacheTime = Time.time + GrenadeCacheIntival;
                    }
                    #endregion
                }
            }
            catch { }
        }
    }
}
