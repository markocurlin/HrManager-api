using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace HRManager.APIv2.Models
{
    public class AuthContext
    {
        public List<SimpleClaim> Claims { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
