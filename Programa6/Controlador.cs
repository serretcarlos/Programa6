using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Programa6
{
    //&p-Parejas
    //&b=63
    class Controlador
    {
        private Archivo archivo;
        private double sumXiYi;
        private double sumXi;
        private double sumYi;
        private double sumXi2;
        private double sumYi2;
        private double sig;
        private double rango;
        private double ls;
        private double li;
        private List<Tuple<double, double>> pares;

        //&i
        public Controlador()
        {
            sumXiYi = 0;
            sumXi = 0;
            sumYi = 0;
            sumXi2 = 0;
            sumYi2 = 0;
            sig = 0;
            rango = 0;
            ls = 0;
            li = 0;
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
                    double sumx = 0, sumy = 0;
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
            double r2 = archivo.Rxy * archivo.Rxy;
            return r2;
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
            double rxy = covarianza / (desviacion1 * desviacion2);
            return rxy;
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
            double yk = archivo.B0 + archivo.B1 * archivo.Xk;
            return yk;
        }

        //&i
        private double calcularSig()
        {
            double x = Math.Abs(archivo.Rxy) * Math.Sqrt(archivo.Parejas - 2);
            x = x / (Math.Sqrt(1 - (archivo.Rxy * archivo.Rxy)));
            double p = calcularP(x, archivo.Parejas - 2);
            return 1.0 - (2.0 * p);
        }

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
            result = result * mean * dX;
            return result;
        }

        private double calcularP(double x, double dof)
        {
            int iNum_seg = 10;
            double dResta;
            double dE = 0.00000000001; // margen de error

            Simpson simAux = new Simpson(x, dof, iNum_seg);
            iNum_seg += 10;
            Simpson simResultado = new Simpson(x, dof, iNum_seg);
            dResta = simAux.FuncionP() - simResultado.FuncionP();
            while (Math.Abs(dResta) >= dE)
            {
                simAux = simResultado;
                iNum_seg += 10;
                simResultado = new Simpson(x, dof, iNum_seg);
                dResta = simAux.FuncionP() - simResultado.FuncionP();
            }
            return simResultado.FuncionP();
        }

        private double calcularX(double originalP, double dof)
        {
            double dE = 0.00000000001;
            double dX = 1.0;
            double dD = 0.5;
            double dP = calcularP(dX, dof);
            double resta = dP - originalP;
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
                resta = dP - originalP;
            }
            return dX;
        }

    }
}
