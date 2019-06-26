namespace TransformCsvFormat.Converters
{
    using CsvHelper;
    using CsvHelper.Configuration;
    using CsvHelper.TypeConversion;

    /// <summary>
    /// Parse out Account Code as per business need.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class AccountCodeConverter<T> : DefaultTypeConverter where T : class
    {
        /// <summary>
        /// Validate and extract account code from composite CSV Identifier field.Throw exception if not valid.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="row"></param>
        /// <param name="memberMapData"></param>
        /// <returns></returns>
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new CsvHelper.MissingFieldException(row.Context, $"Identifier field value is missing in record [{row.Context.RawRecord}] at row number '{row.Context.RawRow.ToString()}'.");
            }

            //Split to separate account code from numeric id contained in CSV Identifier field.
            var splittedCode = text.Split('|');

            //validate if exactly two values formed Identifier field.
            if (splittedCode.Length != 2)
            {
                throw new CsvHelper.FieldValidationException(row.Context, "Identifier", $"Malformed Identifier field :[{text}] in record [{row.Context.RawRecord}] at row number '{row.Context.RawRow.ToString()}'.");
            }
            else
            {
                return splittedCode[1];
            }
        }

    }
}
