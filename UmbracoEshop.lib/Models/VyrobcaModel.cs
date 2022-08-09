using NPoco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UmbracoEshop.lib.Repositories;

namespace UmbracoEshop.lib.Models
{
    public class VyrobcaModel : _BaseModel
    {
        [Required(ErrorMessage = "Názov výrobcu musí byť zadaný")]
        [Display(Name = "Názov výrobcu")]
        public string NazovVyrobcu { get; set; }

        public void CopyDataFrom(Vyrobca src)
        {
            this.pk = src.pk;
            this.NazovVyrobcu = src.NazovVyrobcu;
           
        }

        public void CopyDataTo(Vyrobca trg)
        {
            trg.pk = this.pk;
            trg.NazovVyrobcu = this.NazovVyrobcu;
           
        }

        public static VyrobcaModel CreateCopyFrom(Vyrobca src)
        {
            VyrobcaModel trg = new VyrobcaModel();
            trg.CopyDataFrom(src);

            return trg;
        }

        public static Vyrobca CreateCopyFrom(VyrobcaModel src)
        {
            Vyrobca trg = new Vyrobca();
            src.CopyDataTo(trg);

            return trg;
        }
    }

    //public class VyrobcaListModel : List<VyrobcaModel>
    //{
    //    public HttpRequest CurrentRequest { get; private set; }
    //    public string SessionId { get; set; }
    //    public int PageSize { get; private set; }

    //    private GridPagerModel currentPager;
    //    public GridPagerModel Pager
    //    {
    //        get
    //        {
    //            return GetPager();
    //        }
    //    }

    //    public VyrobcaListModel(HttpRequest request, int pageSize = 25)
    //    {
    //        this.CurrentRequest = request;
    //        this.PageSize = pageSize;
    //    }

    //    public List<VyrobcaModel> GetPageItems()
    //    {
    //        GridPageInfo cpi = this.Pager.GetCurrentPageInfo();

    //        List<VyrobcaModel> resultList = new List<VyrobcaModel>();
    //        for (int i = cpi.FirsItemIndex; i < this.Count && i < cpi.LastItemIndex + 1; i++)
    //        {
    //            resultList.Add(this[i]);
    //        }

    //        return resultList;
    //    }

    //    GridPagerModel GetPager()
    //    {
    //        if (this.currentPager == null || this.currentPager.ItemCnt != this.Count)
    //        {
    //            this.currentPager = new GridPagerModel(this.CurrentRequest, this.Count, this.PageSize);
    //        }

    //        return this.currentPager;
    //    }
    //}

    public class VyrobcaPagingListModel : _PagingModel
    {
        public List<VyrobcaModel> Items { get; set; }

        public static VyrobcaPagingListModel CreateCopyFrom(Page<Vyrobca> srcArray)
        {
            VyrobcaPagingListModel trgArray = new VyrobcaPagingListModel();
            trgArray.ItemsPerPage = (int)srcArray.ItemsPerPage;
            trgArray.TotalItems = (int)srcArray.TotalItems;
            trgArray.Items = new List<VyrobcaModel>(srcArray.Items.Count + 1);

            foreach (Vyrobca src in srcArray.Items)
            {
                trgArray.Items.Add(VyrobcaModel.CreateCopyFrom(src));
            }

            return trgArray;
        }
    }

   
}
