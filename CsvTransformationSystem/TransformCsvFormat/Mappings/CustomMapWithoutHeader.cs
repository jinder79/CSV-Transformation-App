namespace TransformCsvFormat.Mappings
{
    using CsvHelper.Configuration;
    using System;
    using TransformCsvFormat.Converters;
    using TransformCsvFormat.Infrastructure;
    using TransformCsvFormat.Models;

    /// <summary>
    /// Maps StandardFormat class to custom without headers CSV fields.
    /// </summary>
    public sealed class CustomMapWithoutHeader : ClassMap<StandardFormat>, IFormatMapping
    {
        /// <summary>
        /// Called automatically by ClassMap class to set CSV fields mapping.
        /// </summary>
        public CustomMapWithoutHeader()
        {
            MapFields();
        }

        /// <summary>
        /// Map CSV fields to StandardFormat class fields using CSV field indexes.
        /// </summary>
        public void MapFields()
        {
            Map(record => record.OpenDate).Ignore();
            Map(record => record.Name).Index(0).Validate
                (
                field =>
                {
                    if (string.IsNullOrWhiteSpace(field))
                    {
                        throw new ArgumentException($"Account Name CSV field is missing.", "CustomMapWithoutHeader:Name");
                    }

                    return true;
                }
                );
            Map(record => record.Type).Index(1).TypeConverter<AccountTypeConverter<AccountType>>(); ;
            Map(record => record.Currency).Index(2).TypeConverter<CurrencyFormatConverter<string>>();
            Map(record => record.AccountCode).Index(3).Validate
                (
                field =>
                {
                    if (string.IsNullOrWhiteSpace(field))
                    {
                        throw new ArgumentException($"Account Code CSV field is missing.", "CustomMapWithoutHeader:AccountCode");
                    }

                    return true;
                }
                );
        }
    }
}
