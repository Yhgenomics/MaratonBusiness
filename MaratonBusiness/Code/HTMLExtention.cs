using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public static class HtmlHelperExtention
    {
        public static MvcHtmlString CreateState(this HtmlHelper helper, int state)
        {
            string ret = "";
            switch(state)
            {
                case 0:
                    {
                        ret = "";
                    }
                    break;
                case 1:
                    {
                        ret = "warning";
                    }
                    break;
                case 2:
                    {
                        ret = "success";
                    }
                    break;
                case 3:
                    {
                        ret = "success";
                    }
                    break;
                case 5:
                    {
                        ret = "danger";
                    }
                    break;
            }

            return MvcHtmlString.Create(ret);
        }

        public static MvcHtmlString CreatePaginatio(this HtmlHelper helper ,UrlHelper url, MaratonBusiness.Models.Pagination Model , string action , string controller) 
        {
            string html = "";
            html += @"
            <nav>
            <ul class='pagination pagination-sm'>";

                if (Model.HasPreviewPage)
                {
                html += @"<li>
                        <a href='"+ url.Action(action ,controller, new { pageId = (Model.CurrentPage - 1) }) + @"' aria-label='Previous'>
                            <span aria-hidden='true'>&laquo;</span>
                        </a>
                    </li>";
                }
                else
                {
                html += @"<li>
                        <a aria-label='Previous'>
                            <span aria-hidden='true'>&laquo;</span>
                        </a>
                    </li>";
                } 

                for (int i = 0; i < Model.PageNum; i++)
                {
                    if (i == Model.CurrentPage)
                    {
                    html += @"<li class='active'><a>"+(i + 1)+@"</a></li>";
                    }
                    else
                    {
                    html += @"<li class='disabled'><a href='" + url.Action(action, controller, new { pageId = (i+1) }) + @"'>"+(i + 1)+@"</a></li>";
                    }
                }

                if (Model.HasNextPage)
                {
                html += @"< li>
                        <a href='" + url.Action(action, controller, new { pageId = (Model.CurrentPage + 1) }) + @"' aria-label='Next'>
                            <span aria-hidden='true'>&raquo;</span>
                        </a>
                    </li>"; 
                }
                else
                {
                html += @"<li>
                        <a aria-label='Next'>
                            <span aria-hidden='true'>&raquo;</span>
                        </a>
                    </li>";
                }

            html += @"</ul></nav>";

            return MvcHtmlString.Create(html);
        }
    }
}