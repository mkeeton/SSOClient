using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

using Authentication.BasicMVC.Client.Models;
using Authentication.BasicMVC.Client.Repositories;
using Authentication.BasicMVC.Client.Attributes;
using Authentication.BasicMVC.Client;

namespace SSOTest.Controllers
{
  public class HomeController : BaseController
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
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri("https://localhost:44300/");
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    // New code:
        //    HttpResponseMessage response = client.GetAsync("API/Authentication/" + CookieRepository.GetCookieValue("SessionID",Guid.NewGuid().ToString())).Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //      AuthenticationResponse _Response = response.Content.ReadAsAsync<AuthenticationResponse>().Result;
        //      if (_Response.ResponseCode == AuthenticationResponse.AuthenticationResponseCode.Unknown)
        //      {
        //        return Redirect(_Response.RedirectURL + "?sessionID=" + CookieRepository.GetCookieValue("SessionID", Guid.NewGuid().ToString()) + "&returnURL=" + HttpUtility.UrlEncode(Request.Url.Scheme + "://" + Request.Url.Authority + Request.Url.PathAndQuery));
        //      }
        //      else if (_Response.ResponseCode == AuthenticationResponse.AuthenticationResponseCode.NotLoggedIn)
        //      {
        //        ViewBag.TestReturn = "you are not logged in";
        //      }
        //      else if (_Response.ResponseCode == AuthenticationResponse.AuthenticationResponseCode.LoggedIn)
        //      {
        //        ViewBag.TestReturn = "you are logged in";
        //      }
        //    }
        //}
      }
      catch(Exception ex)
      {

      }
      return View();
    }

    [SSOAuthentication]
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