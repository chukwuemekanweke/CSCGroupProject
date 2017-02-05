using CSCGroupProject.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace CSCGroupProject.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }
        
    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string name) : base(name) { }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("IdentityDb")
        {
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInit());
        }

        static ApplicationDbContext()
        {
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInit());
}
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
}
    }

   

    public class ApplicationDbInit:DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        
        protected override void Seed(ApplicationDbContext context)
        {
            ApplicationUserManager usermanager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            ApplicationRoleManager rolemanager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));
            string[] RolesToCreate = new string[] { "Admin", "User" };
            foreach(string RoleName in RolesToCreate)
            {
                if (!rolemanager.RoleExists(RoleName))
                {
                    rolemanager.Create(new ApplicationRole(RoleName));
                }
            }

            string[] UsersToCreate = new string[] { "Emeka", "Emmanuel","Uzor","James","Amara" };
            string[] EmailsForUsers = new string[] { "emekanweke604@gmail.com", "okoyeemmanuel97@gmail.com", "uzor@gmail.com", "james@gmail.com", "amara@gmail.com" };
            string[] PasswordsForUsers = new string[] { "Secret", "Secret", "Secret", "Secret", "Secret" };
           
           for(int i=0; i<UsersToCreate.Length;++i)
           {
               ApplicationUser user = usermanager.FindByName(UsersToCreate[i]);
               if (user == null)
               {
                   usermanager.Create(new ApplicationUser { UserName = UsersToCreate[i], Email = EmailsForUsers[i] }, PasswordsForUsers[i]);
                   user = usermanager.FindByName(UsersToCreate[i]);
               }
               if (!usermanager.IsInRole(user.Id, RolesToCreate[0]))
               {
                   usermanager.AddToRole(user.Id, RolesToCreate[0]);
               }
           }
           
            base.Seed(context);
        }
    }
}