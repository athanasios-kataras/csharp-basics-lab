using System.IO;
using Microsoft.CSharp.Basics.Solid;
using Microsoft.CSharp.Basics.Solid.Code;
using Microsoft.CSharp.Basics.Solid.Code.Raters;
using Microsoft.CSharp.Basics.Solid.UnitTests.Tests;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Microsoft.Workshop.Net.OOPFundamentals.Lab.UnitTests.Solid
{
    public class RatingEngineRate
    {
        private RatingEngine ratingEngine;
        private FakeLogger logger;
        private FakePolicySource policySource;
        public RatingEngineRate()
        {
            logger = new FakeLogger();
            policySource = new FakePolicySource();
            ratingEngine = new RatingEngine(logger, policySource, new JsonPolicySerializer(), new RaterFactory(logger));
        }
        [Test]
        public void ReturnsRatingOf10000For200000LandPolicy()
        {
            var policy = new Policy
            {
                Type = PolicyType.Land.ToString(),
                BondAmount = 200000,
                Valuation = 200000
            };
            string json = JsonConvert.SerializeObject(policy);
            policySource.PolicyString = json;

            Assert.AreEqual(10000, ratingEngine.Rate());
        }

        [Test]
        public void ReturnsRatingOf0For200000BondOn260000LandPolicy()
        {
            var policy = new Policy
            {
                Type = PolicyType.Land.ToString(),
                BondAmount = 200000,
                Valuation = 260000
            };
            string json = JsonConvert.SerializeObject(policy);
            policySource.PolicyString = json;

            Assert.AreEqual(0, ratingEngine.Rate());
        }

        [Test]
        public void LogsStartingLoadingAndCompleting()
        {
            var policy = new Policy
            {
                Type = PolicyType.Land.ToString(),
                BondAmount = 200000,
                Valuation = 260000
            };
            string json = JsonConvert.SerializeObject(policy);
            policySource.PolicyString = json;

            ratingEngine.Rate();

            Assert.Contains("Starting rate.", logger.LoggedMessages);
            Assert.Contains("Loading policy.", logger.LoggedMessages);
            Assert.Contains("Rating completed.", logger.LoggedMessages);
        }
    }
}