using EzyBill.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzyBill.DAL.Entities
{
    public class AddressEntity : BaseEntity
    {
        public string BuildingNumber { get; set; } = string.Empty;
        public string StreetName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip5 { get; set; } = string.Empty;
        public string Zip4 {  get; set; } = string.Empty;
        public int AddressType { get; set; }
        
    }
}
