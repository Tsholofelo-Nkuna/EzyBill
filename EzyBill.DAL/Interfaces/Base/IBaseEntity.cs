using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzyBill.DAL.Interfaces.Base
{
    public interface IBaseEntity 
    {
        Guid Id { get; set; }
        DateTime CreatedOn { get; set; }
        Guid CreatedBy { get; set; }
        DateTime? ModifiedOn { get; set; }
        Guid? ModifiedBy { get; set; }
        bool IsDeleted { get; set; }
    }
}
