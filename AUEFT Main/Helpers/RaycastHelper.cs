using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comfort.Common;
using EFT;
using EFT.Interactive;
using EFT.InventoryLogic;
using EFT.UI;
using UnityEngine;

namespace AuftEftMain.Helpers
{
    public class RaycastHelper
    {
        //  private static int vis_mask = 69632;
        private static readonly LayerMask vis_mask = 1 << 12 | 1 << 16  | 1 << LayerMask.GetMask("Foliage") | 1 << LayerMask.GetMask("Grass");
        //1 << 12 | 1 << 16 | 1 << 18 | 1 << LayerMask.GetMask("Foliage") | 1 << LayerMask.GetMask("Grass");
        //1 << 12 | 1 << 16 | 1 << 18 | 1 << 22 | 1 << 31 | LayerMask.GetMask("TransparentCollider") | LayerMask.GetMask("Foliage")
        // https://imgur.com/RqTa22K
        // 26 is foliage  31 is grass
        private static RaycastHit raycastHit;
    /*    public static string BarrelRayCastTest(Player player)
        {
            try
            {
                Physics.Linecast(player.Fireport.position, player.Fireport.position - player.Fireport.up * 1000f, out raycastHit);
                return raycastHit.transform.gameObject.layer.ToString();
            }
            catch
            {
                return "Unkown";
            }
        }*/

        public static Vector3 FinalVector(Diz.Skinning.Skeleton skeletor, int BoneId)
        {
            try
            {
                return skeletor.Bones.ElementAt(BoneId).Value.position;
            }
            catch { return Vector3.zero; }
        }
        public static LayerMask GetAllCollisionsLayerMask(int layerCollision)
        {
            LayerMask layerMask = 0;
            for (int i = 0; i < 32; i++)
            {
                if (!Physics.GetIgnoreLayerCollision(i, layerCollision))
                {
                    layerMask |= 1 << i;
                }
            }
            return layerMask;
        }
        public static bool Spine1(Player player)
        {
            return Physics.Linecast(
                Camera.main.transform.position,
                player.PlayerBones.Spine1.position,
                out raycastHit,
                vis_mask) &&raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == player.gameObject.transform.root.gameObject;
        }
        public static bool Spine3(Player player)
        {
            return Physics.Linecast(
                Camera.main.transform.position,
                player.PlayerBones.Spine3.position,
                out raycastHit,
                vis_mask) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == player.gameObject.transform.root.gameObject;
        }
        public static bool LeftThigh1(Player player)
        {
            return Physics.Linecast(
                Camera.main.transform.position,
                player.PlayerBones.LeftThigh1.position,
                out raycastHit,
                vis_mask) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == player.gameObject.transform.root.gameObject;
        }
        public static bool LeftThigh2(Player player)
        {
            return Physics.Linecast(
                Camera.main.transform.position,
                player.PlayerBones.LeftThigh2.position,
                out raycastHit,
                vis_mask) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == player.gameObject.transform.root.gameObject;
        }
        public static bool RightThigh1(Player player)
        {
            return Physics.Linecast(
                Camera.main.transform.position,
                player.PlayerBones.RightThigh1.position,
                out raycastHit,
                vis_mask) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == player.gameObject.transform.root.gameObject;
        }
        public static bool RightThigh2(Player player)
        {
            return Physics.Linecast(
                Camera.main.transform.position,
                player.PlayerBones.RightThigh2.position,
                out raycastHit,
                vis_mask) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == player.gameObject.transform.root.gameObject;
        }

        public static bool Head(Player player)
        {
            return Physics.Linecast(
                Camera.main.transform.position,
                player.PlayerBones.Head.position + new Vector3(0, 0.07246377f,0),
                out raycastHit,
                vis_mask) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == player.gameObject.transform.root.gameObject;
        }
        public static bool isPointVisible(Player player, Vector3 BonePos)
        {
            return Physics.Linecast(
                Camera.main.transform.position,
               BonePos,
                out raycastHit,
                vis_mask) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == player.gameObject.transform.root.gameObject;
        }
    }
}
