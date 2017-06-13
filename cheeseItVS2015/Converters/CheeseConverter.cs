using System;
using cheeseItVS2015.Models;

namespace cheeseItVS2015.Converters
{
    public class CheeseConverter
    {
        public Cheese CheeseFromItem(Item item, DateTime dateRecieved)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
            {
                return null;
            }
            else
            {
                int? daysToSell = null;
                if (!string.IsNullOrWhiteSpace(item.DaysToSell))
                {
                    int daysToSellNotNull;
                    if (int.TryParse(item.DaysToSell, out daysToSellNotNull))
                    {
                        daysToSell = daysToSellNotNull;
                    }
                }

                decimal? price = null;
                if (!string.IsNullOrWhiteSpace(item.Price))
                {
                    decimal priceNotNull;
                    if (decimal.TryParse(item.Price, out priceNotNull))
                    {
                        price = priceNotNull;
                    }
                }

                DateTime? bestBeforeDate = null;
                if (!string.IsNullOrWhiteSpace(item.BestBeforeDate))
                {
                    DateTime bestBeforeNotNull;
                    if (DateTime.TryParse(item.BestBeforeDate, out bestBeforeNotNull))
                    {
                        bestBeforeDate = bestBeforeNotNull;
                    }
                }

                var type = CheeseType.Standard;
                if (!string.IsNullOrWhiteSpace(item.Type))
                {
                    CheeseType parsedType;
                    if (Enum.TryParse(item.Type, out parsedType))
                    {
                        type = parsedType;
                    }
                }

                return new Cheese
                {
                    Name = item.Name,
                    BestBeforeDate = bestBeforeDate,
                    DaysToSell = daysToSell,
                    Price = price,
                    Type = type,
                    DateRecieved = dateRecieved
                };
            }
        }
    }
}