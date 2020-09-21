using Microsoft.CSharp.Basics.Solid.Code.Raters;

namespace Microsoft.CSharp.Basics.Solid.UnitTests.Tests
{
    public class FakeRatingUpdater : IRatingUpdater
    {
        public decimal? NewRating { get; private set; }
        public void UpdateRating(decimal rating)
        {
            NewRating = rating;
        }
    }
}