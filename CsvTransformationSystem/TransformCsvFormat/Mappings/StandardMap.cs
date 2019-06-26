namespace TransformCsvFormat.Mappings
{
    using CsvHelper.Configuration;
    using System;
    using TransformCsvFormat.Converters;
    using TransformCsvFormat.Infrastructure;
    using TransformCsvFormat.Models;

    /// <summary>
    /// Maps StandardFormat class to standard CSV fields.
    /// </summary>
    public sealed class StandardMap : ClassMap<StandardFormat>, IFormatMapping
    {
        /// <summary>
        /// Called automatically by ClassMap class to set CSV fields mapping.
        /// </summary>
        public StandardMap()
        {
            MapFields();
        }

        /// <summary>
        /// Map CSV fields to StandardFormat class fields using CSV field names.
        /// </summary>
        public void MapFields()
        {
            Map(record => record.AccountCode).Validate
                (
                    field =>
                    {
                        if (string.IsNullOrWhiteSpace(field))
                        {
                            throw new ArgumentException($"Account Code field is missing in StandardMapping format.", "StandardMap:AccountCode");
                        }

                        return true;
                    }
                );
            Map(record => record.Name).Validate
                (
                field =>
                {
                    if (string.IsNullOrWhiteSpace(field))
                    {
                        throw new ArgumentException($"Account Name field is missing in StandardMapping format.", "StandardMap:Name");
                    }

                    return true;
                }
                );
            Map(record => record.Type).TypeConverter<AccountTypeConverter<AccountType>>(); ;
            Map(record => record.OpenDate).Name("Open Date").Optional().TypeConverter<DateFormatConverter<string>>();
            Map(record => record.Currency).TypeConverter<CurrencyFormatConverter<string>>(); ;
        }
    }
}
