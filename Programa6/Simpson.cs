using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programa6
{
    //&p-Simpson
    //&b=28
    class Simpson
    {
        private double x;       //rango que se calculará ( 0 a x )
        private double dof;     //grado de libertad
        private double num_seg; //número de segmentos
        private double width;   //ancho del segmento

        //&i
        public Simpson()
        {
            x = 0;
            dof = 0;
            num_seg = 0;
            width = 0;
        }
        //&i
        public Simpson(double x, double dof, double num_seg)
        {
            this.x = x;
            this.dof = dof;
            this.num_seg = num_seg;
            this.width = this.x / this.num_seg;
        }
        //&i
        public double FuncionP()
        {
            double dXi, resultado;
            int multiplier;
            dXi = 0;
            resultado = 0;
            Distribucion distribucion;

            for (int i = 0; i <= num_seg; i++)
            {
                distribucion = new Distribucion(dXi, dof);

                if (i == 0 || i == num_seg)
                {
                    multiplier = 1;
                }
                else if (i % 2 == 0)
                {
                    multiplier = 2;
                }
                else
                {
                    multiplier = 4;
                }
                resultado = resultado + (((width / 3) * multiplier * distribucion.FuncionF()));
                dXi = dXi + width;
            }
            return resultado;
        }

    }
}
