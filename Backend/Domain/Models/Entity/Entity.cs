using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Entity
{
    public abstract class Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        public Entity()
        {
            _id = Guid.NewGuid();
            if (CreatedAt == null)
            {
                CreatedAt = DateTime.Now;
            }
            else
            {
                UpdatedAt = DateTime.Now;
            }

        }

        /// <summary>
        /// The _id
        /// </summary>
        private Guid _id;
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public virtual Guid Id
        {
            get
            {
                if (Guid.Empty == _id)
                {
                    _id = Guid.NewGuid();
                }
                return _id;
            }
            set { _id = value; }
        }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        public DateTime? CreatedAt { get; set; }
        /// <summary>
        /// Gets or sets the updated at.
        /// </summary>
        /// <value>
        /// The updated at.
        /// </value>
        public DateTime? UpdatedAt { get; set; }
        /// <summary>
        /// Gets or sets the deleted at.
        /// </summary>
        /// <value>
        /// The deleted at.
        /// </value>
        public DateTime? DeletedAt { get; set; }
        /// <summary>
        /// Gets or sets the deleted at.
        /// </summary>
        /// <value>
        /// The deleted at.
        /// </value>
        public Guid? CreatedBy { get; set; }
        /// <summary>
        /// Gets or sets the deleted at.
        /// </summary>
        /// <value>
        /// The deleted at.
        /// </value>
        public Guid? UpdatedBy { get; set; }
        /// <summary>
        /// Gets or sets the deleted at.
        /// </summary>
        /// <value>
        /// The deleted at.
        /// </value>
        public string? CreatedByName { get; set; }
        /// <summary>
        /// Gets or sets the deleted at.
        /// </summary>
        /// <value>
        /// The deleted at.
        /// </value>
        public string? UpdatedByName { get; set; }
    }
}
