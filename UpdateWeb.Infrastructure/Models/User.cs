﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateWeb.Infrastructure.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public int Area { get; set; }
        public int City { get; set; }
        public int District { get; set; }
        public int School { get; set; }
        public string Khoa { get; set; }
        public string Lop { get; set; }
        public string Class { get; set; }
        public string Status { get; set; }
    }
}
