namespace TransformCsvFormat
{
    using CsvHelper;
    using System;
    using System.Configuration;
    using TransformCsvFormat.Infrastructure;
    using TransformCsvFormat.Mappings;
    using TransformCsvFormat.Models;
    using TransformCsvFormat.Validations;

    /// <summary>
    /// Encapsulate tramsformation logic for business defined format mappings.
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    public sealed class FormatTransformer<TClass> where TClass : IFormatMapping
    {
        #region Private Fields

        private readonly string sourceFilePath;
        private readonly string outputFilePath;

        private CsvReaderFactory readerFactory;
        private CsvWriterFactory writerFactory;

        private CsvReader reader;
        private CsvWriter writer;

        #endregion

        #region Public Prperties

        public CsvReader Reader
        {
            get { return reader ?? (reader = readerFactory.GetInstance()); }
        }

        public CsvWriter Writer
        {
            get { return writer ?? (writer = writerFactory.GetInstance()); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize instance of FormatTransformer class for default config setting input and output file name.
        /// </summary>
        public FormatTransformer()
        {
            this.sourceFilePath = ConfigurationManager.AppSettings["StandardFileSourcePath"];
            this.outputFilePath = ConfigurationManager.AppSettings["StandardFileOutputPath"];
            readerFactory = new CsvReaderFactory(this.sourceFilePath);
            writerFactory = new CsvWriterFactory(this.outputFilePath);
        }

        /// <summary>
        /// Initialize instance of FormatTransformer class for the specified input and output file name.
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="outputFilePath"></param>
        public FormatTransformer(string sourceFilePath, string outputFilePath)
        {
            this.sourceFilePath = sourceFilePath;
            this.outputFilePath = outputFilePath;
            readerFactory = new CsvReaderFactory(this.sourceFilePath);
            writerFactory = new CsvWriterFactory(this.outputFilePath);
        }

        #endregion

        /// <summary>
        /// Read from specified source file, transform as per selected mapping format and write to specified output file.
        /// </summary>
        public void TrnasformFile()
        {
            try
            {
                SetReadWriteConfigurations();

                //Reader.Configuration.RegisterClassMap(typeof(TClass));
                var csvRecords = Reader.GetRecords<StandardFormat>();

                Writer.WriteRecords(csvRecords);
            }
            catch (Exception exception)
            {
                //Temporary for console unit testing, will be removed later on
                Console.WriteLine(exception.Message);

                //Even we can pool exceptions in a lsit object and later on dump into a file or generate a errors report.
                //if ( system exceptions or  FieldValidationException or MissingFieldException)
                //{
                //    ////Looger will be injected here to log exception information in log file.
                //}
            }
            finally
            {
                Reader.Configuration.UnregisterClassMap();
                Writer.Configuration.UnregisterClassMap();
                readerFactory.Dispose();
                writerFactory.Dispose();
                Reader.Dispose();
                Writer.Dispose();
            }
        }

        /// <summary>
        /// Configure read and write setting for constructor specified mapping format.
        /// </summary>
        private void SetReadWriteConfigurations()
        {
            //CsvReader.Configuration.IgnoreQuotes = false;
            Writer.Configuration.SanitizeForInjection = true;

            Reader.Configuration.HeaderValidated = (bool isValid, string[] headerNames, int headerNameIndex, ReadingContext context) =>
            {
                if (!isValid)
                {
                    //Looger will be injected here to log exception information in log file.

                    //Send a meaningful message to client application
                    throw new ArgumentException($"Header fields with names [{string.Join(",", headerNames)}] was not found in {context.RawRecord}.");
                }
            };

            Reader.Configuration.BadDataFound = (readingContext) =>
            {
                if (readingContext.IsFieldBad)
                {
                    //Looger will be injected here to log exception information in log file.

                    //Send a meaningful message to client application
                    throw new ArgumentException($"Bad field value [{readingContext.RawRecord}] found at row number {readingContext.Row.ToString()}.");
                }
            };

            Reader.Configuration.MissingFieldFound = (headerNames, columnIndex, readingContext) =>
            {
                // Check if exception raised for missing CSV field value, not for missing header-name
                if (headerNames == null || headerNames.Length == 0)
                {
                    //Looger will be injected here to log exception information in log file.

                    //Send a meaningful message to client application
                    throw new ArgumentException($"Field value at column index [{columnIndex}] is missing in [{readingContext.RawRecord}].");
                }
            };

            if (typeof(TClass) == typeof(StandardMap))
            {
                Validator.ValidateStandardHeader(Reader);
                Writer.Configuration.HasHeaderRecord = true;
            }
            else if (typeof(TClass) == typeof(CustomMapWithHeader))
            {
                Reader.Configuration.DetectColumnCountChanges = true;
                Reader.Configuration.HasHeaderRecord = true;
                Writer.Configuration.HasHeaderRecord = false;
            }
            else if (typeof(TClass) == typeof(CustomMapWithoutHeader))
            {
                Reader.Configuration.HasHeaderRecord = false;
                Writer.Configuration.HasHeaderRecord = false;
            }
            // Hook CSV mapper class to business defined map class
            Reader.Configuration.RegisterClassMap(typeof(TClass));
        }
    }
}
