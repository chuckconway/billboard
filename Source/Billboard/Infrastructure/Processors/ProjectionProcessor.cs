using Billboard.Data.Access;

namespace Billboard.Infrastructure.Processors
{
    public class ProjectionProcessor : IProjectionProcessor
    {
        private readonly IInjection _injection;

        /// <summary> Constructor. </summary>
        /// <param name="injection"> The injection. </param>
        public ProjectionProcessor(IInjection injection)
        {
            _injection = injection;
        }

        /// <summary>
        /// Process the given args.
        /// </summary>
        /// <typeparam name="TParameters">The type of the T parameters.</typeparam>
        /// <typeparam name="TReturn">Type of the return.</typeparam>
        /// <param name="args">The arguments.</param>
        /// <returns>.</returns>
        public TReturn Process<TParameters, TReturn>(TParameters args) where TParameters : IProjection
        {
            var commandHandler = _injection.Get<IProjectionHandler<TParameters, TReturn>>();
            return commandHandler.Process(args);
        }
    }
}