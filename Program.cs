using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Service_Access_Verifier
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var types = GetInstalledServices();

            var stringArray = File.ReadAllLines(
                 Path.Combine(
                     Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                     "TypeList.txt"
                     )
                 );
               var fileStringDictionary = stringArray.ToString().Split(",").ToDictionary(x => x);
                

            if (args[0] == "/check")
            {
                // if reading from the file, then check each line and the keys.
                for (int i = 0; i < types.Keys.Count; i++)
                {
                    if (types.ContainsKey(@"1", [i]) != fileStringDictionary.ContainsKey(@"1", [i]))
                    {
                        Console.WriteLine("mismatch");
                    }
                }
            }
            if (args[0] == "/writeTextFile")
            {

            }

            if (args[0] == "/readTextFile")
            {

            }



            static Dictionary<string, string> GetInstalledServices()
            {
                Dictionary<string, string> types = new Dictionary<string, string>();

                string softwareRegLoc = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\";
                // Open Registry Key in RegistryHive.LocalMachine
                RegistryKey regKey = Registry.LocalMachine.OpenSubKey(softwareRegLoc);

                foreach (string subKeyName in regKey.GetSubKeyNames())
                {
                    // Open Registry Sub Key
                    RegistryKey subKey = regKey.OpenSubKey(subKeyName);

                    // Read Value from Registry Sub Key

                    string serviceKeyName = subKey.Name;
                    string serviceValueName = (string)subKey.GetValue("Type");

                    if (!string.IsNullOrEmpty(serviceValueName))
                    {
                        types.Add(serviceKeyName, serviceValueName);
                    }
                }
                return types;
            }
        }
    }
}
