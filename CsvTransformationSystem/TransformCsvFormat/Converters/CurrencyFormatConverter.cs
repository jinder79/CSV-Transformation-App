namespace TransformCsvFormat.Converters
{
    using CsvHelper;
    using CsvHelper.Configuration;
    using CsvHelper.TypeConversion;

    /// <summary>
    /// Convert CSV currency fields to standard currency format like 'CAD'.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class CurrencyFormatConverter<T> : DefaultTypeConverter where T : class
    {
        /// <summary>
        /// Validate and convert CSV currency fields to standard currency format. Throw exception if not valid.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="row"></param>
        /// <param name="memberMapData"></param>
        /// <returns></returns>
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new CsvHelper.MissingFieldException(row.Context, $"Currency field value is missing in record [{row.Context.RawRecord}] at row number '{row.Context.RawRow.ToString()}'.");
            }

            if (text.ToUpper().Equals("U") || text.ToUpper().Equals("US") || text.ToUpper().Equals("USD"))
            {
                return "USD";
            }
            else if (text.ToUpper().Equals("C") || text.ToUpper().Equals("CD") || text.ToUpper().Equals("CAD"))
            {
                return "CAD";
            }
            else
            {
                throw new CsvHelper.FieldValidationException(row.Context, "Currency", $"Malformed Currency field :[{text}] in record [{row.Context.RawRecord}] at row number '{row.Context.RawRow.ToString()}'.");
            }
        }
    }
}
