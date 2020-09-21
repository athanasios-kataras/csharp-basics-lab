using Microsoft.CSharp.Basics.Solid.Code.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Microsoft.CSharp.Basics.Solid.Code
{
    public class JsonPolicySerializer: IPolicySerializer
    {
        public Policy GetPolicyFromString(string jsonString)
        {
            return JsonConvert.DeserializeObject<Policy>(jsonString,
                new StringEnumConverter());
        }
    }
}