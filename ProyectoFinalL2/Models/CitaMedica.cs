using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinalL2.Models
{
    public class CitaMedica
    {

        [Key]
        public int CitaMedicaID { get; set; }
        [Display(Name = "Nombre Veterinario")]
        [Required]
        public int VeterinarioID { get; set; }
        public virtual Veterinario Veterinarios { get; set; }
        [Required]
        public int MascotasID { get; set; }

        public virtual Mascotas Mascotas { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Reserva")]
        [Required]
        public DateTime FechaReserva { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "^([0-9]|0[0-9]|1[0-9]|2[0-3]).[0-5][0-9]$", ApplyFormatInEditMode = true)]
        [Display(Name = "Hora")]
        [Required]
        public DateTime Hora { get; set; }
    }
}