using AutoMapper;
using GO_Study_Logic.AutoMapperModule;  // Ensure this namespace is correct

namespace BE_GO_Study.AppStart
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
            });
            IMapper mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
