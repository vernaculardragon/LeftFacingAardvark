using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace LeftFacingAardvark.Models
{
    public class Agent
    {
        public Agent()
        {
            Phone = new Phone();
        }
        [JsonProperty(propertyName: "_id")]
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int? Tier { get; set; }
        public Phone Phone { get; set; }
    }

    public class Phone
    {
        [Phone]
        public string Primary { get; set; }
        [Phone]
        public string Mobile { get; set; }
    }

}
