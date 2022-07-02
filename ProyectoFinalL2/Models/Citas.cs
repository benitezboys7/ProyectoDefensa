using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinalL2.Models
{
    public class Citas
    {
        [Key]
        public int CitasID { get; set; }
        [Display(Name = "Nombre Cliente")]
        [Required]
        public int ClienteID { get; set; }
        public virtual Cliente Clientes { get; set; }
        [Required]
        public int MascotasID { get; set; }

        public virtual Mascotas Mascotas{ get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Reserva")]
        [Required]
        public DateTime FechaReserva { get; set; }
        








    }
}