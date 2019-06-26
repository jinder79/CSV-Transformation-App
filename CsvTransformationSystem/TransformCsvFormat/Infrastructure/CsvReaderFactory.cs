namespace TransformCsvFormat.Infrastructure
{
    using CsvHelper;
    using System.IO;

    /// <summary>
    /// 
    /// </summary>
    public sealed class CsvReaderFactory : Disposable
    {
        #region Private Fields

        private readonly StreamReader streamReader;
        private CsvReader csvReader;
        private readonly string sourceFilePath;

        #endregion

        /// <summary>
        /// Initialize new instance of CsvReaderFactory class for the specified input file name.
        /// </summary>
        /// <param name="sourceFilePath"></param>
        public CsvReaderFactory(string sourceFilePath)
        {
            this.sourceFilePath = sourceFilePath;
            this.streamReader = new StreamReader(sourceFilePath);
        }

        /// <summary>
        /// Return CsvReader class instance associated with StreamReader class.
        /// </summary>
        /// <returns></returns>
        public CsvReader GetInstance()
        {
            return this.csvReader = new CsvReader(streamReader);
        }

        /// <summary>
        /// Dispose CsvReader and StreamReader instances.
        /// </summary>
        protected override void DisposeCore()
        {
            if (csvReader != null)
            {
                csvReader.Dispose();
            }

            if (streamReader != null)
            {
                streamReader.Dispose();
            }
        }

    }
}
