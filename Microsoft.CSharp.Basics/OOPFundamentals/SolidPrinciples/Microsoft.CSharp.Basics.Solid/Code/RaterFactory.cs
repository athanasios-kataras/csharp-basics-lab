using System;
using Microsoft.CSharp.Basics.Solid.Code.Raters;

namespace Microsoft.CSharp.Basics.Solid.Code
{
    public class RaterFactory
    {
        public Rater Create(Policy policy, RatingEngine engine)
        {
            try
            {
                return (Rater)Activator.CreateInstance(
                    Type.GetType($"Microsoft.CSharp.Basics.Solid.Code.Raters.{policy.Type}PolicyRater"),
                        new object[] { engine, engine.Logger });
            }
            catch
            {
                return null;
            }
        }
    }
}