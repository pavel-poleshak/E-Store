using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ninject;
using Moq;
using E_Store.Domain.Abstract;
using E_Store.Domain.Entities;

namespace E_Store.WebUI.Infrastructure
{
    public class NinjectDependencyResolver: IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices (Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            //put bindings here
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product() {Name="Football", Price=1500 },
                new Product() {Name="Surfboard", Price=1170 },
                new Product() {Name = "Running Shoes", Price = 500 }

            });
            kernel.Bind<IProductsRepository>().ToConstant(mock.Object);
        }
    }
}