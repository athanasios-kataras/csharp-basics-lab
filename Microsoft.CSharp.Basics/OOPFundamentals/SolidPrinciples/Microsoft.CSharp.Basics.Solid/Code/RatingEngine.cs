using System;
using System.IO;
using Microsoft.CSharp.Basics.Solid.Code;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Microsoft.CSharp.Basics.Solid
{
    /// <summary>
    /// The RatingEngine reads the policy application details from a file and produces a numeric 
    /// rating value based on the details.
    /// </summary>
    
    public class RatingEngine
    {
        public decimal Rating { get; set; }
        private readonly FileLoader fileLoader;
        private readonly Logger logger;

        private readonly Deserializer deserializer;

        public RatingEngine(FileLoader fileLoader, Logger logger)
        {
            this.fileLoader = fileLoader;
            this.logger = logger;
            this.deserializer =  new Deserializer();
        }

        public void Rate()
        {
            
            this.logger.LogToConsole("Starting rate.");
            this.logger.LogToConsole("Loading policy.");

            string policyJson = this.fileLoader.LoadFromFile("policy.json");

            var policy = this.deserializer.Deserialize<Policy>(policyJson);

            switch (policy.Type)
            {
                case PolicyType.Auto:
                    this.logger.LogToConsole("Rating AUTO policy...");
                    this.logger.LogToConsole("Validating policy.");
                    if (String.IsNullOrEmpty(policy.Make))
                    {
                        this.logger.LogToConsole("Auto policy must specify Make");
                        return;
                    }
                    if (policy.Make == "BMW")
                    {
                        if (policy.Deductible < 500)
                        {
                            Rating = 1000m;
                        }
                        Rating = 900m;
                    }
                    break;

                case PolicyType.Land:
                    this.logger.LogToConsole("Rating LAND policy...");
                    this.logger.LogToConsole("Validating policy.");
                    if (policy.BondAmount == 0 || policy.Valuation == 0)
                    {
                        this.logger.LogToConsole("Land policy must specify Bond Amount and Valuation.");
                        return;
                    }
                    if (policy.BondAmount < 0.8m * policy.Valuation)
                    {
                        this.logger.LogToConsole("Insufficient bond amount.");
                        return;
                    }
                    Rating = policy.BondAmount * 0.05m;
                    break;

                case PolicyType.Life:
                    this.logger.LogToConsole("Rating LIFE policy...");
                    this.logger.LogToConsole("Validating policy.");
                    if (policy.DateOfBirth == DateTime.MinValue)
                    {
                        this.logger.LogToConsole("Life policy must include Date of Birth.");
                        return;
                    }
                    if (policy.DateOfBirth < DateTime.Today.AddYears(-100))
                    {
                        this.logger.LogToConsole("Centenarians are not eligible for coverage.");
                        return;
                    }
                    if (policy.Amount == 0)
                    {
                        this.logger.LogToConsole("Life policy must include an Amount.");
                        return;
                    }
                    int age = DateTime.Today.Year - policy.DateOfBirth.Year;
                    if (policy.DateOfBirth.Month == DateTime.Today.Month &&
                        DateTime.Today.Day < policy.DateOfBirth.Day ||
                        DateTime.Today.Month < policy.DateOfBirth.Month)
                    {
                        age--;
                    }
                    decimal baseRate = policy.Amount * age / 200;
                    if (policy.IsSmoker)
                    {
                        Rating = baseRate * 2;
                        break;
                    }
                    Rating = baseRate;
                    break;

                default:
                    this.logger.LogToConsole("Unknown policy type");
                    break;
            }

            this.logger.LogToConsole("Rating completed.");
        }
    }
}