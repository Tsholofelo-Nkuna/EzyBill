using EzyBill.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzyBill.DAL.Entities
{
    [Table("X_InvoiceProductMap")]
    public class InvoiceProductEntity : BaseEntity
    {
        public Guid InvoiceId { get; set; }

        public Guid ProductId { get; set; }
    }
}
