using Microsoft.CSharp.Basics.Solid.Code;
using NUnit.Framework;

namespace Microsoft.CSharp.Basics.Solid.UnitTests.Tests
{
    public class JsonPolicySerializerGetPolicyFromJsonString
    {
        [Test]
        public void ReturnsDefaultPolicyFromEmptyJsonString()
        {
            var inputJson = "{}";
            var serializer = new JsonPolicySerializer();

            var result = serializer.GetPolicyFromJsonString(inputJson);

            var policy = new Policy();
            AssertPoliciesEqual(result, policy);
        }

        [Test]
        public void ReturnsSimpleAutoPolicyFromValidJsonString()
        {
            var inputJson = @"{
  ""type"": ""Auto"",
  ""make"": ""BMW""
}
";
            var serializer = new JsonPolicySerializer();

            var result = serializer.GetPolicyFromJsonString(inputJson);

            var policy = new Policy { Type = PolicyType.Auto, Make = "BMW" };
            AssertPoliciesEqual(result, policy);
        }

        private static void AssertPoliciesEqual(Policy result, Policy policy)
        {
            Assert.Equals(policy.Address, result.Address);
            Assert.Equals(policy.Amount, result.Amount);
            Assert.Equals(policy.BondAmount, result.BondAmount);
            Assert.Equals(policy.DateOfBirth, result.DateOfBirth);
            Assert.Equals(policy.Deductible, result.Deductible);
            Assert.Equals(policy.FullName, result.FullName);
            Assert.Equals(policy.IsSmoker, result.IsSmoker);
            Assert.Equals(policy.Make, result.Make);
            Assert.Equals(policy.Miles, result.Miles);
            Assert.Equals(policy.Model, result.Model);
            Assert.Equals(policy.Type, result.Type);
            Assert.Equals(policy.Valuation, result.Valuation);
            Assert.Equals(policy.Year, result.Year);
        }
    }
}