using System;
using Microsoft.CSharp.Basics.Solid.Code;
using Microsoft.CSharp.Basics.Solid.Code.Interfaces;
using Microsoft.CSharp.Basics.Solid.Code.Raters;
using Microsoft.CSharp.Basics.Solid.Loggers;

namespace Microsoft.CSharp.Basics.Solid
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Insurance Rating System Starting...");
            ILogger logger = new ConsoleLogger();
            IPolicySource policySource = new FilePolicySource();
            IPolicySerializer policySerializer = new JsonPolicySerializer();
            RaterFactory factory = new RaterFactory(logger);
            var engine = new RatingEngine(logger, policySource, policySerializer, factory);
            var rate = engine.Rate();

            if (rate > 0)
            {
                Console.WriteLine($"Rating: {rate}");
            }
            else
            {
                Console.WriteLine("No rating produced.");
            }

        }
    }
}