using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Historias_C.Models
{
    public enum Especialidad
    {

        [Display(Name = "Alergia e inmunologia")] ALERGIA_E_INMUNOLOGIA,
        [Display(Name = "Anatomia patologica")]ANATOMIA_PATOLOGICA,
        [Display(Name = "Cardiologia")] CARDIOLOGIA,
        [Display(Name = "Cirugia general")] CIRUGIA_GENERAL,
        [Display(Name = "Cirugia plástica")] CIRUGIA_PLASTICA,
        [Display(Name = "Cuidados intensivos")] CUIDADOS_INTENSIVOS,
        [Display(Name = "Dermatología")] DERMATOLOGIA,
        [Display(Name = "Diabetología")] DIABETOLOGIA,
        [Display(Name = "Ecografía y ecodoppler")] ECOGRAFIA_Y_ECODOPPLER,
        [Display(Name = "Ginecología")] GINECOLOGIA,
        [Display(Name = "Guardia médica")] GUARDIA_MEDICA,
        [Display(Name = "Laboratorio")] LABORATORIO,
        [Display(Name = "Neumonología")] NEUMONOLOGIA,
        [Display(Name = "Odontología")] ODONTOLOGIA,
        [Display(Name = "Oftalmología")] OFTALMOLOGIA,
        [Display(Name = "Rehabilitación")] REHABILITACION,
        [Display(Name = "Urología")] UROLOGIA

    }
}
