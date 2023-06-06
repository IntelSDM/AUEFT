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

   class Auth : MonoBehaviour
    {
        public static string Username;
        public static string Password;
        private static double Time;
        public static string Hwid;
        private static readonly string DocPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Auft\\");

        private static readonly string Hash = "Gduidfgwuwsjbjkds123vbwuib5345wiuvbwdj322sf11212w";


        private static void LoadIt()
        {
              RealObject.AddComponent<Globals>();
            RealObject.AddComponent<Caching>();
            RealObject.AddComponent<Controls>();
            RealObject.AddComponent<Misc>();
            RealObject.AddComponent<Aimbot>();
            GameObject.DontDestroyOnLoad(RealObject);

        }
        [ObfuscationAttribute(Exclude = true)]
        private void Awake()
        {

             StartUp();
            Init().Start();
         
        }
       private static GameObject RealObject = new GameObject();
        private async Task Init()
        {
            // auth system removed
            LoadIt();
          

        }
        private void ClearUp()
        {

            if (Username != null && Password != null)
            {
                if (File.Exists(DocPath + "Auft.UI1"))
                {
                    File.Delete(DocPath + "Auft.UI1");
                }
                if (File.Exists(DocPath + "Auft.UI2"))
                {
                    File.Delete(DocPath + "Auft.UI2");
                }
                if (File.Exists(DocPath + "Auft.UI3"))
                {
                    File.Delete(DocPath + "Auft.UI3");
                }
                if (File.Exists(DocPath + "Auft.UI4"))
                {
                    File.Delete(DocPath + "Auft.UI4");
                }
            }
        }
        private string DecryptStatic(string _cipherText)
        {

            try
            {
                byte[] _cipherBytes = Convert.FromBase64String(_cipherText);
                using (Aes _encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes _pdb = new Rfc2898DeriveBytes(Hash, new byte[] { 0xde, 0xad, 0xbe, 0xef, 0xfa, 0x66, 0x07, 0x42, 0x06, 0x95, 0x64, 0x65, 0x76 });
                    _encryptor.Key = _pdb.GetBytes(32);
                    _encryptor.IV = _pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, _encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(_cipherBytes, 0, _cipherBytes.Length);
                            cs.Close();
                        }
                        _cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return _cipherText;
        }
        private void StartUp()
        {
            if (File.Exists(DocPath + "Auft.UI1"))
            {
                Username = DecryptStatic(File.ReadAllText(DocPath + "Auft.UI1"));
                File.Delete(DocPath + "Auft.UI1");
            }
            else
            {

                Environment.Exit(0);
            }
            if (File.Exists(DocPath + "Auft.UI2"))
            {
                Password = DecryptStatic(File.ReadAllText(DocPath + "Auft.UI2"));
                File.Delete(DocPath + "Auft.UI2");
            }
            else
            {

                Environment.Exit(0);
            }
            if (File.Exists(DocPath + "Auft.UI4"))
            {
                Hwid = DecryptStatic(File.ReadAllText(DocPath + "Auft.UI4"));
                File.Delete(DocPath + "Auft.UI4");
            }
            else
            {

                Environment.Exit(0);
            }
            if (File.Exists(DocPath + "Auft.UI3"))
            {
                bool set = true;
                TimeSpan seconds = (DateTime.UtcNow - new DateTime(1970, 1, 1));
                int currenttime = 0;
                if (set)
                {
                    currenttime = (int)seconds.TotalSeconds;
                    set = false;

                }
                try
                {
                    string preparse = DecryptStatic(File.ReadAllText(DocPath + "Auft.UI3"));
                    //if (preparse.Contains(","))
            //            preparse.Replace(",", "."); // checks if they have a comma because for some reason swedish users have commas instead of decimal places
                    Time = (int)int.Parse(preparse);
                   
                }
                catch (Exception ex)
                {
                    File.WriteAllText(DocPath + "Shitdude", double.Parse(DecryptStatic(File.ReadAllText(DocPath + "Auft.UI3"))).ToString());

                }

                       File.Delete(DocPath + "Auft.UI3");
                if (Time + 500 < currenttime)
                {
                  Environment.Exit(0);

                }


            }
            else
            {
              //  Environment.Exit(0);
            }
            if ((Username == string.Empty && File.Exists(DocPath + "Auft.UI1")) || (Password == string.Empty && File.Exists(DocPath + "Auft.UI2")))
            {
                ClearUp();
               // Environment.Exit(0);

            }
        }

    }
}
