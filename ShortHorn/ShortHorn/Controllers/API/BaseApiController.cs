using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShortHorn.Models;
using ShortHorn.Models.Management;
using ShortHorn.Helpers;
using ShortHorn.DataTransferObjects;

namespace Scrumbox.Controllers.API
{
    public class BaseApiController : ApiController
    {
        protected shorthornDb dbContext;
        protected User currentUser = null;

        public BaseApiController()
        {
            this.dbContext = new shorthornDb();
        }

        protected void AuthenticateByQueryString()
        {
            UserManager userModel = new UserManager(dbContext);
            string token = UrlHelper.GetAccessTokenFromQueryString();
            User loggedUser = null;
            if (token != null) loggedUser = userModel.Authenticate(token);
            loggedUser = userModel.Authenticate(token);
            if (loggedUser == null)
            {
                ExceptionHelper.ThrowHttpResponseException(ExceptionHelper.ReasonPhrases.UnauthorizedException, "Invalid login token.", HttpStatusCode.Unauthorized);
            }
            this.currentUser = loggedUser;
        }

        protected void AuthenticateByDTO(BaseDTO authenticationParameters)
        {
            UserManager userModel = new UserManager(dbContext);
            User loggedUser = null;
            if (authenticationParameters.Token != null) loggedUser = userModel.Authenticate(authenticationParameters.Token);
            if (loggedUser == null)
            {
                ExceptionHelper.ThrowHttpResponseException(ExceptionHelper.ReasonPhrases.UnauthorizedException, "Invalid login token", HttpStatusCode.Unauthorized);
            }
            this.currentUser = loggedUser;
        }
    }
}
