using System.Web.Mvc;
using System.Web.Routing;
using System.Reflection;
using Autofac;
using Autofac.Integration.Mvc;
using StockControl.Services.Services;
using StockControl.Data.Context;
using StockControl.Abstraction.Services;

namespace StockControl.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<DataContext>();

            builder.RegisterType<CustomerService>().As<ICustomerService>();
            builder.RegisterType<StockService>().As<IStockService>();
            builder.RegisterType<OrderService>().As<IOrderService>();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
