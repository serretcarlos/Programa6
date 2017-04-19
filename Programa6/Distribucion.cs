using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programa6
{
    //&p-Distribucion
    //&b=32
    class Distribucion
    {
        private double dX;
        private double dof;
        //&i
        public Distribucion()
        {
            dX = 0;
            dof = 0;
        }
        //&i
        public Distribucion(double dX, double dof)
        {
            this.dX = dX;
            this.dof = dof;
        }
        //&i
        public double FuncionF()
        {
            double resultado, dXi, dY, dZ, dA;
            resultado = 0;

            dXi = 1.0 * ((dof + 1) * 0.5);
            dA = FuncionGamma(dXi);
            dY = Math.Pow(dof * Math.PI, 0.5);
            dXi = 1.0 * (dof * 0.5);
            dZ = FuncionGamma(dXi);
            resultado = dA / (dY * dZ);
            dXi = 1.0 + (Math.Pow(dX, 2) / dof);
            dY = -1.0 * ((dof + 1) * 0.5);
            dA = Math.Pow(dXi, dY);
            resultado = resultado * dA;
            return resultado;
        }

        //&i
        public double FuncionGamma(double dx)
        {
            if (dx == 0)
            {
                return -1;
            }
            if (dx == 1.0)
            {
                return 1;
            }
            if (dx == 0.5)
            {
                return Math.Sqrt(Math.PI);
            }
            else
            {
                return (dx - 1) * FuncionGamma(dx - 1);
            }
        }
    }
}

