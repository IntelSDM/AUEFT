using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace AuftEftMain
{
    [ObfuscationAttribute(Exclude = true)]
    public class loader
    {
        

        [ObfuscationAttribute(Exclude = true)]
        public static void Load()
        {

            //    StartUp();
            //      Init().Start();
            Hackobject.AddComponent<Auth>();
            GameObject.DontDestroyOnLoad(Hackobject);
        }
 
        private static GameObject Hackobject = new GameObject();
    }
}
