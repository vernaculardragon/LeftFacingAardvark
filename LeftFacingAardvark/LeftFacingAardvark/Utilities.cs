using LeftFacingAardvark.EF;
using LeftFacingAardvark.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LeftFacingAardvark
{
    public static  class Utilities
    {
        public static void AddStartupData(AardvarkContext db)
        {
            var agents = JsonConvert.DeserializeObject<List<Agent>>(File.ReadAllText("StartupFiles/agents.txt"));
            var agentEntities = agents.Select(Mappers.ToDBAgent).ToList();

            var customers = JsonConvert.DeserializeObject<List<Customer>>(File.ReadAllText("StartupFiles/customers.txt"));

            var tags = customers.SelectMany(x => x.Tags).Distinct().Select(Mappers.ToDBTag).ToList();
            var tagDictionary = tags.ToDictionary(x => x.Value);
            var customerEntities = customers.Select(x=>Mappers.ToDBCustomerInit(x, tagDictionary)).ToList();

            db.Tags.AddRange(tags);
            db.Agents.AddRange(agentEntities);
            db.Customers.AddRange(customerEntities);
            db.SaveChanges(); 

        }
    }
}
