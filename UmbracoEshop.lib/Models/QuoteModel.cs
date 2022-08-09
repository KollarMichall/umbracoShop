using NPoco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmbracoEshop.lib.Repositories;
using UmbracoEshop.lib.Util;

namespace UmbracoEshop.lib.Models
{

    public class QuoteModel : _BaseModel
    {
        public string DateCreate { get; set; }
        public string DateFinished { get; set; }
        public string SessionId { get; set; }
        public string FinishedSessionId { get; set; }
        public int QuoteYear { get; set; }
        public int QuoteNumber { get; set; }

        public string QuotePriceNoVat { get; set; }
        public string QuotePriceWithVat { get; set; }

        public string QuoteState { get; set; }
        public string QuotePriceState { get; set; }
        public string Note { get; set; }

        public void CopyDataFrom(Quote src)
        {
            this.pk = src.pk;
            this.DateCreate = DateTimeUtil.GetDisplayDate(src.DateCreate);
            this.DateFinished = DateTimeUtil.GetDisplayDate(src.DateFinished);
            this.SessionId = src.SessionId;
            this.FinishedSessionId = src.FinishedSessionId;
            this.QuoteYear = src.QuoteYear;
            this.QuoteNumber = src.QuoteNumber;
            this.QuotePriceNoVat = PriceUtil.NumberToEditorString(src.QuotePriceNoVat);
            this.QuotePriceWithVat = PriceUtil.NumberToEditorString(src.QuotePriceWithVat);
            this.QuoteState = src.QuoteState;
            this.QuotePriceState = src.QuotePriceState;
            this.Note = src.Note;
        }

        public void CopyDataTo(Quote trg)
        {
            trg.pk = this.pk;
            trg.DateCreate = DateTimeUtil.DisplayDateToDate(this.DateCreate).Value;
            trg.DateFinished = DateTimeUtil.DisplayDateToDate(this.DateFinished);
            trg.SessionId = this.SessionId;
            trg.FinishedSessionId = this.FinishedSessionId;
            trg.QuoteYear = this.QuoteYear;
            trg.QuoteNumber = this.QuoteNumber;
            trg.QuotePriceNoVat = PriceUtil.NumberFromEditorString(this.QuotePriceNoVat);
            trg.QuotePriceWithVat = PriceUtil.NumberFromEditorString(this.QuotePriceWithVat);
            trg.QuoteState = this.QuoteState;
            trg.QuotePriceState = this.QuotePriceState;
            trg.Note = this.Note;

        }

        public static QuoteModel CreateCopyFrom(Quote src)
        {
            QuoteModel trg = new QuoteModel();
            trg.CopyDataFrom(src);

            return trg;
        }

        public static Quote CreateCopyFrom(QuoteModel src)
        {
            Quote trg = new Quote();
            src.CopyDataTo(trg);

            return trg;
        }
    }

    //public class QuoteListModel : List<QuoteModel>
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

    //    public QuoteListModel(HttpRequest request, int pageSize = 25)
    //    {
    //        this.CurrentRequest = request;
    //        this.PageSize = pageSize;
    //    }

    //    public List<QuoteModel> GetPageItems()
    //    {
    //        GridPageInfo cpi = this.Pager.GetCurrentPageInfo();

    //        List<QuoteModel> resultList = new List<QuoteModel>();
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

    public class QuotePagingListModel : _PagingModel
    {
        public List<QuoteModel> Items { get; set; }

        public static QuotePagingListModel CreateCopyFrom(Page<Quote> srcArray)
        {
            QuotePagingListModel trgArray = new QuotePagingListModel();
            trgArray.ItemsPerPage = (int)srcArray.ItemsPerPage;
            trgArray.TotalItems = (int)srcArray.TotalItems;
            trgArray.Items = new List<QuoteModel>(srcArray.Items.Count + 1);

            foreach (Quote src in srcArray.Items)
            {
                trgArray.Items.Add(QuoteModel.CreateCopyFrom(src));
            }

            return trgArray;
        }
    }
}
