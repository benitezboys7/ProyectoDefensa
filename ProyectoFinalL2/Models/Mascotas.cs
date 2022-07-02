using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinalL2.Models
{
    public class Mascotas
    {

        [Key]
        public int MascotasID { get; set; }
        [Required]
        public string Nombre { get; set; }
        public int Edad { get; set; }

        [Required]
        public string Sexo { get; set; }
        [Required]
        public string Tamano { get; set; }

        public string Area { get; set; }


        public virtual ICollection<Citas> Citas { get; set; }
        public virtual ICollection<Voluntario> Voluntarios { get; set; }
        public virtual ICollection<Veterinario> Veterinarios { get; set; }
    }
}