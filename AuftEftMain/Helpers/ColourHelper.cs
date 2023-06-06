using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AuftEftMain.Helpers
{
    class ColourHelper
    {
        public static void AddColours()
        {
            //  AddColour("BossColour", Color.red);
        }
        public static Color32 GetColour(string identifier)
        {
            if (Globals.Config.Colour.GlobalColors.TryGetValue(identifier, out var toret))
                return toret;
            // get the colour.a and shit as slider then  have button to set colour, eZ PZ, no extra fuckery. make sure to call draw() on config load and when setting colour.
            return Color.magenta;
        }

        public static void AddColour(string id, Color32 c)
        {
            if (!Globals.Config.Colour.GlobalColors.ContainsKey(id))
                Globals.Config.Colour.GlobalColors.Add(id, c);
        }

        public static void SetColour(string id, Color32 c) => Globals.Config.Colour.GlobalColors[id] = c;

        public static string ColourToHex(Color32 color)
        {
            string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
            return hex;
        }
    }
}
