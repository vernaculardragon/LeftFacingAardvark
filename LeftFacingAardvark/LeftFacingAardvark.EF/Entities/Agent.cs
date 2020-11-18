using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LeftFacingAardvark.EF.Entities
{
    public class Agent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int Tier { get; set; }
        public string PrimaryPhone { get; set; }
        public string MobilePhone { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }
}
