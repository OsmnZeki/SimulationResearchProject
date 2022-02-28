using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProgramLibrary;
using RenderLibrary.Graphics.Rendering;

namespace SimulationWFA.MespUtils
{
    public static class AssetUtils
    {
        // supported extensions --> .mat , .texture, .mesh

        public static void CreateAsset(object asset, string fileName)
        {
            IAssetSerialization myTest = asset as IAssetSerialization;

            var assetFileName = FindFileByType(fileName);

            if (assetFileName == null)
            {
                Console.WriteLine("Unsupported extenxtions type");
                return;
            }

            Directory.CreateDirectory(SimPath.GetAssetPath + "/" + assetFileName);
            string directoryPath = Path.Combine(SimPath.GetAssetPath +"/"+assetFileName, fileName);
            File.WriteAllText(directoryPath, JsonConvert.SerializeObject(myTest.Serialization(), Formatting.Indented));
        }

        public static T LoadFromAsset<T>(string fileName) where T : IAssetSerialization, new()
        {
            T data = new T();

            var assetFileName = FindFileByType(fileName);

            if (assetFileName == null)
            {
                Console.WriteLine("Unsupported extenxtions type");
              //  return null;
            }

            string directoryPath = Path.Combine(SimPath.GetAssetPath + "/" + assetFileName, fileName);
            if (!File.Exists(directoryPath)) Console.WriteLine("There is no that named file!");
            var dataString = File.ReadAllText(directoryPath);

            var type = GetAssetSerializationType(data);
            return (T)data.Deserialization(JsonConvert.DeserializeObject(dataString,type));
        }

        public static Type GetAssetSerializationType(object t)
        {
            switch (t)
            {
                case UnlitMaterial unlitMaterial: return typeof(UnlitMaterialSerializationData);
            }
            return null;
        }

        public static string FindFileByType(string fileName)
        {
            string assetTypeName = fileName.Substring(fileName.IndexOf("."));

            switch (assetTypeName)
            {
                case ".mat": return "Materials";
                case ".texture": return "Textures";
                case ".mesh": return "Meshes";
            }

            return null;
        }

        public static string GetAssetPathByType(string fileName)
        {
            var assetFileName = FindFileByType(fileName);
            string directoryPath = Path.Combine(assetFileName, fileName);
            return directoryPath;
        }
    }
}
