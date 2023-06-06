using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using UnityEngine;
using EFT;
using System.IO;
using System.Threading;
using EFT.Interactive;
using Comfort.Common;
using AuftEftMain.CustomObjects;
using AuftEftMain.Helpers;
using EFT.InventoryLogic;
using EFT.UI.Screens;
using EFT.UI;
using System.Reflection;
using EFT.Ballistics;

namespace AuftEftMain
{
    /*
    I dont want to comment all of this, basically it creates a window using gdi, draws in it to create a GUI and a hud.
    */
    #region Controls
    class Controls : MonoBehaviour
    {
        static private double LastTime;

        private void ConfigControls(uint Index, uint MenuIndex, string configname, bool load)
        {
            if (Index == MenuIndex)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (!load)
                    {
                        ConfigHelper.SaveConfig(configname, false);
                    }
                    if (load)
                    {
                        ConfigHelper.LoadConfig(configname);
                    }
                }
            }
        }

        private void MenuVerticalControls(ref uint mainindex, uint maxindex)
        {

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (mainindex != maxindex)
                    mainindex++;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (mainindex != 0)
                {
                    mainindex--;
                }
            }

        }
        private void SubMenuBackwards(ref bool CurrentMenu, ref bool OldMenu)
        {
            // prevent double events
            if (Time.time > LastTime)
            {
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    LastTime = Time.time + 0.2;
                    OldMenu = true;
                    CurrentMenu = false;
                }
            }
        }
        private void SubMenuControl(uint Index, uint MenuIndex, ref bool CurrentMenu, ref bool NextMenu)
        {
            if (Index == MenuIndex)
            {
                // prevents double events
                if (Time.time > LastTime)
                {
                    if (Input.GetKeyUp(KeyCode.Return))
                    {
                        LastTime = Time.time + 0.2;
                        NextMenu = true;
                        CurrentMenu = false;



                    }
                }
            }

        }
        private void ToggleControl(uint index, uint mainindex, ref bool toggle)
        {
            if (index == mainindex)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    toggle = true;
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    toggle = false;
                }
            }

        }
        public static KeyCode ChangeKey()
        {
            Event e = Event.current;
            KeyCode result = KeyCode.None;
            if (e.keyCode != KeyCode.Return && e.keyCode != KeyCode.RightArrow && e.keyCode != KeyCode.DownArrow && e.keyCode != KeyCode.LeftArrow && e.keyCode != KeyCode.UpArrow)
            {
                result = e.keyCode;

            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                result = KeyCode.Mouse0;
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                result = KeyCode.Mouse1;
            }
            if (Input.GetKeyDown(KeyCode.Mouse2))
            {
                result = KeyCode.Mouse2;
            }
            if (Input.GetKeyDown(KeyCode.Mouse3))
            {
                result = KeyCode.Mouse3;
            }
            if (Input.GetKeyDown(KeyCode.Mouse4))
            {
                result = KeyCode.Mouse4;
            }
            if (Input.GetKeyDown(KeyCode.Mouse5))
            {
                result = KeyCode.Mouse5;

            }
            if (Input.GetKeyDown(KeyCode.Mouse6))
            {
                result = KeyCode.Mouse6;

            }
            return result;

        }
        private void KeybindControl(uint index, uint mainindex, ref KeyCode key)
        {
            if (index == mainindex)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Return))
                {
                    //    toggle = true;
                    key = KeyCode.None;
                

                }
                if (key == KeyCode.None)
                {
                    key = ChangeKey();
                }
            }

        }
        private void Slider(uint index, uint mainindex, ref float value, float minvalue, float maxvalue, float increment)
        {
            if (value > maxvalue)
                value = maxvalue;
            if (value < minvalue)
                value = minvalue;

            if (index == mainindex)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    value += increment;
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    value -= increment;
                }
            }

        }
        private void Slider(uint index, uint mainindex, ref int value, int minvalue, int maxvalue, int increment)
        {
            if (value > maxvalue)
                value = maxvalue;
            if (value < minvalue)
                value = minvalue;

            if (index == mainindex)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    value += increment;
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    value -= increment;
                }
            }

        }
        [ObfuscationAttribute(Exclude = true)]
        void Update()
        {
            MenuControls();


        }
        private void MenuControls()
        {
            if (Input.GetKeyDown(KeyCode.Insert))
                Overlay.MenuDraw = !Overlay.MenuDraw;
            if (Overlay.MenuDraw)
            {
                if (Overlay.MainMenu)
                {
                    MenuVerticalControls(ref Overlay.MainIndex, Overlay.MainMaxIndex);
                    SubMenuControl(0, Overlay.MainIndex, ref Overlay.MainMenu, ref Overlay.EspMenu);
                    SubMenuControl(1, Overlay.MainIndex, ref Overlay.MainMenu, ref Overlay.AimbotMenu);
                    SubMenuControl(2, Overlay.MainIndex, ref Overlay.MainMenu, ref Overlay.MiscMenu);
                    SubMenuControl(3, Overlay.MainIndex, ref Overlay.MainMenu, ref Overlay.MenuMenu);
                    SubMenuControl(4, Overlay.MainIndex, ref Overlay.MainMenu, ref Overlay.ConfigMenu);
                }
                if (Overlay.MiscMenu)
                {
                    MenuVerticalControls(ref Overlay.MiscMenuIndex, Overlay.MiscMaxIndex);
                    SubMenuBackwards(ref Overlay.MiscMenu, ref Overlay.MainMenu);
                    SubMenuControl(0, Overlay.MiscMenuIndex, ref Overlay.MiscMenu, ref Overlay.MovementMiscMenu);
                    SubMenuControl(1, Overlay.MiscMenuIndex, ref Overlay.MiscMenu, ref Overlay.WeaponMiscMenu);
                    ToggleControl(2, Overlay.MiscMenuIndex, ref Globals.Config.Misc.NoVisor);
                    ToggleControl(3, Overlay.MiscMenuIndex, ref Globals.Config.Misc.NightVision);
                    ToggleControl(4, Overlay.MiscMenuIndex, ref Globals.Config.Misc.ThermalVision);
                    ToggleControl(5, Overlay.MiscMenuIndex, ref Globals.Config.Misc.NightMode);
                    ToggleControl(6, Overlay.MiscMenuIndex, ref Globals.Config.Misc.LootThroughWalls);
                    ToggleControl(7, Overlay.MiscMenuIndex, ref Globals.Config.Misc.CustomTime);
                    Slider(8, Overlay.MiscMenuIndex, ref Globals.Config.Misc.Time, 1, 24, 1);
                }
                if (Overlay.ConfigMenu)
                {
                    MenuVerticalControls(ref Overlay.ConfigMenuIndex, Overlay.ConfigMaxIndex);
                    SubMenuBackwards(ref Overlay.ConfigMenu, ref Overlay.MainMenu);
                    SubMenuControl(0, Overlay.ConfigMenuIndex, ref Overlay.ConfigMenu, ref Overlay.SaveConfigMenu);
                    SubMenuControl(1, Overlay.ConfigMenuIndex, ref Overlay.ConfigMenu, ref Overlay.LoadConfigMenu);
                }
                if (Overlay.GrenadeEspMenu)
                {
                    MenuVerticalControls(ref Overlay.GrenadeEspMenuIndex, Overlay.GrenadeEspMaxIndex);
                    SubMenuBackwards(ref Overlay.GrenadeEspMenu, ref Overlay.EspMenu);
                    ToggleControl(0, Overlay.GrenadeEspMenuIndex, ref Globals.Config.Grenade.Enable);
                    ToggleControl(1, Overlay.GrenadeEspMenuIndex, ref Globals.Config.Grenade.Distance);
                    Slider(2, Overlay.GrenadeEspMenuIndex, ref Globals.Config.Grenade.MaxDistance, 0, 1100, 25);
                    /*    if (GrenadeEspMenu)
                    {
                        Toggle(b, 0, "Enable", GrenadeEspMenuIndex, Globals.Config.Grenade.Enable);
                        Toggle(b, 1, "Distance", GrenadeEspMenuIndex, Globals.Config.Grenade.Distance);
                        Slider(b, 2, "Max Distance", GrenadeEspMenuIndex, Globals.Config.Grenade.MaxDistance, "m");
                    }*/
                }
                if (Overlay.SaveConfigMenu)
                {
                    MenuVerticalControls(ref Overlay.SaveConfigMenuIndex, Overlay.SaveConfigMaxIndex);
                    SubMenuBackwards(ref Overlay.SaveConfigMenu, ref Overlay.ConfigMenu);
                    ConfigControls(0, Overlay.SaveConfigMenuIndex, "Config 1", false);
                    ConfigControls(1, Overlay.SaveConfigMenuIndex, "Config 2", false);
                    ConfigControls(2, Overlay.SaveConfigMenuIndex, "Config 3", false);
                    ConfigControls(3, Overlay.SaveConfigMenuIndex, "Config 4", false);
                    ConfigControls(4, Overlay.SaveConfigMenuIndex, "Config 5", false);
                    ConfigControls(5, Overlay.SaveConfigMenuIndex, "Config 6", false);
                   
                }
                if (Overlay.LoadConfigMenu)
                {
                    MenuVerticalControls(ref Overlay.LoadConfigMenuIndex, Overlay.LoadConfigMaxIndex);
                    SubMenuBackwards(ref Overlay.LoadConfigMenu, ref Overlay.ConfigMenu);
                    ConfigControls(0, Overlay.LoadConfigMenuIndex, "Config 1", true);
                    ConfigControls(1, Overlay.LoadConfigMenuIndex, "Config 2", true);
                    ConfigControls(2, Overlay.LoadConfigMenuIndex, "Config 3", true);
                    ConfigControls(3, Overlay.LoadConfigMenuIndex, "Config 4", true);
                    ConfigControls(4, Overlay.LoadConfigMenuIndex, "Config 5", true);
                    ConfigControls(5, Overlay.LoadConfigMenuIndex, "Config 6", true);
                }
                if (Overlay.WeaponMiscMenu)
                {
                    MenuVerticalControls(ref Overlay.WeaponMiscMenuIndex, Overlay.WeaponMiscMaxIndex);
                    SubMenuBackwards(ref Overlay.WeaponMiscMenu, ref Overlay.MiscMenu);
                    ToggleControl(0, Overlay.WeaponMiscMenuIndex, ref Globals.Config.Misc.NoRecoil);
                    ToggleControl(1, Overlay.WeaponMiscMenuIndex, ref Globals.Config.Misc.NoSway);
                    ToggleControl(2, Overlay.WeaponMiscMenuIndex, ref Globals.Config.Misc.FullAuto);
                    ToggleControl(3, Overlay.WeaponMiscMenuIndex, ref Globals.Config.Misc.NoCollide);
                    ToggleControl(4, Overlay.WeaponMiscMenuIndex, ref Globals.Config.Misc.UnbreakableBallistics);
                }
                if (Overlay.MovementMiscMenu)
                {
                    MenuVerticalControls(ref Overlay.MovementMiscMenuIndex, Overlay.MovementMiscMaxIndex);
                    SubMenuBackwards(ref Overlay.MovementMiscMenu, ref Overlay.MiscMenu);
                    ToggleControl(0, Overlay.MovementMiscMenuIndex, ref Globals.Config.Misc.UnlimitedStamina);
                    ToggleControl(1, Overlay.MovementMiscMenuIndex, ref Globals.Config.Misc.Speedhack);
                    Slider(2, Overlay.MovementMiscMenuIndex, ref Globals.Config.Misc.Speed, 1, 10, 0.2f);
                    KeybindControl(3, Overlay.MovementMiscMenuIndex, ref Globals.Config.Misc.Key);
                    ToggleControl(4, Overlay.MovementMiscMenuIndex, ref Globals.Config.Misc.BunnyHop);
                    Slider(5, Overlay.MovementMiscMenuIndex, ref Globals.Config.Misc.TimeBetweenHop, 0, 3, 0.01f);
                    KeybindControl(6, Overlay.MovementMiscMenuIndex, ref Globals.Config.Misc.HopKey);
                    ToggleControl(7, Overlay.MovementMiscMenuIndex, ref Globals.Config.Misc.WaitForInput);
                    ToggleControl(8, Overlay.MovementMiscMenuIndex, ref Globals.Config.Misc.FarJump);
                    Slider(9, Overlay.MovementMiscMenuIndex, ref Globals.Config.Misc.FarJumpAmount, 0, 3, 0.1f);
                    KeybindControl(10, Overlay.MovementMiscMenuIndex, ref Globals.Config.Misc.JumpKey);
                    ToggleControl(11, Overlay.MovementMiscMenuIndex, ref Globals.Config.Misc.SlideJump);
                    Slider(12, Overlay.MovementMiscMenuIndex, ref Globals.Config.Misc.SlideJumpTime, 0, 10, 0.25f);
                    KeybindControl(13, Overlay.MovementMiscMenuIndex, ref Globals.Config.Misc.SlideKey);
                }
                if (Overlay.EspMenu)
                {
                    MenuVerticalControls(ref Overlay.EspMenuIndex, Overlay.EspMaxIndex);
                    SubMenuBackwards(ref Overlay.EspMenu, ref Overlay.MainMenu);
                    SubMenuControl(0, Overlay.EspMenuIndex, ref Overlay.EspMenu, ref Overlay.PlayerEspMenu);
                    SubMenuControl(1, Overlay.EspMenuIndex, ref Overlay.EspMenu, ref Overlay.ScavEspMenu);
                    SubMenuControl(2, Overlay.EspMenuIndex, ref Overlay.EspMenu, ref Overlay.ItemEspMenu);
                    SubMenuControl(3, Overlay.EspMenuIndex, ref Overlay.EspMenu, ref Overlay.CorpseEspMenu);
                    SubMenuControl(4, Overlay.EspMenuIndex, ref Overlay.EspMenu, ref Overlay.ContainerEspMenu);
                    SubMenuControl(5, Overlay.EspMenuIndex, ref Overlay.EspMenu, ref Overlay.ExfilEspMenu);
                    SubMenuControl(6, Overlay.EspMenuIndex, ref Overlay.EspMenu, ref Overlay.GrenadeEspMenu);
                }
                if (Overlay.MenuMenu)
                {
                    MenuVerticalControls(ref Overlay.MenuMainIndex, Overlay.MenuMaxIndex);
                    SubMenuBackwards(ref Overlay.MenuMenu, ref Overlay.MainMenu);
                    ToggleControl(0, Overlay.MenuMainIndex, ref Globals.Config.Menu.Radar);
                    ToggleControl(1, Overlay.MenuMainIndex, ref Globals.Config.Menu.Mode2);
                    ToggleControl(2, Overlay.MenuMainIndex, ref Globals.Config.Misc.Chinese);
                }
                if (Overlay.ItemEspMenu)
                {
                    MenuVerticalControls(ref Overlay.ItemEspMenuIndex, Overlay.ItemEspMaxIndex);
                    SubMenuBackwards(ref Overlay.ItemEspMenu, ref Overlay.EspMenu);
                    ToggleControl(0, Overlay.ItemEspMenuIndex, ref Globals.Config.Item.Enable);
                    ToggleControl(1, Overlay.ItemEspMenuIndex, ref Globals.Config.Item.Distance);
                    Slider(2, Overlay.ItemEspMenuIndex, ref Globals.Config.Item.MaxDistance, 0, 1000, 25);
                    ToggleControl(3, Overlay.ItemEspMenuIndex, ref Globals.Config.Item.Value);
                    ToggleControl(4, Overlay.ItemEspMenuIndex, ref Globals.Config.Item.DrawAll);
                    ToggleControl(5, Overlay.ItemEspMenuIndex, ref Globals.Config.Item.DrawQuest);
                    ToggleControl(6, Overlay.ItemEspMenuIndex, ref Globals.Config.Item.DrawSuperRare);
                    ToggleControl(7, Overlay.ItemEspMenuIndex, ref Globals.Config.Item.DrawWhitelisted);
                    Slider(8, Overlay.ItemEspMenuIndex, ref Globals.Config.Item.MinValue, 0, 1000000, 1000);
                    ToggleControl(9, Overlay.ItemEspMenuIndex, ref Globals.Config.Item.IgnoreMinValue);

                }
                if (Overlay.PlayerEspMenu)
                {
                    MenuVerticalControls(ref Overlay.PlayerEspMenuIndex, Overlay.PlayerEspMaxIndex);
                    SubMenuBackwards(ref Overlay.PlayerEspMenu, ref Overlay.EspMenu);
                    ToggleControl(0, Overlay.PlayerEspMenuIndex, ref Globals.Config.Player.Enable);
                    ToggleControl(1, Overlay.PlayerEspMenuIndex, ref Globals.Config.Player.Name);
                    ToggleControl(2, Overlay.PlayerEspMenuIndex, ref Globals.Config.Player.Distance);
                    Slider(3, Overlay.PlayerEspMenuIndex, ref Globals.Config.Player.MaxDistance, 0, 2500, 50);
                    ToggleControl(4, Overlay.PlayerEspMenuIndex, ref Globals.Config.Player.Weapon);
                    ToggleControl(5, Overlay.PlayerEspMenuIndex, ref Globals.Config.Player.Chams);
                    ToggleControl(6, Overlay.PlayerEspMenuIndex, ref Globals.Config.Player.Value);
                    ToggleControl(7, Overlay.PlayerEspMenuIndex, ref Globals.Config.Player.ShowFlags);
                    ToggleControl(8, Overlay.PlayerEspMenuIndex, ref Globals.Config.Player.KDRatio);
                }
                if (Overlay.ScavEspMenu)
                {
                    MenuVerticalControls(ref Overlay.ScavEspMenuIndex, Overlay.ScavEspMaxIndex);
                    SubMenuBackwards(ref Overlay.ScavEspMenu, ref Overlay.EspMenu);
                    ToggleControl(0, Overlay.ScavEspMenuIndex, ref Globals.Config.Scav.Enable);
                    ToggleControl(1, Overlay.ScavEspMenuIndex, ref Globals.Config.Scav.Name);
                    ToggleControl(2, Overlay.ScavEspMenuIndex, ref Globals.Config.Scav.Distance);
                    Slider(3, Overlay.ScavEspMenuIndex, ref Globals.Config.Scav.MaxDistance, 0, 2500, 50);
                    ToggleControl(4, Overlay.ScavEspMenuIndex, ref Globals.Config.Scav.Weapon);
                    ToggleControl(5, Overlay.ScavEspMenuIndex, ref Globals.Config.Scav.Chams);
                }
                if (Overlay.AimbotMenu)
                {
                    MenuVerticalControls(ref Overlay.AimbotMenuIndex, Overlay.AimbotMaxIndex);
                    SubMenuBackwards(ref Overlay.AimbotMenu, ref Overlay.MainMenu);
                    SubMenuControl(0, Overlay.AimbotMenuIndex, ref Overlay.AimbotMenu, ref Overlay.PlayerAimbotMenu);
                    SubMenuControl(1, Overlay.AimbotMenuIndex, ref Overlay.AimbotMenu, ref Overlay.ScavAimbotMenu);
                    ToggleControl(2, Overlay.AimbotMenuIndex, ref Globals.Config.Aimbot.Prediction);
                    Slider(3, Overlay.AimbotMenuIndex, ref Globals.Config.Aimbot.Fov, 0, 1100, 25);
                    ToggleControl(4, Overlay.AimbotMenuIndex, ref Globals.Config.Aimbot.DrawFov);
                }
                if (Overlay.ScavAimbotMenu)
                {
                    MenuVerticalControls(ref Overlay.ScavAimbotMenuIndex, Overlay.ScavAimbotMaxIndex);
                    SubMenuBackwards(ref Overlay.ScavAimbotMenu, ref Overlay.AimbotMenu);
                    ToggleControl(0, Overlay.ScavAimbotMenuIndex, ref Globals.Config.Aimbot.ScavAimbot);
                    ToggleControl(1, Overlay.ScavAimbotMenuIndex, ref Globals.Config.Aimbot.ScavRageAimbot);
                    Slider(2, Overlay.ScavAimbotMenuIndex, ref Globals.Config.Aimbot.ScavMaxDistance, 0, 1100, 25);
                    Slider(3, Overlay.ScavAimbotMenuIndex, ref Globals.Config.Aimbot.ScavHitChance, 0, 100, 5);
                    ToggleControl(4, Overlay.ScavAimbotMenuIndex, ref Globals.Config.Aimbot.ScavAutoShoot);
                    Slider(5, Overlay.ScavAimbotMenuIndex, ref Globals.Config.Aimbot.ScavMaxAutoDistance, 0, 1000, 25);
                    Slider(6, Overlay.ScavAimbotMenuIndex, ref Globals.Config.Aimbot.ScavHeadChance, 0, 100, 2);
                    Slider(7, Overlay.ScavAimbotMenuIndex, ref Globals.Config.Aimbot.ScavBodyChance, 0, 100, 2);
                    Slider(8, Overlay.ScavAimbotMenuIndex, ref Globals.Config.Aimbot.ScavLegChance, 0, 100, 2);

                }
                if (Overlay.PlayerAimbotMenu)
                {
                    MenuVerticalControls(ref Overlay.PlayerAimbotMenuIndex, Overlay.PlayerAimbotMaxIndex);
                    SubMenuBackwards(ref Overlay.PlayerAimbotMenu, ref Overlay.AimbotMenu);
                    ToggleControl(0, Overlay.PlayerAimbotMenuIndex, ref Globals.Config.Aimbot.PlayerAimbot);
                    ToggleControl(1, Overlay.PlayerAimbotMenuIndex, ref Globals.Config.Aimbot.PlayerRageAimbot);
                    Slider(2, Overlay.PlayerAimbotMenuIndex, ref Globals.Config.Aimbot.PlayerMaxDistance, 0, 1100, 25);
                    Slider(3, Overlay.PlayerAimbotMenuIndex, ref Globals.Config.Aimbot.PlayerHitChance, 0, 100, 5);
                    ToggleControl(4, Overlay.PlayerAimbotMenuIndex, ref Globals.Config.Aimbot.PlayerAutoShoot);
                    Slider(5, Overlay.PlayerAimbotMenuIndex, ref Globals.Config.Aimbot.PlayerMaxAutoDistance, 0, 1000, 25);
                    Slider(6, Overlay.PlayerAimbotMenuIndex, ref Globals.Config.Aimbot.PlayerHeadChance, 0, 100, 2);
                    Slider(7, Overlay.PlayerAimbotMenuIndex, ref Globals.Config.Aimbot.PlayerBodyChance, 0, 100, 2);
                    Slider(8, Overlay.PlayerAimbotMenuIndex, ref Globals.Config.Aimbot.PlayerLegChance, 0, 100, 2);
           
                }
                if (Overlay.ExfilEspMenu)
                {
                    MenuVerticalControls(ref Overlay.ExfilEspMenuIndex, Overlay.ExfilEspMaxIndex);
                    SubMenuBackwards(ref Overlay.ExfilEspMenu, ref Overlay.EspMenu);
                    ToggleControl(0, Overlay.ExfilEspMenuIndex, ref Globals.Config.Exfil.Enable);
                    ToggleControl(1, Overlay.ExfilEspMenuIndex, ref Globals.Config.Exfil.Distance);
                    Slider(2, Overlay.ExfilEspMenuIndex, ref Globals.Config.Exfil.MaxDistance, 0, 3000, 50);
                }
                if (Overlay.CorpseEspMenu)
                {
                    MenuVerticalControls(ref Overlay.CorpseEspMenuIndex, Overlay.CorpseEspMaxIndex);
                    SubMenuBackwards(ref Overlay.CorpseEspMenu, ref Overlay.EspMenu);
                    ToggleControl(0, Overlay.CorpseEspMenuIndex, ref Globals.Config.Corpse.Enable);
                    ToggleControl(1, Overlay.CorpseEspMenuIndex, ref Globals.Config.Corpse.Distance);
                    Slider(2, Overlay.CorpseEspMenuIndex, ref Globals.Config.Corpse.MaxDistance, 0, 1100, 25);
                    ToggleControl(3, Overlay.CorpseEspMenuIndex, ref Globals.Config.Corpse.Value);
                    Slider(4, Overlay.CorpseEspMenuIndex, ref Globals.Config.Corpse.MinValue, 0, 1000000, 1000);
                    KeybindControl(5, Overlay.CorpseEspMenuIndex, ref Globals.Config.Corpse.ContentsKey);
                    Slider(6, Overlay.CorpseEspMenuIndex, ref Globals.Config.Corpse.ContentsMinValue, 0, 1000000, 1000);

                }
                if (Overlay.ContainerEspMenu)
                {
                    MenuVerticalControls(ref Overlay.ContainerEspMenuIndex, Overlay.ContainerEspMaxIndex);
                    SubMenuBackwards(ref Overlay.ContainerEspMenu, ref Overlay.EspMenu);
                    ToggleControl(0, Overlay.ContainerEspMenuIndex, ref Globals.Config.Container.Enable);
                    ToggleControl(1, Overlay.ContainerEspMenuIndex, ref Globals.Config.Container.Distance);
                    Slider(2, Overlay.ContainerEspMenuIndex, ref Globals.Config.Container.MaxDistance, 0, 1100, 25);
                    ToggleControl(3, Overlay.ContainerEspMenuIndex, ref Globals.Config.Container.Value);
                    Slider(4, Overlay.ContainerEspMenuIndex, ref Globals.Config.Container.MinValue, 0, 1000000, 1000);
                    KeybindControl(5, Overlay.ContainerEspMenuIndex, ref Globals.Config.Container.ContentsKey);
                    Slider(6, Overlay.ContainerEspMenuIndex, ref Globals.Config.Container.ContentsMinValue, 0, 1000000, 1000);

                }

            }
            if (Input.GetKey(Globals.Config.Container.ContentsKey))
            {
                Overlay.ContainerLoot = true;
            }
            else
            {
                Overlay.ContainerLoot = false;
            }
            if (Input.GetKey(Globals.Config.Corpse.ContentsKey))
            {
                Overlay.CorpseLoot = true;
            }
            else
            {
                Overlay.CorpseLoot = false;
            }
        }
    }
    #endregion
    partial class Overlay : Form 
    {
        public static List<string> Valuable = new List<string>() { "LEDX", "BLion", "Ratchet","RK-2", "WClock","Taiga-1", "Graphics card", "Blue", "Keycard", "Yellow", "Green", "Red", "Violet", "Black", "Key card", "AESA", "GoldenStar", "Intelligence", "Ophthalmoscope", "Defibrillator", "VPX", "0.2BTC", "Bitcoin", "Video", "REAP-IR", "SSD", "T-7" ,"m-2", "sicc" , "Roler", "Box", "Thicc", "Keytool", "KIBA", "Lion", "Case", "Rebel", "Virtex", "Tetriz", "Rivals", "GPU" };
        #region GuiComponents
        private void SubMenu(PaintEventArgs e, uint index ,ref uint mainindex, string Text, string cText)
        {
            if (!Globals.Config.Misc.Chinese)
            {
                if (mainindex == index)
                {
                    DrawText(e, ">[+]" + Text, 12, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.CornflowerBlue, false);

                }
                else
                    DrawText(e, "[+]" + Text, 11, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.White, false);
            }
            else
            {
                if (mainindex == index)
                {
                    DrawText(e, ">[+]" + cText, 12, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.CornflowerBlue, false);

                }
                else
                    DrawText(e, "[+]" + cText, 11, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.White, false);
            }
        }
        private void Config(PaintEventArgs e, uint index, uint mainindex, string Text,string cText)
        {
            if (!Globals.Config.Misc.Chinese)
            {
                if (mainindex == index)
                {
                    DrawText(e, ">[-]" + Text, 12, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.CornflowerBlue, false);

                }
                else
                    DrawText(e, "[-]" + Text, 11, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.White, false);
            }
            else 
            {
                if (mainindex == index)
                {
                    DrawText(e, ">[-]" + cText, 12, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.CornflowerBlue, false);

                }
                else
                    DrawText(e, "[-]" + cText, 11, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.White, false);
            }
        }
        private void Toggle(PaintEventArgs e, uint index, string text,  string ctext, uint mainindex, bool toggle)
        {
            if (!Globals.Config.Misc.Chinese)
            {
                string status = toggle ? "Enabled" : "Disabled";
                if (mainindex == index)
                {
                    DrawText(e, ">" + text + ": " + status, 12, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.CornflowerBlue, false);
                }
                else
                    DrawText(e, text + ": " + status, 11, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.White, false);
            }
            else
            {
                string status = toggle ? "开启" : "未开启";
                if (mainindex == index)
                {
                    DrawText(e, ">" + ctext + ": " + status, 12, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.CornflowerBlue, false);
                }
                else
                    DrawText(e, ctext + ": " + status, 11, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.White, false);
            }
        }
        private void Keybind(PaintEventArgs e, uint index, string text, string ctext, uint mainindex, KeyCode key)
        {
            if (!Globals.Config.Misc.Chinese)
            {
                if (mainindex == index)
                {
                    DrawText(e, ">" + text + ": " + key.ToString(), 12, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.CornflowerBlue, false);
                }
                else
                    DrawText(e, text + ": " + key.ToString(), 11, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.White, false);
            }
            else 
            {
                if (mainindex == index)
                {
                    DrawText(e, ">" + ctext + ": " + key.ToString(), 12, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.CornflowerBlue, false);
                }
                else
                    DrawText(e, ctext + ": " + key.ToString(), 11, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.White, false);
            }
        }

        private void Slider(PaintEventArgs e, uint index, string text, string ctext, uint mainindex, float amount)
        {
            if (!Globals.Config.Misc.Chinese)
            {
                if (mainindex == index)
                {
                    DrawText(e, ">" + text + ": " + amount, 12, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.CornflowerBlue, false);
                }
                else
                    DrawText(e, text + ": " + amount, 11, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.White, false);
            }
            else
            {
                if (mainindex == index)
                {
                    DrawText(e, ">" + ctext + ": " + amount, 12, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.CornflowerBlue, false);
                }
                else
                    DrawText(e, ctext + ": " + amount, 11, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.White, false);
            }
        }
        private void Slider(PaintEventArgs e, uint index, string text, string ctext, uint mainindex, int amount)
        {

            if (!Globals.Config.Misc.Chinese)
            {
                if (mainindex == index)
                {
                    DrawText(e, ">" + text + ": " + amount, 12, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.CornflowerBlue, false);
                }
                else
                    DrawText(e, text + ": " + amount, 11, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.White, false);
            }
            else
            {
                if (mainindex == index)
                {
                    DrawText(e, ">" + ctext + ": " + amount, 12, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.CornflowerBlue, false);
                }
                else
                    DrawText(e, ctext + ": " + amount, 11, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.White, false);
            }
        }
        private void Slider(PaintEventArgs e, uint index, string text, string ctext, uint mainindex, int amount, string ending)
        {
            if (!Globals.Config.Misc.Chinese)
            {
                if (mainindex == index)
                {
                    DrawText(e, ">" + text + ": " + amount + ending, 12, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.CornflowerBlue, false);
                }
                else
                    DrawText(e, text + ": " + amount + ending, 11, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.White, false);
            }
            else
            {
                if (mainindex == index)
                {
                    DrawText(e, ">" + ctext + ": " + amount + ending, 12, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.CornflowerBlue, false);
                }
                else
                    DrawText(e, ctext + ": " + amount + ending, 11, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.White, false);
            }
        }
        private void Slider(PaintEventArgs e, uint index, string text, string ctext, uint mainindex, float amount, string ending)
        {
            if (!Globals.Config.Misc.Chinese)
            {
                if (mainindex == index)
                {
                    DrawText(e, ">" + text + ": " + amount + ending, 12, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.CornflowerBlue, false);
                }
                else
                    DrawText(e, text + ": " + amount + ending, 11, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.White, false);
            }
            else
            {
                if (mainindex == index)
                {
                    DrawText(e, ">" + ctext + ": " + amount + ending, 12, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.CornflowerBlue, false);
                }
                else
                    DrawText(e, ctext + ": " + amount + ending, 11, FontStyle.Bold, 20, index * 20 + 45, System.Drawing.Color.White, false);
            }
        }
        #endregion

            #region MenuVars
        public static bool MenuDraw = true;

        public static bool MainMenu = true;
        public static uint MainIndex = 0;
        public static uint MainMaxIndex = 4;

        public static bool MenuMenu = false;
        public static uint MenuMainIndex = 0;
        public static uint MenuMaxIndex = 2;

        public static bool EspMenu = false;
        public static uint EspMenuIndex = 0;
        public static uint EspMaxIndex = 6;

        public static bool GrenadeEspMenu = false;
        public static uint GrenadeEspMenuIndex = 0;
        public static uint GrenadeEspMaxIndex = 2;

        public static bool MiscMenu = false;
        public static uint MiscMenuIndex = 0;
        public static uint MiscMaxIndex = 8;

        public static bool MovementMiscMenu = false;
        public static uint MovementMiscMenuIndex = 0;
        public static uint MovementMiscMaxIndex = 13;

        public static bool WeaponMiscMenu = false;
        public static uint WeaponMiscMenuIndex = 0;
        public static uint WeaponMiscMaxIndex = 4;

        public static bool ExfilEspMenu = false;
        public static uint ExfilEspMenuIndex = 0;
        public static uint ExfilEspMaxIndex = 7;

        public static bool CorpseEspMenu = false;
        public static uint CorpseEspMenuIndex = 0;
        public static uint CorpseEspMaxIndex = 6;

        public static bool ContainerEspMenu = false;
        public static uint ContainerEspMenuIndex = 0;
        public static uint ContainerEspMaxIndex = 6;


        public static bool ItemEspMenu = false;
        public static uint ItemEspMenuIndex = 0;
        public static uint ItemEspMaxIndex = 9;

        public static bool PlayerEspMenu = false;
        public static uint PlayerEspMenuIndex = 0;
        public static uint PlayerEspMaxIndex = 8;

        public static bool ScavEspMenu = false;
        public static uint ScavEspMenuIndex = 0;
        public static uint ScavEspMaxIndex = 5;

        public static bool AimbotMenu = false;
        public static uint AimbotMenuIndex = 0;
        public static uint AimbotMaxIndex = 4;

        public static bool ScavAimbotMenu = false;
        public static uint ScavAimbotMenuIndex = 0;
        public static uint ScavAimbotMaxIndex = 8;

        public static bool PlayerAimbotMenu = false;
        public static uint PlayerAimbotMenuIndex = 0;
        public static uint PlayerAimbotMaxIndex = 8;

        public static bool ConfigMenu = false;
        public static uint ConfigMenuIndex = 0;
        public static uint ConfigMaxIndex = 1;

        public static bool SaveConfigMenu = false;
        public static uint SaveConfigMenuIndex = 0;
        public static uint SaveConfigMaxIndex = 5;

        public static bool LoadConfigMenu = false;
        public static uint LoadConfigMenuIndex = 0;
        public static uint LoadConfigMaxIndex = 5;

        public static bool CorpseLoot = false;
        public static bool ContainerLoot = false;

        #endregion
        private void DrawMenu(PaintEventArgs b)
        {

   //         
            if (!(Globals.Config.Aimbot.ScavHeadChance + Globals.Config.Aimbot.ScavLegChance + Globals.Config.Aimbot.ScavBodyChance == 100))
            {
                DrawText(b, $"Please Make Sure Scav Aimbot Head,Leg,Body Hitchance Adds To 100", 15, FontStyle.Bold, 600, 100, System.Drawing.Color.Red, false);
            }
            if (!(Globals.Config.Aimbot.PlayerHeadChance + Globals.Config.Aimbot.PlayerLegChance + Globals.Config.Aimbot.PlayerBodyChance == 100))
            {
                DrawText(b, $"Please Make Sure Player Aimbot Head,Leg,Body Hitchance Adds To 100", 15, FontStyle.Bold, 600, 200, System.Drawing.Color.Red, false);
            }
            try
            {
                Weapon wpn = (Weapon)Globals.LocalPlayer.HandsController.Item;
                
                //  DrawText(b, $"{Helpers.RaycastHelper.BarrelRayCastTest(Globals.LocalPlayer)}", 15, FontStyle.Bold, 600, 300, System.Drawing.Color.Red, false);  
                //    DrawText(b, $"{Globals.GameWorld.SharedBallisticsCalculator.DefaultHitBody.transform.position  + "|" + Globals.LocalPlayer.Transform.position}", 10, FontStyle.Bold, 100, 300, System.Drawing.Color.Red, false);
                DrawText(b, $"Gun: {wpn.ShortName.Localized()} | Ammo:{wpn.GetCurrentMagazine().Count}/{wpn.GetCurrentMagazine().MaxCount}", 8, FontStyle.Bold, UnityEngine.Screen.width / 2, 150, System.Drawing.Color.White, true);
            }
            catch
            { 
            }
            //  DrawText(b, RaycastHelper.BarrelRayCastTest(Globals.LocalPlayer), 14, FontStyle.Bold, 400, 100, System.Drawing.Color.White, false);
            DrawText(b, $"Players: {Globals.PlayerList.Count()}", 8, FontStyle.Bold, 10, UnityEngine.Screen.height - 155, System.Drawing.Color.White, false);
            DrawText(b, $"Exfil: {Globals.ExfilList.Count()}", 8, FontStyle.Bold, 10, UnityEngine.Screen.height - 140, System.Drawing.Color.White, false);
            DrawText(b, $"Items: {Globals.LootList.Count()}", 8, FontStyle.Bold, 10, UnityEngine.Screen.height - 125, System.Drawing.Color.White, false);
            DrawText(b, $"Corpses: {Globals.CorpseList.Count()}", 8, FontStyle.Bold, 10, UnityEngine.Screen.height - 110, System.Drawing.Color.White, false);
            DrawText(b, $"Grenades: {Globals.GrenadeList.Count()}", 8, FontStyle.Bold, 10, UnityEngine.Screen.height - 95, System.Drawing.Color.White, false);
            DrawText(b, $"Containers: {Globals.ContainerList.Count()}", 8, FontStyle.Bold, 10, UnityEngine.Screen.height - 80, System.Drawing.Color.White, false);

            DrawText(b, $"Auft.Net EFT", 8, FontStyle.Bold, 20, 20, System.Drawing.Color.White, false);

            if (Globals.Config.Menu.Radar)
            {
                b.Graphics.DrawEllipse(new Pen(new SolidBrush(System.Drawing.Color.White)), new Rectangle(UnityEngine.Screen.width - 280, 20, 250, 250)); // outline circle
                b.Graphics.FillEllipse(new SolidBrush(System.Drawing.Color.FromArgb(20, 255, 255, 255)), new Rectangle(UnityEngine.Screen.width - 280, 20, 250, 250)); // filled circle
                b.Graphics.DrawLine(new Pen(new SolidBrush(System.Drawing.Color.White)), new Point(UnityEngine.Screen.width - 153, 20), new Point(UnityEngine.Screen.width - 153, 270)); // from bottom to top
                b.Graphics.DrawLine(new Pen(new SolidBrush(System.Drawing.Color.White)), new Point(UnityEngine.Screen.width - 279, 145), new Point(UnityEngine.Screen.width - 30, 145)); // from right to left
            }                                                                                                                                                                       //     Point[] points = { new Point(UnityEngine.Screen.width - 153 + , 10), new Point(100, 10), new Point(50, 100) };
                                                                                                                                                                                     //b.Graphics.FillPolygon(new SolidBrush(System.Drawing.Color.Red), points);
                                                                                                                                                                                     //   b.Graphics.FillEllipse(new SolidBrush(System.Drawing.Color.Red), UnityEngine.Screen.width - 159, 140, 12, 11); // centre of circles
                                                                                                                                                                                     // UnityEngine.Screen.width - 153, 145,1,1 centre point
                                                                                                                                                                                     //   b.Graphics.DrawLine(new Pen(new SolidBrush(System.Drawing.Color.Red)), UnityEngine.Screen.width - 153, 145, UnityEngine.Screen.width - 153, 145);


            if (MenuDraw)
            {
                if (MainMenu)
                {
                    //     MenuVerticalControls(ref MainIndex, MainMaxIndex);
                    SubMenu(b, 0, ref MainIndex, "Esp", "透视");
                    SubMenu(b, 1, ref MainIndex, "Aimbot", "自瞄");
                    SubMenu(b, 2, ref MainIndex, "Misc", "杂项");
                    SubMenu(b, 3, ref MainIndex, "Menu", "菜单");
                    SubMenu(b, 4, ref MainIndex, "Config", "配置");
                }
                if (MenuMenu)
                {
                    Toggle(b, 0, "Radar", "雷达", MenuMainIndex, Globals.Config.Menu.Radar);
                    Toggle(b, 1, "Overlay Mode 2", "Overlay Mode 2", MenuMainIndex, Globals.Config.Menu.Mode2);
                    Toggle(b, 2, "Chinese Language", "Chinese Language", MenuMainIndex, Globals.Config.Misc.Chinese);
                }
                if (GrenadeEspMenu)
                {
                    Toggle(b, 0, "Enable", "开启", GrenadeEspMenuIndex, Globals.Config.Grenade.Enable);
                    Toggle(b, 1, "Distance", "距离", GrenadeEspMenuIndex, Globals.Config.Grenade.Distance);
                    Slider(b, 2, "Max Distance", "最大距离", GrenadeEspMenuIndex, Globals.Config.Grenade.MaxDistance, "m");
                }
                if (ConfigMenu)
                {
                    SubMenu(b, 0, ref ConfigMenuIndex, "Save", "节省");
                    SubMenu(b, 1, ref ConfigMenuIndex, "Load", "加载");
                }
                if (SaveConfigMenu)
                {
                    Config(b, 0, SaveConfigMenuIndex, "Config 1", "配置 1");
                    Config(b, 1, SaveConfigMenuIndex, "Config 2", "配置 2");
                    Config(b, 2, SaveConfigMenuIndex, "Config 3", "配置 3");
                    Config(b, 3, SaveConfigMenuIndex, "Config 4", "配置 4");
                    Config(b, 4, SaveConfigMenuIndex, "Config 5", "配置 5");
                    Config(b, 5, SaveConfigMenuIndex, "Config 6", "配置 6");
                }
                if (LoadConfigMenu)
                {
                    Config(b, 0, LoadConfigMenuIndex, "Config 1", "配置 1");
                    Config(b, 1, LoadConfigMenuIndex, "Config 2", "配置 2");
                    Config(b, 2, LoadConfigMenuIndex, "Config 3", "配置 3");
                    Config(b, 3, LoadConfigMenuIndex, "Config 4", "配置 4");
                    Config(b, 4, LoadConfigMenuIndex, "Config 5", "配置 5");
                    Config(b, 5, LoadConfigMenuIndex, "Config 6", "配置 6");
                }
                if (MiscMenu)
                {
                    SubMenu(b, 0, ref MiscMenuIndex, "Movement", "移动方式");
                    SubMenu(b, 1, ref MiscMenuIndex, "Weapon", "武器");
                    Toggle(b, 2, "No Visor", "无面罩", MiscMenuIndex, Globals.Config.Misc.NoVisor);
                    Toggle(b, 3, "Night Vision", "夜视", MiscMenuIndex, Globals.Config.Misc.NightVision);
                    Toggle(b, 4, "Thermal Vision", "热视觉", MiscMenuIndex, Globals.Config.Misc.ThermalVision);
                    Toggle(b, 5, "Night Mode", "夜间模式", MiscMenuIndex, Globals.Config.Misc.NightMode);
                    Toggle(b, 6, "Loot Through Walls", "通过墙壁抢劫", MiscMenuIndex, Globals.Config.Misc.LootThroughWalls);
                    Toggle(b, 7, "Custom Time", "自订时间", MiscMenuIndex, Globals.Config.Misc.CustomTime);
                    Slider(b, 8, "Time", "时间", MiscMenuIndex, Globals.Config.Misc.Time);
                }
                if (WeaponMiscMenu)
                {

                    Toggle(b, 0, "No Recoil", "无后座", WeaponMiscMenuIndex, Globals.Config.Misc.NoRecoil);
                    Toggle(b, 1, "No Sway", "无扩散", WeaponMiscMenuIndex, Globals.Config.Misc.NoSway);
                    Toggle(b, 2, "Full Auto", "全自动", WeaponMiscMenuIndex, Globals.Config.Misc.FullAuto);
                    Toggle(b, 3, "Weapon No Collide", "没有武器碰撞", WeaponMiscMenuIndex, Globals.Config.Misc.NoCollide);
                    Toggle(b, 4, "Ballistics Never Break", "弹道永远不断", WeaponMiscMenuIndex, Globals.Config.Misc.UnbreakableBallistics);
                }
                if (MovementMiscMenu)
                {
                    Toggle(b, 0, "Unlimited Stamina", "无线耐力", MovementMiscMenuIndex, Globals.Config.Misc.UnlimitedStamina);
                    Toggle(b, 1, "Speedhack", "速度切换", MovementMiscMenuIndex, Globals.Config.Misc.Speedhack);
                    Slider(b, 2, "Speed", "速度", MovementMiscMenuIndex, Globals.Config.Misc.Speed);
                    Keybind(b, 3, "Speed Toggle", "速度快捷键", MovementMiscMenuIndex, Globals.Config.Misc.Key);
                    Toggle(b, 4, "BunnyHop", "兔子跳", MovementMiscMenuIndex, Globals.Config.Misc.BunnyHop);
                    Slider(b, 5, "Time Between Hop", "跳跃之间的时间", MovementMiscMenuIndex, Globals.Config.Misc.TimeBetweenHop,"s");
                    Keybind(b, 6, "BunnyHop Toggle", "兔子跳键", MovementMiscMenuIndex, Globals.Config.Misc.HopKey);
                    Toggle(b, 7, "Bhop On Input", "走路时兔子跳", MovementMiscMenuIndex, Globals.Config.Misc.WaitForInput);
                    Toggle(b, 8, "Far Jump", "长跳", MovementMiscMenuIndex, Globals.Config.Misc.FarJump);
                    Slider(b, 9, "Far Jump Height", "跳远高度", MovementMiscMenuIndex, Globals.Config.Misc.FarJumpAmount);
                    Keybind(b, 10, "Far Jump Toggle", "跳远按钮", MovementMiscMenuIndex, Globals.Config.Misc.JumpKey);
                    Toggle(b, 11, "Slide Jump", "滑跳", MovementMiscMenuIndex, Globals.Config.Misc.SlideJump);
                    Slider(b, 12, "Slide Jump Time", "滑跳时间", MovementMiscMenuIndex, Globals.Config.Misc.SlideJumpTime);
                    Keybind(b, 13, "Slide Jump Toggle", "滑动按钮", MovementMiscMenuIndex, Globals.Config.Misc.SlideKey);
                }
               
                if (ItemEspMenu)
                {
                    Toggle(b, 0, "Enable", "开启", ItemEspMenuIndex, Globals.Config.Item.Enable);
                    Toggle(b, 1, "Distance", "距离", ItemEspMenuIndex, Globals.Config.Item.Distance);
                    Slider(b, 2, "Max Distance", "最大距离", ItemEspMenuIndex, Globals.Config.Item.MaxDistance, "m");
                    Toggle(b, 3, "Value", "价值", ItemEspMenuIndex, Globals.Config.Item.Value);
                    Toggle(b, 4, "All Items", "所有项目", ItemEspMenuIndex, Globals.Config.Item.DrawAll);
                    Toggle(b, 5, "Draw Quest Items", "任务项", ItemEspMenuIndex, Globals.Config.Item.DrawQuest);
                    Toggle(b, 6, "Draw Rare Items", "稀有物品", ItemEspMenuIndex, Globals.Config.Item.DrawSuperRare);
                    Toggle(b, 7, "Draw Wwhitelisted Items", "白名单项目", ItemEspMenuIndex, Globals.Config.Item.DrawWhitelisted);
                    Slider(b, 8, "Min Value", "最小值", ItemEspMenuIndex, Globals.Config.Item.MinValue, " rubs");
                    Toggle(b, 9, "Ignore Min Value", "忽略最小值", ItemEspMenuIndex, Globals.Config.Item.IgnoreMinValue);
                }
                if (EspMenu)
                {
                    SubMenu(b, 0, ref EspMenuIndex, "Player Esp", "玩家透视");
                    SubMenu(b, 1, ref EspMenuIndex, "Scav Esp", "Sacv 透视");
                    SubMenu(b, 2, ref EspMenuIndex, "Item Esp", "战利品透视");
                    SubMenu(b, 3, ref EspMenuIndex, "Corpse Esp", "尸体透视");
                    SubMenu(b, 4, ref EspMenuIndex, "Container Esp", "容器透视");
                    SubMenu(b, 5, ref EspMenuIndex, "Exfil Esp", "撤离点透视");
                    SubMenu(b, 6, ref EspMenuIndex, "Grenade Esp", "手榴弹透视");
                }
                if (CorpseEspMenu)
                {
                    Toggle(b, 0, "Enable", "开启", CorpseEspMenuIndex, Globals.Config.Corpse.Enable);
                    Toggle(b, 1, "Distance", "距离", CorpseEspMenuIndex, Globals.Config.Corpse.Distance);
                    Slider(b, 2, "Max Distance", "最大距离", CorpseEspMenuIndex, Globals.Config.Corpse.MaxDistance, "m");
                    Toggle(b, 3, "Value", "价值", CorpseEspMenuIndex, Globals.Config.Corpse.Value);
                    Slider(b, 4, "Min Value", "最小值", CorpseEspMenuIndex, Globals.Config.Corpse.MinValue, " rubs");
                    Keybind(b, 5, "Contents Key", "预览内容", CorpseEspMenuIndex, Globals.Config.Corpse.ContentsKey);
                    Slider(b, 6, "Contents Min Value","最小容器价值", CorpseEspMenuIndex, Globals.Config.Corpse.ContentsMinValue, " rubs");
                }
                if (ContainerEspMenu)
                {
                    Toggle(b, 0, "Enable", "开启", ContainerEspMenuIndex, Globals.Config.Container.Enable);
                    Toggle(b, 1, "Distance", "距离", ContainerEspMenuIndex, Globals.Config.Container.Distance);
                    Slider(b, 2, "Max Distance", "最大距离", ContainerEspMenuIndex, Globals.Config.Container.MaxDistance, "m");
                    Toggle(b, 3, "Value", "价值", ContainerEspMenuIndex, Globals.Config.Container.Value);
                    Slider(b, 4, "Min Value", "最小值", ContainerEspMenuIndex, Globals.Config.Container.MinValue, " rubs");
                    Keybind(b, 5, "Contents Key", "预览内容", ContainerEspMenuIndex, Globals.Config.Container.ContentsKey);
                    Slider(b, 6, "Contents Min Value", "最小容器价值", ContainerEspMenuIndex, Globals.Config.Container.ContentsMinValue, " rubs");
                }
                if (ExfilEspMenu)
                {
                    Toggle(b, 0, "Enable", "开启", ExfilEspMenuIndex, Globals.Config.Exfil.Enable);
                    Toggle(b, 1, "Distance", "距离", ExfilEspMenuIndex, Globals.Config.Exfil.Distance);
                    Slider(b, 2, "Max Distance", "最大距离", ExfilEspMenuIndex, Globals.Config.Exfil.MaxDistance, "m");
                }
                if (PlayerEspMenu)
                {
                    Toggle(b, 0, "Enable", "开启", PlayerEspMenuIndex, Globals.Config.Player.Enable);
                    Toggle(b, 1, "Name", "名称", PlayerEspMenuIndex, Globals.Config.Player.Name);
                    Toggle(b, 2, "Distance", "距离", PlayerEspMenuIndex, Globals.Config.Player.Distance);
                    Slider(b, 3, "Max Distance", "最大距离", PlayerEspMenuIndex, Globals.Config.Player.MaxDistance,"m");
                    Toggle(b, 4, "Weapon", "武器+子弹", PlayerEspMenuIndex, Globals.Config.Player.Weapon);
                    Toggle(b, 5, "Chams", "热能", PlayerEspMenuIndex, Globals.Config.Player.Chams);
                    Toggle(b, 6, "Value", "库存价值", PlayerEspMenuIndex, Globals.Config.Player.Value);
                    Toggle(b, 7, "Show Flags", "显示管理员", PlayerEspMenuIndex, Globals.Config.Player.ShowFlags);
                    Toggle(b, 8, "KD", "杀死比", PlayerEspMenuIndex, Globals.Config.Player.KDRatio);
                }
                if (ScavEspMenu)
                {
                    Toggle(b, 0, "Enable", "开启", ScavEspMenuIndex, Globals.Config.Scav.Enable);
                    Toggle(b, 1, "Name", "名称", ScavEspMenuIndex, Globals.Config.Scav.Name);
                    Toggle(b, 2, "Distance", "距离", ScavEspMenuIndex, Globals.Config.Scav.Distance);
                    Slider(b, 3, "Max Distance", "最大距离", ScavEspMenuIndex, Globals.Config.Scav.MaxDistance, "m");
                    Toggle(b, 4, "Weapon", "武器+子弹", ScavEspMenuIndex, Globals.Config.Scav.Weapon);
                    Toggle(b, 5, "Chams", "热能", ScavEspMenuIndex, Globals.Config.Scav.Chams);
                }
                if (AimbotMenu)
                {
                    SubMenu(b, 0, ref AimbotMenuIndex, "Player Silent Aim", "玩家自瞄");
                    SubMenu(b, 1, ref AimbotMenuIndex, "Scav Silent Aim", "scav 自瞄");
                    Toggle(b, 2, "Prediction", "子弹下落预测", AimbotMenuIndex, Globals.Config.Aimbot.Prediction);
                    Slider(b, 3, "Fov", "视野范围", AimbotMenuIndex, (int)Globals.Config.Aimbot.Fov,"px");
                    Toggle(b, 4, "Draw Fov", "显示视野", AimbotMenuIndex, Globals.Config.Aimbot.DrawFov);
                }
                if (ScavAimbotMenu)
                {
                    Toggle(b, 0, "Enable", "开启", ScavAimbotMenuIndex, Globals.Config.Aimbot.ScavAimbot);
                    Toggle(b, 1, "Rage Aimbot", "暴力自瞄", ScavAimbotMenuIndex, Globals.Config.Aimbot.ScavRageAimbot);
                    Slider(b, 2, "Max Distance", "最大距离", ScavAimbotMenuIndex, (int)Globals.Config.Aimbot.ScavMaxDistance, "m");
                    Slider(b, 3, "Hitchance", "击中概率", ScavAimbotMenuIndex, (int)Globals.Config.Aimbot.ScavHitChance," %");
                    Toggle(b, 4, "Auto Shoot", "自动射击", ScavAimbotMenuIndex, Globals.Config.Aimbot.ScavAutoShoot);
                    Slider(b, 5, "Auto Shoot Max Distance", "自动拍摄最大距离", ScavAimbotMenuIndex, (int)Globals.Config.Aimbot.ScavMaxAutoDistance, "m");
                    Slider(b, 6, "Legit Aimbot Head Chance", "头部击中概率", ScavAimbotMenuIndex, (int)Globals.Config.Aimbot.ScavHeadChance, " %");
                    Slider(b, 7, "Legit Aimbot Body Chance", "身体击中概率", ScavAimbotMenuIndex, (int)Globals.Config.Aimbot.ScavBodyChance, "%");
                    Slider(b, 8, "Legit Aimbot Leg Chance", "腿部击中概率", ScavAimbotMenuIndex, (int)Globals.Config.Aimbot.ScavLegChance, "%");
                  
                }
                if (PlayerAimbotMenu)
                {
                    Toggle(b, 0, "Enable", "开启", PlayerAimbotMenuIndex, Globals.Config.Aimbot.PlayerAimbot);
                    Toggle(b, 1, "Rage Aimbot", "暴力自瞄", PlayerAimbotMenuIndex, Globals.Config.Aimbot.PlayerRageAimbot);
                    Slider(b, 2, "Max Distance", "最大距离", PlayerAimbotMenuIndex, (int)Globals.Config.Aimbot.PlayerMaxDistance, "m");
                    Slider(b, 3, "Hitchance", "击中概率", PlayerAimbotMenuIndex, (int)Globals.Config.Aimbot.PlayerHitChance, " %");
                    Toggle(b, 4, "Auto Shoot", "自动射击", PlayerAimbotMenuIndex, Globals.Config.Aimbot.PlayerAutoShoot);
                    Slider(b, 5, "Auto Shoot Max Distance", "自动拍摄最大距离", PlayerAimbotMenuIndex, (int)Globals.Config.Aimbot.PlayerMaxAutoDistance, "m");
                    Slider(b, 6, "Legit Aimbot Head Chance", "头部击中概率", PlayerAimbotMenuIndex, (int)Globals.Config.Aimbot.PlayerHeadChance, " %");
                    Slider(b, 7, "Legit Aimbot Body Chance", "身体击中概率", PlayerAimbotMenuIndex, (int)Globals.Config.Aimbot.PlayerBodyChance, "%");
                    Slider(b, 8, "Legit Aimbot Leg Chance", "腿部击中概率", PlayerAimbotMenuIndex, (int)Globals.Config.Aimbot.PlayerLegChance, "%");

                }
                int InitialStyle = GetWindowLong(this.Handle, -20);
                if (Globals.Config.Menu.Mode2)
                {
                    SetWindowLong(this.Handle, -20, InitialStyle | 0x002 | 0x20);
                    modeset = true;
                }

            }

        }
        bool modeset = false;
        public Overlay()
        {
            InitializeComponent();


        }
    
        public struct RECT
        {
            public int left, top, right, bottom;
        }


        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string IpClassName, string IpWindowName);

      
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT IpRect);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd,
           int hWndInsertAfter, int x, int y, int cx, int cy, int uFlags);


        public const string WINDOW_NAME = "EscapeFromTarkov";


        int handle2 = (int)FindWindow(null, WINDOW_NAME);
        public IntPtr handle = FindWindow(null, WINDOW_NAME);
     
        IntPtr OurWindow;

        private const int HWND_TOP = 0;
        RECT rect;

        private static System.Random random = new System.Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private void Overlay_Load(object sender, EventArgs e)
        {
          
            Globals.FormInstance = this;
            System.Windows.Forms.Cursor.Hide();
            this.Text = RandomString(25);
            OurWindow = FindWindow(null, this.Text);

            this.DoubleBuffered = true;
            this.BackColor = System.Drawing.Color.Black;
            this.TransparencyKey = System.Drawing.Color.Black;
            //   this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;

            int InitialStyle = GetWindowLong(this.Handle, -20);


            //     DwmExtendFrameIntoClientArea(OurWindow, ref marg);
            /*                                   WS_EX_NOREDIRECTIONBITMAP         WS_EX_TRANSPARENT              */
            //       SetWindowLong(this.Handle, -20, InitialStyle | 0x002 | 0x20);
            /*                                   WS_EX_NOREDIRECTIONBITMAP         WS_EX_TRANSPARENT              */
            if (Globals.Config.Menu.Mode2)
            {
                SetWindowLong(this.Handle, -20, InitialStyle | 0x002 | 0x20);
                modeset = true;
            }
            else
            {
                SetWindowLong(this.Handle, -20, InitialStyle | 0x002);
                modeset = false;
            }
            GetWindowRect(handle, out rect);
            this.Size = new Size(rect.right - rect.left, rect.bottom - rect.top);
            this.Top = rect.top;
            this.Left = rect.left;

        
        }

 
     
        public static bool IsScreenPointVisible(Vector3 screenPoint)
        {
            return screenPoint.z > 0.01f && screenPoint.x > -5f && screenPoint.y > -5f && screenPoint.x < (float)UnityEngine.Screen.width && screenPoint.y < (float)UnityEngine.Screen.height;
        }
        #region EspObjects
        public void DrawText(PaintEventArgs b,string text,int size, FontStyle style, float x, float y, System.Drawing.Color colour, bool centre)
        {
            Font font = new Font("Courier New", size, style);
            if (centre)
            {
              
                SizeF sizeF = b.Graphics.MeasureString(text, font);
                x -= (int)sizeF.Width/2;

                StringFormat stringFormat = new StringFormat();
                // b.Graphics.DrawString(text, font, new SolidBrush(colour), new Point((int)x, (int)y));
                TextRenderer.DrawText(b.Graphics, text, font, new Point((int)x, (int)y), System.Drawing.Color.Black);
                TextRenderer.DrawText(b.Graphics,text, font, new Point((int)x , (int)y),colour);
          
            }
            else
            {
                //  b.Graphics.DrawString(text, font, new SolidBrush(colour), new Point((int)x, (int)y));
                TextRenderer.DrawText(b.Graphics, text, font, new Point((int)x, (int)y), System.Drawing.Color.Black);
                TextRenderer.DrawText(b.Graphics, text, font, new Point((int)x, (int)y), colour);
                
            }
        
        }
        public void DrawCornerBox(PaintEventArgs b, float HeadPosy, float BasePosx, float BasePosy, int Width, bool Filled, System.Drawing.Color Colour, System.Drawing.Color FilledColour)
        {
                Pen pen = new Pen(Colour);
            Brush brush = new SolidBrush(Colour);
            Brush black = new SolidBrush(System.Drawing.Color.Black);
            //   b.Graphics.DrawLine(pen,new Point((int)HeadPosx))

            b.Graphics.FillEllipse(new SolidBrush(System.Drawing.Color.White), new RectangleF(new PointF(BasePosx, BasePosy), new SizeF(5, 5)));
            b.Graphics.FillEllipse(new SolidBrush(System.Drawing.Color.White), new RectangleF(new PointF(BasePosx, HeadPosy), new SizeF(5, 5)));

            int num = (int)(Width / 4f);
            int num2 = num;

            // top left outline
            b.Graphics.FillRectangle(black, new Rectangle((int)BasePosx - Width / 2 - 1, (int)HeadPosy - 1, num + 2, 3));
            b.Graphics.FillRectangle(black, new Rectangle((int)BasePosx - Width / 2 - 1, (int)HeadPosy - 1 ,3, num2 + 2));

     //       b.Graphics.FillRectangle(black, new Rectangle((int)BasePosx - Width / 2 -num-1, (int)HeadPosy - 1, num +2, 3));
     //       b.Graphics.FillRectangle(black, new Rectangle((int)BasePosx - Width / 2  - 1, (int)HeadPosy - 1, 3, num2 + 2));



            // top left
            /*     b.Graphics.DrawLine(pen, new Point(100, 100), new Point(100, 110));
                 b.Graphics.DrawLine(pen, new Point(100, 100), new Point(110, 100));

                 // bottom left
                 b.Graphics.DrawLine(pen, new Point(100, 160), new Point(100, 170));
                 b.Graphics.DrawLine(pen, new Point(100, 170), new Point(110, 170));


                 // top right
                 b.Graphics.DrawLine(pen, new Point(170, 100), new Point(170, 110));
                 b.Graphics.DrawLine(pen, new Point(160, 100), new Point(170, 100));

                 // bottom right
                 b.Graphics.DrawLine(pen, new Point(170, 160), new Point(170, 170));
                 b.Graphics.DrawLine(pen, new Point(160, 170), new Point(170, 170));*/

        }

        #endregion
      
    
        private void Overlay_Paint(object sender, PaintEventArgs e)
        {
          
            Invalidate();
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            Pen whitepen = new Pen(System.Drawing.Color.White);
         

            if (Globals.Config.Aimbot.DrawFov)
                e.Graphics.DrawEllipse(whitepen, (UnityEngine.Screen.width / 2) - (Globals.Config.Aimbot.Fov), (UnityEngine.Screen.height / 2) - (Globals.Config.Aimbot.Fov), Globals.Config.Aimbot.Fov *2, Globals.Config.Aimbot.Fov *2);
            try
            {
                //   DrawCornerBox(e, 200, 250, 250, 25, false, System.Drawing.Color.Red, System.Drawing.Color.White);
             //   ConsoleScreen.AutoEmptyWorkingSet = true;
             
                #region ExfilEsp
                if (Globals.Config.Exfil.Enable)
                {
                    foreach (ExfilObject exfil in Globals.ExfilList)
                    {
                        if (exfil != null)
                        {
                            Vector3 screenpos = exfil.w2s;

                            if (IsScreenPointVisible(screenpos))
                            {
                                if (exfil.exfilpoint.Status != EExfiltrationStatus.NotPresent)
                                {
                                    //  if (!Globals.GameWorld.ExfiltrationController.ScavExfiltrationPoints.Contains(exfil))

                                    string Name = "";
                                    #region ExfilNameSwitch
                                    switch (exfil.exfilpoint.Settings.Name)
                                    {
                                        case "Gate m":
                                            Name = "Med Tent";
                                            break;
                                        case "SE Exfil":
                                            Name = "Emercom";
                                            break;
                                        case "NW Exfil":
                                            Name = "Railway";
                                            break;
                                        case "Alpinist":
                                            Name = "Cliff Decent";
                                            break;

                                        case "un-sec":
                                            Name = "Northern Roadblock";
                                            break;


                                        case "South V-Ex":
                                            Name = "Bridge V-Ex";
                                            break;

                                        case "EXFIL_ZB013":
                                            Name = "ZB013";
                                            break;


                                        case "EXFIL_Bunker_D2":
                                            Name = "Bunker D2";
                                            break;
                                        case "EXFIL_ScavCooperation":
                                            Name = "Scav Co-op";
                                            break;
                                        case "EXFIL_Bunker":
                                            Name = "Bunker";
                                            break;
                                        case "EXFIL_Vent":
                                            Name = "Vent";
                                            break;
                                        case "EXFIL_Train":
                                            Name = "Trains";
                                            break;
                                        default:
                                            Name = exfil.exfilpoint.Settings.Name;

                                            break;

                                    }

                                    #endregion
                                    System.Drawing.Color ExfilColour = System.Drawing.Color.FromArgb(255, 0, 255, 50);
                                    float posx = 0;
                                    float posy = 0;
                                    int distance = (int)Vector3.Distance(Globals.MainCamera.transform.position, exfil.exfilpoint.transform.position);
                                    string DistanceString = Globals.Config.Exfil.Distance == true ? $"({distance}m)" : "";
                                    

                                    posx = screenpos.x;
                                    posy = screenpos.y;
                                    string Status = string.Empty;
                                    if (exfil.exfilpoint.Status == EExfiltrationStatus.RegularMode)
                                        Status = "";
                                    if (exfil.exfilpoint.Status == EExfiltrationStatus.UncompleteRequirements)
                                        Status = "[Requirements]";
                                    if (exfil.exfilpoint.Status == EExfiltrationStatus.AwaitsManualActivation)
                                        Status = "[Activation]";
                                    if(distance <= Globals.Config.Exfil.MaxDistance)
                                    DrawText(e, Name + Status + DistanceString, 8, FontStyle.Bold, posx, posy, ExfilColour, true);

                                }
                            }

                        }

                    }
                }
                    #endregion
                #region PlayerEsp
                    foreach (PlayerObject player in Globals.PlayerList)
                    {


                        if (player != null && player.player.HealthController.IsAlive)
                        {

                     //   BodyPartCollider[] Colliders = player.player.gameObject.GetComponentsInChildren<BodyPartCollider>();
                        //     foreach (BodyPartCollider bodypartcollider in Colliders)
                        //      {
                        //        bodypartcollider.transform.position = Globals.LocalPlayer.Transform.position + Camera.main.transform.forward * 3;
                        //       bodypartcollider.TypeOfMaterial = EFT.Ballistics.MaterialType.Glass;
                        //   }
                      //  foreach (var ballistics in Globals.GameWorld.ballisticsc)
                            Vector3 screenpos = player.w2s;

                            System.Drawing.Color PlayerColour = System.Drawing.Color.FromArgb(255, 255, 255, 255);
                            Vector3 Headpos = player.w2shead;


                            bool isscav = false;

                            float posx = 0;
                            float posy = 0;

                            float headposx = 0;
                            float headposy = 0;

                            posx = screenpos.x;
                            posy = screenpos.y;

                            headposx = Headpos.x;
                            headposy = Headpos.y;

                            string name = "";
                            int distance = int.MinValue;
                            string Weapon = "Empty";
                            int AmmoCurrent = 0;
                            int AmmoMax = 0;

                            isscav = player.player.Profile.Info.RegistrationDate <= 0;


                        try
                        {
                            if (player.player?.HandsController?.Item is Weapon)
                            {
                                Weapon wpn = (Weapon)player.player?.HandsController?.Item;

                                if (wpn != null)
                                {
                                    if (wpn.ShortName.Localized() != null)
                                    {
                                        Weapon = wpn.ShortName.Localized();
                                        AmmoCurrent = wpn.GetCurrentMagazine().Count;
                                        AmmoMax = wpn.GetCurrentMagazine().MaxCount;
                                    }
                                    else
                                    {
                                        Weapon = "Empty";
                                        AmmoCurrent = 0;
                                        AmmoMax = 0;
                                    }
                                }
                                else
                                {
                                    Weapon = "Empty";
                                    AmmoCurrent = 0;
                                    AmmoMax = 0;
                                }

                            }
                        }
                        catch
                        {
                            Weapon = "Empty";
                            AmmoCurrent = 0;
                            AmmoMax = 0;
                        }

                            distance = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.player.Transform.position);
                            if (!isscav)
                            {
                                name = player.player.Profile.Info.Nickname.Localized();
                            }
                        if (Globals.IsInYourGroup(player.player))
                        {
                            PlayerColour = System.Drawing.Color.Coral;
                        }
                            if (isscav && !player.player.Profile.Info.Settings.Role.IsBoss())
                            {
                                name = "Scav";
                                PlayerColour = System.Drawing.Color.Cyan;
                            }
                            if (isscav && player.player.Profile.Info.Settings.Role.IsBoss())
                            {
                                switch (player.player.Profile.Info.Settings.Role)
                                {
                                    case WildSpawnType.assaultGroup:
                                        name = "Raider";
                                        break;
                                    case WildSpawnType.assault:
                                        name = "Raider";
                                        break;
                                    case WildSpawnType.bossKilla:
                                        name = "Killa";
                                        break;
                                    case WildSpawnType.bossSanitar:
                                        name = "Sanitar";
                                        break;
                                    case WildSpawnType.bossGluhar:
                                        name = "Glukhar";
                                        break;
                                    case WildSpawnType.bossKojaniy:
                                        name = "Shturman";
                                        break;
                                    case WildSpawnType.bossBully:
                                        name = "Reshala";
                                        break;
                                    case WildSpawnType.bossTagilla:
                                        name = "Tagilla";
                                        break;
                                    case WildSpawnType.followerBully:
                                        name = "Guard";
                                        break;
                                    case WildSpawnType.followerGluharAssault:
                                        name = "Guard";
                                        break;
                                    case WildSpawnType.followerGluharScout:
                                        name = "Guard";
                                        break;
                                    case WildSpawnType.followerGluharSecurity:
                                        name = "Guard";
                                        break;
                                    case WildSpawnType.followerGluharSnipe:
                                        name = "Guard";
                                        break;
                                    case WildSpawnType.followerKojaniy:
                                        name = "Guard";
                                        break;
                                    case WildSpawnType.followerSanitar:
                                        name = "Guard";
                                        break;
                                    case WildSpawnType.followerTagilla:
                                        name = "Guard";
                                        break;
                                    default:
                                        name = "Raider";
                                        break;

                                }
                                PlayerColour = System.Drawing.Color.Red;
                            }

                        if (player.player == Aimbot.Targetplayer)
                           PlayerColour = System.Drawing.Color.FromArgb(255, 102, 0, 255);

                            Brush brush = new SolidBrush(PlayerColour);
                            Pen pen1 = new Pen(PlayerColour);


                            if (IsScreenPointVisible(screenpos))
                            {
                                if (isscav == false && Globals.Config.Player.Enable)
                                {
                                    if (distance < Globals.Config.Player.MaxDistance)
                                    {
                                    float invcost = 0;
                                    foreach (Item item in player.player.Profile.Inventory.Equipment.GetAllItems(false))
                                    {

                                        invcost += item.Template.CreditsPrice;
                                        if (item.Template._parent == "5448bf274bdc2dfc2f8b456a" || item.Template._parent == "5447e1d04bdc2dff2f8b4567")
                                        {
                                            foreach (Item otem in item.GetAllItems(false))
                                            {
                                                invcost -= otem.Template.CreditsPrice;
                                            }

                                        }
                                    }
                                    if (Globals.Config.Player.ShowFlags)
                                    {
                                        string flag = "";
                                        switch (player.player.Profile.Info.MemberCategory)
                                        {
                                            case EMemberCategory.ChatModerator:
                                                flag = "Mod";
                                                break;
                                            case EMemberCategory.ChatModeratorWithPermanentBan:
                                                flag = "Mod+";
                                                break;
                                            case EMemberCategory.Developer:
                                                flag = "Developer";
                                                break;
                                            case EMemberCategory.Emissary:
                                                flag = "Emissary";
                                                break;
                                            case EMemberCategory.Sherpa:
                                                flag = "Sherpa";
                                                break;
                                            default:
                                                flag = "";
                                                break;
                                        }
                                        DrawText(e, flag, 8, FontStyle.Bold, headposx, headposy - 15, PlayerColour, true);

                                    }

                                    long allLong5 = player.player.Profile.Stats.OverallCounters.GetAllLong(new object[]
                                     {
                                     EFT.Counters.CounterTag.KilledPmc
                                     });
                                    long allLong6 = player.player.Profile.Stats.OverallCounters.GetAllLong(new object[]
                                     {
                                    EFT.Counters.CounterTag.ExitStatus,
                            ExitStatus.Killed
                                     });

                                    if (allLong6 == 0)
                                        allLong6 = 1;

                                    string KD = Globals.Config.Player.KDRatio == true ? $"(KD:{(Math.Round(((float)allLong5/(float)allLong6),2))})" : "";
                                    string GunString = $"{Weapon}({AmmoCurrent}/{AmmoMax})";
                                        string DistanceString = Globals.Config.Player.Distance == true ? $"({distance}m)" : "";
                                    string Value = Globals.Config.Player.Value == true ? $"({(int)invcost/1000}k)" : "";

                                        if (Globals.Config.Player.Name == false)
                                            name = "";
                                        if (Globals.Config.Player.Distance == false)
                                            DistanceString = "";

                                    if (Globals.Config.Player.Chams)
                                        ShaderHelper.ApplyShader(ShaderHelper.Shaders["Chams"], player.player.gameObject, new Color32(0, 255, 255, 255), new Color32(145, 0, 255, 255));
                                    if(!Globals.Config.Player.Chams)
                                        ShaderHelper.RemoveShaders(player.player.gameObject);

                                    string NameDistance = name + DistanceString + Value + KD;

                                        if (Globals.Config.Player.Name || Globals.Config.Player.Distance)
                                            DrawText(e, NameDistance, 8, FontStyle.Bold, headposx, posy, PlayerColour, true);

                                        if (Globals.Config.Player.Weapon)
                                            DrawText(e, GunString, 8, FontStyle.Bold, headposx, posy + 15, PlayerColour, true);

                                    if (!Globals.Config.Player.Chams)
                                        ShaderHelper.RemoveShaders(player.player.gameObject);

                                }
                                }
                                if (isscav == true && Globals.Config.Scav.Enable)
                                {
                                    if (distance < Globals.Config.Scav.MaxDistance)
                                    {
                                        string GunString = $"{Weapon}({AmmoCurrent}/{AmmoMax})";
                                    string DistanceString = Globals.Config.Player.Distance == true ? $"({distance}m)" : "";

                                    if (Globals.Config.Scav.Chams)
                                        ShaderHelper.ApplyShader(ShaderHelper.Shaders["Chams"], player.player.gameObject, new Color32(0, 255, 255, 255), new Color32(145, 0, 255, 255));
                            //        if(!Globals.Config.Scav.Chams)
                                    //    ShaderHelper.RemoveShaders(player.player.gameObject);
                                        if (Globals.Config.Scav.Name == false)
                                            name = "";
                                        if (Globals.Config.Scav.Distance == false)
                                            DistanceString = "";


                                        string NameDistance = name + DistanceString;

                                        if (Globals.Config.Scav.Name || Globals.Config.Scav.Distance)
                                            DrawText(e, NameDistance, 8, FontStyle.Bold, headposx, posy, PlayerColour, true);

                                        if (Globals.Config.Scav.Weapon)
                                            DrawText(e, GunString, 8, FontStyle.Bold, headposx, posy + 15, PlayerColour, true);

                                    if (!Globals.Config.Scav.Chams)
                                        ShaderHelper.RemoveShaders(player.player.gameObject);
                                     
                                    }
                                }

                            }




                        #region Radar

                        if (Globals.Config.Menu.Radar)
                        {
                            Vector3 centerPos = Globals.LocalPlayer.Transform.position;
                            Vector3 extPos = player.player.Transform.position;

                            float dist = Vector3.Distance(centerPos, extPos);

                            float dx = centerPos.x - extPos.x;
                            float dz = centerPos.z - extPos.z;

                            float deltay = Mathf.Atan2(dx, dz) * Mathf.Rad2Deg - 270 - Globals.LocalPlayer.Transform.eulerAngles.y;

                            float bX = dist * Mathf.Cos(deltay * Mathf.Deg2Rad);
                            float bY = dist * Mathf.Sin(deltay * Mathf.Deg2Rad);
                            // 400 is the max distance
                            bX = bX * ((float)250 / (float)400) / 2f;
                            bY = bY * ((float)250 / (float)400) / 2f;


                            if (dist < 400 && Vector2.Distance(new Vector2(UnityEngine.Screen.width - 153, 145), new Vector2(UnityEngine.Screen.width - 157 + bX, 141 + bY)) < 230)
                                e.Graphics.FillEllipse(new SolidBrush(System.Drawing.Color.Red), UnityEngine.Screen.width - 157 + bX, 141 + bY, 8, 9);

                            //   b.Graphics.FillEllipse(new SolidBrush(System.Drawing.Color.Red), UnityEngine.Screen.width - 159, 140, 12, 11); // centre of circles
                            // UnityEngine.Screen.width - 153, 145,1,1 centre point
                            //  e.Graphics.draw
                        }

                            #endregion
                        }
                    }
                #endregion
                #region ItemEsp
                if (Globals.Config.Item.Enable)
                {
                    foreach (ItemObject item in Globals.LootList)
                    {
                        if (item != null)
                        {
                            Vector3 screenpos = item.w2s;
                            if (IsScreenPointVisible(screenpos))
                            {
                                float distance = Vector3.Distance(Globals.MainCamera.transform.position, item.item.transform.position);
                                if (distance <= Globals.Config.Item.MaxDistance)
                                {
                                    string Name = item.item.Item.ShortName.Localized();
                                    string Distance = Globals.Config.Item.Distance == true ? $"({(int)distance}m)" : "";
                                    string Value = Globals.Config.Item.Value == true ? $"({(int)item.item.Item.Template.CreditsPrice / 1000}k)" : "";
                                    item.item.Item.Template.ExamineTime = 0;
                                    System.Drawing.Color ItemColour = System.Drawing.Color.Magenta;
                                    if (Globals.Config.Item.DrawAll)
                                    {
                                        if (item.item.Item.Template.CreditsPrice >= Globals.Config.Item.MinValue && !Globals.Config.Item.IgnoreMinValue)
                                        {
                                            DrawText(e, Name + Distance + Value, 8, FontStyle.Bold, screenpos.x, screenpos.y, ItemColour, true);
                                        }

                                    }
                                    if (!Globals.Config.Item.DrawAll)
                                    {
                                        if (item.item.Item.Template.CreditsPrice >= Globals.Config.Item.MinValue && !Globals.Config.Item.IgnoreMinValue)
                                        {
                                        
                                            if (item.item.Item.Template.QuestItem && Globals.Config.Item.DrawQuest)
                                            {
                                                DrawText(e, Name + Distance + Value, 8, FontStyle.Bold, screenpos.x, screenpos.y, ItemColour, true);
                                            }
                                            if (Valuable.Contains(Name) && Globals.Config.Item.DrawWhitelisted)
                                            {
                                                DrawText(e, Name + Distance + Value, 8, FontStyle.Bold, screenpos.x, screenpos.y, ItemColour, true);
                                            }
                                            if (item.item.Item.Template.Rarity == JsonType.ELootRarity.Superrare && Globals.Config.Item.DrawSuperRare && !(Valuable.Contains(Name) && Globals.Config.Item.DrawWhitelisted))
                                            {
                                                DrawText(e, Name + Distance + Value, 8, FontStyle.Bold, screenpos.x, screenpos.y, ItemColour, true);
                                            }


                                        }
                                    }
                                }


                            }

                        }

                    }
                }
                #endregion
                #region Container
                if (Globals.Config.Container.Enable)
                {
                    foreach (ContainerObject container in Globals.ContainerList)
                    {
                        Vector3 Screenpos = container.w2s;
                        float distance = Vector3.Distance(Globals.MainCamera.transform.position, container.container.transform.position);
                        if (IsScreenPointVisible(Screenpos))
                        {
                            if (distance <= Globals.Config.Container.MaxDistance)
                            {
                                int value = 0;
                                List<Item> itemlist = container.container.ItemOwner.RootItem.GetAllItems(false).ToList();
                                System.Drawing.Color ContainerColour = System.Drawing.Color.FromArgb(255, 255, 255, 0);
                                string Distance = Globals.Config.Container.Distance == true ? $"({(int)distance}m)" : "";
                                foreach (Item item in container.container.ItemOwner.RootItem.GetAllItems(false).ToList())
                                {
                                    value += item.Template.CreditsPrice;
                                    item.Template.ExamineTime = 0f;
                              
                                    // remove items if they are melee weapons or secure containers
                                    if (item.Template._parent == "5448bf274bdc2dfc2f8b456a" || item.Template._parent == "5447e1d04bdc2dff2f8b4567")
                                    {
                                        foreach (Item otem in item.GetAllItems(false))
                                        {
                                            value -= otem.Template.CreditsPrice;
                                            //itemlist.Remove(otem);
                                            otem.Template.ExamineTime = 0f;

                                            itemlist.Remove(otem);
                                        }

                                    }
                                    if (item.ShortName.Localized() == "Default Inventory")
                                        itemlist.Remove(item);
                                    if (item.Template.CreditsPrice < Globals.Config.Container.ContentsMinValue)
                                        itemlist.Remove(item);

                                  
                                }
                                foreach (Item item in container.container.ItemOwner.RootItem.GetAllItems(false).ToList())
                                {
                                    if (Overlay.ContainerLoot && value >= Globals.Config.Container.MinValue)
                                    {
                                        if (itemlist.Contains(item))
                                        {
                                            string itemvalue = Globals.Config.Container.Value == true ? $"({(int)item.Template.CreditsPrice / 1000}k)" : "";
                                            DrawText(e, item.ShortName.Localized() + itemvalue, 8, FontStyle.Bold, Screenpos.x, Screenpos.y + 19 + (itemlist.IndexOf(item) * 18), System.Drawing.Color.White, true);
                                        }
                                    }

                                }
                                string Value = Globals.Config.Corpse.Value == true ? $"({(int)value / 1000}k)" : "";
                                if (value >= Globals.Config.Container.MinValue)
                                {
                                    DrawText(e, $"{container.container.Template.LocalizedShortName()}" + Distance + Value, 8, FontStyle.Bold, Screenpos.x, Screenpos.y, ContainerColour, true);
                                }
                            }
                        }

                    }
                }
                #endregion
                #region Corpse
                if (Globals.Config.Corpse.Enable)
                {
                    foreach (CorpseObject corpse in Globals.CorpseList)
                    {
                        Vector3 Screenpos = corpse.w2s;
                        float distance = Vector3.Distance(Globals.MainCamera.transform.position, corpse.corpse.transform.position);
                        if (IsScreenPointVisible(Screenpos))
                        {
                            if (distance <= Globals.Config.Corpse.MaxDistance)
                            {
                               

                                int value = 0;
                                List<Item> itemlist = corpse.corpse.ItemOwner.RootItem.GetAllItems(false).ToList();
                                System.Drawing.Color CorpseColour = System.Drawing.Color.FromArgb(255, 100, 100, 255);
                                string Distance = Globals.Config.Corpse.Distance == true ? $"({(int)distance})m" : "";
                               
                                foreach (Item item in corpse.corpse.ItemOwner.RootItem.GetAllItems(false).ToList())
                                {
                                    value += item.Template.CreditsPrice;
                                    item.Template.ExamineTime = 0f;
                                 
                                    // remove items if they are melee weapons or secure containers
                                    if (item.Template._parent == "5448bf274bdc2dfc2f8b456a" || item.Template._parent == "5447e1d04bdc2dff2f8b4567")
                                    {
                                        
                                        foreach (Item otem in item.GetAllItems(false))
                                        {
                                            value -= otem.Template.CreditsPrice;
                                            //itemlist.Remove(otem);
                                            otem.Template.ExamineTime = 0f;

                                            itemlist.Remove(otem);
                                        }

                                    }
                                    if (item.ShortName.Localized() == "Default Inventory")
                                        itemlist.Remove(item);
                                    if (item.Template.CreditsPrice < Globals.Config.Corpse.ContentsMinValue)
                                        itemlist.Remove(item);
                                 
                                 

                                }
                                foreach (Item item in corpse.corpse.ItemOwner.RootItem.GetAllItems(false).ToList())
                                {
                                    if (Overlay.CorpseLoot && value >= Globals.Config.Corpse.MinValue)
                                    {
                                        if (itemlist.Contains(item))
                                        {
                                            string itemvalue = Globals.Config.Corpse.Value == true ? $"({(int)item.Template.CreditsPrice / 1000}k)" : "";
                                            DrawText(e, item.ShortName.Localized() + itemvalue, 8, FontStyle.Bold, Screenpos.x, Screenpos.y + 19 + (itemlist.IndexOf(item) * 18), System.Drawing.Color.White, true);
                                        }
                                    }
                                }
                                string Value = Globals.Config.Corpse.Value == true ? $"({(int)value / 1000}k)" : "";
                                if (value >= Globals.Config.Corpse.MinValue)
                                {
                                    DrawText(e, $"Corpse" + Distance + Value, 8, FontStyle.Bold, Screenpos.x, Screenpos.y, CorpseColour, true);
                                }
                               
                                

                                }
                            }

                    }


                }
                #endregion
                #region Grenade
                foreach (GrenadeObject nade in Globals.GrenadeList)
                {
                    if (nade.Grenade != null)
                    {
                        Vector3 screenpos = nade.w2s;

                        if (IsScreenPointVisible(screenpos))
                        {

                            System.Drawing.Color NadeColour = System.Drawing.Color.Red;
                            int distance = (int)Vector3.Distance(Globals.MainCamera.transform.position, nade.Grenade.transform.position);
                            string DistanceString = Globals.Config.Grenade.Distance == true ? $"({distance}m)" : "";
                          
                            if (distance <= Globals.Config.Grenade.MaxDistance)
                            {
                                DrawText(e, "Grenade" + DistanceString, 8, FontStyle.Bold, screenpos.x, screenpos.x, NadeColour, true);
                            }

                        }

                    }
                }
                #endregion


            }
            catch
                {

                }


            // dont move this, only do this 1 time a frame to prevent you losing 30+ fps


            DrawMenu(e);
            System.Windows.Forms.Cursor.Hide();
            SetWindowPos(handle, (int)OurWindow, 0, 0, 0, 0, 0x0002 | 0x0001);
      


            //    CacheEsp();
            //         Invalidate();
            //     Update();


        }


        private void timer1_Tick(object sender, EventArgs e)
        {
     

        }

        private void Overlay_FormClosing(object sender, FormClosingEventArgs e)
        {
        //w    Environment.Exit(0);
        }

        private void Overlay_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Show();
        }

        private void DrawTimer_Tick(object sender, EventArgs e)
        {
         
        }

        private void Overlay_VisibleChanged(object sender, EventArgs e)
        {
          //  File.WriteAllText("vis.txt", "visible issue");
        }

        private void Overlay_FormClosed(object sender, FormClosedEventArgs e)
        {
         //   File.WriteAllText("close.txt", "close issue");

        }
    }
}
