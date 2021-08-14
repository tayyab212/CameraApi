using CameraSearch.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyCsvParser.Mapping;

namespace CameraSearch.Models
{
    public class CsvUserDetailsMapping : CsvMapping<UserDetails>
    {
        public CsvUserDetailsMapping()
            : base()
        {
            MapProperty(0, x => x.ID);
            //MapProperty(1, x => x.Name);
            //MapProperty(2, x => x.City);
            //MapProperty(3, x => x.Country);
        }
    }

    public class UserDetails
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
