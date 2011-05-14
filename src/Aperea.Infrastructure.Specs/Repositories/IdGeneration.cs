using System;
using System.Collections.Generic;

namespace Aperea.Specs.Repositories
{
    public static class IdGeneration
    {
        static readonly Dictionary<Type, int> Dictionary = new Dictionary<Type, int>();

        public static int GetNextId(Type type)
        {
            int id = Dictionary[type];
            id++;
            Dictionary[type] = id;
            return id;
        }

        public static void ResetId(Type type)
        {
            Dictionary[type] = 0;
        }
    }
}