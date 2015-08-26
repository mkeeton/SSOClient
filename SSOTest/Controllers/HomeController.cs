using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using SSOTest.Authorization;

namespace SSOTest.Controllers
{
  public class HomeController : BaseController
  {
    public ActionResult Index()
    {
      using (var client = new HttpClient())
      {
          client.BaseAddress = new Uri("https://localhost:44300/");
          client.DefaultRequestHeaders.Accept.Clear();
          client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

          // New code:
          HttpResponseMessage response = client.GetAsync("API/Authentication/" + Repositories.CookieRepository.GetCookieValue("SessionID",Guid.NewGuid().ToString())).Result;
          if (response.IsSuccessStatusCode)
          {
            Authentication.BasicMVC.Domain.Models.AuthenticationResponse _Response = response.Content.ReadAsAsync<Authentication.BasicMVC.Domain.Models.AuthenticationResponse>().Result;
            if (_Response.ResponseCode == Authentication.BasicMVC.Domain.Models.AuthenticationResponse.AuthenticationResponseCode.Unknown)
            {
              return Redirect(_Response.RedirectURL + "?sessionID=" + Repositories.CookieRepository.GetCookieValue("SessionID", Guid.NewGuid().ToString()) + "&returnURL=" + HttpUtility.UrlEncode(Request.Url.Scheme + "://" + Request.Url.Authority + Request.Url.PathAndQuery));
            }
            else if (_Response.ResponseCode == Authentication.BasicMVC.Domain.Models.AuthenticationResponse.AuthenticationResponseCode.NotLoggedIn)
            {
              ViewBag.TestReturn = "you are not logged in";
            }
            else if (_Response.ResponseCode == Authentication.BasicMVC.Domain.Models.AuthenticationResponse.AuthenticationResponseCode.LoggedIn)
            {
              ViewBag.TestReturn = "you are logged in";
            }
          }
      }
      return View();
    }

    [SSOAuthorization]
    public ActionResult Secure()
    {
      //using (var client = new HttpClient())
      //{
      //  client.BaseAddress = new Uri("https://localhost:44300/");
      //  client.DefaultRequestHeaders.Accept.Clear();
      //  client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

      //  // New code:
      //  HttpResponseMessage response = client.GetAsync("API/Authentication/" + Repositories.CookieRepository.GetCookieValue("SessionID",Guid.NewGuid().ToString())).Result;
      //  if (response.IsSuccessStatusCode)
      //  {
      //    Authentication.BasicMVC.Domain.Models.AuthenticationResponse _Response = response.Content.ReadAsAsync<Authentication.BasicMVC.Domain.Models.AuthenticationResponse>().Result;
      //    ViewBag.TestReturn = _Response.RedirectURL;
      //    if (_Response.ResponseCode == Authentication.BasicMVC.Domain.Models.AuthenticationResponse.AuthenticationResponseCode.Unknown)
      //    {
      //      return Redirect(_Response.RedirectURL + "?sessionID=" + Repositories.CookieRepository.GetCookieValue("SessionID", Guid.NewGuid().ToString()) + "&returnURL=" + HttpUtility.UrlEncode(Request.Url.Scheme + "://" + Request.Url.Authority + Request.Url.PathAndQuery));
      //    }
      //    else if(_Response.ResponseCode == Authentication.BasicMVC.Domain.Models.AuthenticationResponse.AuthenticationResponseCode.NotLoggedIn)
      //    {
      //      return Redirect(_Response.RedirectURL + "?sessionID=" + Repositories.CookieRepository.GetCookieValue("SessionID", Guid.NewGuid().ToString()) + "&returnURL=" + HttpUtility.UrlEncode(Request.Url.Scheme + "://" + Request.Url.Authority + Request.Url.PathAndQuery));
      //    }
      //    else if (_Response.ResponseCode == Authentication.BasicMVC.Domain.Models.AuthenticationResponse.AuthenticationResponseCode.LoggedIn)
      //    {
      //      ViewBag.TestReturn = "you are logged in";
      //    }
      //  }
      //}
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