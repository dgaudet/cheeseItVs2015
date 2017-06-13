using System;

namespace cheeseItVS2015.Models
{
    public static class CheeseConstants
    {
        public static readonly Decimal MAX_PRICE = 20M;
        public static readonly Decimal STANARD_PRICE_DECREASE_RATE = -0.05M;
        public static readonly Decimal FRESH_PRICE_DECREASE_RATE = -0.1M;
        public static readonly Decimal AGED_PRICE_INCREASE_RATE = 0.05M;
        public static readonly Decimal SPECIAL_PRICE_INCREASE_RATE = 0.05M;
    }

    public enum CheeseType
    {
        Fresh,
        Unique,
        Special,
        Aged,
        Standard
    }

    public class Cheese
    {
        public string Name { get; set; }
        public DateTime DateRecieved { get; set; }
        public DateTime? BestBeforeDate { get; set; }
        public int? DaysToSell { get; set; }
        public Decimal? Price { get; set; }
        public CheeseType Type { get; set; }

        public Decimal? PriceForDay(DateTime day)
        {
            if (Price == null || DateRecieved == DateTime.MinValue)
            {
                return null;
            }
            var daysOld = Convert.ToDecimal((day.Date - DateRecieved.Date).TotalDays);

            if (Type != CheeseType.Unique && daysOld > DaysToSell)
            {
                return null;
            }

            var changeRate = ChangeRate(day, daysOld);
            var calculatedPrice = Price + Price * changeRate * daysOld;
            if (calculatedPrice > CheeseConstants.MAX_PRICE)
            {
                return CheeseConstants.MAX_PRICE;
            }
            if (calculatedPrice < 0)
            {
                return 0M;
            }
            return calculatedPrice;
        }

        private Decimal ChangeRate(DateTime day, Decimal daysOld)
        {
            var changeRate = CheeseConstants.STANARD_PRICE_DECREASE_RATE;
            var bestBeforeChangeRate = 2;
            switch (Type)
            {
                case (CheeseType.Aged):
                    changeRate = CheeseConstants.AGED_PRICE_INCREASE_RATE;
                    bestBeforeChangeRate = 1;
                    break;
                case (CheeseType.Unique):
                    changeRate = 0;
                    break;
                case (CheeseType.Fresh):
                    changeRate = CheeseConstants.FRESH_PRICE_DECREASE_RATE;
                    break;
                case (CheeseType.Special):
                    changeRate = CheeseConstants.SPECIAL_PRICE_INCREASE_RATE;
                    if (DaysToSell - daysOld < 6)
                    {
                        changeRate = CheeseConstants.SPECIAL_PRICE_INCREASE_RATE * 2;
                    }
                    break;
                default:
                    changeRate = CheeseConstants.STANARD_PRICE_DECREASE_RATE;
                    break;
            }
            if (BestBeforeDate != null && Type != CheeseType.Special)
            {
                DateTime nonNullBestBefore = BestBeforeDate ?? DateTime.Now;
                if (nonNullBestBefore.Date < day.Date)
                {
                    changeRate = changeRate * bestBeforeChangeRate;
                }
            }
            return changeRate;
        }
    }
}