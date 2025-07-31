using ESAPI.IRepositories;
using ESAPI.Providers;
using ESApplication.Commands.Business;
using ESApplication.Commands.BusinessHours;
using ESApplication.Commands.BusinessImages;
using ESApplication.Commands.Category;
using ESApplication.Commands.CommonData;
using ESApplication.Commands.Promotion;
using ESApplication.Commands.SiteData;
using ESApplication.Commands.TokenDetails;
using ESApplication.Commands.UserDetails;
using ESDomain.IRepositories;
using ESInfrastructure.DBContext;
using ESInfrastructure.Repository;
namespace ESAPI.AppStart
{
    public static class ApplicationServices
    {
        public static void RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterServices(services);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient(typeof(CreateUserDetailsCommand), typeof(CreateUserDetailsCommand));
            services.AddTransient(typeof(UpdateUserDetailsCommand), typeof(UpdateUserDetailsCommand));
            services.AddTransient(typeof(DeleteUserDetailsCommand), typeof(DeleteUserDetailsCommand));
            services.AddTransient(typeof(IUserDetailsRepository), typeof(UserDetailsRepository));
            services.AddScoped(typeof(UserDetailsDbContext), typeof(UserDetailsDbContext));

            services.AddTransient(typeof(CreateSiteDataCommand), typeof(CreateSiteDataCommand));
            services.AddTransient(typeof(UpdateSiteDataCommand), typeof(UpdateSiteDataCommand));
            services.AddTransient(typeof(DeleteSiteDataCommand), typeof(DeleteSiteDataCommand));
            services.AddTransient(typeof(ISiteDataRepository), typeof(SiteDataRepository));
            services.AddScoped(typeof(SiteDataDbContext), typeof(SiteDataDbContext));

            services.AddTransient(typeof(CreateCategoryCommand), typeof(CreateCategoryCommand));
            services.AddTransient(typeof(UpdateCategoryCommand), typeof(UpdateCategoryCommand));
            services.AddTransient(typeof(DeleteCategoryCommand), typeof(DeleteCategoryCommand));
            services.AddTransient(typeof(ICategoryRepository), typeof(CategoryRepository));
            services.AddScoped(typeof(CategoryDbContext), typeof(CategoryDbContext));

            services.AddTransient(typeof(CreatePromotionCommand), typeof(CreatePromotionCommand));
            services.AddTransient(typeof(UpdatePromotionCommand), typeof(UpdatePromotionCommand));
            services.AddTransient(typeof(DeletePromotionCommand), typeof(DeletePromotionCommand));
            services.AddTransient(typeof(IPromotionRepository), typeof(PromotionRepository));
            services.AddScoped(typeof(PromotionDbContext), typeof(PromotionDbContext));

            services.AddTransient(typeof(CreateBusinessCommand), typeof(CreateBusinessCommand));
            services.AddTransient(typeof(UpdateBusinessCommand), typeof(UpdateBusinessCommand));
            services.AddTransient(typeof(DeleteBusinessCommand), typeof(DeleteBusinessCommand));
            services.AddTransient(typeof(IBusinessRepository), typeof(BusinessRepository));
            services.AddScoped(typeof(BusinessDbContext), typeof(BusinessDbContext));

            services.AddTransient(typeof(CreateBusinessHoursCommand), typeof(CreateBusinessHoursCommand));
            services.AddTransient(typeof(UpdateBusinessHoursCommand), typeof(UpdateBusinessHoursCommand));
            services.AddTransient(typeof(DeleteBusinessHoursCommand), typeof(DeleteBusinessHoursCommand));
            services.AddTransient(typeof(IBusinessHoursRepository), typeof(BusinessHoursRepository));
            services.AddScoped(typeof(BusinessHoursDbContext), typeof(BusinessHoursDbContext));

            services.AddTransient(typeof(UploadBusinessImagesCommand), typeof(UploadBusinessImagesCommand));
            services.AddTransient(typeof(IBusinessImageRepository), typeof(BusinessImageRepository));
            services.AddScoped(typeof(BusinessImagesDbContext), typeof(BusinessImagesDbContext));

            services.AddTransient(typeof(CreateCommonCommand), typeof(CreateCommonCommand));
            services.AddTransient(typeof(ICommonRepository), typeof(CommonRepository));
            services.AddScoped(typeof(CommonDbContext), typeof(CommonDbContext));

            services.AddTransient(typeof(CreateTokenCommand), typeof(CreateTokenCommand));
            //services.AddTransient(typeof(UpdateUserDetailsCommand), typeof(UpdateUserDetailsCommand));
            //services.AddTransient(typeof(DeleteUserDetailsCommand), typeof(DeleteUserDetailsCommand));
            services.AddTransient(typeof(ITokenDetailsRepository), typeof(TokenDetailsRepository));
            services.AddScoped(typeof(TokenDetailsDbContext), typeof(TokenDetailsDbContext));

            services.AddScoped<IAuthMiddleware, AuthMiddleware>(); 
        }
    }
}
