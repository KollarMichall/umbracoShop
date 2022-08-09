using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Web.Mvc;
using UmbracoEshop.lib.Models;
using UmbracoEshop.lib.Repositories;

namespace UmbracoEshop.lib.Controllers
{
    [PluginController("UmbracoEshop")]
    public class QuoteApiController : _BaseApiController
    {
        public string AddProductToQuote(string id)
        {
            try
            {
                string[] items = id.Split('|');
                Guid pkProduct = new Guid(items[0]);
                string sessionId = items[1];

                QuoteRepository quoteRepository = new QuoteRepository();
                Quote quote = quoteRepository.GetForSession(sessionId);

            VyrobokRepository rep = new VyrobokRepository();
            Vyrobok vyrobok = rep.Get(pkProduct);

                Product2QuoteRepository productRep = new Product2QuoteRepository();
                Product2Quote quoteItem = productRep.Get(quote.pk, vyrobok.pk);
                if (quoteItem == null)
                {
                    quoteItem = new Product2Quote();
                quoteItem.PkQuote = quote.pk;
                quoteItem.PkProduct = pkProduct;
                quoteItem.ItemCode = vyrobok.KodVyrobku;
                quoteItem.ItemName = vyrobok.NazovVyrobku;
                quoteItem.UnitPriceNoVat = vyrobok.CenaVyrobku;
                quoteItem.UnitPriceWithVat = vyrobok.CenaVyrobku;
                quoteItem.ItemPcs = 1;
                quoteItem.UnitName = "ks";
                } else
                {
                    quoteItem.ItemPcs += 1;
                }

                productRep.Save(quoteItem);

            return string.Format("Pridane do kosika: {0}, {1}", vyrobok.KodVyrobku, vyrobok.NazovVyrobku);

            }
            catch(Exception e)
            {
                return string.Format("Vznikla chyba pri pridavani productu '{0}' do kosika. {1}", id, e.ToString());
            }
        }
        public Api_Basket BasketInfo(string id)
        {
            Api_Basket ret = new Api_Basket();

            BasketModel model = new BasketModel(id);
            ret.Pocet = model.PocetPoloziek();
            ret.Cena = model.CenaCelkom();

            return ret;
        }
    }
    public class Api_Basket
    {
        public string Pocet { get; set; }
        public string Cena { get; set; }
    }
}
