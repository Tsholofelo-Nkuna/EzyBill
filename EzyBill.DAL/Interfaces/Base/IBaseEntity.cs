using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzyBill.DAL.Interfaces.Base
{
    public interface IBaseEntity<TUserKey> 
    {
        Guid Id { get; set; }
        DateTime CreatedOn { get; set; }
        TUserKey CreatedBy { get; set; }
        DateTime? ModifiedOn { get; set; }
        TUserKey? ModifiedBy { get; set; }
        bool IsDeleted { get; set; }
    }
}
