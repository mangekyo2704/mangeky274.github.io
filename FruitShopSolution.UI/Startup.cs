using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FruitShopSolution.Application.Catalog.Admin;
using FruitShopSolution.Application.Catalog.Cart;
using FruitShopSolution.Application.Catalog.Categories;
using FruitShopSolution.Application.Catalog.Order;
using FruitShopSolution.Application.Catalog.Users;
using FruitShopSolution.Application.Catalog.Products;
using FruitShopSolution.Application.Common.Email;
using FruitShopSolution.Data.EF;
using FruitShopSolution.UI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FruitShopSolution.Application.Catalog.Promotion;
using FruitShopSolution.UI.Stripe;
using Stripe;

namespace FruitShopSolution.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();           // Đăng ký dịch vụ lưu cache trong bộ nhớ (Session sẽ sử dụng nó)
            services.AddSession(cfg =>
            {                    // Đăng ký dịch vụ Session
                cfg.Cookie.Name = "admin";             // Đặt tên Session - tên này sử dụng ở Browser (Cookie)
                cfg.IdleTimeout = new TimeSpan(0, 30, 0);    // Thời gian tồn tại của Session
            });
            services.AddOptions();                                         // Kích hoạt Options
            var mailsettings = Configuration.GetSection("MailSettings");  // đọc config
            services.Configure<MailSettings>(mailsettings);
            var stripestring = Configuration.GetSection("StripeSetting");
            // đăng ký để Inject
            services.Configure<StripeSetting>(Configuration.GetSection("StripeSetting"));  
            // đăng ký để Inject
            services.AddControllersWithViews();
            services.AddDbContext<FruitShopDbContext>(options =>
        options.UseSqlServer("Data Source =.\\sqlexpress; Initial Catalog = FruitShopDatabase; Integrated Security = True"));
            services.AddTransient<Application.Catalog.Products.IProductService, Application.Catalog.Products.ProductService>();
            services.AddTransient<Models.IProductService, Models.ProductService>();
            services.AddTransient<ISendMailService, SendMailService>();
            /*            services.AddTransient<IProductPublicService, PublicProductService>();*/
            /*            services.AddTransient<IProductManageService, ManageProductService>();
                        // services.AddTransient<IProductImageService, ProductImageService>();
                        services.AddTransient<IStorageService, StorageService>();*/
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IOrderService, Application.Catalog.Order.OrderService>();
            services.AddTransient<IPromotionService, PromotionService>();
            /*services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<ICategoryService, CategoryService>();*/

            IMvcBuilder builder = services.AddRazorPages();
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

#if DEBUG
            if (environment == Environments.Development)
            {
                builder.AddRazorRuntimeCompilation();
            }
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            StripeConfiguration.SetApiKey(Configuration.GetSection("Stripe")["SecretKey"]);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseRouting();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
