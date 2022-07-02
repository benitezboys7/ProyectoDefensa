using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ProyectoFinalL2.Models
{
    public class Veterinario
    {

        [Key]
        public int VeterinarioID { get; set; }

        [Required]
        [Display(Name = "Nombre Veterinario")]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }

        [Display(Name = "Edad")]
        public int Edad { get; set; }

        [Display(Name = "Cedula")]
        [Required]
        public int Cedula { get; set; }

        [Display(Name = "Telefono")]
        [Required]
        public int Telefono { get; set; }

        public virtual ICollection<Mascotas> Mascotas { get; set; }
       
        public virtual ICollection<Veterinario> Veterinarios { get; set; }

    }
}