using Microsoft.CSharp.Basics.Solid.Code;
using Microsoft.CSharp.Basics.Solid.Code.Interfaces;
using Microsoft.CSharp.Basics.Solid.Code.Raters;

namespace Microsoft.CSharp.Basics.Solid
{
    /// <summary>
    /// The RatingEngine reads the policy application details from a file and produces a numeric 
    /// rating value based on the details.
    /// </summary>
    public class RatingEngine
    {
        private readonly ILogger logger;
        private readonly IPolicySource policySource;
        private readonly IPolicySerializer policySerializer;
        private readonly RaterFactory raterFactory;

        public RatingEngine(ILogger logger, IPolicySource policySource, IPolicySerializer policySerializer, RaterFactory raterFactory)
        {
            this.logger = logger;
            this.policySource = policySource;
            this.policySerializer = policySerializer;
            this.raterFactory = raterFactory;
        }

        public decimal Rate()
        {
            logger.Log("Starting rate.");

            logger.Log("Loading policy.");

            string policyString = policySource.GetPolicyFromSource();

            var policy =  policySerializer.GetPolicyFromString(policyString);

            var rater = raterFactory.Create(policy);

            var rate = rater.Rate(policy);

            logger.Log("Rating completed.");

            return rate;
        }
    }
}