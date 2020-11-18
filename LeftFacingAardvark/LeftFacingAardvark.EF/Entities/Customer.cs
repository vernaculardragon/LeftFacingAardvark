using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LeftFacingAardvark.EF.Entities
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AgentId { get; set; }
        public Guid Guid { get; set; }
        public bool IsActive { get; set; }
        public decimal Balance { get; set; }
        public int Age { get; set; }
        public string EyeColor { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime Registered { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public virtual Agent Agent { get; set; }
        public ICollection<CustomerTag> CustomerTags { get; set; }

    }
}
