using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAndBeauty.Models
{
    [Table("google_maps")]
    public class Address
    {   
        [Column("Id")]
        public int Id { get; set; }

        [Column("latitude")]
        public double Latitude { get; set; }

        [Column("longtitude")]
        public double Longtitude { get; set; }

        [Column("address_name")]
        public string AddressName { get; set; }

        [Column("description")]
        public string Description { get; set; }

    }
}
