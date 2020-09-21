using System;
using Microsoft.CSharp.Basics.Solid.Code.Raters;

namespace Microsoft.CSharp.Basics.Solid.Code
{
    public class RaterFactory
    {
        public Rater Create(Policy policy, IRatingContext context)
        {
            try
            {
                return (Rater)Activator.CreateInstance(
                    Type.GetType($"Microsoft.CSharp.Basics.Solid.Code.Raters.{policy.Type}PolicyRater"),
                        new object[] { new RatingUpdater(context.Engine) });
            }
            catch
            {
                return new UnknownPolicyRater(new RatingUpdater(context.Engine));
            }
        }
    }
}