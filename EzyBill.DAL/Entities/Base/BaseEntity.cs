using EzyBill.DAL.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzyBill.DAL.Entities.Base
{
    public class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public Guid CreatedBy { get; set; }
        public bool IsDeleted { get ; set; } = false;
        public DateTime? ModifiedOn { get; set; } = null;
        public Guid? ModifiedBy { get; set; } = null;
    }
}
