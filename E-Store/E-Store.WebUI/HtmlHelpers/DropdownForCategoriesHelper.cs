using E_Store.Domain.Entities;
using E_Store.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace E_Store.WebUI.HtmlHelpers
{
    public static class DropdownForCategoriesHelper
    {
        public static MvcHtmlString CreateDropdownForProductCategories(this HtmlHelper htmlHelper, IEnumerable<SubCategoryDTO> listSubCategories, string tagName, int? currentVal=null)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder select = new TagBuilder("select");
            foreach (SubCategoryDTO category in listSubCategories)
            {

                TagBuilder optGroup = new TagBuilder("optgroup");
                optGroup.MergeAttribute("label", category.CategoryName);
                foreach (SubCategory subCategory in category.SubCategories)
                {
                    TagBuilder option = new TagBuilder("option");
                    option.MergeAttribute("value", subCategory.Id.ToString());
                    option.InnerHtml = subCategory.Name;
                    optGroup.InnerHtml += option;
                }
                select.InnerHtml += optGroup;                
            }
            select.AddCssClass("selectpicker show-tick");
            select.MergeAttribute("name", tagName);
            select.MergeAttribute("id", tagName);
            select.MergeAttribute("data-live-search", "true");
            select.MergeAttribute("data-header", "Выберите категорию");
            select.MergeAttribute("data-size", "8");
            result.Append(select.ToString());

            return MvcHtmlString.Create(result.ToString());
        }

    }
}