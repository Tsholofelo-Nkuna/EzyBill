using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzyBill.DAL.Entities
{
    public class DataSourceEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string TypeCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Abbreviation {  get; set; } = string.Empty; 
        public int Value { get; set; }
    }
}
