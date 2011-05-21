using System.Data.Entity;
using Aperea.Data;
using Aperea.EntityModels;

namespace ApereaStart.Data
{
    public class DbEntities : ApereaDbContext
    {
        public IDbSet<Login> Logins { get; set; }
        public IDbSet<RemoteAction> RemoteActions { get; set; }
        public IDbSet<MailTemplate> MailTemplates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}