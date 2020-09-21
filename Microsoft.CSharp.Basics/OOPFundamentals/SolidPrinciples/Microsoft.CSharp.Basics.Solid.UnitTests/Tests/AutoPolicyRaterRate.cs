using System.Linq;
using Microsoft.CSharp.Basics.Solid.Code.Raters;
using NUnit.Framework;

namespace Microsoft.CSharp.Basics.Solid.UnitTests.Tests
{
   public class AutoPolicyRaterRate
    {
        [Test]
        public void LogsMakeRequiredMessageGivenPolicyWithoutMake()
        {
            var policy = new Policy() { Type = "Auto" };
            var logger = new FakeLogger();
            var rater = new AutoPolicyRater(null);
            rater.Logger = logger;

            rater.Rate(policy);

            Assert.AreEqual("Auto policy must specify Make", logger.LoggedMessages.Last());
        }

        [Test]
        public void SetsRatingTo1000ForBMWWith250Deductible()
        {
            var policy = new Policy()
            {
                Type = "Auto",
                Make = "BMW",
                Deductible = 250m
            };
            var ratingUpdater = new FakeRatingUpdater();
            var rater = new AutoPolicyRater(ratingUpdater);

            rater.Rate(policy);

            Assert.AreEqual(1000m, ratingUpdater.NewRating.Value);
        }

        [Test]
        public void SetsRatingTo900ForBMWWith500Deductible()
        {
            var policy = new Policy()
            {
                Type = "Auto",
                Make = "BMW",
                Deductible = 500m
            };
            var ratingUpdater = new FakeRatingUpdater();
            var rater = new AutoPolicyRater(ratingUpdater);

            rater.Rate(policy);

            Assert.AreEqual(900m, ratingUpdater.NewRating.Value);
        }
    }
}