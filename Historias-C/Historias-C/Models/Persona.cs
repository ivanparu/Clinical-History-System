﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Historias_C.Models
{
    public class Persona : IdentityUser<int>
    {
        private const string _reqMsg = "El campo {0} es requerido.";

        private const string _reqRange = "El texto debe tener entre {2} y {1} caracteres.";
       // public int Id { get; set; }

        [Required(ErrorMessage = _reqMsg)]
        [StringLength(30, MinimumLength = 8, ErrorMessage = _reqRange)]
        public override string UserName
        {
            get { return base.UserName; }
            set { base.UserName = value; }
        }


        [Required(ErrorMessage = _reqMsg)]
        [EmailAddress]
        public override string Email { 
            get { return base.Email; }
            set { base.Email = value; } 
        }


        [Required(ErrorMessage = _reqMsg)]
        [DataType(DataType.Date)]
        [Display(Name ="Fecha de alta")]
        public DateTime FechaAlta { get; set; } = DateTime.Now;

        [Required(ErrorMessage = _reqMsg)]
        [StringLength(30, MinimumLength = 2, ErrorMessage = _reqRange)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = _reqMsg)]
        [StringLength(30, MinimumLength = 2, ErrorMessage = _reqRange)]
        public string Apellido { get; set; }


        [Required(ErrorMessage = _reqMsg)]
        [RegularExpression(@"^\d{6,14}$", ErrorMessage = "El DNI debe contener entre 6 y 14 dígitos.")]
        public int DNI { get; set; }

        [Required(ErrorMessage = _reqMsg)]
        [DataType(DataType.PhoneNumber)]
        public int Telefono { get; set; }

        //@TO DO: FALTA PONER en REQUIRE cuando se defina Direccon clase aparte.
      
        public Direccion Direccion { get; set; }


    }

}
