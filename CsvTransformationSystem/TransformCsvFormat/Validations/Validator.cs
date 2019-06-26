namespace TransformCsvFormat.Validations
{
    using CsvHelper;
    using TransformCsvFormat.Models;

    /// <summary>
    /// Define methods for CSV fields validations.
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Validate StandardFormat CSV file header for specified CsvReader pointing to defined mapping format.
        /// </summary>
        /// <param name="csvReader"></param>
        public static void ValidateStandardHeader(CsvReader csvReader)
        {
            //csvReader.Configuration.DetectColumnCountChanges = true;
            csvReader.Configuration.HasHeaderRecord = true;
            csvReader.Read();
            csvReader.ReadHeader();
            csvReader.ValidateHeader(typeof(StandardFormat));
        }
    }
}
