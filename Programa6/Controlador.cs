using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Programa6
{
    //&p-Parejas
    class Controlador
    {
        private Archivo archivo;
        private double sumXiYi;
        private double sumXi;
        private double sumYi;
        private double sumXi2;
        private double sumYi2;

        //&i
        public Controlador()
        {
            sumXiYi = 0;
            sumXi = 0;
            sumYi = 0;
            sumXi2 = 0;
            sumYi2 = 0;
        }


        /// <summary>
        /// Procesa el archivo y lee línea por línea de este, calculando totales correspondientes
        /// </summary>
        /// <param name="nombreArchivo"></param>
        //&i
        public void ProcesarArchivo(string nombreArchivo)
        {
            archivo = new Archivo();
            StreamReader entrada = null;
            bool bAbierto = false;
            try
            {
                entrada = File.OpenText(nombreArchivo);
                bAbierto = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (bAbierto)
            {
                try
                {
                    string sLinea = entrada.ReadLine();
                    archivo.Xk = double.Parse(sLinea);
                    sLinea = entrada.ReadLine();
                    while (sLinea != null)
                    {
                        string[] arrPareja = sLinea.Split(',');
                        sumXi += double.Parse(arrPareja[0]);
                        sumYi += double.Parse(arrPareja[1]);
                        sumXiYi += (double.Parse(arrPareja[0]) * double.Parse(arrPareja[1]));
                        sumXi2 += Math.Pow(double.Parse(arrPareja[0]), 2);
                        sumYi2 += Math.Pow(double.Parse(arrPareja[1]), 2);
                        archivo.Parejas++;
                        sLinea = entrada.ReadLine();
                    }
                    archivo.Rxy = CalcularRxy();
                    archivo.R2 = CalcularR2();
                    archivo.B1 = CalcularB1();
                    archivo.B0 = CalcularB0();
                    archivo.Yk = CalcularYk();
                    archivo.toString();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }


        /// <summary>
        /// Se calcula el valor de r2
        /// </summary>
        /// <returns>valor tipo doble r2</returns>
        //&i
        private double CalcularR2()
        {
            double r2 = archivo.Rxy * archivo.Rxy;
            return r2;
        }

        /// <summary>
        /// Se calcula el valor de rxy
        /// </summary>
        /// <returns>valor tipo doble rxy</returns>
        //&i
        private double CalcularRxy()
        {
            double mediaXi = sumXi / archivo.Parejas;
            double mediaYi = sumYi / archivo.Parejas;
            double covarianza = sumXiYi / archivo.Parejas - mediaXi * mediaYi;
            double desviacion1 = Math.Sqrt(sumXi2 / archivo.Parejas - (mediaXi * mediaXi));
            double desviacion2 = Math.Sqrt(sumYi2 / archivo.Parejas - (mediaYi * mediaYi));
            double rxy = covarianza / (desviacion1 * desviacion2);
            return rxy;
        }

        /// <summary>
        /// Se calcula el valor de b0
        /// </summary>
        /// <returns>valor tipo doble b0</returns>
        //&i
        private double CalcularB0()
        {
            return sumYi / archivo.Parejas - archivo.B1 * (sumXi / archivo.Parejas);
        }

        /// <summary>
        /// Se calcula el valor de b1
        /// </summary>
        /// <returns>valor tipo doble b1</returns>
        //&i
        private double CalcularB1()
        {
            return (sumXiYi - (archivo.Parejas * (sumXi / archivo.Parejas) * (sumYi / archivo.Parejas))) /
                (sumXi2 - (archivo.Parejas * ((sumXi / archivo.Parejas) * (sumXi / archivo.Parejas))));
        }

        /// <summary>
        /// Se calcula el valor de yk
        /// </summary>
        /// <returns>valor tipo doble yk</returns>
        //&i
        private double CalcularYk()
        {
            double yk = archivo.B0 + archivo.B1 * archivo.Xk;
            return yk;
        }

    }
}
