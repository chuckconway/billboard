using Billboard.Data.Access;

namespace Billboard.Infrastructure.Processors
{
    public interface IProjectionProcessor
    {
        /// <summary>
        /// Processes the specified args.
        /// </summary>
        /// <typeparam name="TParameters">The type of the T parameters.</typeparam>
        /// <typeparam name="TReturn">The type of the T return.</typeparam>
        /// <param name="args">The args.</param>
        /// <returns>``1.</returns>
        TReturn Process<TParameters, TReturn>(TParameters args) where TParameters : IProjection;
    }
}
