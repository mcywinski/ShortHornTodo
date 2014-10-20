using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShortHorn.Helpers;
using ShortHorn.Models;
using ShortHorn.Models.Management;
using ShortHorn.Services;
using ShortHorn.DataTransferObjects;
using System.Web;

namespace ShortHorn.Controllers.API
{
    public class UsersController : ApiController
    {
        /// <summary>
        /// Creates new user. Returns HTTP 200 status if registration is successful. Otherwise HTTP 500 Status is returned.
        /// </summary>
        /// <param name="registrationCredentials">Basic registration data</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Register(SignupCredentialsDTO registrationCredentials)
        {
            //Initial data consistency validation
            bool invalidData = false;
            if (!Validation.AreEqual(registrationCredentials.Email, registrationCredentials.EmailConfirmed) || !Validation.IsEmailAddress(registrationCredentials.Email))
            {
                invalidData = true;
            }

            if (!Validation.AreEqual(registrationCredentials.Password, registrationCredentials.PasswordConfirmed) || !Validation.IsLongerEqual(registrationCredentials.Password, 6))
            {
                invalidData = true;
            }

            if (invalidData) //Database validation unecessary
            {
                ExceptionHelper.ThrowHttpResponseException("Registration exception", "Inconsistent registration credentials.", HttpStatusCode.InternalServerError);
            }

            //Database-side validation
            shorthornDb context = new shorthornDb();
            UserManager model = new UserManager(context);

            User u = model.GetUserByEmail(registrationCredentials.Email); //Check if E-Mail exists
            if (u != null)
            {
                ExceptionHelper.ThrowHttpResponseException("Registration exception", "An user with such E-Mail already exists.", HttpStatusCode.InternalServerError);
            }

            u = model.GetUserByLogin(registrationCredentials.Login); //Check if Login exists
            if (u != null)
            {
                ExceptionHelper.ThrowHttpResponseException("Registration exception", "An user with such login already exists.", HttpStatusCode.InternalServerError);
            }

            //Registration allowed
            User registerUser = new User
            {
                ActivationToken = Guid.NewGuid().ToString(),
                Active = false,
                Email = registrationCredentials.Email,
                Login = registrationCredentials.Login,
                Password = registrationCredentials.Password,
                PrivilegeLevel = 0,
                DateRegistered = DateTime.Now
            };

            if (!model.CreateUser(registerUser)) //Save
            {
                ExceptionHelper.ThrowHttpResponseException("Registration exception", "An error occured while creating a new user account.", HttpStatusCode.InternalServerError);
            }

            IMailService mailService = new ExternalMailService(); //Email with registration code
            mailService.SendMail(registerUser.Email, "Successful registration at shorthorn.me!", "Hey! You're registered! Activate your account at http://localhost/#/users/activate/" + registerUser.ActivationToken + "?userId=" + registerUser.Id);

            return new HttpResponseMessage(HttpStatusCode.OK) //OK
            {
                Content = new StringContent("User registration successful!"),
                ReasonPhrase = "Registration successful"
            };
        }

        [HttpPost]
        public HttpResponseMessage Activate(UserActivationParametersDTO activationParams)
        {
            //Data preparation
            string activationToken = activationParams.ActivationToken;
            int userId = activationParams.Id;

            shorthornDb context = new shorthornDb();
            UserManager model = new UserManager(context);
            User u = model.GetUserById(userId);

            //Validation
            if (u == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("User with such ID doesn't exists."),
                    ReasonPhrase = "Activation exception"
                });
            }

            if (u.Active) //Already activated
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("This user is already active."),
                    ReasonPhrase = "Activation exception"
                });
            }

            if (!Validation.AreEqual(u.ActivationToken, activationToken))
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Invalid activation token has been provided."),
                    ReasonPhrase = "Activation exception"
                });
            }

            u.Active = true;
            if (model.SaveUser(u))
            {
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Activation successful"),
                    ReasonPhrase = "Activation successful"
                };
            }
            else
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occured while activating the user account."),
                    ReasonPhrase = "Activation exception"
                });
            }

        }

        [HttpPost]
        public LoginTokenDTO Login(LoginCredentialsDTO loginCredentials)
        {
            LoginTokenDTO loginToken = new LoginTokenDTO();
            loginToken.Success = false;
            shorthornDb context = new shorthornDb();
            UserManager model = new UserManager(context);
            User checkUser = model.GetUserByLogin(loginCredentials.Login);

            //Credentials check
            if (checkUser == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("User with such login doesn't exist."),
                    ReasonPhrase = "Login exception"
                });
            }
            if (!Validation.AreEqual(checkUser.Password, loginCredentials.Password))
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Wrong password provided."),
                    ReasonPhrase = "Login exception"
                });
            }

            //Logging in
            LoginToken dbToken = model.GetLoginTokenForUser(checkUser);
            if (dbToken == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Inactive user."),
                    ReasonPhrase = "Login exception"
                });
            }

            loginToken.DateCreated = dbToken.DateCreated;
            loginToken.Success = true;
            loginToken.Token = dbToken.Token;

            return loginToken;
        }
    }
}
