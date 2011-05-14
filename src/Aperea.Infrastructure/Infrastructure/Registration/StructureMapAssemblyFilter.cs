using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace Aperea.Infrastructure.Registration
{
    public static class StructureMapAssemblyFilter
    {
        private static readonly List<string> AssemblyStarts;

        static StructureMapAssemblyFilter()
        {
            AssemblyStarts = new List<string>();
            AssemblyStarts.AddRange(GetAssemblyStartsFromAppSettings());
            AssemblyStarts.Add(typeof (RegisterStructureMap).Assembly.GetName().Name.Split('.')[0]);
        }

        private static IEnumerable<string> GetAssemblyStartsFromAppSettings()
        {
            var starts = new string[0];
            var names = ConfigurationManager.AppSettings["IoC.IncludeAssemblies"];
            if (!string.IsNullOrWhiteSpace(names))
            {
                starts = names.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries);
            }
            return starts;
        }

        public static bool Filter(Assembly assemblyFilter)
        {
            if (AssemblyStarts.Count == 0)
                return true;
            var assemblyStart = assemblyFilter.GetName().Name.Split('.')[0];
            return AssemblyStarts.Contains(assemblyStart);
        }
    }
}