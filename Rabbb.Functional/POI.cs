namespace Rabbb.Functional
{
    /// <summary>
    /// An function result that has true result when succeed, or false result when failed. <br/>2022-5-9 10:17:17 Ciaran
    /// </summary>
    /// <typeparam name="T">True result type.</typeparam>
    /// <typeparam name="F">False result type.</typeparam>
    public class POI<T , F>
    {
        /// <summary>
        /// True result.
        /// </summary>
        public T? Resolve { get; set; }

        /// <summary>
        /// False result.
        /// </summary>
        public F? Reject { get; set; }

        /// <summary>
        /// Neither True result or False result, it's a program exception. <br/>2022-5-9 10:17:17 Ciaran
        /// </summary>
        public Exception? Exception { get; set; }
    }
}