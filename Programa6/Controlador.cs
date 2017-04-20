using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Programa6
{
    //&p-Controlador
    //&b=63
    class Controlador
    {
        private Archivo archivo;
        private double sumXiYi;
        private double sumXi;
        private double sumYi;
        private double sumXi2;
        private double sumYi2;
        private List<Tuple<double, double>> pares;

        //&i
        public Controlador()
        {
            sumXiYi = 0;
            sumXi = 0;
            sumYi = 0;
            sumXi2 = 0;
            sumYi2 = 0;
            pares = new List<Tuple<double, double>>();
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
                    Tuple<double, double> pair;
                    while (sLinea != null)
                    {
                        string[] arrPareja = sLinea.Split(',');
                        sumXi += double.Parse(arrPareja[0]);
                        sumYi += double.Parse(arrPareja[1]);
                        pair = new Tuple<double, double>(double.Parse(arrPareja[0]), double.Parse(arrPareja[1]));
                        pares.Add(pair);
                        sumXiYi += (double.Parse(arrPareja[0]) * double.Parse(arrPareja[1]));
                        sumXi2 += Math.Pow(double.Parse(arrPareja[0]), 2);
                        sumYi2 += Math.Pow(double.Parse(arrPareja[1]), 2);
                        archivo.Parejas++;
                        sLinea = entrada.ReadLine();
                    }
                    archivo.Rxy = calcularRxy();
                    archivo.R2 = calcularR2();
                    archivo.B1 = calcularB1();
                    archivo.B0 = calcularB0();
                    archivo.Yk = calcularYk();
                    archivo.Sig = calcularSig();
                    archivo.Rango = calcularRango();
                    archivo.Ls = archivo.Yk + archivo.Rango;
                    if ((archivo.Yk - archivo.Rango) <= 0)
                    {
                        archivo.Li = 0.0;
                    }
                    else
                    {
                        archivo.Li = archivo.Yk - archivo.Rango;
                    }
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
        private double calcularR2()
        {
            return archivo.Rxy * archivo.Rxy;
        }

        /// <summary>
        /// Se calcula el valor de rxy
        /// </summary>
        /// <returns>valor tipo doble rxy</returns>
        //&i
        private double calcularRxy()
        {
            double mediaXi = sumXi / archivo.Parejas;
            double mediaYi = sumYi / archivo.Parejas;
            double covarianza = sumXiYi / archivo.Parejas - mediaXi * mediaYi;
            double desviacion1 = Math.Sqrt(sumXi2 / archivo.Parejas - (mediaXi * mediaXi));
            double desviacion2 = Math.Sqrt(sumYi2 / archivo.Parejas - (mediaYi * mediaYi));
            return covarianza / (desviacion1 * desviacion2);
        }

        /// <summary>
        /// Se calcula el valor de b0
        /// </summary>
        /// <returns>valor tipo doble b0</returns>
        //&i
        private double calcularB0()
        {
            return sumYi / archivo.Parejas - archivo.B1 * (sumXi / archivo.Parejas);
        }

        /// <summary>
        /// Se calcula el valor de b1
        /// </summary>
        /// <returns>valor tipo doble b1</returns>
        //&i
        private double calcularB1()
        {
            return (sumXiYi - (archivo.Parejas * (sumXi / archivo.Parejas) * (sumYi / archivo.Parejas))) /
                (sumXi2 - (archivo.Parejas * ((sumXi / archivo.Parejas) * (sumXi / archivo.Parejas))));
        }

        /// <summary>
        /// Se calcula el valor de yk
        /// </summary>
        /// <returns>valor tipo doble yk</returns>
        //&i
        private double calcularYk()
        {
            return  archivo.B0 + archivo.B1 * archivo.Xk;
        }

        /// <summary>
        /// Calcula el valor de la significancia de correlación
        /// </summary>
        /// <returns>valor tipo doble</returns>
        //&i
        private double calcularSig()
        {
            double x = Math.Abs(archivo.Rxy) * Math.Sqrt(archivo.Parejas - 2);
            x = x / (Math.Sqrt(1 - (archivo.Rxy * archivo.Rxy)));
            double p = calcularP(x, archivo.Parejas - 2);
            return 1.0 - (2.0 * p);
        }

        /// <summary>
        /// Calcula el rango
        /// </summary>
        /// <returns>valor tipo doble</returns>
        //&i
        private double calcularRango()
        {
            double dX = calcularX(0.35, archivo.Parejas - 2);
            double sumY = 0;
            double sumX = 0;
            double avgX = 0;
            double Xk = archivo.Xk * 1.0;
            double N = archivo.Parejas * 1.0;
            double temp = 0;

            for (int i = 0; i < pares.Count; i++)
            {
                temp += pares[i].Item1;
            }
            avgX = temp / archivo.Parejas;

            for (int i = 0; i < pares.Count; i++)
            {
                double auxY, auxX;
                auxY = pares[i].Item2 - archivo.B0 - (archivo.B1 * pares[i].Item1);
                sumY += Math.Pow(auxY, 2);
                auxX = pares[i].Item1 - avgX;
                sumX += Math.Pow(auxX, 2);
            }

            double mean = 1 / (N - 2);
            mean = Math.Sqrt(mean * sumY);
            double result = Math.Pow(Xk - avgX, 2);
            result = result / sumX;
            result = result + (1 + (1 / N));
            result = Math.Sqrt(result);
            return result * mean * dX;
        }

        /// <summary>
        /// Se calcula la P 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="dof"></param>
        /// <returns>Valor tipo doble res</returns>
        //&i
        private double calcularP(double x, double dof)
        {
            int num_seg = 10;
            double resta;
            double dE = 0.00000000001; 

            Simpson simpsonA = new Simpson(x, dof, num_seg);
            num_seg += 10;
            Simpson simpsonB = new Simpson(x, dof, num_seg);
            resta = simpsonA.FuncionP() - simpsonB.FuncionP();
            while (Math.Abs(resta) >= dE)
            {
                simpsonA = simpsonB;
                num_seg += 10;
                simpsonB = new Simpson(x, dof, num_seg);
                resta = simpsonA.FuncionP() - simpsonB.FuncionP();
            } 
            return simpsonB.FuncionP();
        }

        /// <summary>
        /// Se calcula la x
        /// </summary>
        /// <param name="firstP"></param>
        /// <param name="dof"></param>
        /// <returns>valor tipo doble dx</returns>
        //&i
        private double calcularX(double firstP, double dof)
        {
            double dE = 0.00000000001;
            double dX = 1.0;
            double dD = 0.5;
            double dP = calcularP(dX, dof);
            double resta = dP - firstP;
            double dAux = resta;
            
            while (Math.Abs(resta) >= dE)
            {
                if (resta > 0)
                {   
                    dX -= dD;
                }
                else
                {  
                    dX += dD;
                }
                
                if ((dAux > 0 && resta < 0) || (dAux < 0 && resta > 0))
                {
                    dD = dD / 2;
                }
                
                dP = calcularP(dX, dof);
                dAux = resta;
                resta = dP - firstP;
            }
            return dX;
        }

    }
}
