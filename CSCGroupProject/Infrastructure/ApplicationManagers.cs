using CSCGroupProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace CSCGroupProject.Infrastructure
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {   
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            ApplicationDbContext db = context.Get<ApplicationDbContext>();
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            manager.PasswordValidator = new PasswordValidator
            {

                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
                 
            };

            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true,
            };
            return manager;
        }

      
    }

    public class ApplicationRoleManager : RoleManager<ApplicationRole>, IDisposable
    {
        public ApplicationRoleManager(RoleStore<ApplicationRole> store)
            : base(store)
        {
        }
        public static ApplicationRoleManager Create(
        IdentityFactoryOptions<ApplicationRoleManager> options,
        IOwinContext context)
        {
            return new ApplicationRoleManager(new
            RoleStore<ApplicationRole>(context.Get<ApplicationDbContext>()));
        }
        
    }
}