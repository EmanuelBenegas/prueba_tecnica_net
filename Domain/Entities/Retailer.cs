using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Retailer
    {
        public int Id { get; set; }

        public string CodingScheme { get; set; }

        public string Country { get; set; }

        public string ReName {  get; set; }

        public string ReCode { get; set; }
    }
}
