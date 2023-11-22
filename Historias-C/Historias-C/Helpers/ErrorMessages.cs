

namespace Historias_C.Models
{
    public class ErrorMessages
    {
        //MSG: Epicrisis
        public const string _reqStrMinMax = "Debe tener entre {2} y {1} caracteres.";
        //MSG: Diagnostico
        public const string _reqMsg = "Campo requerido, porfavor ingrese datos.";
        public const string _reqRange = "Debe tener entre {1} y {2} caracteres.";
        public const string MatriculaExistente = "Matricula existente";
        public const string DNIExistente = "DNI existente";


    }

}











//TO-DO: Cada uno en su clase deberá implementar el mensaje de error correspondiente. Usar PUBLIC CONST ( Juntar todos los error messages aca / dividirlos por seccion )
// Ejemplo: Utilizarlo dentro del parametro ( ErrorMessage = ErrorMessages._ReqStringMinMax ) 

