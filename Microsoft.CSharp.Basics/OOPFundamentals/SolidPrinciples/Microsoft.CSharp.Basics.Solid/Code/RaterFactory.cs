using System;
using Microsoft.CSharp.Basics.Solid.Code.Interfaces;
using Microsoft.CSharp.Basics.Solid.Code.Raters;

namespace Microsoft.CSharp.Basics.Solid.Code
{
    public class RaterFactory
    {
        private readonly ILogger logger;

        public RaterFactory(ILogger logger)
        {
            this.logger = logger;
        }
        public Rater Create(Policy policy)
        {
            try
            {
                return (Rater)Activator.CreateInstance(
                    Type.GetType($"Microsoft.CSharp.Basics.Solid.Code.Raters.{policy.Type}PolicyRater"), 
                        new object[] {logger}
                    );
            }
            catch
            {
                return new UnknownPolicyRater(logger);
            }
        }
    }
}