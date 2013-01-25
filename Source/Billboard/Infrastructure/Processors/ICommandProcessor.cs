using Billboard.Data.Access;

namespace Billboard.Infrastructure.Processors
{
    public interface ICommandProcessor
    {
        /// <summary>
        /// Processes the specified command.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command">The command.</param>
        void Execute<T>(T command) where T : ICommand;
    }
}
