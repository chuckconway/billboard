namespace Billboard.Data.Access
{
    public interface IProjectionHandler<in InType, out OutType> where InType : IProjection
    {
        /// <summary>
        /// Handles the specified args.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns>`1.</returns>
        OutType Process(InType args);
    }
}
