using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace THD.Domain.Entities
{
    public class Holdings: BaseEntity
    {
        [ForeignKey(nameof(Client))]
        public string ClientId { get; set; }
        public string Symbol { get; set; }
        public int Shares { get; set; }
        public int PurPrice { get; set; }
        public DateTime PurDate { get; set; }
        public virtual Client Client { get; set; }
    }
}
