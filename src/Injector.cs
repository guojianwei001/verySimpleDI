using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VerySimpleDI
{
    public class ServiceDescriptor
    {
        public Type ServiceType { get; set; }
        public Type ImplementationType { get; set; }
    }

    public class Injector
    {
        private List<ServiceDescriptor> _serviceDescriptors = new List<ServiceDescriptor>();

        public void AddTransient<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _serviceDescriptors.Add(new ServiceDescriptor
            {
                ServiceType = typeof(TService),
                ImplementationType = typeof(TImplementation)
            });
        }

        public T GetInstance<T>()
        {
            Type type = typeof(T);

            var instance = this.getInstance(type);

            return (T)instance;
        }

        private object getInstance(Type type)
        {
            var serviceDescriptor = _serviceDescriptors.FirstOrDefault(x => x.ServiceType == type);

            if (serviceDescriptor == null)
            {
                throw new ArgumentException($"{type.Name} not exist!");
            }

            var ctorInfo = serviceDescriptor.ImplementationType.GetConstructors().FirstOrDefault();
            var prameters = ctorInfo.GetParameters();
            var paremeterInstanceList = new List<object>();

            foreach (var parameter in prameters)
            {
                var parameterInstance = this.getInstance(parameter.ParameterType);
                paremeterInstanceList.Add(parameterInstance);
            }

            var instance = ctorInfo.Invoke(paremeterInstanceList.ToArray());

            return instance;
        }
    }
}
