using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbbigliamentoECommerceEntity
{
    public class User
    {
        public string Id { get; set; }
        public string nome { get; set; }
        public string cognome { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string TelefoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string email { get; set; }
        public string Password { get; set; }
        //public string ConfirmePassword { get; set; }
    }
}