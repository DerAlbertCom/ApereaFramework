using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Aperea.MVC.UrlBuilders
{
    public interface IActionUrlBuilder
    {
        string BuildActionUrl<TController>(Expression<Action<TController>> expression)
            where TController : Controller;
    }
}