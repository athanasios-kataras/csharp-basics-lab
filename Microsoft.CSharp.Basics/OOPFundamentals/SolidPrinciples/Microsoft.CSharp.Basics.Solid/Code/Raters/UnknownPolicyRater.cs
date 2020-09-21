namespace Microsoft.CSharp.Basics.Solid.Code.Raters
{
    public class UnknownPolicyRater : Rater
    {
        public UnknownPolicyRater(IRatingUpdater ratingUpdater)
            : base(ratingUpdater)
        {
        }

        public override void Rate(Policy policy)
        {
            Logger.Log("Unknown policy type");
        }
    }
}