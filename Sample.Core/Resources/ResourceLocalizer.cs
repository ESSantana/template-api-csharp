using Microsoft.Extensions.Localization;
using System.Reflection;

namespace Sample.Core.Resources
{
    public class ResourceLocalizer : IResourceLocalizer
    {
        private readonly IStringLocalizer _localizer;

        public ResourceLocalizer(IStringLocalizerFactory factory)
        {
            var type = typeof(Resource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("Resource", assemblyName.Name);
        }

        public string GetString(string key)
        {
            var result = _localizer[key];
            return result;
        }
    }
}
