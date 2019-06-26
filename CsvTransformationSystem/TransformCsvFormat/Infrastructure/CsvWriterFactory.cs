namespace TransformCsvFormat.Infrastructure
{
    using CsvHelper;
    using System.IO;

    /// <summary>
    /// Creates CsvWriter class instance associated with StreamWriter class.
    /// </summary>
    public class CsvWriterFactory : Disposable
    {
        #region Private Fields

        private StreamWriter streamWriter;
        private CsvWriter csvWriter;
        private readonly string outputFilePath;

        #endregion

        /// <summary>
        /// Initialize new instance of CsvWriterFactory class for the specified input file name.
        /// </summary>
        /// <param name="outputFilePath"></param>
        public CsvWriterFactory(string outputFilePath)
        {
            this.outputFilePath = outputFilePath;
            streamWriter = new StreamWriter(this.outputFilePath, true);
        }

        /// <summary>
        /// Return CsvWriter class instance associated with StreamWriter class.
        /// </summary>
        /// <returns></returns>
        public CsvWriter GetInstance()
        {
            return csvWriter = new CsvWriter(streamWriter);
        }

        /// <summary>
        /// Dispose CsvWriter and StreamWriter instances.
        /// </summary>
        protected override void DisposeCore()
        {
            if (csvWriter != null)
            {
                csvWriter.Dispose();
            }

            if (streamWriter != null)
            {
                streamWriter.Dispose();
            }
        }

    }
}