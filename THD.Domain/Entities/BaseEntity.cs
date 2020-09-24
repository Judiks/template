using System;
using System.ComponentModel.DataAnnotations;

namespace THD.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public string Id { get; set; }
        public DateTime CreationDate { get; set; }

        public BaseEntity()
        {
            CreationDate = DateTime.UtcNow;
            Id = Guid.NewGuid().ToString();
        }
    }
}
