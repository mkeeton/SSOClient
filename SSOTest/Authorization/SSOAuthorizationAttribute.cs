using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

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
        HttpResponseMessage response = client.GetAsync("API/Authentication/" + Repositories.CookieRepository.GetCookieValue("SessionID", Guid.NewGuid().ToString())).Result;
        if (response.IsSuccessStatusCode)
        {
          Authentication.BasicMVC.Domain.Models.AuthenticationResponse _Response = response.Content.ReadAsAsync<Authentication.BasicMVC.Domain.Models.AuthenticationResponse>().Result;
          if (_Response.ResponseCode == Authentication.BasicMVC.Domain.Models.AuthenticationResponse.AuthenticationResponseCode.Unknown)
          {
            context.Result = new RedirectResult(_Response.RedirectURL + "?sessionID=" + Repositories.CookieRepository.GetCookieValue("SessionID", Guid.NewGuid().ToString()) + "&returnURL=" + HttpUtility.UrlEncode(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.Url.PathAndQuery));
            return;
          }
          else if (_Response.ResponseCode == Authentication.BasicMVC.Domain.Models.AuthenticationResponse.AuthenticationResponseCode.NotLoggedIn)
          {
            context.Result = new RedirectResult(_Response.RedirectURL + "?sessionID=" + Repositories.CookieRepository.GetCookieValue("SessionID", Guid.NewGuid().ToString()) + "&returnURL=" + HttpUtility.UrlEncode(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.Url.PathAndQuery));
            return;
          }
          else if (_Response.ResponseCode == Authentication.BasicMVC.Domain.Models.AuthenticationResponse.AuthenticationResponseCode.LoggedIn)
          {
            //context.Result = new System.Web.Mvc.HttpStatusCodeResult((int)System.Net.HttpStatusCode.Accepted);
          }
        }
      }
    }
  }
}