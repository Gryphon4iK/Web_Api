using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Builder;
using AspNet_Programm_1.Models;

namespace AspNet_Programm_1
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //устанавливаем контекст данных
            services.AddDbContext<HotelsContext>(options => options.UseSqlServer(SqlConnectionIntegratedSecurity));
            services.AddControllers();
        }
        public static string SqlConnectionIntegratedSecurity
        {
            get
            {
                var sb = new SqlConnectionStringBuilder
                {
                    DataSource = "Mega64RUS-PC",
                    IntegratedSecurity = true,
                    InitialCatalog = "Tours"
                };
                return sb.ConnectionString;
            }
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseRouting();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
