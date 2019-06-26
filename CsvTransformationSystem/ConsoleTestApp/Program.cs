namespace ConsoleTestApp
{
    using System.Configuration;
    using TransformCsvFormat;
    using TransformCsvFormat.Mappings;

    internal class Program
    {
        private static void Main(string[] args)
        {
            // Standard to standard with validation
            FormatTransformer<StandardMap> standardTransformer = new FormatTransformer<StandardMap>();
            standardTransformer.TrnasformFile();

            // Source account file #1 with header transformation
            FormatTransformer<CustomMapWithHeader> customHeaderTransformer =
                new FormatTransformer<CustomMapWithHeader>(ConfigurationManager.AppSettings["CustomFormatWithHeaderFileSourcePath"], ConfigurationManager.AppSettings["StandardFileOutputPath"]);

            customHeaderTransformer.TrnasformFile();

            // Source account file #2 without header transformation
            FormatTransformer<CustomMapWithoutHeader> customWithoutHeaderTransformer =
                new FormatTransformer<CustomMapWithoutHeader>(ConfigurationManager.AppSettings["CustomFormatWithoutHeaderFileSourcePath"], ConfigurationManager.AppSettings["StandardFileOutputPath"]);

            customWithoutHeaderTransformer.TrnasformFile();

        }
    }
}
