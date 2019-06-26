namespace TransformCsvFormat.Mappings
{
    using CsvHelper.Configuration;
    using System;
    using TransformCsvFormat.Converters;
    using TransformCsvFormat.Infrastructure;
    using TransformCsvFormat.Models;

    /// <summary>
    /// Maps StandardFormat class to custom with headers CSV fields.
    /// </summary>
    public sealed class CustomMapWithHeader : ClassMap<StandardFormat>, IFormatMapping
    {
        /// <summary>
        /// Called automatically by ClassMap class to set CSV fields mapping.
        /// </summary>
        public CustomMapWithHeader()
        {
            MapFields();
        }

        /// <summary>
        /// Map CSV fields to StandardFormat class fields using CSV fields names.
        /// </summary>
        public void MapFields()
        {
            Map(record => record.AccountCode).Name("Identifier").TypeConverter<AccountCodeConverter<string>>();
            Map(record => record.Name).Validate
                (
                field =>
                {
                    if (string.IsNullOrWhiteSpace(field))
                    {
                        throw new ArgumentException($"Account Name field is missing in CustomMapWithHeader format.", "CustomMapWithHeader:Name");
                    }

                    return true;
                }
                );
            Map(record => record.Type).TypeConverter<AccountTypeConverter<AccountType>>();
            Map(record => record.OpenDate).Name("Opened").TypeConverter<DateFormatConverter<string>>();
            Map(record => record.Currency).TypeConverter<CurrencyFormatConverter<string>>();
        }

    }
}
