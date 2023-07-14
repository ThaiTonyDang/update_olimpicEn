﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateWeb.Domain.ViewModels
{
    public class SchoolItemViewModel
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string SchoolType { get; set; }
    }
}
