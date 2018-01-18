using System;
using Newtonsoft.Json;

namespace Crypto.Lib.Json.Converters
{
    public class NAConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if ("N/A".Equals(reader.Value))
                return null;
            var underlyingType = Nullable.GetUnderlyingType(objectType);
            if (underlyingType != null)
            {
                try
                {
                    return Convert.ChangeType(reader.Value, underlyingType);
                }
                catch (FormatException)
                {
                    try
                    {
                        return Convert.ChangeType(Convert.ToString(reader.Value).Replace(" ", ""), underlyingType);
                    }
                    catch (FormatException)
                    {
                        return null;
                    }
                }
            }
            return reader.Value;
        }

        public override bool CanConvert(Type objectType)
        {
            var underlyingType = Nullable.GetUnderlyingType(objectType);
            return underlyingType != null && IsNumericType(underlyingType);
        }

        public static bool IsNumericType(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
    }
}
