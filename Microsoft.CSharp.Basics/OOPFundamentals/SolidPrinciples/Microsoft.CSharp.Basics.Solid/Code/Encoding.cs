namespace Microsoft.CSharp.Basics.Solid
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class Encoding
    {
        public Solid.Policy ReadJSON(string data)
        {
            return JsonConvert.DeserializeObject<Policy>(data, new StringEnumConverter());
        }
    }
}