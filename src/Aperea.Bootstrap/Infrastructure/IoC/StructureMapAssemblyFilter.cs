using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using Aperea.Settings;

namespace Aperea.Infrastructure.IoC
{
    public static class StructureMapAssemblyFilter
    {
        static readonly List<string> AssemblyStarts;
        static readonly ApplicationSettings settings = new ApplicationSettings();

        static StructureMapAssemblyFilter()
        {
            AssemblyStarts = new List<string>();
            AddStartOfAssemblyName(typeof (RegisterStructureMap).Assembly);
            AddStartOfAssemblyName(Assembly.GetCallingAssembly());
            AssemblyStarts.AddRange(GetAssemblyStartsFromAppSettings());
        }

        static void AddStartOfAssemblyName(Assembly assembly)
        {
            AssemblyStarts.Add(assembly.GetName().Name.Split('.')[0]);
        }

        static IEnumerable<string> GetAssemblyStartsFromAppSettings()
        {
            var starts = new string[0];
            var names = settings.Get("IoC.IncludeAssemblies", () => "");
            if (!string.IsNullOrWhiteSpace(names))
            {
                starts = names.Split(new[] {';', ','}, StringSplitOptions.RemoveEmptyEntries);
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