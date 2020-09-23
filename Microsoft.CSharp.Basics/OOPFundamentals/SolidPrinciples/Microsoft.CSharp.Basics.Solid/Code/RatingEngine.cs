using System;
using System.IO;


namespace Microsoft.CSharp.Basics.Solid
{
    /// <summary>
    /// The RatingEngine reads the policy application details from a file and produces a numeric 
    /// rating value based on the details.
    /// </summary>
    public class RatingEngine
    {
        public decimal Rating { get; set; }
        public void Rate()
        {
            Logging.WriteLog("Starting rate.");

            Logging.WriteLog("Loading policy.");

            // load policy - open file policy.json
            Repository repository = new Repository();
            string policyJson = repository.GetPolicy();

            Encoding encoding = new Encoding();
            var policy = encoding.ReadJSON(policyJson);

            switch (policy.Type)
            {
                case PolicyType.Auto:
                    Logging.WriteLog("Rating AUTO policy...");
                    Logging.WriteLog("Validating policy.");
                    if (String.IsNullOrEmpty(policy.Make))
                    {
                        Logging.WriteLog("Auto policy must specify Make");
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
                    Logging.WriteLog("Rating LAND policy...");
                    Logging.WriteLog("Validating policy.");
                    if (policy.BondAmount == 0 || policy.Valuation == 0)
                    {
                        Logging.WriteLog("Land policy must specify Bond Amount and Valuation.");
                        return;
                    }
                    if (policy.BondAmount < 0.8m * policy.Valuation)
                    {
                        Logging.WriteLog("Insufficient bond amount.");
                        return;
                    }
                    Rating = policy.BondAmount * 0.05m;
                    break;

                case PolicyType.Life:
                    Logging.WriteLog("Rating LIFE policy...");
                    Logging.WriteLog("Validating policy.");
                    if (policy.DateOfBirth == DateTime.MinValue)
                    {
                        Logging.WriteLog("Life policy must include Date of Birth.");
                        return;
                    }
                    if (policy.DateOfBirth < DateTime.Today.AddYears(-100))
                    {
                        Logging.WriteLog("Centenarians are not eligible for coverage.");
                        return;
                    }
                    if (policy.Amount == 0)
                    {
                        Logging.WriteLog("Life policy must include an Amount.");
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
                    Logging.WriteLog("Unknown policy type");
                    break;
            }

            Logging.WriteLog("Rating completed.");
        }
    }
}