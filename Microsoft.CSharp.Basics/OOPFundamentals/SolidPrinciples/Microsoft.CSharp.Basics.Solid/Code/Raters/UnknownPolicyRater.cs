using Microsoft.CSharp.Basics.Solid.Code.Interfaces;

namespace Microsoft.CSharp.Basics.Solid.Code.Raters
{
    public class UnknownPolicyRater : Rater
    {
        public UnknownPolicyRater(ILogger logger)
            : base(logger)
        {
        }

        public override decimal Rate(Policy policy)
        {
            Logger.Log("Unknown policy type");
            return 0m;
        }
    }
}