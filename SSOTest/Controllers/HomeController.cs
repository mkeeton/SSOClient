using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

using Authentication.BasicMVC.Client.Domain.Models;
using Authentication.BasicMVC.Client.Repositories;
using Authentication.BasicMVC.Client.Attributes;
using Authentication.BasicMVC.Client;

namespace SSOTest.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      try
      { 
        Guid userId = Authentication.BasicMVC.Client.Authentication.GetUserId();
        if(userId==Guid.Empty)
        {
          ViewBag.TestReturn = "You are not logged in";
        }
        else
        {
          ViewBag.TestReturn = "You are logged in as user: " + userId.ToString();
        }
      }
      catch(Exception ex)
      {

      }
      return View();
    }

    [SSOAuthentication]
    public ActionResult Secure()
    {
      return View();
    }

    public ActionResult About()
    {
      ViewBag.Message = "Your application description page.";

      return View();
    }

    public ActionResult Contact()
    {
      ViewBag.Message = "Your contact page.";

      return View();
    }
  }
}