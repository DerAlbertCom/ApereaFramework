using System;

namespace Aperea.EntityModels
{
    public class Module
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
        public int DatabaseVersion { get; set; }
        public DateTime Updated { get; set; }
    }
}