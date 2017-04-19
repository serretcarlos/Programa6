using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programa6
{
    //&p-Programa
    //&b=7
    class Program
    {
        //&i    
        static void Main(string[] args)
        {
            string archivo;
            archivo = Console.ReadLine();
            Controlador controlador = new Controlador();
            controlador.ProcesarArchivo(archivo);
            Console.ReadLine();
        }
    }
}
