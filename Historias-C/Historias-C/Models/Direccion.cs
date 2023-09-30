namespace Historias_C.Models
{ 
public class Direccion
{
	public int Id { get; set; }
	public string calle { get; set; }
	public string altura { get; set; }
	public string barrio { get; set; }
	public string ciudad { get; set; }
    

	public Direccion(string calle, string altura, string barrio, string ciudad)
	{

		this.calle = calle;
		this.altura = altura;
		this.barrio = barrio;
		this.ciudad = ciudad;


	}




}

}