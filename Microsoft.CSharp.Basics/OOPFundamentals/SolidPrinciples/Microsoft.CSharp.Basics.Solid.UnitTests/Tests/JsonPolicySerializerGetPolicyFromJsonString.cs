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
            Assert.AreEqual(policy.Address, result.Address);
            Assert.AreEqual(policy.Amount, result.Amount);
            Assert.AreEqual(policy.BondAmount, result.BondAmount);
            Assert.AreEqual(policy.DateOfBirth, result.DateOfBirth);
            Assert.AreEqual(policy.Deductible, result.Deductible);
            Assert.AreEqual(policy.FullName, result.FullName);
            Assert.AreEqual(policy.IsSmoker, result.IsSmoker);
            Assert.AreEqual(policy.Make, result.Make);
            Assert.AreEqual(policy.Miles, result.Miles);
            Assert.AreEqual(policy.Model, result.Model);
            Assert.AreEqual(policy.Type, result.Type);
            Assert.AreEqual(policy.Valuation, result.Valuation);
            Assert.AreEqual(policy.Year, result.Year);
        }
    }
}