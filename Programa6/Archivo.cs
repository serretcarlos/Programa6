using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programa6
{
    //&p-Archivo
    class Archivo
    {
        private double parejas = 0;
        private double xk;
        private double rxy;
        private double r2;
        private double b0;
        private double b1;
        private double yk;

        //&i
        public Archivo()
        {
            parejas = 0;
            xk = 0;
            rxy = 0;
            r2 = 0;
            b0 = 0;
            b1 = 0;
            yk = 0;
        }

        //&i
        public double Parejas
        {
            set { parejas = value; }
            get { return parejas; }
        }

        //&i
        public double Xk
        {
            set { xk = value; }
            get { return xk; }
        }

        //&i
        public double Rxy
        {
            set { rxy = value; }
            get { return rxy; }
        }

        //&i
        public double R2
        {
            set { r2 = value; }
            get { return r2; }
        }

        //&i
        public double B0
        {
            set { b0 = value; }
            get { return b0; }
        }

        //&i
        public double B1
        {
            set { b1 = value; }
            get { return b1; }
        }

        //&i
        public double Yk
        {
            set { yk = value; }
            get { return yk; }
        }

        //&i
        public void toString()
        {
            Console.WriteLine("N = " + parejas + "\nxk = " + xk + "\nr = " + Math.Round(rxy, 5) + "\nr2 = " +
                Math.Round(r2, 5) + "\nb0 = " + Math.Round(b0, 5) + "\nb1 = " + Math.Round(b1, 5) + "\nyk = " + Math.Round(yk, 5));
        }
    }
}
