/////////////////////////////////////////////////////////////////
//
//              AUTO-GENERATED | DON'T CHANGE
//
/////////////////////////////////////////////////////////////////

 
using MailKit;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509.Qualified;
 
using DataAccess.Model;
using DataAccess.Repositories;
using GO_Study_Logic.Service;

namespace FSAM.BusinessLogic.Generations.DependencyInjection
{
    public static class DependencyInjectionResolverGen
    {
        public static void InitializerDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<DbContext, GOStudyContext>();

            services.AddScoped<IClassroomRepository, ClassroomRepository>();
            services.AddScoped<IClassroomService, ClassroomService>();
            
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddScoped<LoginService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<ISemestersRepository, SemestersRepository>();
            services.AddScoped<ISpecializationRepository, SpecializationRepository>();
             
        }
    }
}
