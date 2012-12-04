namespace Billboard.Data.Model
{
    public class Timezone
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
        /// Gets or sets the offset hour.
        /// </summary>
        /// <value>The offset hour.</value>
        public virtual short OffsetHour { get; set; }

        /// <summary>
        /// Gets or sets the offset minutes.
        /// </summary>
        /// <value>The offset minutes.</value>
        public virtual short OffsetMinutes { get; set; }
    }
}
