using EzyBill.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzyBill.DAL.Entities
{
    public class InvoiceEntity : BaseEntity
    {
        public CustomerEntity? Customer { get; set; }
        public DateTime? DueDate { get; set; }
        public double PaidAmount { get; set; }
        public double AmountDue  { get; set; }

        [NotMapped]
        public List<ProductEntity>? Items { get; set; } //read from X_InvoiceProducts table
        
    }
}
