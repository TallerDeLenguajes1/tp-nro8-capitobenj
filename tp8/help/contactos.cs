using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Help
{
    public class Empleado
    {
        public string nombre;
        public string apellido;
        public string FechadeNac;
        public int Edad;
        public string EC;   //Estado civíl
        public string Sexo;
        public string FIngreso; //Fecha de ingreso a la empresa
        public int Antig; //Antigüedad del empleado en la empresa
        public int Jubil; //Años faltantes para jubilarse
        public string SB; //Sueldo Básico
        public double Sueldo;
        public int hijos;
        public string Cargo;
        public int id;
        public void MostrarTag(List<Empleado> Agenda)
        {
            Console.WriteLine("{0}, {1}", apellido, nombre);
            Console.WriteLine("---------------------------------------------");

        }
        public void MostrarEmpleado()
        {
            Console.WriteLine("\n================================================================================");
            Console.WriteLine("{0}, {1}", apellido, nombre);
            Console.WriteLine("\n--------------------------------------------------------------------------------");
            Console.Write("Fecha de Nacimiento: {0}\n", FechadeNac);
            Console.Write("Edad: {0}\n", Edad);
            Console.Write("Estado Civíl: {0}\n", EC);
            Console.Write("Sexo: {0}\n", Sexo);
            Console.Write("Fecha de Ingreso: {0}\n", FIngreso);
            Console.Write("Sueldo Básico: {0}\n", SB);
            Console.Write("Cargo: {0}\n", Cargo);
            Console.WriteLine("Hijos: {0}", hijos);
            Console.WriteLine("Antigüedad en la empresa: {0}", Antig);
            if (Jubil == 0) Console.WriteLine("Ya puede jubilarse.");
            else Console.WriteLine("Años restantes para jubilarse: {0}", Jubil);
            Console.WriteLine("\n--------------------------------------------------------------------------------");
            Console.WriteLine("Sueldo: {0}", Sueldo);
            Console.WriteLine("\n================================================================================");
        }
    }
}
