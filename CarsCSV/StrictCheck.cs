using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Reflection;

namespace CarCSV;

public class StrictTypeConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return null;
        }

        try
        {
            var propertyInfo = memberMapData.Member as PropertyInfo;
            if (propertyInfo == null)
            {
                return null;
            }

            Type targetType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ??
                            propertyInfo.PropertyType;

            return Convert.ChangeType(text, targetType);
        }
        catch
        {
            return null;
        }
    }
}