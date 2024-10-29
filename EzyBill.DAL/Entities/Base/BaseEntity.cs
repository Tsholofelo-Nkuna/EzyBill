using EzyBill.DAL.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzyBill.DAL.Entities.Base
{
    public class BaseEntity : IBaseEntity<string>
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = string.Empty;
        public bool IsDeleted { get ; set; } = false;
        public DateTime? ModifiedOn { get; set; } = null;
        public string? ModifiedBy { get; set; } = null;
    }
}
