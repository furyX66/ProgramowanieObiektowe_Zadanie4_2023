using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;

namespace app.Services
{
    public static class SerializeService
    {
        #region Public methods
        public static List<T> DeserializeFromFile<T>(string filePath) //Deserializes json file
        {
            string jsonContent = File.ReadAllText(filePath);
            List<T> itemList = JsonConvert.DeserializeObject<List<T>>(jsonContent);
            return itemList;
        }
        public static void SerializeToFile<T>(string fileName, T data)  //Serializes to json file
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(fileName, json);
        }

        #endregion
    }
}

