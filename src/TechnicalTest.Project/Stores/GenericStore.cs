using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TechnicalTest.Project.Stores
{
    public class GenericStore
    {
        public IEnumerable<T> ReadAll<T>()
        {
            var file = File.ReadAllText($"{Directory.GetCurrentDirectory()}/Stores/Data/modalities.json");
            var test = JsonConvert.DeserializeObject<IEnumerable<T>>(file);
            
            return test;
        }
    }
}