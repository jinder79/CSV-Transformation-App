namespace TransformCsvFormat.Converters
{
    using CsvHelper;
    using CsvHelper.Configuration;
    using CsvHelper.TypeConversion;
    using System;

    /// <summary>
    /// Convert CSV date fields to standard date format 'yyyy-MM-dd'.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class DateFormatConverter<T> : DefaultTypeConverter where T : class
    {
        /// <summary>
        /// Validate and convert CSV date fields to standard date format 'yyyy-MM-dd'. Throw exception if not valid.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="row"></param>
        /// <param name="memberMapData"></param>
        /// <returns></returns>
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new CsvHelper.MissingFieldException(row.Context, $"Opened Date field value is missing in record [{row.Context.RawRecord}] at row number '{row.Context.RawRow.ToString()}'.");
            }

            string[] formats = { "dd-MM-yyyy", "d-M-yyyy", "dd/MM/yyyy", "d/M/yyyy" };

            DateTime result;

            if (DateTime.TryParseExact(text, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result))
            {
                return result.ToString("yyyy-MM-dd");
            }
            else
            {
                throw new CsvHelper.FieldValidationException(row.Context, "Opened", $"Malformed Opened Date field :[{text}] in record [{row.Context.RawRecord}] at row number '{row.Context.RawRow.ToString()}'.");
            }
        }
    }
}
