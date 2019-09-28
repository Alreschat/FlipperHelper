using System;
using System.Collections.Generic;
using System.Linq;

namespace Flipper_Extended
{
    struct IndividualListing
    {
        public double item, price, pricePerUnit;
        public int stock;
        public IndividualListing(Result i, bool selling)
        {
            item = i.listing.price.item.amount;
            price = i.listing.price.exchange.amount;
            stock = i.listing.price.item.stock;
            pricePerUnit = selling ? item / price : price / item;
        }
    }
    class SubCurrency
    {
        public double alpha = .06f, delta = .2f;
        public double mean, max, min;
        public List<IndividualListing> listings;
        public SubCurrency(List<Result> res, bool selling = false)
        {
            listings = res.Select((element) => new IndividualListing(element, selling)).ToList();
            filterOne();
            double total = 0;
            max = 0;
            min = Double.MaxValue;
            foreach (IndividualListing k in listings)
            {
                total += k.pricePerUnit;
                max = Math.Max(max, k.pricePerUnit);
                min = Math.Min(min, k.pricePerUnit);
            }
            mean = (total / listings.Count);
            filterTwo();
        }
        void filterOne()
        {
            listings.Reverse();
            listings = listings.TakeWhile((element, index) => index == 0 || Math.Abs(listings[index - 1].pricePerUnit - element.pricePerUnit) / Math.Min(listings[index - 1].pricePerUnit, element.pricePerUnit) < alpha).ToList();
            listings.Reverse();


            // I mean not counting the reverses i managed to lower all these to one line. On the other hand that is one big line up there.
            /*int cutOff = -1;
            double prev = listings[listings.Count - 1].pricePerUnit;
            for (int i = listings.Count - 1; i >= 0; i--)
            {
                double tmpVal = listings[i].pricePerUnit;
                if (Math.Abs(tmpVal- prev) / Math.Min(tmpVal, prev) > alpha)
                {
                    cutOff = i;
                    break;
                }
                prev = listings[i].pricePerUnit;
            }
            if(cutOff >= 0)
            {
                listings = listings.Take(cutOff + 1).ToList();
            }*/
        }
        void filterTwo()
        {
            foreach (IndividualListing i in listings)
            {
                if (Math.Abs(i.pricePerUnit - mean) / Math.Min(i.pricePerUnit, mean) > delta)
                    listings.Remove(i);
                else
                    return;
            }
        }
    }

    class CurrencyInfo
    {
        public SubCurrency chaosBuy, chaosSell, exaltBuy, exaltSell;
        public CurrencyInfo()
        {
            initLists();
        }
        public CurrencyInfo(List<Result> chaosB, List<Result> chaosS, List<Result> exaltB, List<Result> exaltS)
        {
            chaosBuy = new SubCurrency(chaosB);
            chaosSell = new SubCurrency(chaosS, true);
            exaltBuy = new SubCurrency(exaltB);
            exaltSell = new SubCurrency(exaltS, true);
        }
        private void initLists()
        {
            chaosBuy.listings = new List<IndividualListing>();
            chaosSell.listings = new List<IndividualListing>();
            exaltBuy.listings = new List<IndividualListing>();
            exaltSell.listings = new List<IndividualListing>();
        }
    }
}
