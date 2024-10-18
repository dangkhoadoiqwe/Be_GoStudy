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
using GO_Study_Logic.Service.VNPAY;
using GO_Study_Logic.Service.Interface;
using GOStudy_Logic.Service;


namespace BE_GOStudy.DependencyInjection
{
    public static class DependencyInjectionResolverGen
    {
        public static void InitializerDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<DbContext, GOStudyContext>();

            services.AddScoped<IFriendRequestsRepository, FriendRequestsRepository>();

            services.AddScoped<IClassroomRepository, ClassroomRepository>();
            services.AddScoped<IClassroomService, ClassroomService>();
            
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddScoped<LoginService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<ISemestersRepository, SemestersRepository>();

            services.AddScoped<ISpecializationRepository, SpecializationRepository>();
            services.AddScoped<ISpecializationService, SpecializationService>();

            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskService, TaskService>();

            services.AddScoped<IBlogPostService, BlogPostService>();
            services.AddScoped<IBlogPostRepository, BlogPostRepository>();

            services.AddScoped<IPaymentTransactionRepository, PaymentTransactionRepository>(); 
            services.AddScoped<IPaymentService, PaymentService>();

            services.AddScoped<IJwtService, JwtService>();

            services.AddScoped<IVnPayService, VnPayService>();

            services.AddScoped<IPackageService, PackageService>();
            services.AddScoped<IPackageRepository, PackageRepository>();

            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddScoped<IAttendanceRepository, AttendanceRepository>();

            services.AddScoped<ISqlService, SqlService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IConnectionService, ConnectionService>();
            
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICommentRepository, CommentRepository>();

            services.AddScoped<ContactInfoService>();


        }
    }
}
