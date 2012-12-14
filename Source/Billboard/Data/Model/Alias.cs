namespace Billboard.Data.Model
{
    public class Alias
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>The number.</value>
        public virtual string Number { get; set; }

        /// <summary>
        /// Gets or sets the event id.
        /// </summary>
        /// <value>The event id.</value>
        public virtual Event Event { get; set; }
    }
}
