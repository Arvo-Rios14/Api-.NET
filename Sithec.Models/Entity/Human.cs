using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sithec.Models.Entity
{
    public class Human
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "El nombre debe contener 50 caracteres como máximo."), Required(ErrorMessage = "El nombre no puede ser un dato nulo"), MaxLength(50)]
        public string Nombre { get; set; }
        [StringLength(20, ErrorMessage = "El sexo debe contener 20 caracteres como máximo."), Required(ErrorMessage = "El sexo no puede ser un dato nulo"), MaxLength(20)]
        public string Sexo { get; set; }
        [Required(ErrorMessage = "La edad no puede ser un dato nulo")]
        public int Edad { get; set; }
        public float? Altura { get; set; }
        public float? Peso { get; set; }
    }
}
