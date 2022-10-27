using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APITruck2.Models
{
    [Table("Modelos")]
    public class Modelo
    {
        [Key()]
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
