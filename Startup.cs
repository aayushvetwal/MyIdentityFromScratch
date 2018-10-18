using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Sample.Startup))]

namespace Sample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new IdentityDbContext("DefaultConnection"));
            app.CreatePerOwinContext<UserStore<IdentityUser>>((opt, ctx) => new UserStore<IdentityUser>(ctx.Get<IdentityDbContext>()));
            app.CreatePerOwinContext<UserManager<IdentityUser>>((opt, ctx) => new UserManager<IdentityUser>(ctx.Get<UserStore<IdentityUser>>()));
            app.CreatePerOwinContext<SignInManager<IdentityUser, string>>((opt, ctx) => new SignInManager<IdentityUser, string>(ctx.Get<UserManager<IdentityUser>>(), ctx.Authentication));
        }
    }
}
