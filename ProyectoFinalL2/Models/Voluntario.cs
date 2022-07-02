using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinalL2.Models
{
    public class Voluntario
    {
        [Key]

        public int VoluntarioID { get; set; }

        [Display(Name = "Ingrese su nombre")]
        [Required]
        public string NombreV { get; set; }

        [Display(Name = "Ingrese su cedula")]
        [Required]
        public int CedulaV { get; set; }

        [Display(Name = "Ingrese su correo")]
        [Required]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
        ErrorMessage = "Please enter correct email address")]
        public string Correo { get; set; }

        [Display(Name = "Seleccione una mascota")]
        [Required]
        public int MascotasID { get; set; }

        public virtual Mascotas Mascotas { get; set; }


    }
}