using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzyBill.BLL.Models.Auth
{
    public class JwtTokenOptions
    {
        public int RefreshTokenTimeSpanMinutes { get; set; }
        public int TokenTimeSpanMinutes { get;}
    }
}
