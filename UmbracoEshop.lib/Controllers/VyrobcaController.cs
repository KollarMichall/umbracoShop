using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using UmbracoEshop.lib.Models;
using UmbracoEshop.lib.Repositories;
using UmbracoEshop.lib.Util;

namespace UmbracoEshop.lib.Controllers
{
   
    [PluginController("UmbracoEshop")]
    [Authorize(Roles = "EshopAdmin")]
    public class VyrobcaController : _BaseController
    {
        public ActionResult GetRecords(int page = 1, string sort = "nazovVyrobcu", string sortDir = "ASC")
        {
            try
            {
                return GetRecordsView(page, sort, sortDir);
            }
            catch
            {
               
                return GetRecordsView(page, sort, sortDir);
            }
        }
        ActionResult GetRecordsView(int page, string sort, string sortDir)
        {
           

            VyrobcaRepository repository = new VyrobcaRepository();
            Page<Vyrobca> strankaVyrobcov = repository.GetPage(page, _PagingModel.DefaultItemsPerPage, sort, sortDir);
            
            VyrobcaPagingListModel model = VyrobcaPagingListModel.CreateCopyFrom(strankaVyrobcov);

          

            return View(model);
        }

        
        public ActionResult InsertRecord()
        {
            return View("EditRecord", new VyrobcaModel());
        }

       
        public ActionResult EditRecord(string id)
        {
            VyrobcaModel model = VyrobcaModel.CreateCopyFrom(new VyrobcaRepository().Get(new Guid(id)));

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveRecord(VyrobcaModel model)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            if (!new VyrobcaRepository().Save(VyrobcaModel.CreateCopyFrom(model)))
            {
                ModelState.AddModelError("", "Nastala chyba pri zápise záznamu do systému. Skúste akciu zopakovať a ak sa chyba vyskytne znovu, kontaktujte nás prosím.");
            }
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            return this.RedirectToEshopUmbracoPage(ConfigurationUtil.EshopZoznamVyrobcovFormId);
        }


     
        public ActionResult ConfirmDeleteRecord(string id)
        {
            VyrobcaModel model = VyrobcaModel.CreateCopyFrom(new VyrobcaRepository().Get(new Guid(id)));

            return View(model);
        }
        [HttpPost]
       
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRecord(VyrobcaModel model)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            VyrobcaRepository repository = new VyrobcaRepository();
            try
            {
                if (!repository.Delete(VyrobcaModel.CreateCopyFrom(model)))
                {
                    ModelState.AddModelError("", "Nastala chyba pri zápise záznamu do systému. Skúste akciu zopakovať a ak sa chyba vyskytne znovu, kontaktujte nás prosím.");
                }
            }
            catch (Exception exc)
            {
                ModelState.AddModelError("", "Výrobcu nie je možné odstrániť pretože je priradený k niektorým produktom.");
                this.Logger.Error(typeof(VyrobcaController), "DeleteRecord error", exc);
            }
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            return this.RedirectToEshopUmbracoPage(ConfigurationUtil.EshopZoznamVyrobcovFormId);
        }
    }
}
