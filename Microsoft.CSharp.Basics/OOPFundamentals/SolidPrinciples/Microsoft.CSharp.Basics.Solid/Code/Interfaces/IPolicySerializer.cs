namespace Microsoft.CSharp.Basics.Solid.Code.Interfaces
{
    public interface IPolicySerializer
    {
         Policy GetPolicyFromString(string policyString);
    }
}