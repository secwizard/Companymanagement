using System.Collections.Generic;
using System.Linq;

namespace CompanyManagement.Api.Helpers
{
    public class Utility
    {
        public struct ZoneSettingPattern
        {
            public static readonly ZoneSettingPattern SinglePostal = new ZoneSettingPattern(1, "Single Postal Code");
            public static readonly ZoneSettingPattern MultiplePostal = new ZoneSettingPattern(2, "Multiple Postal Code");
            public static readonly ZoneSettingPattern PostalRange = new ZoneSettingPattern(3, "Postal Code Range");
            public static readonly ZoneSettingPattern SingleCity = new ZoneSettingPattern(4, "Single City");
            public static readonly ZoneSettingPattern MultipleCity = new ZoneSettingPattern(5, "Multiple City");
            public static readonly ZoneSettingPattern SingleState = new ZoneSettingPattern(6, "Single State");
            public static readonly ZoneSettingPattern MultipleState = new ZoneSettingPattern(7, "Multiple State");

            public byte PatternId { get; set; }
            public string PatternName { get; set; }

            private static IEnumerable<ZoneSettingPattern> TypeList => typeof(ZoneSettingPattern).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                .Where(k => k.FieldType == typeof(ZoneSettingPattern))
                .Select(k => (ZoneSettingPattern)k.GetValue(null));

            public ZoneSettingPattern(byte id, string name)
            {
                PatternId = id;
                PatternName = name;
            }

            public static IReadOnlyList<ZoneSettingPattern> GetAllZoneSettingPatterns()
            {
                return TypeList.ToList();
            }
        }
    }
}