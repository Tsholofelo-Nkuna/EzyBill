using EzyBill.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzyBill.DAL.Entities
{
    public class CustomerEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string Email {  get; set; } = string.Empty ;
        public string Phone { get; set; } = string.Empty;
        public AddressEntity? Address { get; set; }

    }
}
