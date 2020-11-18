using LeftFacingAardvark.EF.Entities;
using LeftFacingAardvark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LeftFacingAardvark
{
    public static class Mappers
    {
        public static Func<Models.Agent, EF.Entities.Agent> ToDBAgent = x =>
          (new EF.Entities.Agent
          {
              Address = x.Address,
              City = x.City,
              Id = x.Id??0,
              MobilePhone =x.Phone?.Mobile,
              Name = x.Name,
              PrimaryPhone = x.Phone?.Primary,
              State = x.State,
              Tier = x.Tier??0,
              ZipCode = x.ZipCode
            });

        public static Func<string, Tag> ToDBTag = x =>
          (new Tag
          {
              Value = x
          });


    

        public static Expression<Func<EF.Entities.Agent, Models.Agent>> ToAgentSummary = x =>
          (new Models.Agent
          {
              Id = x.Id,
              Phone =  new Phone
              {
                  Primary=x.PrimaryPhone,
                  Mobile = x.MobilePhone,
              },
              Name = x.Name,
              State = x.State,
          });

        public static Expression<Func<EF.Entities.Agent, Models.Agent>> ToAgentDetails = x =>
         (new Models.Agent
         {
             Address = x.Address,
             City = x.City,
             Id = x.Id,
             Phone = new Phone
             {
                 Primary = x.PrimaryPhone,
                 Mobile = x.MobilePhone,
             },
             Name = x.Name,
             State = x.State,
             Tier = x.Tier,
             ZipCode = x.ZipCode
         });

        public static Func<EF.Entities.Agent, Models.Agent> ToAgentDetailsFunc = x =>
       (new Models.Agent
       {
           Address = x.Address,
           City = x.City,
           Id = x.Id,
           Phone = new Phone
           {
               Primary = x.PrimaryPhone,
               Mobile = x.MobilePhone,
           },
           Name = x.Name,
           State = x.State,
           Tier = x.Tier,
           ZipCode = x.ZipCode
       });

        public static Func<Models.Customer, EF.Entities.Customer> ToDBCustomer = x =>
         (new EF.Entities.Customer
         {
             Address = x.Address,
             Id = x.Id??0,
             Age = x.Age,
             AgentId = x.Agent_id,
             Balance = x.Balance,
             Company = x.Company,
             Email = x.Email,
             EyeColor = x.EyeColor,
             FirstName = x.Name.First,
             LastName = x.Name.Last,
             Guid = x.Guid,
             IsActive = x.IsActive,
             Latitude = x.Latitude,
             Longitude = x.Longitude,
             Phone = x.Phone,
             Registered = x.Registered,
           
         });
        public static Func<Models.Customer, Dictionary<string, Tag>, EF.Entities.Customer> ToDBCustomerInit = (x, tags) =>
        {
            var customer = ToDBCustomer(x);

            customer.CustomerTags = x.Tags.Select(x => new CustomerTag
            {
                Tag = tags[x]
            }).ToList();
            return customer;
        };

        public static Expression<Func<EF.Entities.Customer, Models.Customer>> ToCustomerSummary = x =>
       (new Models.Customer
       {
           Address = x.Address,
           Id = x.Id,
           Name = new Name { First = x.FirstName, Last = x.LastName},
           Guid = x.Guid,
           IsActive = x.IsActive,
           Latitude = x.Latitude,
           Longitude = x.Longitude,

       });
        public static Expression<Func<EF.Entities.Customer, Models.Customer>> ToCustomerDetails = x =>
     (new Models.Customer
     {
         Address = x.Address,
         Id = x.Id,
         Age = x.Age,
         Agent_id = x.AgentId,
         Balance = x.Balance,
         Company = x.Company,
         Email = x.Email,
         EyeColor = x.EyeColor,
         Name = new Name { First = x.FirstName, Last = x.LastName },
         Guid = x.Guid,
         IsActive = x.IsActive,
         Latitude = x.Latitude,
         Longitude = x.Longitude,
         Phone = x.Phone,
         Registered = x.Registered,
         Tags = x.CustomerTags.Select(y=>y.Tag.Value).ToList()

     });
    }
}
