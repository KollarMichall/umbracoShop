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
using UmbracoEshop.lib.Util;

namespace UmbracoEshop.lib.Models
{
    public class VyrobokModel : _BaseModel
    {
        [Required(ErrorMessage = "Kod výrobku musí byť zadaný")]
        [Display(Name = "Kod výrobku")]
        public string KodVyrobku { get; set; }

        [Required(ErrorMessage = "Názov výrobku musí byť zadaný")]
        [Display(Name = "Názov výrobku")]
        public string NazovVyrobku { get; set; }

        [Required(ErrorMessage = "Cena výrobku musí byť zadaný")]
        [Display(Name = "Cena výrobku")]
        public string CenaVyrobku { get; set; }

        [AllowHtml]
        [Display(Name = "Popis výrobku")]
        public string PopisVyrobku { get; set; }

        public string CenaVyrobkuAMena()
        {
            return PriceUtil.GetPriceStringWithCurrency(this.CenaVyrobku);
        }

        public void CopyDataFrom(Vyrobok src)
        {
            this.pk = src.pk;
            this.NazovVyrobku = src.NazovVyrobku;
            this.KodVyrobku = src.KodVyrobku;
            this.CenaVyrobku = PriceUtil.NumberToEditorString(src.CenaVyrobku);
            this.PopisVyrobku = src.PopisVyrobku;

        }

        public void CopyDataTo(Vyrobok trg)
        {
            trg.pk = this.pk;
            trg.NazovVyrobku = this.NazovVyrobku;
            trg.KodVyrobku = this.KodVyrobku;
            trg.CenaVyrobku = PriceUtil.NumberFromEditorString(this.CenaVyrobku);
            trg.PopisVyrobku = this.PopisVyrobku;

        }

        public static VyrobokModel CreateCopyFrom(Vyrobok src)
        {
            VyrobokModel trg = new VyrobokModel();
            trg.CopyDataFrom(src);

            return trg;
        }

        public static Vyrobok CreateCopyFrom(VyrobokModel src)
        {
            Vyrobok trg = new Vyrobok();
            src.CopyDataTo(trg);

            return trg;
        }
    }

    //public class VyrobokListModel : List<VyrobokModel>
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

    //    public VyrobokListModel(HttpRequest request, int pageSize = 25)
    //    {
    //        this.CurrentRequest = request;
    //        this.PageSize = pageSize;
    //    }

    //    public List<VyrobokModel> GetPageItems()
    //    {
    //        GridPageInfo cpi = this.Pager.GetCurrentPageInfo();

    //        List<VyrobokModel> resultList = new List<VyrobokModel>();
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

    public class VyrobokPagingListModel : _PagingModel
    {
        public List<VyrobokModel> Items { get; set; }

        public string SessionId { get; set; }   

        public static VyrobokPagingListModel CreateCopyFrom(Page<Vyrobok> srcArray)
        {
            VyrobokPagingListModel trgArray = new VyrobokPagingListModel();
            trgArray.ItemsPerPage = (int)srcArray.ItemsPerPage;
            trgArray.TotalItems = (int)srcArray.TotalItems;
            trgArray.Items = new List<VyrobokModel>(srcArray.Items.Count + 1);

            foreach (Vyrobok src in srcArray.Items)
            {
                trgArray.Items.Add(VyrobokModel.CreateCopyFrom(src));
            }

            return trgArray;
        }
    }

   
}
