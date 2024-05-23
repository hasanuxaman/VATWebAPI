using Microsoft.Owin;
using Owin;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using Microsoft.Owin.Security.OAuth;
using VATWebAPI.Configurations;
using VATWebAPI.Security;

[assembly: OwinStartup(typeof(VATWebAPI.Startup))]
namespace VATWebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Enable CORS (cross origin resource sharing) for making request using browser from different domains
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            IMapper mapper = MapperConfig();
            IContainer container = DIConfig(mapper);


            HttpConfiguration config = new HttpConfiguration { DependencyResolver = new AutofacWebApiDependencyResolver(container) };
            WebApiConfig.Register(config);

            AuthConfig(app, container);


            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);



        }

        private static IMapper MapperConfig()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
            IMapper mapper = mapperConfiguration.CreateMapper();
            return mapper;
        }

        private IContainer DIConfig(IMapper mapper)
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterInstance(mapper).As<IMapper>();

            builder
                .RegisterType<SimpleAuthorizationServerProvider>()
                .As<IOAuthAuthorizationServerProvider>()
                .PropertiesAutowired() // to automatically resolve IUserService
                .SingleInstance(); // you only need one instance of this provider


            return builder.Build();
        }

        private void AuthConfig(IAppBuilder app, IContainer container)
        {
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                //The Path For generating the Token
                TokenEndpointPath = new PathString("/token"),
                //Setting the Token Expired Time (24 hours)
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                //SimpleAuthorizationServerProvider class will validate the user credentials
                Provider = container.Resolve<IOAuthAuthorizationServerProvider>()
            };

            //Token Generations
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions() { });

        }
    }
}
