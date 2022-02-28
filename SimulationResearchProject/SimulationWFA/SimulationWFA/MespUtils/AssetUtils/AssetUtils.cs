using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProgramLibrary;

namespace SimulationWFA.MespUtils
{
    public static class AssetUtils
    {
        // supported extensions --> .mat , .texture

        public static void CreateAsset(object asset, string fileName)
        {
            var assetFileName = FindFileByType(fileName);

            if (assetFileName == null)
            {
                Console.WriteLine("Unsupported extenxtions type");
                return;
            }

            Directory.CreateDirectory(SimPath.GetAssetPath + "/" + assetFileName);
            string directoryPath = Path.Combine(SimPath.GetAssetPath +"/"+assetFileName, fileName);
            File.WriteAllText(directoryPath, JsonConvert.SerializeObject(asset, Formatting.Indented));
        }

        public static string FindFileByType(string fileName)
        {
            string assetTypeName = fileName.Substring(fileName.IndexOf("."));

            switch (assetTypeName)
            {
                case ".mat": return "Material";
                case ".texture": return "Texture";
            }

            return null;
        }
    }
}
