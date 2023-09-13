﻿namespace Historias_C.Models
{
    public class Medico : Empleado
    {

        public string Matricula { get; set; }
        public string Especialidad { get; set; }
        public Medico(string nombre, string apellido, int dni, int telefono, string direccion,int id, string legajo, string matricula, string especialidad) : base(nombre, apellido, dni, telefono, direccion, id, legajo)
        {
            this.Matricula = matricula;
            this.Especialidad = especialidad;
        }



    }
}
