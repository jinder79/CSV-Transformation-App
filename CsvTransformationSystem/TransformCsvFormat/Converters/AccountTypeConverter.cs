namespace TransformCsvFormat.Converters
{
    using CsvHelper;
    using CsvHelper.Configuration;
    using CsvHelper.TypeConversion;
    using System;
    using TransformCsvFormat.Models;

    /// <summary>
    /// Maps account numbers to account names and vice versa.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class AccountTypeConverter<T> : DefaultTypeConverter where T : struct
    {
        /// <summary>
        /// Validate and parse out account types. Throw exception if not valid.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="row"></param>
        /// <param name="memberMapData"></param>
        /// <returns></returns>
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new CsvHelper.MissingFieldException(row.Context, $"Account type field value is missing in record [{row.Context.RawRecord}] at row number '{row.Context.RawRow.ToString()}'.");
            }
            T result;

            if (Enum.TryParse(text, out result) && Enum.IsDefined(typeof(AccountType), result))
            {
                return result;
            }

            throw new CsvHelper.FieldValidationException(row.Context, "Account Type", $"Malformed Account type field :[{text}] in record [{row.Context.RawRecord}] at row number '{row.Context.RawRow.ToString()}'.");

            //throw new InvalidCastException(String.Format("Invalid value to EnumConverter. Type: {0} Value: {1}", typeof(TEnum), text));
        }
    }
}
