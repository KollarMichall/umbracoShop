using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmbracoEshop.lib.Repositories;
using UmbracoEshop.lib.Util;

namespace UmbracoEshop.lib.Models
{
    public class BasketModel
    {
        public QuoteModel Quote { get; set; }
        public List<Product2QuoteModel> QuoteItems { get; set; }

        public BasketModel()
        {

        }
        public BasketModel(string sessionId)
        {
            QuoteRepository quoteRep = new QuoteRepository();
            Quote quote = quoteRep.GetForSession(sessionId);
            this.Quote = QuoteModel.CreateCopyFrom(quote);

            this.QuoteItems = new List<Product2QuoteModel>();
            Product2QuoteRepository p2qRep = new Product2QuoteRepository();
            foreach(Product2Quote item in p2qRep.GetQuoteItems(quote.pk))
            {
                this.QuoteItems.Add(Product2QuoteModel.CreateCopyFrom(item));
            }

        }

        public string PocetPoloziek()
        {
            decimal numTotal = 0;
            foreach(Product2QuoteModel item in this.QuoteItems)
            {
                decimal num = PriceUtil.NumberFromEditorString(item.ItemPcs);
                numTotal += num;
            }
            return PriceUtil.NumberToEditorString(numTotal);
        }
        public string CenaCelkom()
        {
            return PriceUtil.GetPriceStringWithCurrency(this.Quote.QuotePriceWithVat);
        }
    }
}
