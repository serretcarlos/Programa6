using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programa6
{
    //&p-Archivo
    //&b=40
    class Archivo
    {
        private double parejas = 0;
        private double xk;
        private double rxy;
        private double r2;
        private double b0;
        private double b1;
        private double yk;
        private double sig;
        private double rango;
        private double ls;
        private double li;

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
            sig = 0;
            rango = 0;
            li = 0;
            ls = 0;
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
        public double Sig
        {
            set { sig = value; }
            get { return sig; }
        }

        //&i
        public double Rango
        {
            set { rango = value; }
            get { return rango; }
        }

        //&i

        public double Li
        {
            set { li = value; }
            get { return li; }
        }

        //&i
        public double Ls
        {
            set { ls = value; }
            get { return ls; }
        }

        //&i
        public void toString()
        {
            Console.WriteLine("N = " + parejas + "\nxk = " + xk.ToString("N5") + "\nr = " + rxy.ToString("N5") + "\nr2 = " +
                r2.ToString("N5") + "\nb0 = " + b0.ToString("N5") + "\nb1 = " + b1.ToString("N5") + "\nyk = " + yk.ToString("N5") +
                "\nsig = " + sig.ToString("N10") + "\nran = " + rango.ToString("N5") + "\nLS = " + ls.ToString("N5") + "\nLI = " + li.ToString("N5"));//&m
        }
    }
}
