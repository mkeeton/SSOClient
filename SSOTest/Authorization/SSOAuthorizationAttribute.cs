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

namespace SSOTest.Authorization
{
  public class SSOAuthorization : AuthorizeAttribute
  {
    public SSOAuthorization()
    {
    
    }

    public override void OnAuthorization(AuthorizationContext context)
    {
      using (
      var client = new HttpClient())
      {
        client.BaseAddress = new Uri("https://localhost:44300/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        // New code:
        HttpResponseMessage response = client.GetAsync("API/Authentication/" + CookieRepository.GetCookieValue("SessionID", Guid.NewGuid().ToString())).Result;
        if (response.IsSuccessStatusCode)
        {
          AuthenticationResponse _Response = response.Content.ReadAsAsync<AuthenticationResponse>().Result;
          if (_Response.ResponseCode == AuthenticationResponse.AuthenticationResponseCode.Unknown)
          {
            context.Result = new RedirectResult(_Response.RedirectURL + "?sessionID=" + CookieRepository.GetCookieValue("SessionID", Guid.NewGuid().ToString()) + "&returnURL=" + HttpUtility.UrlEncode(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.Url.PathAndQuery));
            return;
          }
          else if (_Response.ResponseCode == AuthenticationResponse.AuthenticationResponseCode.NotLoggedIn)
          {
            context.Result = new RedirectResult(_Response.RedirectURL + "?sessionID=" + CookieRepository.GetCookieValue("SessionID", Guid.NewGuid().ToString()) + "&returnURL=" + HttpUtility.UrlEncode(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.Url.PathAndQuery));
            return;
          }
          else if (_Response.ResponseCode == AuthenticationResponse.AuthenticationResponseCode.Error)
          {
            context.Result = new RedirectResult("~/Error/Authentication");
            return;
          }
        }
      }
    }
  }
}