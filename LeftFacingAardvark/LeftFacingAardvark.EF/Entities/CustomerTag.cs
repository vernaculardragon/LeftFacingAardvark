using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LeftFacingAardvark.EF.Entities
{
    public class CustomerTag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int TagID { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
