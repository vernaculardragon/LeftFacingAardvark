using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeftFacingAardvark.Models
{

    public class Customer
    {
        [JsonProperty(propertyName:"_id")]
        public int? Id { get; set; }
        public int Agent_id { get; set; }
        public Guid Guid { get; set; }
        public bool IsActive { get; set; }
        [JsonConverter(typeof(CurrencyConverter))]
        public decimal Balance { get; set; }
        public int Age { get; set; }
        public string EyeColor { get; set; }
        public Name Name { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        [JsonConverter(typeof(DateConverter))]
        public DateTime Registered { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public List<string> Tags { get; set; }
    }

    public class Name
    {
        public string First { get; set; }
        public string Last { get; set; }
    }

}
