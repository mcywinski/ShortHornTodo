using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShortHorn.Models;
using ShortHorn.Services;

namespace ShortHorn.Models.Management
{
    /// <summary>
    /// Contains basic operations on system users.
    /// </summary>
    public class UserManager : BaseManager
    {
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="container">The database context.</param>
        public UserManager(ShorthornDb context) : base(context) { }

        /// <summary>
        /// Retrieves a specific user based on login criteria
        /// </summary>
        /// <param name="login"></param>
        /// <returns>The user</returns>
        public User GetUserByLogin(string login)
        {
            return context.Users.SingleOrDefault(u => u.Login == login);
        }

        /// <summary>
        /// Retrieves a specific user based on E-Mail criteria.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>The user.</returns>
        public User GetUserByEmail(string email)
        {
            return context.Users.SingleOrDefault(u => u.Email == email);
        }

        /// <summary>
        /// Saves an user in database.
        /// </summary>
        /// <param name="user">THe user to be created.</param>
        /// <returns>True for successful operation, false otherwise.</returns>
        public bool CreateUser(User user)
        {
            context.Users.Add(user);
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                LogService.Logger.Fatal(LogService.GetCrudErrorLogMessage(typeof(User), LogService.CrudOperationType.Create), ex);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Retrieves a specific user based on id criteria
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>The user.</returns>
        public User GetUserById(int userId)
        {
            return context.Users.SingleOrDefault(u => u.Id == userId);
        }

        /// <summary>
        /// Saves changes made to user object in database.
        /// </summary>
        /// <param name="user">The user to be modified.</param>
        /// <returns>True for successful operation, false otherwise.</returns>
        public bool SaveUser(User user)
        {
            context.Entry(user).State = System.Data.EntityState.Modified;
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                LogService.Logger.Fatal(LogService.GetCrudErrorLogMessage(typeof(User), LogService.CrudOperationType.Update), ex);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Generates, saves and returns back an individual login token for user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Unique login token or null if user is not activated or if an error occurs</returns>
        public LoginToken GetLoginTokenForUser(User user)
        {
            if (!user.Active) return null; //No need for token

            LoginToken loginToken;
            string token = string.Empty;
            bool tokenOk = false;

            //Ensures that token is unique
            do
            {
                token = Guid.NewGuid().ToString();
                if (context.LoginTokens.SingleOrDefault(t => t.Token == token) == null)
                {
                    tokenOk = true;
                }
            }
            while (!tokenOk);
            loginToken = new LoginToken()
            {
                User = user,
                Token = token,
                DateCreated = DateTime.Now
            };

            //Save to db
            context.LoginTokens.Add(loginToken);
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                LogService.Logger.Fatal(LogService.GetCrudErrorLogMessage(typeof(User), LogService.CrudOperationType.Delete), ex);
                return null;

            }

            return loginToken;
        }

        /// <summary>
        /// Checks if user is properly logged in and authorized to do basic actions.
        /// </summary>
        /// <param name="token">Unique login token.</param>
        /// <returns>User object in case of success, null otherwise.</returns>
        public User Authenticate(string token)
        {
            LoginToken checkToken = context.LoginTokens.SingleOrDefault(lt => lt.Token == token);
            if (checkToken != null) return checkToken.User;
            else return null;
        }
    }
}