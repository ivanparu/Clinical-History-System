using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Historias_C.Models
{
    public class Paciente : Persona
    {
        [Required]
        public ObraSocial ObraSocial { get; set; }
        
        [Required]
        public HistoriaClinica HistoriaClinica { get; set; }

        [ForeignKey("HistoriaClinica")]
        public int HistoriaClinicaId { get; set; }
       

        public Paciente(string nombre, string apellido, int dni, int telefono, Direccion direccion, int id, ObraSocial obraSocial) : base(
             nombre, apellido, dni, telefono, direccion, id)
        {
            ObraSocial = obraSocial;
            HistoriaClinica = new HistoriaClinica();
        }
        public Paciente() 
        {
            HistoriaClinica = new HistoriaClinica();
        }


    }
}
