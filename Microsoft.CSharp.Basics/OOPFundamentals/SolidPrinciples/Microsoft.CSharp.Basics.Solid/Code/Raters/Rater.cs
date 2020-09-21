using Microsoft.CSharp.Basics.Solid.Code.Interfaces;

namespace Microsoft.CSharp.Basics.Solid.Code.Raters
{
    public abstract class Rater
    {
        public ILogger Logger { get; set; }
        
        protected Rater(ILogger logger)
        {
            Logger = logger;
        }

        public abstract decimal Rate(Policy policy);
    }
}