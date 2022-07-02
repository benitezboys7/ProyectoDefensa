using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ProyectoFinalL2.Models;



namespace ProyectoFinalL2.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteID { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }
        public int Edad { get; set; }
        [Required]

        public int EdadMascota { get; set; }
        [Required]
        public int Cedula { get; set; }

        public int Telefono { get; set; }
        [Required]
        public string Direccion { get; set; }

        public string Tamano { get; set; }

        public string Area { get; set; }

    
        public virtual ICollection<Citas> Citas { get; set; }
    }
}