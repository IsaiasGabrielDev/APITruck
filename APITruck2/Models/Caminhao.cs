using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APITruck.Models
{
    [Table("Caminhoes")]
    public class Caminhao
    {
        [Key()]
        public int Id { get; set;}    
        public int AnoFabricacao { get; set; }
        public int AnoModelo { get; set; }
        public ModeloNome NomeModelo { get; set; }

    }
}
