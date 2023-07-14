using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateWeb.Domain.ViewModels
{
    public class CityItemViewModel
    {
        public int Id { get; set; }
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public string Name { get; set; }
    }
}
