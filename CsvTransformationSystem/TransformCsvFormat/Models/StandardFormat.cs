namespace TransformCsvFormat.Models
{
    using CsvHelper.Configuration.Attributes;

    /// <summary>
    /// Maps business Account Type names to numbers.
    /// </summary>
    public enum AccountType
    {
        Trading = 1, RRSP = 2, RESP = 3, Fund = 4
    }

    /// <summary>
    /// Encapsulate standard business CSV field names.
    /// </summary>
    public class StandardFormat
    {
        public string AccountCode { get; set; }
        public string Name { get; set; }
        public AccountType Type { get; set; }
        [Optional]
        public string OpenDate { get; set; }
        public string Currency { get; set; }
    }
}
