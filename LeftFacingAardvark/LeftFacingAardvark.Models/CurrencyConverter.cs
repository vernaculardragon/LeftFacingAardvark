using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LeftFacingAardvark.Models
{
    public class CurrencyConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(decimal).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            decimal value = 0.0m;
            var token = JToken.Load(reader);
            var stringValue = token.Value<string>();
            decimal.TryParse(stringValue, NumberStyles.Currency,null,out value);

            return value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var decimalValue = (decimal)value;

            writer.WriteValue(decimalValue.ToString("C"));

        }
    }
}
