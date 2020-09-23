using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Microsoft.CSharp.Basics.Solid.Code
{
    public class Deserializer
    {
        public T Deserialize<T>(string  strObject)
        {
            return JsonConvert.DeserializeObject<T>(strObject,
                new StringEnumConverter());
        }
    }
}