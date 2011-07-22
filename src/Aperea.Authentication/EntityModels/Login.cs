using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Aperea.Services;

namespace Aperea.EntityModels
{
    public class Login
    {
        protected Login()
        {
            Groups = new HashSet<LoginGroup>();
        }


        public Login(string loginname, string email):this()
        {
            Created = DateTime.UtcNow;
            Updated = DateTime.UtcNow;
            Loginname = loginname;
            EMail = email;
            PasswordHash = string.Empty;
            Confirmed = false;
            Active = false;
        }

        public string Id { get; private set; }

        [Required]
        public DateTime Created { get; private set; }

        [Required]
        public DateTime Updated { get; private set; }

        [Required]
        [StringLength(128)]
        public string Loginname { get; private set; }

        [Required]
        [StringLength(256)]
        public string EMail { get; private set; }

        [Required]
        [StringLength(64)]
        public string PasswordHash { get; private set; }

        [Required]
        public bool Confirmed { get; private set; }

        [Required]
        public bool Active { get; private set; }

        public DateTime? LastLogin { get;  private set; }

        public void SetPassword(string password, IHashing hashing)
        {
            PasswordHash = hashing.GetHash(password, GetLoginSalt());
        }

        string GetLoginSalt()
        {
            return (Created.Second*Created.Minute*Created.Hour).ToString() + Loginname.ToLowerInvariant();
        }

        public bool IsPasswordValid(string password, IHashing hashing)
        {
            return PasswordHash == hashing.GetHash(password, GetLoginSalt());
        }

        public virtual ICollection<LoginGroup> Groups { get; set; }

        public void Confirm()
        {
            Confirmed = true;
            Active = true;
            Changed();
        }

        void Changed()
        {
            Updated = DateTime.UtcNow;
        }

        public void AddGroup(LoginGroup adminGroup)
        {
            if (Groups.Any(g=>g.GroupName==adminGroup.GroupName))
            {
                return;
            }
            Groups.Add(adminGroup);
        }

        public void LoggedIn()
        {
            LastLogin = DateTime.UtcNow;
        }
    }
}