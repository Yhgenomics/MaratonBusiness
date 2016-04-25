using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaratonBusiness.Models
{
    public class Pagination
    {
        public Pagination()
        {
            PageSize = 10;
            HasNextPage = true;
            HasPreviewPage = true;
        }

        private int TotalCount_ = 0;
        public int TotalCount
        {
            get
            {
                return TotalCount_;
            }
            set
            {
                TotalCount_ = value;
                PageNum = TotalCount_ / this.PageSize + 1;
            }
        }

        public int PageSize { get; set; }

        private int CurrentPage_ = 0;
        public int CurrentPage {
            get
            {
                return CurrentPage_;
            }
            set
            {
                CurrentPage_ = value;
                if( CurrentPage_ >= ( PageNum-1 ) )
                {
                    this.HasNextPage = false;
                }

                if( CurrentPage_ <=0 )
                {
                    this.HasPreviewPage = false;
                }
            }
        }


        public int PageNum { get; private set; }

        public bool HasNextPage { get; private set; }
        public bool HasPreviewPage { get; private set; }
    }
}