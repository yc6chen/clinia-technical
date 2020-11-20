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
            return JsonConvert.DeserializeObject<IEnumerable<T>>(file);
        }
    }
}