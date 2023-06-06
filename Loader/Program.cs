using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Loader
{
    /*
    All Auth related stuff has been removed so some of the code might not make sense, might have errors etc.
    */
    class Program
    {
        private static string Username;
        private static string Password;
        private static readonly string DocPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Auft\\");


        static async Task Main(string[] args)
        {



            foreach (Process p in Process.GetProcesses())
            {
                if (p.ProcessName == "EscapeFromTarkov")
                {

                    Console.WriteLine("Close EFT Before Continuing");
                    // prevents people having the game open preventing our loader opening the game with the payload, you cant close the game in time to open it again in the loader

                }
            }



            // get username
            Console.Write("Username:");
            Username = Console.ReadLine();

            // get password
            Console.Write("Password:");
            Password = Console.ReadLine();


            /*
            This method stupidly relies on the game being on the c drive, yeah stupid. should have just used registry.
            But old code, what can you do aye?
            */
            bool passed = false;
            foreach (DriveInfo d in DriveInfo.GetDrives()) // loop drives
            {
                if (d.Name.Contains("C:")) // check for c drive
                {
                    if (d.AvailableFreeSpace > 2684354560) // does it have 26.8gb free space?
                    {
                        passed = true;
                    }
                    else
                    {
                        // not enough space
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Error Temps Uncreatable");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Press Any Key To Close");
                        Console.ReadKey();
                        Environment.Exit(0);

                    }
                }



            }
            if (passed) // has enough space
            {
                if (Verify())
                {

                    if (!Directory.Exists(DocPath))
                        Directory.CreateDirectory(DocPath); // create our config path


                    byte[] byters = await API.Auth.GetFile("eb7f027247ab6011f1ac552041d3a868"); // send assembly c sharp over to client (md5 hash)
                    if (byters.Length != File.ReadAllBytes("C:/Battlestate Games/EFT/EscapeFromTarkov_Data/Managed/Assembly-CSharp.dll").Length) // check the file length to see if the game version is the same
                    {
                        // notify the user
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Cheat Outdated Please Notify Staff In Discord");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Press Any Key To Close");
                        Console.ReadKey();
                    }
                    else
                    {

                        byte[] byters3 = await API.Auth.GetFile("4466bbca169be0ebd06fbcc086a5c445"); // this is a file with all of our file paths in to remove them from string data in compiled binaries
                        string StreamedData = Encoding.Default.GetString(byters3);


                        /*      Console.WriteLine(StreamedData.Remove(48, 200)); // C:/Battlestate Games/EFT/BattlEye/BELauncher.ini
                              Console.WriteLine(StreamedData.Remove(0, 48).Remove(46, 154)); // C:/Battlestate Games/EFT/EscapeFromTarkoe_Data
                              Console.WriteLine(StreamedData.Remove(0, 94).Remove(45, 109)); // C:/Battlestate Games/EFT/EscapeFromTarkov.exe
                              Console.WriteLine(StreamedData.Remove(0, 139).Remove(45, 64)); // C:/Battlestate Games/EFT/EscapeFromTarkoe.exe
                              Console.WriteLine(StreamedData.Remove(0, 184).Remove(46, 18)); // C:/Battlestate Games/EFT/EscapeFromTarkov_Data
                              Console.WriteLine(StreamedData.Remove(0, 230)); // AssemblyCsharp.dll
                        */
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Cheat Loading. Please Wait.");

                        // here we just extract all of the instruction information from here so we can reference everything in byte arrays and not touch string memory until needed
                        if (File.Exists(StreamedData.Remove(48, 200))) // C:/Battlestate Games/EFT/BattlEye/BELauncher.ini
                        {
                            if (!Directory.Exists(StreamedData.Remove(0, 48).Remove(46, 154))) // C:/Battlestate Games/EFT/EscapeFromTarkoe_Data
                            {


                                //C:/Battlestate Games/EFT/EscapeFromTarkov_Data                //C:/Battlestate Games/EFT/EscapeFromTarkoe_Data
                                /*
                                So we copy all of the escapefromtarkovdata folder to a new folder called escapefromtarkoe which is where the new game is now running
                                */
                                CopyFilesRecursively(StreamedData.Remove(0, 184).Remove(46, 18), StreamedData.Remove(0, 48).Remove(46, 154));
                            }
                            else
                            {
                                // clean traces by deleting the directory of the old files and also remove outdated stuff
                                Directory.Delete(StreamedData.Remove(0, 48).Remove(46, 154), true); // C:/Battlestate Games/EFT/EscapeFromTarkoe_Data
                                if (!Directory.Exists(StreamedData.Remove(0, 48).Remove(46, 154))) // C:/Battlestate Games/EFT/EscapeFromTarkoe_Data
                                {
                                    //C:/Battlestate Games/EFT/EscapeFromTarkov_Data                //C:/Battlestate Games/EFT/EscapeFromTarkoe_Data
                                    /*
                                     So we copy all of the escapefromtarkovdata folder to a new folder called escapefromtarkoe which is where the new game is now running
                                    */
                                    CopyFilesRecursively(StreamedData.Remove(0, 184).Remove(46, 18), StreamedData.Remove(0, 48).Remove(46, 154));
                                }
                            }
                            // C:/Battlestate Games/EFT/EscapeFromTarkoe.exe
                            if (!File.Exists(StreamedData.Remove(0, 139).Remove(45, 64)))
                            {
                                // C:/Battlestate Games/EFT/EscapeFromTarkov.exe                // C:/Battlestate Games/EFT/EscapeFromTarkoe.exe
                                File.Copy(StreamedData.Remove(0, 94).Remove(45, 109), StreamedData.Remove(0, 139).Remove(45, 64));
                            }
                            else
                            {
                                // C:/Battlestate Games/EFT/EscapeFromTarkoe.exe
                                File.Delete(StreamedData.Remove(0, 139).Remove(45, 64));
                                // C:/Battlestate Games/EFT/EscapeFromTarkoe.exe
                                if (!File.Exists(StreamedData.Remove(0, 139).Remove(45, 64)))
                                {
                                    // C:/Battlestate Games/EFT/EscapeFromTarkov.exe                // C:/Battlestate Games/EFT/EscapeFromTarkoe.exe
                                    File.Copy(StreamedData.Remove(0, 94).Remove(45, 109), StreamedData.Remove(0, 139).Remove(45, 64));
                                }

                            }
                            TimeSpan seconds = (DateTime.UtcNow - new DateTime(1970, 1, 1));
                            int secs = (int)seconds.TotalSeconds; // get time in seconds
                            File.WriteAllText(DocPath + "Auft.UI1", EncryptStatic(Username)); // write username
                            File.WriteAllText(DocPath + "Auft.UI2", EncryptStatic(Password)); // write password(yeah not a smart thing to do, but this is old and stupid)
                            File.WriteAllText(DocPath + "Auft.UI3", EncryptStatic(secs.ToString())); // write time
                            File.WriteAllText(DocPath + "Auft.UI4", EncryptStatic(Loader.API.Hwid.Generate())); // get hwid


                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("(1) Normal Build");
                            Console.WriteLine("(2) No Hook Build");
                            string line = Console.ReadLine();
                            if (line == "1" || line == "2")
                            {
                                /*
                                Lets explain what we are doing here. So at this point our bypass was already set up, its very easy to miss what we are doing
                                We are actually taking the game files and editting the belauncher.ini for our own belauncher.ini file which changes the name of the game therefore breaking integrity checks as the hardcoded path changes
                                */
                                if (line == "1")
                                {

                                    byte[] byters2 = await API.Auth.GetFile("971033a805e2340c74aec62832a1d964"); //bemod
                                    File.WriteAllBytes(StreamedData.Remove(48, 200), byters2); // C:/Battlestate Games/EFT/BattlEye/BELauncher.ini

                                    byte[] byters4 = await API.Auth.GetFile("848465b4264bd6c4d3e4a3ecf9e1c26b"); //Specialer.dll
                                    string StreamedData2 = Encoding.Default.GetString(byters4);

                                    byte[] byters5 = await API.Auth.GetFile("1b260390ea629104635519eb70fdf40d"); //Payload.dll
                                    File.WriteAllBytes(StreamedData2, byters5);
                                }

                                if (line == "2")
                                {

                                    byte[] byters2 = await API.Auth.GetFile("971033a805e2340c74aec62832a1d964"); //bemod
                                    File.WriteAllBytes(StreamedData.Remove(48, 200), byters2); // C:/Battlestate Games/EFT/BattlEye/BELauncher.ini

                                    byte[] byters4 = await API.Auth.GetFile("848465b4264bd6c4d3e4a3ecf9e1c26b"); //Specialer.dll
                                    string StreamedData2 = Encoding.Default.GetString(byters4);

                                    byte[] byters5 = await API.Auth.GetFile("5f54b4d85474287850911ac3fadc6d64"); //Payload2.dll
                                    File.WriteAllBytes(StreamedData2, byters5);
                                }
                            }
                            else
                            {
                                Environment.Exit(0);
                            }

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Waiting For Game");
                            bool big = false;
                            while (big == false)
                            {
                                foreach (Process p in Process.GetProcesses())
                                {
                                    if (p.MainWindowTitle == "EscapeFromTarkov")
                                    {
                                        byte[] byters6 = await API.Auth.GetFile("5966fe2890b628bcd39644cdd499605c"); //bemod
                                        File.WriteAllBytes(StreamedData.Remove(48, 200), byters6); // C:/Battlestate Games/EFT/BattlEye/BELauncher.ini
                                        big = true;
                                        // prevents people having the game open preventing our loader opening the game with the payload, you cant close the game in time to open it again in the loader

                                    }
                                }
                            }
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Cheat Loaded!");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Press Any Key To Close");
                            Console.ReadKey();
                            Environment.Exit(0);

                        }

                        else
                        {

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Unknown Error");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Press Any Key To Close");
                            Console.ReadKey();
                        }


                    }

                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Validate Issue 88");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Press Any Key To Close");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("DD Issue");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Press Any Key To Close");
                Console.ReadKey();
                Environment.Exit(0);

            }






        }

        private static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
        }
        private static bool Verify() // verify if the game directory is legit
        {
            string path = "C:/Battlestate Games/EFT"; // so eft exists

            if (File.Exists(path + "/EscapeFromTarkov.exe")) // tarkov exe exists
            {
                if (File.Exists(path + "/MonoBleedingEdge/EmbedRuntime/mono-2.0-bdwgc.dll")) // mono dll exists
                {
                    if (File.Exists(path + "/Battleye/BEClient_x64.dll")) // beclient exists
                    {
                        if (File.Exists(path + "/EscapeFromTarkov_Data/Managed/Assembly-CSharp.dll")) // assembly c sharp exists
                        {
                            return true;
                        }

                    }

                }

            }
            return false;
        }
        private static string EncryptStatic(string _clearText)
        {


            byte[] _clearBytes = Encoding.Unicode.GetBytes(_clearText);
            using (Aes _encryptor = Aes.Create())
            {

                Rfc2898DeriveBytes _pdb = new Rfc2898DeriveBytes("Gduidfgwuwsjbjkds123vbwuib5345wiuvbwdj322sf11212w", new byte[] { 0xde, 0xad, 0xbe, 0xef, 0xfa, 0x66, 0x07, 0x42, 0x06, 0x95, 0x64, 0x65, 0x76 });
                _encryptor.Key = _pdb.GetBytes(32);
                _encryptor.IV = _pdb.GetBytes(16);
                using (MemoryStream _ms = new MemoryStream())
                {
                    using (CryptoStream _cs = new CryptoStream(_ms, _encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        _cs.Write(_clearBytes, 0, _clearBytes.Length);
                        _cs.Close();
                    }
                    _clearText = Convert.ToBase64String(_ms.ToArray());
                }
            }
            return _clearText;
        }

    }
}
