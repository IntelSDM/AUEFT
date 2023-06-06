using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EFT;
using System.Reflection;
using AuftEftMain.Helpers;
using EFT.Ballistics;
using EFT.InventoryLogic;
using static EFT.Player;

namespace AuftEftMain
{
    class Aimbot : MonoBehaviour
    {
        private static DumbHook CreateShot_Silent;
        private static DumbHook Ricochet;
        public static Player Targetplayer;

        private static Vector3 HitPos = new Vector3();
        public static Vector3 ScreenHitPos = new Vector3();

        private static float bulletdistance;
        private static float bullettraveltime;
        public static Vector3 WorldPointToScreenPoint(Vector3 worldPoint)
        {
            Vector3 vector = Globals.MainCamera.WorldToScreenPoint(worldPoint);
            vector.y = (float)Screen.height - vector.y;
            return vector;
        }
        private static float _nextShot;
        private static float _nextLoop;
        [ObfuscationAttribute(Exclude = true)]

        public virtual bool Deflects(float _hitCosDirectionToNormal, object shot1, Vector3 hitPoint, Vector3 shotNormal, Vector3 shotDirection)
        {
            // old attempt at ricochet silent aim before it was a thing that any cheats had. 
            for (int idx = 0; idx < Globals.GameWorld._sharedBallisticsCalculator.Shots.Count; idx++)
            {
                var shot = Globals.GameWorld._sharedBallisticsCalculator.Shots[idx];
                if (shot.IsShotFinished)
                    continue;

                shot.SetPrivateField("HitCosDirectionToNormal", 0f);
                shot.IsForwardHit = false;
                // shot.SetPrivateField("CurrentDirection", new Vector3(0, 0, 0));
                shot.DeviationChance = 0;
                shot.Direction = Aimbot.Targetplayer.PlayerBones.Head.position;
                
            }
         /*   for (int idx = 0; idx < Colliders.Length; idx++)
            {
                var collider = Colliders[idx];
                //  collider.PenetrationChance = 1.0f;
                // collider.PenetrationLevel = 0.0f;
                // collider.RicochetChance = 0.0f;
                collider.FragmentationChance = 0.0f;
                collider.TrajectoryDeviationChance = 0.0f;
                collider.TrajectoryDeviation = 0.0f;
            }*/
            return true;
        }
        [ObfuscationAttribute(Exclude = true)]
        void Update()
        {
            Targetplayer = GetTargetPlayer(); // set our target player

            try
            {
                if (Globals.Config.Aimbot.PlayerAutoShoot) // auto shoot is active
                {
                    Player player = GetTargetPlayer();
                    if (!(GetTargetPlayer().Profile.Info.RegistrationDate <= 0) && Globals.Config.Aimbot.PlayerAimbot)
                    {
                        // check for visibility, shoot if anything is visible and aimbot will route the shot
                        #region PlayerRageAimbot
                        if (Globals.Config.Aimbot.PlayerRageAimbot)
                        {

                            if (RaycastHelper.Head(GetTargetPlayer()))
                            {
                                if (RaycastHelper.RightThigh1(GetTargetPlayer()))
                                {
                                    if (_nextShot < Time.time)
                                    {

                                        Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(true);
                                        _nextShot = Time.time + 0.064f;
                                        Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(false);
                                    }
                                }
                                if (RaycastHelper.LeftThigh1(GetTargetPlayer()))
                                {
                                    if (_nextShot < Time.time)
                                    {

                                        Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(true);
                                        _nextShot = Time.time + 0.064f;
                                        Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(false);
                                    }
                                }
                                if (RaycastHelper.RightThigh2(GetTargetPlayer()))
                                {
                                    if (_nextShot < Time.time)
                                    {

                                        Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(true);
                                        _nextShot = Time.time + 0.064f;
                                        Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(false);
                                    }
                                }
                                if (RaycastHelper.LeftThigh2(GetTargetPlayer()))
                                {
                                    if (_nextShot < Time.time)
                                    {

                                        Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(true);
                                        _nextShot = Time.time + 0.064f;
                                        Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(false);
                                    }
                                }
                                if (RaycastHelper.isPointVisible(GetTargetPlayer(), GetTargetPlayer().PlayerBones.RightShoulder.position))
                                {
                                    if (_nextShot < Time.time)
                                    {

                                        Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(true);
                                        _nextShot = Time.time + 0.064f;
                                        Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(false);
                                    }
                                }
                                if (RaycastHelper.isPointVisible(GetTargetPlayer(), GetTargetPlayer().PlayerBones.LeftShoulder.position))
                                {
                                    if (_nextShot < Time.time)
                                    {

                                        Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(true);
                                        _nextShot = Time.time + 0.064f;
                                        Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(false);
                                    }
                                }
                                if (RaycastHelper.Spine3(GetTargetPlayer()))
                                {
                                    if (_nextShot < Time.time)
                                    {

                                        Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(true);
                                        _nextShot = Time.time + 0.064f;
                                        Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(false);
                                    }
                                }
                                if (RaycastHelper.Spine1(GetTargetPlayer()))
                                {
                                    if (_nextShot < Time.time)
                                    {

                                        Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(true);
                                        _nextShot = Time.time + 0.064f;
                                        Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(false);
                                    }

                                }
                                if (RaycastHelper.Head(GetTargetPlayer()))
                                {
                                    if (_nextShot < Time.time)
                                    {

                                        Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(true);
                                        _nextShot = Time.time + 0.064f;
                                        Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(false);
                                    }

                                }
                            }
                        }



                        #endregion
                    }
                }
                if (Globals.Config.Aimbot.ScavAutoShoot)
                {
                    Player player = GetTargetPlayer();
                    if ((GetTargetPlayer().Profile.Info.RegistrationDate <= 0) && Globals.Config.Aimbot.ScavAimbot)
                    {
                        // check for visibility, shoot if anything is visible and aimbot will route the shot
                        #region ScavRageAimbot
                        if (Globals.Config.Aimbot.ScavRageAimbot)
                        {

                            // we do this from decending order, least important to most important limb so then if a more important one is visible it will go through the code and get to the most important one that is visible.
                            if (RaycastHelper.RightThigh1(GetTargetPlayer()))
                            {
                                if (_nextShot < Time.time)
                                {

                                    Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(true);
                                    _nextShot = Time.time + 0.064f;
                                    Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(false);
                                }
                                // direction = (GetTargetPlayer().PlayerBones.RightThigh1.position - origin).normalized;
                            }
                            if (RaycastHelper.LeftThigh1(GetTargetPlayer()))
                            {
                                if (_nextShot < Time.time)
                                {

                                    Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(true);
                                    _nextShot = Time.time + 0.064f;
                                    Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(false);
                                }
                            }
                            if (RaycastHelper.RightThigh2(GetTargetPlayer()))
                            {
                                if (_nextShot < Time.time)
                                {

                                    Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(true);
                                    _nextShot = Time.time + 0.064f;
                                    Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(false);
                                }
                            }
                            if (RaycastHelper.LeftThigh2(GetTargetPlayer()))
                            {
                                if (_nextShot < Time.time)
                                {

                                    Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(true);
                                    _nextShot = Time.time + 0.064f;
                                    Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(false);
                                }
                            }
                            if (RaycastHelper.isPointVisible(GetTargetPlayer(), GetTargetPlayer().PlayerBones.RightShoulder.position))
                            {
                                if (_nextShot < Time.time)
                                {

                                    Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(true);
                                    _nextShot = Time.time + 0.064f;
                                    Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(false);
                                }

                            }
                            if (RaycastHelper.isPointVisible(GetTargetPlayer(), GetTargetPlayer().PlayerBones.LeftShoulder.position))
                            {
                                if (_nextShot < Time.time)
                                {

                                    Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(true);
                                    _nextShot = Time.time + 0.064f;
                                    Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(false);
                                }
                            }
                            if (RaycastHelper.Spine3(GetTargetPlayer()))
                            {
                                if (_nextShot < Time.time)
                                {

                                    Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(true);
                                    _nextShot = Time.time + 0.064f;
                                    Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(false);
                                }
                            }
                            if (RaycastHelper.Spine1(GetTargetPlayer()))
                            {
                                if (_nextShot < Time.time)
                                {

                                    Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(true);
                                    _nextShot = Time.time + 0.064f;
                                    Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(false);
                                }
                            }
                            if (RaycastHelper.Head(GetTargetPlayer()))
                            {

                                if (_nextShot < Time.time)
                                {

                                    Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(true);
                                    _nextShot = Time.time + 0.064f;
                                    Globals.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(false);
                                }
                            }

                        }
                        #endregion
                    }

                }
            }
            catch
            {

            }
        }
        private static float BulletDrop(Vector3 startVector, Vector3 endVector, float BulletSpeed)
        {
            // this is before the projectile system was changed to the current ballistic coeffieint system.
            bulletdistance = Vector3.Distance(startVector, endVector);
            if (bulletdistance >= 50f)
            {
                bullettraveltime = bullettraveltime / BulletSpeed;
                return (float)(4.905 * (double)bullettraveltime * (double)bullettraveltime);
            }
            return 0f;
        }
        [ObfuscationAttribute(Exclude = true)]
        void Start()
        {
      // hook create shot
            CreateShot_Silent = new DumbHook();
            CreateShot_Silent.Init(typeof(BallisticsCalculator).GetMethod("CreateShot"), typeof(Aimbot).GetMethod("CreateShot_SilentHook"));
            CreateShot_Silent.Hook();
        }
        public void AimbotStuff(ref Vector3 direction, ref Vector3 origin)
        {

            if (GetTargetPlayer() != null)
            {
                Weapon wpn = Globals.LocalPlayerWeapon;
                System.Random hitchance = new System.Random();
                //  speedFactor = 1000;
                // not going to comment all of this but rage aimbot just scans for anything visible
                // legit aimbot uses hitchances for each bone set and randomises if there are many bones to pick the correct set of bones
                if (!(GetTargetPlayer().Profile.Info.RegistrationDate <= 0) && Globals.Config.Aimbot.PlayerAimbot)
                {
                
                    #region PlayerRageAimbot
                    if (Globals.Config.Aimbot.PlayerRageAimbot)
                    {
                        if (hitchance.Next(1, 100) <= Globals.Config.Aimbot.PlayerHitChance)
                        {
                            if (RaycastHelper.RightThigh1(GetTargetPlayer()))
                            {
                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                if (Globals.Config.Aimbot.Prediction)
                                {
                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, GetTargetPlayer().Transform.position);
                                    float num2 = (float)num1;
                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                    hitvector.x += GetTargetPlayer().Velocity.x * num3;
                                    hitvector.y += GetTargetPlayer().Velocity.y * num3;
                                    direction = (hitvector - origin).normalized;

                                }
                                else
                                    direction = (hitvector - origin).normalized;
                                // direction = (GetTargetPlayer().PlayerBones.RightThigh1.position - origin).normalized;
                            }
                            if (RaycastHelper.LeftThigh1(GetTargetPlayer()))
                            {
                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh1.position;
                                if (Globals.Config.Aimbot.Prediction)
                                {
                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, GetTargetPlayer().Transform.position);
                                    float num2 = (float)num1;
                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                    hitvector.x += GetTargetPlayer().Velocity.x * num3;
                                    hitvector.y += GetTargetPlayer().Velocity.y * num3;
                                    direction = (hitvector - origin).normalized;
                                }
                                else
                                    direction = (hitvector - origin).normalized;
                            }
                            if (RaycastHelper.RightThigh2(GetTargetPlayer()))
                            {
                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                if (Globals.Config.Aimbot.Prediction)
                                {
                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, GetTargetPlayer().Transform.position);
                                    float num2 = (float)num1;
                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                    hitvector.x += GetTargetPlayer().Velocity.x * num3;
                                    hitvector.y += GetTargetPlayer().Velocity.y * num3;
                                    direction = (hitvector - origin).normalized;
                                }
                                else
                                    direction = (hitvector - origin).normalized;
                            }
                            if (RaycastHelper.LeftThigh2(GetTargetPlayer()))
                            {
                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                if (Globals.Config.Aimbot.Prediction)
                                {
                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, GetTargetPlayer().Transform.position);
                                    float num2 = (float)num1;
                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                    hitvector.x += GetTargetPlayer().Velocity.x * num3;
                                    hitvector.y += GetTargetPlayer().Velocity.y * num3;
                                    direction = (hitvector - origin).normalized;
                                }
                                else
                                    direction = (hitvector - origin).normalized;
                            }
                            if (RaycastHelper.isPointVisible(GetTargetPlayer(), GetTargetPlayer().PlayerBones.RightShoulder.position))
                            {
                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightShoulder.position;
                                if (Globals.Config.Aimbot.Prediction)
                                {
                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, GetTargetPlayer().Transform.position);
                                    float num2 = (float)num1;
                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                    hitvector.x += GetTargetPlayer().Velocity.x * num3;
                                    hitvector.y += GetTargetPlayer().Velocity.y * num3;
                                    direction = (hitvector - origin).normalized;
                                }
                                else
                                    direction = (hitvector - origin).normalized;
                            }
                            if (RaycastHelper.isPointVisible(GetTargetPlayer(), GetTargetPlayer().PlayerBones.LeftShoulder.position))
                            {
                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftShoulder.position;
                                if (Globals.Config.Aimbot.Prediction)
                                {
                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, GetTargetPlayer().Transform.position);
                                    float num2 = (float)num1;
                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                    hitvector.x += GetTargetPlayer().Velocity.x * num3;
                                    hitvector.y += GetTargetPlayer().Velocity.y * num3;
                                    direction = (hitvector - origin).normalized;
                                }
                                else
                                    direction = (hitvector - origin).normalized;
                            }
                            if (RaycastHelper.Spine3(GetTargetPlayer()))
                            {
                                Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine3.position;
                                if (Globals.Config.Aimbot.Prediction)
                                {
                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, GetTargetPlayer().Transform.position);
                                    float num2 = (float)num1;
                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                    hitvector.x += GetTargetPlayer().Velocity.x * num3;
                                    hitvector.y += GetTargetPlayer().Velocity.y * num3;
                                    direction = (hitvector - origin).normalized;
                                }
                                else
                                    direction = (hitvector - origin).normalized;
                            }
                            if (RaycastHelper.Spine1(GetTargetPlayer()))
                            {
                                Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine1.position;
                                if (Globals.Config.Aimbot.Prediction)
                                {
                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, GetTargetPlayer().Transform.position);
                                    float num2 = (float)num1;
                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                    hitvector.x += GetTargetPlayer().Velocity.x * num3;
                                    hitvector.y += GetTargetPlayer().Velocity.y * num3;
                                    direction = (hitvector - origin).normalized;
                                }
                                else
                                    direction = (hitvector - origin).normalized;
                            }
                            if (RaycastHelper.Head(GetTargetPlayer()))
                            {
                                Vector3 hitvector = GetTargetPlayer().PlayerBones.Head.position + new Vector3(0, 0.07246377f, 0);
                                if (Globals.Config.Aimbot.Prediction)
                                {
                                 
                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, GetTargetPlayer().Transform.position);
                                    float num2 = (float)num1;
                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                    hitvector.x += GetTargetPlayer().Velocity.x * num3;
                                    hitvector.y += GetTargetPlayer().Velocity.y * num3;
                                    direction = (hitvector - origin).normalized;
                                }
                                else
                                    direction = (hitvector - origin).normalized;

                            }
                        }
                    }
                    #endregion
                    #region PlayerLegitAimbot
                    else
                    {
                        Player player = GetTargetPlayer();
                        if (hitchance.Next(1, 100) <= Globals.Config.Aimbot.PlayerHitChance)
                        {
                            if (Globals.Config.Aimbot.PlayerHeadChance + Globals.Config.Aimbot.PlayerLegChance + Globals.Config.Aimbot.PlayerBodyChance == 100)
                            {
                                int temphead = (int)Globals.Config.Aimbot.PlayerHeadChance;
                                int tempbody = (int)Globals.Config.Aimbot.PlayerBodyChance;
                                int templeg = (int)Globals.Config.Aimbot.PlayerLegChance;
                                System.Random random = new System.Random();
                                int chance = random.Next(1, 100);
                            
                                // body
                                if (Globals.Config.Aimbot.PlayerBodyChance > 1)
                                {
                                    if (!RaycastHelper.Head(player) && ((RaycastHelper.Spine1(player) || RaycastHelper.Spine3(player)) && !(RaycastHelper.LeftThigh1(player) || RaycastHelper.RightThigh1(player) || RaycastHelper.LeftThigh2(player) || RaycastHelper.RightThigh2(player))))
                                    {
                                        if (RaycastHelper.isPointVisible(player, player.PlayerBones.LeftShoulder.position) && RaycastHelper.isPointVisible(player, player.PlayerBones.RightShoulder.position))
                                        {
                                            System.Random random2 = new System.Random();
                                            int coinflip = random2.Next(1, 100);
                                            if (coinflip <= 50)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightShoulder.position;

                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                               //     Weapon wpn = (Weapon)Globals.LocalPlayer.HandsController.Item;
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (coinflip > 50)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftShoulder.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                   //   Weapon wpn = (Weapon)Globals.LocalPlayer.HandsController.Item;
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                        }
                                        if (RaycastHelper.isPointVisible(player, player.PlayerBones.LeftShoulder.position) && !RaycastHelper.isPointVisible(player, player.PlayerBones.RightShoulder.position))
                                        {
                                            Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftShoulder.position;
                                            if (Globals.Config.Aimbot.Prediction)
                                            {
                                                int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                float num2 = (float)num1;
                                                float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                hitvector.x += player.Velocity.x * num3;
                                                hitvector.y += player.Velocity.y * num3;
                                                direction = (hitvector - origin).normalized;

                                            }
                                            direction = (hitvector - origin).normalized;
                                        }
                                        if (RaycastHelper.isPointVisible(player, player.PlayerBones.RightShoulder.position) && !RaycastHelper.isPointVisible(player, player.PlayerBones.LeftShoulder.position))
                                        {
                                            Vector3 hitvector = GetTargetPlayer().PlayerBones.RightShoulder.position;
                                            if (Globals.Config.Aimbot.Prediction)
                                            {
                                                int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                float num2 = (float)num1;
                                                float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                hitvector.x += player.Velocity.x * num3;
                                                hitvector.y += player.Velocity.y * num3;
                                                direction = (hitvector - origin).normalized;

                                            }
                                            direction = (hitvector - origin).normalized;
                                        }
                                        if (RaycastHelper.Spine1(player) && RaycastHelper.Spine3(player))
                                        {
                                            System.Random random2 = new System.Random();
                                            int coinflip = random2.Next(1, 100);
                                            if (coinflip <= 50)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine1.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                HitPos = hitvector;
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (coinflip > 50)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine3.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                HitPos = hitvector;
                                                direction = (hitvector - origin).normalized;
                                            }

                                        }
                                        // just spine3
                                        if (RaycastHelper.Spine3(player) && !RaycastHelper.Spine1(player))
                                        {
                                            Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine3.position;
                                            if (Globals.Config.Aimbot.Prediction)
                                            {
                                                int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                float num2 = (float)num1;
                                                float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                hitvector.x += player.Velocity.x * num3;
                                                hitvector.y += player.Velocity.y * num3;
                                                direction = (hitvector - origin).normalized;

                                            }
                                            direction = (hitvector - origin).normalized;
                                        }
                                        // just spine1
                                        if (RaycastHelper.Spine1(player) && !RaycastHelper.Spine3(player))
                                        {

                                            Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine1.position;
                                            if (Globals.Config.Aimbot.Prediction)
                                            {
                                                int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                float num2 = (float)num1;
                                                float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                hitvector.x += player.Velocity.x * num3;
                                                hitvector.y += player.Velocity.y * num3;
                                                direction = (hitvector - origin).normalized;

                                            }
                                            direction = (hitvector - origin).normalized;
                                        }
                                    }
                                }
                                if (Globals.Config.Aimbot.PlayerLegChance > 1)
                                {
                                    if (!RaycastHelper.Head(player) && !(RaycastHelper.Spine1(player) || RaycastHelper.Spine3(player) && (RaycastHelper.LeftThigh1(player) || RaycastHelper.RightThigh1(player) || RaycastHelper.LeftThigh2(player) || RaycastHelper.RightThigh2(player))))
                                    {
                                        if (RaycastHelper.RightThigh1(player) && RaycastHelper.RightThigh2(player) && RaycastHelper.LeftThigh1(player) && RaycastHelper.RightThigh2(player))
                                        {
                                            System.Random random2 = new System.Random();
                                            int coinflip = random2.Next(1, 100);
                                            if (coinflip < 25)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (coinflip >= 25 && coinflip < 50)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh1.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (coinflip >= 50 && coinflip < 75)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (coinflip >= 75)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }

                                        }
                                        if (RaycastHelper.RightThigh1(player) && RaycastHelper.RightThigh2(player))
                                        {
                                            System.Random random2 = new System.Random();
                                            int coinflip = random2.Next(1, 100);
                                            if (coinflip < 50)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (coinflip >= 50)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }


                                        }
                                        if (RaycastHelper.LeftThigh2(player) && RaycastHelper.RightThigh2(player))
                                        {
                                            System.Random random2 = new System.Random();
                                            int coinflip = random2.Next(1, 100);
                                            if (coinflip < 50)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (coinflip >= 50)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }


                                        }
                                        if (RaycastHelper.LeftThigh1(player) && RaycastHelper.LeftThigh2(player))
                                        {
                                            System.Random random2 = new System.Random();
                                            int coinflip = random2.Next(1, 100);
                                            if (coinflip < 50)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (coinflip >= 50)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }


                                        }
                                        if (RaycastHelper.RightThigh1(player) && RaycastHelper.LeftThigh1(player))
                                        {
                                            System.Random random2 = new System.Random();
                                            int coinflip = random2.Next(1, 100);
                                            if (coinflip < 50)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (coinflip >= 50)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh1.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }


                                        }
                                        if (RaycastHelper.LeftThigh1(player) && !RaycastHelper.LeftThigh2(player) & !RaycastHelper.RightThigh1(player) && !RaycastHelper.RightThigh2(player))
                                        {
                                            Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh1.position;
                                            if (Globals.Config.Aimbot.Prediction)
                                            {
                                                int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                float num2 = (float)num1;
                                                float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                hitvector.x += player.Velocity.x * num3;
                                                hitvector.y += player.Velocity.y * num3;
                                                direction = (hitvector - origin).normalized;

                                            }
                                            direction = (hitvector - origin).normalized;
                                        }
                                        if (!RaycastHelper.LeftThigh1(player) && RaycastHelper.LeftThigh2(player) & !RaycastHelper.RightThigh1(player) && !RaycastHelper.RightThigh2(player))
                                        {
                                            Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                            if (Globals.Config.Aimbot.Prediction)
                                            {
                                                int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                float num2 = (float)num1;
                                                float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                hitvector.x += player.Velocity.x * num3;
                                                hitvector.y += player.Velocity.y * num3;
                                                direction = (hitvector - origin).normalized;

                                            }
                                            direction = (hitvector - origin).normalized;
                                        }
                                        if (!RaycastHelper.LeftThigh1(player) && !RaycastHelper.LeftThigh2(player) & RaycastHelper.RightThigh1(player) && !RaycastHelper.RightThigh2(player))
                                        {
                                            Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                            if (Globals.Config.Aimbot.Prediction)
                                            {
                                                int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                float num2 = (float)num1;
                                                float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                hitvector.x += player.Velocity.x * num3;
                                                hitvector.y += player.Velocity.y * num3;
                                                direction = (hitvector - origin).normalized;

                                            }
                                            direction = (hitvector - origin).normalized;
                                        }
                                        if (!RaycastHelper.LeftThigh1(player) && !RaycastHelper.LeftThigh2(player) & !RaycastHelper.RightThigh1(player) && RaycastHelper.RightThigh2(player))
                                        {
                                            Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                            if (Globals.Config.Aimbot.Prediction)
                                            {
                                                int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                float num2 = (float)num1;
                                                float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                hitvector.x += player.Velocity.x * num3;
                                                hitvector.y += player.Velocity.y * num3;
                                                direction = (hitvector - origin).normalized;

                                            }
                                            direction = (hitvector - origin).normalized;
                                        }
                                    }
                                }
                                // if head is only visible
                                if (Globals.Config.Aimbot.PlayerHeadChance > 1)
                                {
                                    if (RaycastHelper.Head(player) && !((RaycastHelper.Spine1(player) || RaycastHelper.Spine3(player)) && !(RaycastHelper.LeftThigh1(player) || RaycastHelper.RightThigh1(player) || RaycastHelper.LeftThigh2(player) || RaycastHelper.RightThigh2(player))))
                                    {
                                        Vector3 hitvector = GetTargetPlayer().PlayerBones.Head.position + new Vector3(0, 0.07246377f, 0);
                                        if (Globals.Config.Aimbot.Prediction)
                                        {
                                            int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                            float num2 = (float)num1;
                                            float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                            hitvector.x += player.Velocity.x * num3;
                                            hitvector.y += player.Velocity.y * num3;
                                            direction = (hitvector - origin).normalized;

                                        }
                                        direction = (hitvector - origin).normalized;
                                    }
                                }
                                // body legs
                                if (Globals.Config.Aimbot.PlayerBodyChance > 1 && Globals.Config.Aimbot.PlayerLegChance > 1)
                                {
                                    if (!RaycastHelper.Head(player) && ((RaycastHelper.Spine1(player) || RaycastHelper.Spine3(player)) && (RaycastHelper.LeftThigh1(player) || RaycastHelper.RightThigh1(player) || RaycastHelper.LeftThigh2(player) || RaycastHelper.RightThigh2(player))))
                                    {
                                        templeg += temphead / 2;
                                        tempbody += temphead / 2;
                                        int temp = templeg + tempbody;
                                        if (chance < temp - tempbody)
                                        {
                                            //legs
                                            if (RaycastHelper.RightThigh1(player) && RaycastHelper.RightThigh2(player) && RaycastHelper.LeftThigh1(player) && RaycastHelper.RightThigh2(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 25)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 25 && coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh1.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50 && coinflip < 75)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 75)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }

                                            }
                                            if (RaycastHelper.RightThigh1(player) && RaycastHelper.RightThigh2(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }


                                            }
                                            if (RaycastHelper.LeftThigh2(player) && RaycastHelper.RightThigh2(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }


                                            }
                                            if (RaycastHelper.LeftThigh1(player) && RaycastHelper.LeftThigh2(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }


                                            }
                                            if (RaycastHelper.RightThigh1(player) && RaycastHelper.LeftThigh1(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh1.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }


                                            }
                                            if (RaycastHelper.LeftThigh1(player) && !RaycastHelper.LeftThigh2(player) & !RaycastHelper.RightThigh1(player) && !RaycastHelper.RightThigh2(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh1.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (!RaycastHelper.LeftThigh1(player) && RaycastHelper.LeftThigh2(player) & !RaycastHelper.RightThigh1(player) && !RaycastHelper.RightThigh2(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (!RaycastHelper.LeftThigh1(player) && !RaycastHelper.LeftThigh2(player) & RaycastHelper.RightThigh1(player) && !RaycastHelper.RightThigh2(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (!RaycastHelper.LeftThigh1(player) && !RaycastHelper.LeftThigh2(player) & !RaycastHelper.RightThigh1(player) && RaycastHelper.RightThigh2(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                        }
                                        if (chance >= temp - tempbody)
                                        {
                                            // body
                                            if (RaycastHelper.isPointVisible(player, player.PlayerBones.LeftShoulder.position) && RaycastHelper.isPointVisible(player, player.PlayerBones.RightShoulder.position))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip <= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightShoulder.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip > 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftShoulder.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                            }
                                            if (RaycastHelper.isPointVisible(player, player.PlayerBones.LeftShoulder.position) && !RaycastHelper.isPointVisible(player, player.PlayerBones.RightShoulder.position))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftShoulder.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (RaycastHelper.isPointVisible(player, player.PlayerBones.RightShoulder.position) && !RaycastHelper.isPointVisible(player, player.PlayerBones.LeftShoulder.position))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightShoulder.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (RaycastHelper.Spine1(player) && RaycastHelper.Spine3(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip <= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine1.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip > 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine3.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }

                                            }
                                            // just spine3
                                            if (RaycastHelper.Spine3(player) && !RaycastHelper.Spine1(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine3.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                            // just spine1
                                            if (RaycastHelper.Spine1(player) && !RaycastHelper.Spine3(player))
                                            {

                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine1.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                        }
                                    }
                                }
                                // head body
                                if (Globals.Config.Aimbot.PlayerBodyChance > 1 && Globals.Config.Aimbot.PlayerHeadChance > 1)
                                {
                                    if (RaycastHelper.Head(player) && ((RaycastHelper.Spine1(player) || RaycastHelper.Spine3(player)) && !(RaycastHelper.LeftThigh1(player) || RaycastHelper.RightThigh1(player) || RaycastHelper.LeftThigh2(player) || RaycastHelper.RightThigh2(player))))
                                    {
                                        temphead += templeg / 2;
                                        tempbody += templeg / 2;
                                        int temp = temphead + tempbody;
                                        if (chance < temp - tempbody)
                                        {
                                            // head
                                            Vector3 hitvector = GetTargetPlayer().PlayerBones.Head.position + new Vector3(0, 0.07246377f, 0);
                                            if (Globals.Config.Aimbot.Prediction)
                                            {
                                                int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                float num2 = (float)num1;
                                                float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                hitvector.x += player.Velocity.x * num3;
                                                hitvector.y += player.Velocity.y * num3;
                                                direction = (hitvector - origin).normalized;

                                            }
                                            direction = (hitvector - origin).normalized;
                                        }
                                        if (chance >= temp - tempbody)
                                        {
                                            // body
                                            if (RaycastHelper.isPointVisible(player, player.PlayerBones.LeftShoulder.position) && RaycastHelper.isPointVisible(player, player.PlayerBones.RightShoulder.position))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip <= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightShoulder.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip > 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftShoulder.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                            }
                                            if (RaycastHelper.isPointVisible(player, player.PlayerBones.LeftShoulder.position) && !RaycastHelper.isPointVisible(player, player.PlayerBones.RightShoulder.position))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftShoulder.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (RaycastHelper.isPointVisible(player, player.PlayerBones.RightShoulder.position) && !RaycastHelper.isPointVisible(player, player.PlayerBones.LeftShoulder.position))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightShoulder.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                            // both visible
                                            if (RaycastHelper.Spine1(player) && RaycastHelper.Spine3(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip <= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine1.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip > 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine3.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }

                                            }
                                            // just spine3
                                            if (RaycastHelper.Spine3(player) && !RaycastHelper.Spine1(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine3.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                            // just spine1
                                            if (RaycastHelper.Spine1(player) && !RaycastHelper.Spine3(player))
                                            {

                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine1.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                        }

                                    }
                                }
                                // head legs
                                if (Globals.Config.Aimbot.PlayerLegChance > 1 && Globals.Config.Aimbot.PlayerBodyChance > 1)
                                {
                                    if (RaycastHelper.Head(player) && !((RaycastHelper.Spine1(player) || RaycastHelper.Spine3(player)) && (RaycastHelper.LeftThigh1(player) || RaycastHelper.RightThigh1(player) || RaycastHelper.LeftThigh2(player) || RaycastHelper.RightThigh2(player))))
                                    {
                                        templeg += tempbody / 2;
                                        temphead += tempbody / 2;
                                        int temp = templeg + temphead;

                                        if (chance <= temp - temphead)
                                        {
                                            //legs
                                            if (RaycastHelper.RightThigh1(player) && RaycastHelper.RightThigh2(player) && RaycastHelper.LeftThigh1(player) && RaycastHelper.RightThigh2(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 25)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 25 && coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh1.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50 && coinflip < 75)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 75)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }

                                            }
                                            if (RaycastHelper.RightThigh1(player) && RaycastHelper.RightThigh2(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }


                                            }
                                            if (RaycastHelper.LeftThigh2(player) && RaycastHelper.RightThigh2(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }


                                            }
                                            if (RaycastHelper.LeftThigh1(player) && RaycastHelper.LeftThigh2(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }


                                            }
                                            if (RaycastHelper.RightThigh1(player) && RaycastHelper.LeftThigh1(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh1.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }


                                            }
                                            if (RaycastHelper.LeftThigh1(player) && !RaycastHelper.LeftThigh2(player) & !RaycastHelper.RightThigh1(player) && !RaycastHelper.RightThigh2(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh1.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (!RaycastHelper.LeftThigh1(player) && RaycastHelper.LeftThigh2(player) & !RaycastHelper.RightThigh1(player) && !RaycastHelper.RightThigh2(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (!RaycastHelper.LeftThigh1(player) && !RaycastHelper.LeftThigh2(player) & RaycastHelper.RightThigh1(player) && !RaycastHelper.RightThigh2(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (!RaycastHelper.LeftThigh1(player) && !RaycastHelper.LeftThigh2(player) & !RaycastHelper.RightThigh1(player) && RaycastHelper.RightThigh2(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                        }
                                        if (chance > temp - temphead)
                                        {
                                            // head
                                            Vector3 hitvector = GetTargetPlayer().PlayerBones.Head.position + new Vector3(0, 0.07246377f, 0);
                                            if (Globals.Config.Aimbot.Prediction)
                                            {
                                                int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                float num2 = (float)num1;
                                                float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                hitvector.x += player.Velocity.x * num3;
                                                hitvector.y += player.Velocity.y * num3;
                                                direction = (hitvector - origin).normalized;

                                            }
                                            direction = (hitvector - origin).normalized;
                                        }
                                    }
                                }

                                // head body legs
                                if (Globals.Config.Aimbot.PlayerBodyChance > 1 && Globals.Config.Aimbot.PlayerLegChance > 1 && Globals.Config.Aimbot.PlayerHeadChance > 1)
                                {
                                    if (RaycastHelper.Head(player) && ((RaycastHelper.Spine1(player) || RaycastHelper.Spine3(player)) && (RaycastHelper.LeftThigh1(player) || RaycastHelper.RightThigh1(player) || RaycastHelper.LeftThigh2(player) || RaycastHelper.RightThigh2(player))))
                                    {

                                        int temp = templeg + temphead + tempbody;
                                        int templegbody = templeg + tempbody;
                                        // leg = 20 body = 30 head=50

                                        if (chance >= temp - templeg) // over 80
                                        {
                                            if (RaycastHelper.RightThigh1(player) && RaycastHelper.RightThigh2(player) && RaycastHelper.LeftThigh1(player) && RaycastHelper.RightThigh2(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 25)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 25 && coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh1.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50 && coinflip < 75)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 75)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }

                                            }
                                            if (RaycastHelper.RightThigh1(player) && RaycastHelper.RightThigh2(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }


                                            }
                                            if (RaycastHelper.LeftThigh2(player) && RaycastHelper.RightThigh2(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }


                                            }
                                            if (RaycastHelper.LeftThigh1(player) && RaycastHelper.LeftThigh2(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }


                                            }
                                            if (RaycastHelper.RightThigh1(player) && RaycastHelper.LeftThigh1(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh1.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }


                                            }
                                            if (RaycastHelper.LeftThigh1(player) && !RaycastHelper.LeftThigh2(player) & !RaycastHelper.RightThigh1(player) && !RaycastHelper.RightThigh2(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh1.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (!RaycastHelper.LeftThigh1(player) && RaycastHelper.LeftThigh2(player) & !RaycastHelper.RightThigh1(player) && !RaycastHelper.RightThigh2(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (!RaycastHelper.LeftThigh1(player) && !RaycastHelper.LeftThigh2(player) & RaycastHelper.RightThigh1(player) && !RaycastHelper.RightThigh2(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (!RaycastHelper.LeftThigh1(player) && !RaycastHelper.LeftThigh2(player) & !RaycastHelper.RightThigh1(player) && RaycastHelper.RightThigh2(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                        }
                                        if (chance <= temp - templegbody) // under 50
                                        {
                                            // head
                                            Vector3 hitvector = GetTargetPlayer().PlayerBones.Head.position + new Vector3(0, 0.07246377f, 0);
                                            if (Globals.Config.Aimbot.Prediction)
                                            {
                                                int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                float num2 = (float)num1;
                                                float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                hitvector.x += player.Velocity.x * num3;
                                                hitvector.y += player.Velocity.y * num3;
                                                direction = (hitvector - origin).normalized;

                                            }
                                            direction = (hitvector - origin).normalized;
                                        }
                                        // 80                               // 50
                                        if (chance < temp - templeg && chance > temp - templegbody) // between 51 and 79
                                        {
                                            // body
                                            if (RaycastHelper.isPointVisible(player, player.PlayerBones.LeftShoulder.position) && RaycastHelper.isPointVisible(player, player.PlayerBones.RightShoulder.position))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip <= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightShoulder.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip > 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftShoulder.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                            }
                                            if (RaycastHelper.isPointVisible(player, player.PlayerBones.LeftShoulder.position) && !RaycastHelper.isPointVisible(player, player.PlayerBones.RightShoulder.position))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftShoulder.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (RaycastHelper.isPointVisible(player, player.PlayerBones.RightShoulder.position) && !RaycastHelper.isPointVisible(player, player.PlayerBones.LeftShoulder.position))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightShoulder.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (RaycastHelper.Spine1(player) && RaycastHelper.Spine3(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip <= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine1.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip > 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine3.position;
                                                    if (Globals.Config.Aimbot.Prediction)
                                                    {
                                                        int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                        float num2 = (float)num1;
                                                        float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                        hitvector.x += player.Velocity.x * num3;
                                                        hitvector.y += player.Velocity.y * num3;
                                                        direction = (hitvector - origin).normalized;

                                                    }
                                                    direction = (hitvector - origin).normalized;
                                                }

                                            }
                                            // just spine3
                                            if (RaycastHelper.Spine3(player) && !RaycastHelper.Spine1(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine3.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                            // just spine1
                                            if (RaycastHelper.Spine1(player) && !RaycastHelper.Spine3(player))
                                            {

                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine1.position;
                                                if (Globals.Config.Aimbot.Prediction)
                                                {
                                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position);
                                                    float num2 = (float)num1;
                                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                                    hitvector.x += player.Velocity.x * num3;
                                                    hitvector.y += player.Velocity.y * num3;
                                                    direction = (hitvector - origin).normalized;

                                                }
                                                direction = (hitvector - origin).normalized;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }
                if ((GetTargetPlayer().Profile.Info.RegistrationDate <= 0) && Globals.Config.Aimbot.ScavAimbot)
                {
                  
                    #region ScavRageAimbot
                    if (Globals.Config.Aimbot.ScavRageAimbot)
                    {
                        if (hitchance.Next(1, 100) <= Globals.Config.Aimbot.ScavHitChance)
                        {
                            // we do this from decending order, least important to most important limb so then if a more important one is visible it will go through the code and get to the most important one that is visible.
                            if (RaycastHelper.RightThigh1(GetTargetPlayer()))
                            {
                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                if (Globals.Config.Aimbot.Prediction)
                                {
                                    
                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, GetTargetPlayer().Transform.position);
                                    float num2 = (float)num1;
                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                    hitvector.x += GetTargetPlayer().Velocity.x * num3;
                                    hitvector.y += GetTargetPlayer().Velocity.y * num3;
                                    direction = (hitvector - origin).normalized;

                                }
                                else
                                    direction = (hitvector - origin).normalized;
                                // direction = (GetTargetPlayer().PlayerBones.RightThigh1.position - origin).normalized;
                            }
                            if (RaycastHelper.LeftThigh1(GetTargetPlayer()))
                            {
                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh1.position;
                                if (Globals.Config.Aimbot.Prediction)
                                {
                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, GetTargetPlayer().Transform.position);
                                    float num2 = (float)num1;
                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                    hitvector.x += GetTargetPlayer().Velocity.x * num3;
                                    hitvector.y += GetTargetPlayer().Velocity.y * num3;
                                    direction = (hitvector - origin).normalized;
                                }
                                else
                                    direction = (hitvector - origin).normalized;
                            }
                            if (RaycastHelper.RightThigh2(GetTargetPlayer()))
                            {
                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                if (Globals.Config.Aimbot.Prediction)
                                {
                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, GetTargetPlayer().Transform.position);
                                    float num2 = (float)num1;
                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                    hitvector.x += GetTargetPlayer().Velocity.x * num3;
                                    hitvector.y += GetTargetPlayer().Velocity.y * num3;
                                    direction = (hitvector - origin).normalized;
                                }
                                else
                                    direction = (hitvector - origin).normalized;
                            }
                            if (RaycastHelper.LeftThigh2(GetTargetPlayer()))
                            {
                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                if (Globals.Config.Aimbot.Prediction)
                                {
                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, GetTargetPlayer().Transform.position);
                                    float num2 = (float)num1;
                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                    hitvector.x += GetTargetPlayer().Velocity.x * num3;
                                    hitvector.y += GetTargetPlayer().Velocity.y * num3;
                                    direction = (hitvector - origin).normalized;
                                }
                                else
                                    direction = (hitvector - origin).normalized;
                            }
                            if (RaycastHelper.isPointVisible(GetTargetPlayer(), GetTargetPlayer().PlayerBones.RightShoulder.position))
                            {
                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightShoulder.position;
                                if (Globals.Config.Aimbot.Prediction)
                                {
                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, GetTargetPlayer().Transform.position);
                                    float num2 = (float)num1;
                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                    hitvector.x += GetTargetPlayer().Velocity.x * num3;
                                    hitvector.y += GetTargetPlayer().Velocity.y * num3;
                                    direction = (hitvector - origin).normalized;
                                }
                                else
                                    direction = (hitvector - origin).normalized;
                            }
                            if (RaycastHelper.isPointVisible(GetTargetPlayer(), GetTargetPlayer().PlayerBones.LeftShoulder.position))
                            {
                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftShoulder.position;
                                if (Globals.Config.Aimbot.Prediction)
                                {
                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, GetTargetPlayer().Transform.position);
                                    float num2 = (float)num1;
                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                    hitvector.x += GetTargetPlayer().Velocity.x * num3;
                                    hitvector.y += GetTargetPlayer().Velocity.y * num3;
                                    direction = (hitvector - origin).normalized;
                                }
                                else
                                    direction = (hitvector - origin).normalized;
                            }
                            if (RaycastHelper.Spine3(GetTargetPlayer()))
                            {
                                Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine3.position;
                                if (Globals.Config.Aimbot.Prediction)
                                {
                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, GetTargetPlayer().Transform.position);
                                    float num2 = (float)num1;
                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                    hitvector.x += GetTargetPlayer().Velocity.x * num3;
                                    hitvector.y += GetTargetPlayer().Velocity.y * num3;
                                    direction = (hitvector - origin).normalized;
                                }
                                else
                                    direction = (hitvector - origin).normalized;
                            }
                            if (RaycastHelper.Spine1(GetTargetPlayer()))
                            {
                                Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine1.position;
                                if (Globals.Config.Aimbot.Prediction)
                                {
                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, GetTargetPlayer().Transform.position);
                                    float num2 = (float)num1;
                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                    hitvector.x += GetTargetPlayer().Velocity.x * num3;
                                    hitvector.y += GetTargetPlayer().Velocity.y * num3;
                                    direction = (hitvector - origin).normalized;
                                }
                                else
                                    direction = (hitvector - origin).normalized;
                            }
                            if (RaycastHelper.Head(GetTargetPlayer()))
                            {
                                Vector3 hitvector = GetTargetPlayer().PlayerBones.Head.position + new Vector3(0, 0.07246377f, 0);
                                if (Globals.Config.Aimbot.Prediction)
                                {
                                    int num1 = (int)Vector3.Distance(Globals.MainCamera.transform.position, GetTargetPlayer().Transform.position);
                                    float num2 = (float)num1;
                                    float num3 = (float)num1 / wpn.CurrentAmmoTemplate.InitialSpeed;
                                    hitvector.x += GetTargetPlayer().Velocity.x * num3;
                                    hitvector.y += GetTargetPlayer().Velocity.y * num3;
                                    direction = (hitvector - origin).normalized;
                                }
                                else
                                    direction = (hitvector - origin).normalized;

                            }
                        }
                    }
                    #endregion
                    #region ScavLegitAimbot
                    else
                    {
                        Player player = GetTargetPlayer();
                        if (hitchance.Next(1, 100) <= Globals.Config.Aimbot.ScavHitChance)
                        {
                            if (Globals.Config.Aimbot.ScavHeadChance + Globals.Config.Aimbot.ScavLegChance + Globals.Config.Aimbot.ScavBodyChance == 100)
                            {
                                int temphead = (int)Globals.Config.Aimbot.ScavHeadChance;
                                int tempbody = (int)Globals.Config.Aimbot.ScavBodyChance;
                                int templeg = (int)Globals.Config.Aimbot.ScavLegChance;
                                System.Random random = new System.Random();
                                int chance = random.Next(1, 100);
                                // note to self, this all needs to be oredered correctly

                                // body
                                if (Globals.Config.Aimbot.ScavBodyChance > 1)
                                    if (!RaycastHelper.Head(player) && ((RaycastHelper.Spine1(player) || RaycastHelper.Spine3(player)) && !(RaycastHelper.LeftThigh1(player) || RaycastHelper.RightThigh1(player) || RaycastHelper.LeftThigh2(player) || RaycastHelper.RightThigh2(player))))
                                    {
                                        if (RaycastHelper.isPointVisible(player, player.PlayerBones.LeftShoulder.position) && RaycastHelper.isPointVisible(player, player.PlayerBones.RightShoulder.position))
                                        {
                                            System.Random random2 = new System.Random();
                                            int coinflip = random2.Next(1, 100);
                                            if (coinflip <= 50)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightShoulder.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (coinflip > 50)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftShoulder.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                        }
                                        if (RaycastHelper.isPointVisible(player, player.PlayerBones.LeftShoulder.position) && !RaycastHelper.isPointVisible(player, player.PlayerBones.RightShoulder.position))
                                        {
                                            Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftShoulder.position;
                                            direction = (hitvector - origin).normalized;
                                        }
                                        if (RaycastHelper.isPointVisible(player, player.PlayerBones.RightShoulder.position) && !RaycastHelper.isPointVisible(player, player.PlayerBones.LeftShoulder.position))
                                        {
                                            Vector3 hitvector = GetTargetPlayer().PlayerBones.RightShoulder.position;
                                            direction = (hitvector - origin).normalized;
                                        }
                                        if (RaycastHelper.Spine1(player) && RaycastHelper.Spine3(player))
                                        {
                                            System.Random random2 = new System.Random();
                                            int coinflip = random2.Next(1, 100);
                                            if (coinflip <= 50)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine1.position;
                                                HitPos = hitvector;
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (coinflip > 50)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine3.position;
                                                HitPos = hitvector;
                                                direction = (hitvector - origin).normalized;
                                            }

                                        }
                                        // just spine3
                                        if (RaycastHelper.Spine3(player) && !RaycastHelper.Spine1(player))
                                        {
                                            Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine3.position;
                                            direction = (hitvector - origin).normalized;
                                        }
                                        // just spine1
                                        if (RaycastHelper.Spine1(player) && !RaycastHelper.Spine3(player))
                                        {

                                            Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine1.position;
                                            direction = (hitvector - origin).normalized;
                                        }
                                    }
                                // legs
                                if (Globals.Config.Aimbot.ScavLegChance > 1)
                                    if (!RaycastHelper.Head(player) && !(RaycastHelper.Spine1(player) || RaycastHelper.Spine3(player) && (RaycastHelper.LeftThigh1(player) || RaycastHelper.RightThigh1(player) || RaycastHelper.LeftThigh2(player) || RaycastHelper.RightThigh2(player))))
                                    {
                                        if (RaycastHelper.RightThigh1(player) && RaycastHelper.RightThigh2(player) && RaycastHelper.LeftThigh1(player) && RaycastHelper.RightThigh2(player))
                                        {
                                            System.Random random2 = new System.Random();
                                            int coinflip = random2.Next(1, 100);
                                            if (coinflip < 25)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (coinflip >= 25 && coinflip < 50)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh1.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (coinflip >= 50 && coinflip < 75)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (coinflip >= 75)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                direction = (hitvector - origin).normalized;
                                            }

                                        }
                                        if (RaycastHelper.RightThigh1(player) && RaycastHelper.RightThigh2(player))
                                        {
                                            System.Random random2 = new System.Random();
                                            int coinflip = random2.Next(1, 100);
                                            if (coinflip < 50)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (coinflip >= 50)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                direction = (hitvector - origin).normalized;
                                            }


                                        }
                                        if (RaycastHelper.LeftThigh2(player) && RaycastHelper.RightThigh2(player))
                                        {
                                            System.Random random2 = new System.Random();
                                            int coinflip = random2.Next(1, 100);
                                            if (coinflip < 50)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (coinflip >= 50)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                direction = (hitvector - origin).normalized;
                                            }


                                        }
                                        if (RaycastHelper.LeftThigh1(player) && RaycastHelper.LeftThigh2(player))
                                        {
                                            System.Random random2 = new System.Random();
                                            int coinflip = random2.Next(1, 100);
                                            if (coinflip < 50)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (coinflip >= 50)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                direction = (hitvector - origin).normalized;
                                            }


                                        }
                                        if (RaycastHelper.RightThigh1(player) && RaycastHelper.LeftThigh1(player))
                                        {
                                            System.Random random2 = new System.Random();
                                            int coinflip = random2.Next(1, 100);
                                            if (coinflip < 50)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (coinflip >= 50)
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh1.position;
                                                direction = (hitvector - origin).normalized;
                                            }


                                        }
                                        if (RaycastHelper.LeftThigh1(player) && !RaycastHelper.LeftThigh2(player) & !RaycastHelper.RightThigh1(player) && !RaycastHelper.RightThigh2(player))
                                        {
                                            Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh1.position;
                                            direction = (hitvector - origin).normalized;
                                        }
                                        if (!RaycastHelper.LeftThigh1(player) && RaycastHelper.LeftThigh2(player) & !RaycastHelper.RightThigh1(player) && !RaycastHelper.RightThigh2(player))
                                        {
                                            Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                            direction = (hitvector - origin).normalized;
                                        }
                                        if (!RaycastHelper.LeftThigh1(player) && !RaycastHelper.LeftThigh2(player) & RaycastHelper.RightThigh1(player) && !RaycastHelper.RightThigh2(player))
                                        {
                                            Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                            direction = (hitvector - origin).normalized;
                                        }
                                        if (!RaycastHelper.LeftThigh1(player) && !RaycastHelper.LeftThigh2(player) & !RaycastHelper.RightThigh1(player) && RaycastHelper.RightThigh2(player))
                                        {
                                            Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                            direction = (hitvector - origin).normalized;
                                        }
                                    }
                                // if head is only visible
                                if (Globals.Config.Aimbot.ScavHeadChance > 1)
                                    if (RaycastHelper.Head(player) && !((RaycastHelper.Spine1(player) || RaycastHelper.Spine3(player)) && !(RaycastHelper.LeftThigh1(player) || RaycastHelper.RightThigh1(player) || RaycastHelper.LeftThigh2(player) || RaycastHelper.RightThigh2(player))))
                                    {
                                        Vector3 hitvector = GetTargetPlayer().PlayerBones.Head.position + new Vector3(0, 0.07246377f, 0);
                                        direction = (hitvector - origin).normalized;
                                    }

                                // body legs
                                if (Globals.Config.Aimbot.ScavBodyChance > 1 && Globals.Config.Aimbot.ScavLegChance > 1)
                                    if (!RaycastHelper.Head(player) && ((RaycastHelper.Spine1(player) || RaycastHelper.Spine3(player)) && (RaycastHelper.LeftThigh1(player) || RaycastHelper.RightThigh1(player) || RaycastHelper.LeftThigh2(player) || RaycastHelper.RightThigh2(player))))
                                    {
                                        templeg += temphead / 2;
                                        tempbody += temphead / 2;
                                        int temp = templeg + tempbody;
                                        if (chance < temp - tempbody)
                                        {
                                            //legs
                                            if (RaycastHelper.RightThigh1(player) && RaycastHelper.RightThigh2(player) && RaycastHelper.LeftThigh1(player) && RaycastHelper.RightThigh2(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 25)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 25 && coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh1.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50 && coinflip < 75)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 75)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                    direction = (hitvector - origin).normalized;
                                                }

                                            }
                                            if (RaycastHelper.RightThigh1(player) && RaycastHelper.RightThigh2(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                    direction = (hitvector - origin).normalized;
                                                }


                                            }
                                            if (RaycastHelper.LeftThigh2(player) && RaycastHelper.RightThigh2(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                    direction = (hitvector - origin).normalized;
                                                }


                                            }
                                            if (RaycastHelper.LeftThigh1(player) && RaycastHelper.LeftThigh2(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                    direction = (hitvector - origin).normalized;
                                                }


                                            }
                                            if (RaycastHelper.RightThigh1(player) && RaycastHelper.LeftThigh1(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh1.position;
                                                    direction = (hitvector - origin).normalized;
                                                }


                                            }
                                            if (RaycastHelper.LeftThigh1(player) && !RaycastHelper.LeftThigh2(player) & !RaycastHelper.RightThigh1(player) && !RaycastHelper.RightThigh2(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh1.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (!RaycastHelper.LeftThigh1(player) && RaycastHelper.LeftThigh2(player) & !RaycastHelper.RightThigh1(player) && !RaycastHelper.RightThigh2(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (!RaycastHelper.LeftThigh1(player) && !RaycastHelper.LeftThigh2(player) & RaycastHelper.RightThigh1(player) && !RaycastHelper.RightThigh2(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (!RaycastHelper.LeftThigh1(player) && !RaycastHelper.LeftThigh2(player) & !RaycastHelper.RightThigh1(player) && RaycastHelper.RightThigh2(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                        }
                                        if (chance >= temp - tempbody)
                                        {
                                            // body
                                            if (RaycastHelper.isPointVisible(player, player.PlayerBones.LeftShoulder.position) && RaycastHelper.isPointVisible(player, player.PlayerBones.RightShoulder.position))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip <= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightShoulder.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip > 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftShoulder.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                            }
                                            if (RaycastHelper.isPointVisible(player, player.PlayerBones.LeftShoulder.position) && !RaycastHelper.isPointVisible(player, player.PlayerBones.RightShoulder.position))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftShoulder.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (RaycastHelper.isPointVisible(player, player.PlayerBones.RightShoulder.position) && !RaycastHelper.isPointVisible(player, player.PlayerBones.LeftShoulder.position))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightShoulder.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (RaycastHelper.Spine1(player) && RaycastHelper.Spine3(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip <= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine1.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip > 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine3.position;
                                                    direction = (hitvector - origin).normalized;
                                                }

                                            }
                                            // just spine3
                                            if (RaycastHelper.Spine3(player) && !RaycastHelper.Spine1(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine3.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                            // just spine1
                                            if (RaycastHelper.Spine1(player) && !RaycastHelper.Spine3(player))
                                            {

                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine1.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                        }
                                    }
                                // head body
                                if (Globals.Config.Aimbot.ScavBodyChance > 1 && Globals.Config.Aimbot.ScavHeadChance > 1)
                                    if (RaycastHelper.Head(player) && ((RaycastHelper.Spine1(player) || RaycastHelper.Spine3(player)) && !(RaycastHelper.LeftThigh1(player) || RaycastHelper.RightThigh1(player) || RaycastHelper.LeftThigh2(player) || RaycastHelper.RightThigh2(player))))
                                    {
                                        temphead += templeg / 2;
                                        tempbody += templeg / 2;
                                        int temp = temphead + tempbody;
                                        if (chance < temp - tempbody)
                                        {
                                            // head
                                            Vector3 hitvector = GetTargetPlayer().PlayerBones.Head.position + new Vector3(0, 0.07246377f, 0);
                                            direction = (hitvector - origin).normalized;
                                        }
                                        if (chance >= temp - tempbody)
                                        {
                                            // body
                                            if (RaycastHelper.isPointVisible(player, player.PlayerBones.LeftShoulder.position) && RaycastHelper.isPointVisible(player, player.PlayerBones.RightShoulder.position))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip <= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightShoulder.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip > 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftShoulder.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                            }
                                            if (RaycastHelper.isPointVisible(player, player.PlayerBones.LeftShoulder.position) && !RaycastHelper.isPointVisible(player, player.PlayerBones.RightShoulder.position))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftShoulder.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (RaycastHelper.isPointVisible(player, player.PlayerBones.RightShoulder.position) && !RaycastHelper.isPointVisible(player, player.PlayerBones.LeftShoulder.position))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightShoulder.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                            // both visible
                                            if (RaycastHelper.Spine1(player) && RaycastHelper.Spine3(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip <= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine1.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip > 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine3.position;
                                                    direction = (hitvector - origin).normalized;
                                                }

                                            }
                                            // just spine3
                                            if (RaycastHelper.Spine3(player) && !RaycastHelper.Spine1(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine3.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                            // just spine1
                                            if (RaycastHelper.Spine1(player) && !RaycastHelper.Spine3(player))
                                            {

                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine1.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                        }

                                    }
                                // head legs
                                if (Globals.Config.Aimbot.ScavLegChance > 1 && Globals.Config.Aimbot.ScavBodyChance > 1)
                                    if (RaycastHelper.Head(player) && !((RaycastHelper.Spine1(player) || RaycastHelper.Spine3(player)) && (RaycastHelper.LeftThigh1(player) || RaycastHelper.RightThigh1(player) || RaycastHelper.LeftThigh2(player) || RaycastHelper.RightThigh2(player))))
                                    {
                                        templeg += tempbody / 2;
                                        temphead += tempbody / 2;
                                        int temp = templeg + temphead;

                                        if (chance <= temp - temphead)
                                        {
                                            //legs
                                            if (RaycastHelper.RightThigh1(player) && RaycastHelper.RightThigh2(player) && RaycastHelper.LeftThigh1(player) && RaycastHelper.RightThigh2(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 25)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 25 && coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh1.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50 && coinflip < 75)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 75)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                    direction = (hitvector - origin).normalized;
                                                }

                                            }
                                            if (RaycastHelper.RightThigh1(player) && RaycastHelper.RightThigh2(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                    direction = (hitvector - origin).normalized;
                                                }


                                            }
                                            if (RaycastHelper.LeftThigh2(player) && RaycastHelper.RightThigh2(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                    direction = (hitvector - origin).normalized;
                                                }


                                            }
                                            if (RaycastHelper.LeftThigh1(player) && RaycastHelper.LeftThigh2(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                    direction = (hitvector - origin).normalized;
                                                }


                                            }
                                            if (RaycastHelper.RightThigh1(player) && RaycastHelper.LeftThigh1(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh1.position;
                                                    direction = (hitvector - origin).normalized;
                                                }


                                            }
                                            if (RaycastHelper.LeftThigh1(player) && !RaycastHelper.LeftThigh2(player) & !RaycastHelper.RightThigh1(player) && !RaycastHelper.RightThigh2(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh1.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (!RaycastHelper.LeftThigh1(player) && RaycastHelper.LeftThigh2(player) & !RaycastHelper.RightThigh1(player) && !RaycastHelper.RightThigh2(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (!RaycastHelper.LeftThigh1(player) && !RaycastHelper.LeftThigh2(player) & RaycastHelper.RightThigh1(player) && !RaycastHelper.RightThigh2(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (!RaycastHelper.LeftThigh1(player) && !RaycastHelper.LeftThigh2(player) & !RaycastHelper.RightThigh1(player) && RaycastHelper.RightThigh2(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                        }
                                        if (chance > temp - temphead)
                                        {
                                            // head
                                            Vector3 hitvector = GetTargetPlayer().PlayerBones.Head.position + new Vector3(0, 0.07246377f, 0);
                                            direction = (hitvector - origin).normalized;
                                        }
                                    }

                                // head body legs
                                if (Globals.Config.Aimbot.ScavBodyChance > 1 && Globals.Config.Aimbot.ScavLegChance > 1 && Globals.Config.Aimbot.ScavHeadChance > 1)
                                    if (RaycastHelper.Head(player) && ((RaycastHelper.Spine1(player) || RaycastHelper.Spine3(player)) && (RaycastHelper.LeftThigh1(player) || RaycastHelper.RightThigh1(player) || RaycastHelper.LeftThigh2(player) || RaycastHelper.RightThigh2(player))))
                                    {

                                        int temp = templeg + temphead + tempbody;
                                        int templegbody = templeg + tempbody;
                                        // leg = 20 body = 30 head=50

                                        if (chance >= temp - templeg) // over 80
                                        {
                                            if (RaycastHelper.RightThigh1(player) && RaycastHelper.RightThigh2(player) && RaycastHelper.LeftThigh1(player) && RaycastHelper.RightThigh2(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 25)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 25 && coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh1.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50 && coinflip < 75)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 75)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                    direction = (hitvector - origin).normalized;
                                                }

                                            }
                                            if (RaycastHelper.RightThigh1(player) && RaycastHelper.RightThigh2(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                    direction = (hitvector - origin).normalized;
                                                }


                                            }
                                            if (RaycastHelper.LeftThigh2(player) && RaycastHelper.RightThigh2(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                    direction = (hitvector - origin).normalized;
                                                }


                                            }
                                            if (RaycastHelper.LeftThigh1(player) && RaycastHelper.LeftThigh2(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                    direction = (hitvector - origin).normalized;
                                                }


                                            }
                                            if (RaycastHelper.RightThigh1(player) && RaycastHelper.LeftThigh1(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip < 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip >= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh1.position;
                                                    direction = (hitvector - origin).normalized;
                                                }


                                            }
                                            if (RaycastHelper.LeftThigh1(player) && !RaycastHelper.LeftThigh2(player) & !RaycastHelper.RightThigh1(player) && !RaycastHelper.RightThigh2(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh1.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (!RaycastHelper.LeftThigh1(player) && RaycastHelper.LeftThigh2(player) & !RaycastHelper.RightThigh1(player) && !RaycastHelper.RightThigh2(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftThigh2.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (!RaycastHelper.LeftThigh1(player) && !RaycastHelper.LeftThigh2(player) & RaycastHelper.RightThigh1(player) && !RaycastHelper.RightThigh2(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh1.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (!RaycastHelper.LeftThigh1(player) && !RaycastHelper.LeftThigh2(player) & !RaycastHelper.RightThigh1(player) && RaycastHelper.RightThigh2(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightThigh2.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                        }
                                        if (chance <= temp - templegbody) // under 50
                                        {
                                            // head
                                            Vector3 hitvector = GetTargetPlayer().PlayerBones.Head.position + new Vector3(0, 0.07246377f, 0);
                                            direction = (hitvector - origin).normalized;
                                        }
                                        // 80                               // 50
                                        if (chance < temp - templeg && chance > temp - templegbody) // between 51 and 79
                                        {
                                            // body
                                            if (RaycastHelper.isPointVisible(player, player.PlayerBones.LeftShoulder.position) && RaycastHelper.isPointVisible(player, player.PlayerBones.RightShoulder.position))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip <= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.RightShoulder.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip > 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftShoulder.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                            }
                                            if (RaycastHelper.isPointVisible(player, player.PlayerBones.LeftShoulder.position) && !RaycastHelper.isPointVisible(player, player.PlayerBones.RightShoulder.position))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.LeftShoulder.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (RaycastHelper.isPointVisible(player, player.PlayerBones.RightShoulder.position) && !RaycastHelper.isPointVisible(player, player.PlayerBones.LeftShoulder.position))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.RightShoulder.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                            if (RaycastHelper.Spine1(player) && RaycastHelper.Spine3(player))
                                            {
                                                System.Random random2 = new System.Random();
                                                int coinflip = random2.Next(1, 100);
                                                if (coinflip <= 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine1.position;
                                                    direction = (hitvector - origin).normalized;
                                                }
                                                if (coinflip > 50)
                                                {
                                                    Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine3.position;
                                                    direction = (hitvector - origin).normalized;
                                                }

                                            }
                                            // just spine3
                                            if (RaycastHelper.Spine3(player) && !RaycastHelper.Spine1(player))
                                            {
                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine3.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                            // just spine1
                                            if (RaycastHelper.Spine1(player) && !RaycastHelper.Spine3(player))
                                            {

                                                Vector3 hitvector = GetTargetPlayer().PlayerBones.Spine1.position;
                                                direction = (hitvector - origin).normalized;
                                            }
                                        }
                                    }

                            }
                        }
                    }
                    #endregion
                }

            }

        }
        [ObfuscationAttribute(Exclude = true)]
        public object CreateShot_SilentHook(object ammo, Vector3 origin1, Vector3 direction1, int fireIndex, Player player1, Item weapon, float speedFactor = 1f, int fragmentIndex = 0)
        {
            //      speedFactor = 5; // used to be able to do instant hit 
            AimbotStuff(ref direction1, ref origin1); // in this hook we call this aimbot function and then we pass a pointer to direction and origin to it which it will edit

            CreateShot_Silent.Unhook();


            object[] parameters = new object[]
               {
                    ammo,
                    origin1,
                    direction1,
                    fireIndex,
                    player1,
                    weapon,
                    speedFactor,
                    fragmentIndex
               };
            object result = CreateShot_Silent.OriginalMethod.Invoke(this, parameters);

            CreateShot_Silent.Hook();
            return result;
        }
        public static List<Player> SortClosestToCrosshair(List<Player> p)
        {
            return (from tempPlayer in p
                    orderby Vector2.Distance(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)), Camera.main.WorldToScreenPoint(tempPlayer.Transform.position))
                    select tempPlayer).ToList<Player>();
        }


        public static Player GetTargetPlayer()
        {
            Player baseplayer = new Player();
            try
            {

                if (Globals.GameWorld != null)
                {
                    List<Player> playerlist = SortClosestToCrosshair(Globals.GameWorld.RegisteredPlayers); // sort the player list

                    foreach (Player player in playerlist)
                    {

                        if (player != null && player != Globals.LocalPlayer && !Globals.IsInYourGroup(player))
                        {
                            bool isscav = false;
                            isscav = player.Profile.Info.RegistrationDate <= 0;
                            Vector2 vector = new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2));
                            int num = (int)Vector2.Distance(Globals.MainCamera.WorldToScreenPoint(player.PlayerBones.Head.position), vector); // fov check
                            int num2 = (int)Vector3.Distance(Globals.MainCamera.transform.position, player.Transform.position); // distance from player
                          
                            if (num <= Globals.Config.Aimbot.Fov)
                            {
                                if (!isscav && Globals.Config.Aimbot.PlayerAimbot)
                                {
                                    if (num2 < Globals.Config.Aimbot.PlayerMaxDistance)
                                    {
                                        if (Globals.Config.Aimbot.PlayerRageAimbot)
                                        {
                                            if (RaycastHelper.Head(player) || RaycastHelper.isPointVisible(player, player.PlayerBones.LeftShoulder.position) || RaycastHelper.isPointVisible(player, player.PlayerBones.RightShoulder.position) || RaycastHelper.Spine1(player) || RaycastHelper.Spine3(player) || RaycastHelper.RightThigh1(player) || RaycastHelper.RightThigh2(player) || RaycastHelper.LeftThigh1(player) || RaycastHelper.LeftThigh2(player))
                                            {
                                                return player; // check for visibility and hitscan any bone we can

                                            }
                                        }
                                        else
                                        {
                                            if (Globals.Config.Aimbot.PlayerHeadChance > 0 && RaycastHelper.Head(player)) // check if player has selected head aimbot and head is visible
                                            {
                                                return player;
                                            }
                                            if (Globals.Config.Aimbot.PlayerBodyChance > 0 && (RaycastHelper.Spine1(player) || RaycastHelper.Spine3(player) || RaycastHelper.isPointVisible(player, player.PlayerBones.LeftShoulder.position) || RaycastHelper.isPointVisible(player, player.PlayerBones.RightShoulder.position)))
                                            {
                                                // check if player has selected body aimbot and body is visible
                                                return player;
                                            }
                                            if (Globals.Config.Aimbot.PlayerLegChance > 0 && (RaycastHelper.RightThigh1(player) || RaycastHelper.RightThigh2(player) || RaycastHelper.LeftThigh1(player) || RaycastHelper.LeftThigh2(player)))
                                            {
                                                // check if leg aimbot is selected and legs are visible
                                                return player;
                                            }
                                        }
                                    }
                                }
                                if (isscav && Globals.Config.Aimbot.ScavAimbot)
                                {
                                    if (num2 <= Globals.Config.Aimbot.ScavMaxDistance)
                                    {

                                        if (Globals.Config.Aimbot.ScavRageAimbot)
                                        {

                                            if (RaycastHelper.Head(player) || RaycastHelper.isPointVisible(player, player.PlayerBones.LeftShoulder.position) || RaycastHelper.isPointVisible(player, player.PlayerBones.RightShoulder.position) || RaycastHelper.Spine1(player) || RaycastHelper.Spine3(player) || RaycastHelper.RightThigh1(player) || RaycastHelper.RightThigh2(player) || RaycastHelper.LeftThigh1(player) || RaycastHelper.LeftThigh2(player))
                                            {
                                                // hit scan for any visible bone
                                                return player;

                                            }
                                        }
                                        else
                                        {
                                            if (Globals.Config.Aimbot.ScavHeadChance > 0 && RaycastHelper.Head(player))
                                            {
                                                return player;// check if player has selected head aimbot and head is visible
                                            }
                                            if (Globals.Config.Aimbot.ScavBodyChance > 0 && (RaycastHelper.Spine1(player) || RaycastHelper.Spine3(player) || RaycastHelper.isPointVisible(player, player.PlayerBones.LeftShoulder.position) || RaycastHelper.isPointVisible(player, player.PlayerBones.RightShoulder.position)))
                                            {
                                                return player;  // check if player has selected body aimbot and body is visible
                                            }
                                            if (Globals.Config.Aimbot.ScavLegChance > 0 && (RaycastHelper.RightThigh1(player) || RaycastHelper.RightThigh2(player) || RaycastHelper.LeftThigh1(player) || RaycastHelper.LeftThigh2(player)))
                                            {
                                                return player;          // check if leg aimbot is selected and legs are visible
                                            }

                                            // vis checks with the chance shit so then we dont get a targetplayer from their legs and the user doesn't have a legs hitchance
                                        }
                                    }

                                }
                            }


                        }
                    }
                }


            }
            catch
            { }
            return baseplayer;
        }
    }
}
