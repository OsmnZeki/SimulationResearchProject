using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLibrary
{
    public static class SimPath
    {
        public static string currentDirectory = Directory.GetCurrentDirectory();
        private static string AssetPath = null;

        public static string GetAssetPath { 
            get {
                if(AssetPath == null)
                {
                    AssetPath = FindAssetPath();
                }
                return AssetPath;
            } }

        public static string FindAssetPath()
        {
            DirectoryInfo d = new DirectoryInfo(currentDirectory);
            int controlCounter = 0;

            while (true && controlCounter < 10)
            {
                foreach (var directory in d.Parent.GetDirectories())
                {
                    if (directory.Name == "Assets")
                    {
                        return directory.FullName;
                    }
                }
                d = d.Parent;
                controlCounter++;
            }
            

            Console.WriteLine("Asset pahti bulunamadı!");
            return null;
        }
    }
}
