using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ReviewVaiApp
{
    public static class WebApiConfig
    {
		public static ReferenceLoopHandling ReferenceLoopHandling { get; private set; }

		public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
			config.Filters.Add(new AuthFilter());
			config.SuppressDefaultHostAuthentication();
			config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
			config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("multipart/form-data"));
			((DefaultContractResolver)config.Formatters.JsonFormatter.SerializerSettings.ContractResolver).IgnoreSerializableAttribute = true;
			// Web API routes
			var settings = config.Formatters.JsonFormatter.SerializerSettings;
			//settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			settings.Formatting = Newtonsoft.Json.Formatting.Indented;
			config.MapHttpAttributeRoutes();
			//var json = config.Formatters.JsonFormatter;
			var json = config.Formatters.JsonFormatter;
			//json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
			//config.Formatters.Remove(config.Formatters.XmlFormatter);
			//json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
			//ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
			config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling= Newtonsoft.Json.ReferenceLoopHandling.Ignore;

			config.Routes.MapHttpRoute(
				  name: "ActionApi",
				  routeTemplate: "api/{controller}/{action}/{id}",
				  defaults: new { id = RouteParameter.Optional });
			config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
		
		}
    }
}
