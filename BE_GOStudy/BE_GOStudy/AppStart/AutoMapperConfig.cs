using AutoMapper;
using GO_Study_Logic.AutoMapperModule;  // Ensure this namespace is correct

namespace BE_GOStudy.AppStart
{
    public static class AutoMapperConfig
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(mc =>
            {
                mc.ConfigClassRoomModule();
                mc.ConfigUserModule();
                mc.ConfigSemesterModule();
                mc.ConfigSpecializationModule();
                mc.ConfigTasksModule();
                mc.ConfigPaymentModule();
                mc.ConfigBlogModule();
                mc.ConfigPackageModeule();
                mc.ConfigCommentModule();
            });
            IMapper mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
