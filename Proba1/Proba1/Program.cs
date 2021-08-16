using System;

namespace Proba1
{
    class Program
    {
        static void Main(string[] args)
        {
            double sirina, duzina, povrsina;
            string s, d;
            Console.WriteLine("Unesi sirinu: ");
            s = Console.ReadLine();
            sirina = double.Parse(s);
            Console.WriteLine("Unesi duzinu: ");
            d = Console.ReadLine();
            duzina = double.Parse(d);


            Pravokutnik r = new Pravokutnik();
            r.height = duzina;
            r.width = sirina;
            Console.WriteLine($"povrsina 1. pravokutnika: { r.area()}");
            Console.WriteLine($"opseg 1. pravokutnika: {r.perimeter()}");
            double a = 4;
            double b = 8;
            Pravokutnik r1 = new Pravokutnik(a,b);
            Console.WriteLine($"povrsina 2. pravokutnika: { r1.area()}");
            Console.WriteLine($"opseg 2. pravokutnika: {r1.perimeter()}");

            Console.WriteLine("_____________");

            ipravokutnik ip = new ipravokutnik();
            ip.Name();

            T[] array<T> (T a, T b, T c, T d)
            {
                return new T[] { a, b, c, d };
            }

            array<double>(sirina, duzina, a, b);
            
            
        }

    }

    class Objekt
    {
        public double height;
        public double width;

        public Objekt(double a = 0, double b = 0)
        {
            height = a;
            width = b;
        }

        /*public void area(double height, double width)
        {
            double compute;
            compute = height * width;
            Console.WriteLine("Povrsina je " + compute.ToString());
        }*/
        public virtual double area()
        {
            //Console.WriteLine("Povrsina objekta: ");
            return 0;
        }
        public virtual double perimeter()
        {
            //Console.WriteLine("Povrsina objekta: ");
            return 0;
        }
    }

    class Pravokutnik : Objekt
    {
        public Pravokutnik(double a = 0, double b = 0) : base(a, b) { }


        public override double area()
        {
            //Console.WriteLine("pravokutnik ");
            return width * height;


        }
        public override double perimeter()
        {
            return 2 * width + 2 * height;
        }

    }


    interface IPravokunik
    {
        void Name(); 
    }
    class ipravokutnik : IPravokunik
    {
        public void Name()
        {
            Console.WriteLine("Objekt: pravokutnik.");
        }
    }
}