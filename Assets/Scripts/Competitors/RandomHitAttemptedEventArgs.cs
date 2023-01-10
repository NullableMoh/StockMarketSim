using System;

namespace RvveSplit.Competitors
{
    public class RandomHitAttemptedEventArgs : EventArgs
    {
        public bool HitSuccessful { get; private set; }
        public string CompetitorStockName { get; private set; }

        public RandomHitAttemptedEventArgs(bool hitSuccessful, string competitorStockName)
        {
            HitSuccessful = hitSuccessful;
            CompetitorStockName = competitorStockName;
        }
    }
}
