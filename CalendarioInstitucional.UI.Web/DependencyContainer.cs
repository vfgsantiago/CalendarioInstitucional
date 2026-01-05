using CalendarioInstitucional.Data;
using CalendarioInstitucional.Repository;

namespace CalendarioInstitucional.UI.Web
{
    public class DependencyContainer
    {
        public static void RegisterContainers(IServiceCollection services)
        {
            #region Site
            services.AddScoped<EventoREP>();
            services.AddScoped<CategoriaREP>();
            services.AddScoped<IconeREP>();
            services.AddScoped<CorREP>();
            #endregion

            #region Login
            services.AddScoped<LoginREP>();
            services.AddScoped<UsuarioREP>();
            services.AddScoped<SistemaREP>();
            #endregion

            #region Services
            services.AddScoped<HttpClient>();
            services.AddScoped<AcessaDados>();
            #endregion
        }
    }
}
