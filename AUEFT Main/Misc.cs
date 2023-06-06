using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EFT;
using System.Reflection;
using BSG.CameraEffects;
using AuftEftMain.Helpers;
using EFT.Interactive;
using System.IO;
using EFT.InventoryLogic;

namespace AuftEftMain
{
    class Misc : MonoBehaviour
    {

        
        private static int int_3;
        private static int int_1 = LayerMask.GetMask(new string[]
        {
                "Interactive",
                "Deadbody",
                "Player",
                "Loot"
        });

        private static DumbHook InteractHook;

        private static LayerMask intmask = 1 << 12 | 1 << 18 |  1<< 13 |  1 << 9 | 1 << 22;
        public static bool Raycast1(Ray ray, out RaycastHit hitInfo, float maxDistance, int layerMask)
        {
            bool result = Physics.Raycast(ray, out hitInfo, maxDistance,layerMask);
            return result;
        }

        [ObfuscationAttribute(Exclude = true)]
        public static GameObject FindInteractable(Ray ray, out RaycastHit hit)
        {
            // loot through walls
            // changing the loot raycast to 0
            GameObject gameObject = Raycast1(ray, out hit, Mathf.Max(EFTHardSettings.Instance.LOOT_RAYCAST_DISTANCE, EFTHardSettings.Instance.PLAYER_RAYCAST_DISTANCE + EFTHardSettings.Instance.BEHIND_CAST), int_1) ? hit.collider.gameObject : null;
            if (gameObject && !Physics.Linecast(ray.origin, hit.point, int_3))
            {
                return gameObject;
            }
            return null;
        }

       
        [ObfuscationAttribute(Exclude = true)]
        void Start()
        {
            InteractHook = new DumbHook();
            InteractHook.Init(typeof(GameWorld).GetMethod("FindInteractable"), typeof(Misc).GetMethod("FindInteractable"));
            InteractHook.Hook();

           Init().Start();

         
          
          
            
        }
        public float lasttime2;
        public float lasttime;
        public bool Cache = true;
        public float CacheAmount;

    

        [ObfuscationAttribute(Exclude = true)]
        public void Update()
        {
            if (Globals.GameWorld != null) 
            {
               
               if(Globals.LocalPlayer?.HandsController?.Item is Weapon)
                    Globals.LocalPlayerWeapon = (Weapon)Globals.LocalPlayer?.HandsController?.Item;
              

                try
                {
                    if (Globals.Config.Misc.LootThroughWalls)
                    {
                        EFTHardSettings.Instance.LOOT_RAYCAST_DISTANCE = 4; // extend loot range
                        int_3 = LayerMask.GetMask(new string[]
                        {

                         });
                    }
                    else
                    {
                        int_3 = LayerMask.GetMask(new string[]
                          {
                              "HighPolyCollider",
                "TransparentCollider"
                           });
                    }
                }
                catch
                { }
                //   Globals.LocalPlayer.GetComponent<EFT.UI.ItemViewAnimation>().StopLoading();
                // Globals.LocalPlayer.MovementContext.CurrentState.ChangeSpeed(1000);
                try
                {
                    
                    if (Input.GetKeyDown(Globals.Config.Misc.SlideKey))
                    {
                        Globals.Config.Misc.SlideJump = !Globals.Config.Misc.SlideJump; 
                    }
                    if (Globals.Config.Misc.SlideJump)
                    {
                        if (Globals.LocalPlayer.CurrentStateName == EPlayerState.Jump)
                        {
                            if (Time.time > lasttime2)
                            {
                                EFTHardSettings.Instance.JumpTimeDescendingForStateExit = 0; // instantly go into a slide
                                lasttime2 = Time.time + (Globals.Config.Misc.SlideJumpTime * Time.timeScale); // allows you to slide after jumping for set time, adjust it for the timescale to prevent speedhack messing it up
                            }
                            else
                            {
                                EFTHardSettings.Instance.JumpTimeDescendingForStateExit = 100000000; // never slide
                            }

                        }
                        else
                        {

                        }


                    }
                    else
                    {
                        EFTHardSettings.Instance.JumpTimeDescendingForStateExit = 0;// user will slide but only for the game's set amount
                    }
                }
                catch
                {

                }
                try
                {

                    if (Input.GetKeyDown(Globals.Config.Misc.JumpKey)) 
                    {
                        Globals.Config.Misc.FarJump = !Globals.Config.Misc.FarJump; // toggle far jump on and off on key
                    }
                    if (Globals.Config.Misc.FarJump)
                    {
                        if (Globals.LocalPlayer.Skills != null)
                        {
                            if (Cache)
                            {
                                CacheAmount = Globals.LocalPlayer.Skills.StrengthBuffJumpHeightInc.Value; // cache jump height
                                Cache = false;
                            }
                        }
                        Globals.LocalPlayer.Skills.StrengthBuffJumpHeightInc.Value = Globals.Config.Misc.FarJumpAmount; // set our height to the user set jump height
                    }
                    else
                    {
                        if (Cache == false)
                            Globals.LocalPlayer.Skills.StrengthBuffJumpHeightInc.Value = CacheAmount; // set back to cache
                    }
                }
                catch
                { }
                try
                {
                    if (Input.GetKeyDown(Globals.Config.Misc.HopKey))
                    {
                        Globals.Config.Misc.BunnyHop = !Globals.Config.Misc.BunnyHop; // toggle bunnyhop
                    }
                    if (Globals.Config.Misc.BunnyHop)
                    {
                        foreach (Player.ESpeedLimit speedlimit in Enum.GetValues(typeof(EFT.Player.ESpeedLimit)))
                        {
                            if (speedlimit != Player.ESpeedLimit.SurfaceNormal)
                            {
                                Globals.LocalPlayer.RemoveStateSpeedLimit(speedlimit); // remove the speedlimit on jjumping
                            }
                        }
                        if (Time.time > lasttime)
                        {
                            if (Globals.Config.Misc.WaitForInput)
                            {
                                // check for movement annd then jump so we jump in that direction
                                if (Input.GetKey(KeyCode.W))
                                    Globals.LocalPlayer.MovementContext.TryJump();
                                if (Input.GetKey(KeyCode.S))
                                    Globals.LocalPlayer.MovementContext.TryJump();
                                if (Input.GetKey(KeyCode.A))
                                    Globals.LocalPlayer.MovementContext.TryJump();
                                if (Input.GetKey(KeyCode.D))
                                    Globals.LocalPlayer.MovementContext.TryJump();
                            }
                            else
                            {
                                // just jump constantly
                                Globals.LocalPlayer.MovementContext.TryJump();
                            }
                            lasttime = Time.time + Globals.Config.Misc.TimeBetweenHop; // hop delay
                        }
                    }
                }
                catch
                {
                }
                try
                {
                    if (Globals.Config.Misc.NoSway)
                    {
                        // set no sway properties 
                        Globals.LocalPlayer.ProceduralWeaponAnimation.Breath.Intensity = 0f;
                        Globals.LocalPlayer.ProceduralWeaponAnimation.WalkEffectorEnabled = false;
                        Globals.LocalPlayer.ProceduralWeaponAnimation.Walk.Intensity = 0f;
                        Globals.LocalPlayer.ProceduralWeaponAnimation.MotionReact.Intensity = 0f;
                        Globals.LocalPlayer.ProceduralWeaponAnimation.MotionReact.Velocity = Vector3.zero;

                        Globals.LocalPlayer.ProceduralWeaponAnimation.ForceReact.Intensity = 0f;

                        Globals.LocalPlayer.ProceduralWeaponAnimation.AimingDisplacementStr = 0;
                        Globals.LocalPlayer.ProceduralWeaponAnimation.Breath.HipPenalty = 0f;

                    }
                    else
                    {

                    }
                }
                catch
                { }
                try
                {
                    if (Globals.Config.Misc.NoRecoil && Globals.LocalPlayerWeapon != null)
                    {
                        // set no recoil properties
                        Globals.LocalPlayerWeapon.CurrentAmmoTemplate.MalfFeedChance = 0;
                        Globals.LocalPlayerWeapon.CurrentAmmoTemplate.MalfMisfireChance = 0;
                        Globals.LocalPlayerWeapon.Template.AllowJam = false;
                        Globals.LocalPlayerWeapon.Template.AllowFeed = true;
                        Globals.LocalPlayerWeapon.Template.BaseMalfunctionChance = 0;
                        Globals.LocalPlayer.ProceduralWeaponAnimation.Shootingg.Intensity = 0f;
                        Globals.LocalPlayer.ProceduralWeaponAnimation.ForceReact.Intensity = 0f;
                        Globals.LocalPlayer.ProceduralWeaponAnimation.Breath.Overweight = 0f;
                        Globals.LocalPlayer.ProceduralWeaponAnimation.Pitch = 0f;
                        Globals.LocalPlayer.ProceduralWeaponAnimation.SwayFalloff = 0f;
                    }
                    else
                    { }

                }
                catch
                {
                }
                try
                {

                    if (Globals.Config.Misc.FullAuto)
                    {
                        // force the gun to not be bolt action
                        Globals.LocalPlayer.GetComponent<Player.FirearmController>().Item.Template.BoltAction = false;
                    }
                }
                catch
                { }
                try
                {

                    if (Globals.Config.Misc.UnbreakableBallistics && Globals.LocalPlayerWeapon != null)
                    {
                       // allows higher penetration
                        Globals.LocalPlayerWeapon.Template.DefAmmoTemplate.RicochetChance = 0.00000000001f;
                        Globals.LocalPlayerWeapon.Template.DefAmmoTemplate.FragmentationChance = 0.00000000001f;
                    }
                }
                catch
                { }
                try
                {
                    if (Globals.Config.Misc.NoCollide)
                    {
                        // gun clips through walls
                        Globals.LocalPlayer.ProceduralWeaponAnimation.Mask = EFT.Animations.EProceduralAnimationMask.ForceReaction;
                        Globals.LocalPlayer.ProceduralWeaponAnimation._shouldMoveWeaponCloser = true;
                    }
                }
                catch
                {
                }
                try
                {
                    if (Input.GetKeyDown(Globals.Config.Misc.Key))
                    {
                        Globals.Config.Misc.Speedhack = !Globals.Config.Misc.Speedhack; // toggle speedhack on and off
                    }

                    if (Globals.Config.Misc.Speedhack)
                    {
                        Time.timeScale = Globals.Config.Misc.Speed; // set time scale to speed
                    }
                    else
                    {
                        if (Globals.LocalPlayer.CurrentStateName != EPlayerState.Jump)
                        {
                            Time.timeScale = 1; // reset 
                        }
                    }

                    if (Globals.Config.Misc.UnlimitedStamina)
                    {
                        // set stamina  and hand stamina
                            Globals.LocalPlayer.Physical.HandsStamina.Current = 100;
                            Globals.LocalPlayer.Physical.Stamina.Current = 100;

                        
                    }
                }
                catch
                { }
                try
                {
                    if (Globals.Config.Misc.NoVisor)
                    {
                        // remove visor effects
                        Globals.MainCamera.GetComponent<VisorEffect>().Intensity = 0f;
                        Globals.MainCamera.GetComponent<VisorEffect>().enabled = true;
                    }
                    if (Globals.Config.Misc.NightVision)
                    {
                        Globals.MainCamera.GetComponent<NightVision>().SetPrivateField("_on", true); // find night vision bool called "_on" and set it
                    }
                    else
                    {
                        Globals.MainCamera.GetComponent<NightVision>().SetPrivateField("_on", false);
                    }
                    if (Globals.Config.Misc.ThermalVision)
                    {
                        Globals.MainCamera.GetComponent<ThermalVision>().On = true; // set thermal vision
                    }
                    else
                    {
                        Globals.MainCamera.GetComponent<ThermalVision>().On = false;

                    }
                    if (Globals.GameWorld != null)
                    {
                        FullBright_UpdateObject(Globals.Config.Misc.NightMode);
                        FullBright_SpawnObject();
                        if (Globals.Config.Misc.NightMode && !Globals.Config.Misc.CustomTime)
                        {
                            if (Time.time > temptime) // only do this once every 15 seconds.
                            {
                                TOD_Sky.Instance.Components.Time.GameDateTime = null;
                                TOD_Sky Sky_Obj = (TOD_Sky)UnityEngine.Object.FindObjectOfType(typeof(TOD_Sky)); // heavy on performance so we do it unoften
                                Sky_Obj.Cycle.Hour = 22f; // set night time

                                temptime = Time.time + 15;
                            }
                        }
                        else
                        {

                        }
                        if (Globals.Config.Misc.CustomTime)
                        {
                            if (Time.time > temptime) // wait 15 seconds
                            {
                                TOD_Sky.Instance.Components.Time.GameDateTime = null;
                                TOD_Sky Sky_Obj = (TOD_Sky)UnityEngine.Object.FindObjectOfType(typeof(TOD_Sky));
                                Sky_Obj.Cycle.Hour = Globals.Config.Misc.Time; // set time to the time the user wants

                                temptime = Time.time + 15;
                            }

                        }
                    }

                }
                catch
                {

                }
            }
        
        }
        // this is stolen from maoci. Thats why its a different naming convention. 
        private float temptime;
        public static bool Enabled = false;

        public static GameObject lightGameObject;

        public static Light FullBrightLight;

        public static bool _LightEnabled = true;

        public static bool lightCalled;

        private static Vector3 tempPosition = Vector3.zero;

        public static void FullBright_SpawnObject()
        {
            if (Globals.LocalPlayer != null && !lightCalled && Enabled)
            {
                lightGameObject = new GameObject("Fullbright"); // create full bright object
                FullBrightLight = lightGameObject.AddComponent<Light>(); // add a light to it
                FullBrightLight.color = new Color(1f, 0.839f, 0.66f, 1f); // set colour to yellowy white
                FullBrightLight.range = 2000f; //
                FullBrightLight.intensity = 0.6f;
                lightCalled = true; // set light to called to prevent spawning loads of them
            }
        }

        public static void FullBright_UpdateObject(bool set)
        {
          
                Enabled = set;
                if (set)
                {
                    if (FullBrightLight == null)
                    {
                        return;
                    }
                    tempPosition = Globals.LocalPlayer.Transform.position; // update the position
                    tempPosition.y = tempPosition.y + 0.2f;
                    lightGameObject.transform.position = tempPosition;
                    return;
                }
                else
                {
                    if (FullBrightLight != null)
                    {
                        UnityEngine.Object.Destroy(FullBrightLight); // destroy the light because it was turned off
                    }
                    lightCalled = false;
                }
            
        }
    }
}
