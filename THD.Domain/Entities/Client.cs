using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace THD.Domain.Entities
{
    public class Client: BaseEntity
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string Telephone { get; set; }
        public DateTime DateOpen { get; set; }
        public int SSNumber { get; set; }
        public string Picture { get; set; }
        public DateTime BiryhDate { get; set; }
        public string Occupation { get; set; }
        public string RiskLevel { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Objectives { get; set; }
        public string Imterests { get; set; }
        public string Image { get; set; }
        public ICollection<Holdings> Holdings { get; set; }
    }
}
