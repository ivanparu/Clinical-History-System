using Historias_C.Data;
using Historias_C.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Historias_C
{ using Historias_C;
    public class Program
    {
        public static void Main(string[] args)
        {

            var app = Startup.InicializarApp(args);
      
            


          

            app.Run();
        }
    }
}