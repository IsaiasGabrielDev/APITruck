using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APITruck2.Models
{
    [Table("Caminhoes")]
    public class Caminhao
    {
        [Key()]
        public int Id { get; set;}      
        [ForeignKey("Modelo")]
        public int ModeloId { get; set; }
        public virtual Modelo Modelo { get; set; }
        public int AnoFabricacao { get; set; }
        public int AnoModelo { get; set; }
        [NotMapped]
        public ModeloNome NomeModelo { get; set; }

    }
}
