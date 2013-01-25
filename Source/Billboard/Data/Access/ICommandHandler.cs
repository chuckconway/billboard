namespace Billboard.Data.Access
{
    public interface ICommandHandler<InType> where InType : ICommand
    {
        /// <summary>
        /// Handles the specified args.
        /// </summary>
        /// <param name="args">The args.</param>
        void Execute(InType args);
    }
}
