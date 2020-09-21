using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Microsoft.CSharp.Basics.Solid.Code
{
    public class JsonPolicySerializer
    {
        public Policy GetPolicyFromJsonString(string jsonString)
        {
            return JsonConvert.DeserializeObject<Policy>(jsonString,
                new StringEnumConverter());
        }
    }
}