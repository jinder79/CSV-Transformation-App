namespace TransformCsvFormat.Infrastructure
{
    /// <summary>
    /// Implement this for format mapping classes.
    /// </summary>
    public interface IFormatMapping
    {
        /// <summary>
        /// Define fields mapping for CSVHelper.ClassMap derived classes.
        /// </summary>
        void MapFields();
    }
}
