using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using OpenGameListWebApp.Data;
using OpenGameListWebApp.Data.Users;
using Newtonsoft.Json;

namespace OpenGameListWebApp.Controllers
{
  [Route("api/[controller]")]
  public class BaseController : Controller
  {
    #region Private Fields
    protected ApplicationDbContext DbContext;
    #endregion Private Fields

    #region Constructor
    public BaseController(ApplicationDbContext context)
    { 
      // Dependency Injection
      DbContext = context;
    }
    #endregion Constructor

    #region Common Methods
    /// <summary>
    /// Retrieves the .NET Core Identity User Id 
    /// for the current ClaimsPrincipal.
    /// </summary>
    /// <returns></returns>
    public string GetCurrentUserId()
    {
      // if the user is not authenticated, throw an exception
      if (!User.Identity.IsAuthenticated)
        throw new NotSupportedException();

      return User.FindFirst(ClaimTypes.NameIdentifier).Value;
    }

    #endregion Common Methods

    #region Common Properties
    /// <summary>
    /// Returns a suitable JsonSerializerSettings object 
    /// that can be used to generate the JsonResult return value 
    /// for this Controller's methods.
    /// </summary>
    protected JsonSerializerSettings DefaultJsonSettings
    {
      get
      {
        return new JsonSerializerSettings()
        {
          Formatting = Formatting.Indented
        };
      }
    }
    #endregion Common Properties
  }
}
