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
    public class VyrobokController : _BaseController
    {
        public ActionResult GetRecords(int page = 1, string sort = "nazovVyrobku", string sortDir = "ASC")
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
           

            VyrobokRepository repository = new VyrobokRepository();
            Page<Vyrobok> strankaVyrobcov = repository.GetPage(page, _PagingModel.DefaultItemsPerPage, sort, sortDir);
            
            VyrobokPagingListModel model = VyrobokPagingListModel.CreateCopyFrom(strankaVyrobcov);

          

            return View(model);
        }

        
        public ActionResult InsertRecord()
        {
            return View("EditRecord", new VyrobokModel());
        }

       
        public ActionResult EditRecord(string id)
        {
            VyrobokModel model = VyrobokModel.CreateCopyFrom(new VyrobokRepository().Get(new Guid(id)));

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveRecord(VyrobokModel model)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            if (!new VyrobokRepository().Save(VyrobokModel.CreateCopyFrom(model)))
            {
                ModelState.AddModelError("", "Nastala chyba pri zápise záznamu do systému. Skúste akciu zopakovať a ak sa chyba vyskytne znovu, kontaktujte nás prosím.");
            }
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            return this.RedirectToEshopUmbracoPage(ConfigurationUtil.EshopZoznamVyrobkovFormId);
        }


     
        public ActionResult ConfirmDeleteRecord(string id)
        {
            VyrobokModel model = VyrobokModel.CreateCopyFrom(new VyrobokRepository().Get(new Guid(id)));

            return View(model);
        }
        [HttpPost]
       
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRecord(VyrobokModel model)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            VyrobokRepository repository = new VyrobokRepository();
            try
            {
                if (!repository.Delete(VyrobokModel.CreateCopyFrom(model)))
                {
                    ModelState.AddModelError("", "Nastala chyba pri zápise záznamu do systému. Skúste akciu zopakovať a ak sa chyba vyskytne znovu, kontaktujte nás prosím.");
                }
            }
            catch (Exception exc)
            {
                ModelState.AddModelError("", "Výrobok nie je možné odstrániť.");
                this.Logger.Error(typeof(VyrobokController), "DeleteRecord error", exc);
            }
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            return this.RedirectToEshopUmbracoPage(ConfigurationUtil.EshopZoznamVyrobkovFormId);
        }
    }
}
