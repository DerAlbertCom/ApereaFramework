using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Aperea.Services;

namespace Aperea.EntityModels
{
    public class Login
    {
        protected Login()
        {
            Groups = new HashSet<LoginGroup>();
        }


        public Login(string loginname, string email)
        {
            Created = DateTime.UtcNow;
            Updated = DateTime.UtcNow;
            Loginname = loginname;
            EMail = email;
            PasswordHash = string.Empty;
            Confirmed = false;
            Active = false;
        }

        public int Id { get; private set; }

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

        public DateTime? LastLogin { get;  set; }

        public void SetPassword(string password, IHashing hashing)
        {
            PasswordHash = hashing.GetHash(password, Created.Millisecond.ToString());
        }

        public bool IsPasswordValid(string password, IHashing hashing)
        {
            return PasswordHash == hashing.GetHash(password, Created.Millisecond.ToString());
        }

        public ICollection<LoginGroup> Groups
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

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
    }
}