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

namespace UmbracoEshop.lib.Controllers
{
    [PluginController("UmbracoEshop")]
    public class PublicProductController : _BaseController
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
            model.SessionId = this.CurrentSessionId;


        return View(model);
    }
    }

}
