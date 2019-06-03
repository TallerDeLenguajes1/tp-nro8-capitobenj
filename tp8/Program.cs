using System;
using System.Collections.Generic;
using System.Timers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Help;
using System.IO;

namespace Lsita
{
    class Program
    {
        public const string archivo = @"C:\Empleados\Empleados.csv";
        public const string dir = @"C:\Empleados\";
        public const string arch = @"Empleados.csv";
        public const string backup = @"";
        public const string backupdir = @"C:\BackUpAgenda";
        public enum cargo { Auxiliar, Administrativo, Ingeniero, Especialista, Investigador };
        static void Main(string[] args)
        {
            int i = 1;
            int once = 0;
            Directory.CreateDirectory(dir);
            char key;
            List<Empleado> Agenda = new List<Empleado>();
            Empleado empleado;
            do
            {
                Console.WriteLine("1. Ver Agenda (Contactos: {0})", CantidaddeEmpleados(Agenda));
                Console.WriteLine("2. Agregar Empleado a la Agenda");
                Console.WriteLine("3. Monto Total de los Empleados de la Agenda");
                Console.WriteLine("4.BackUp");
                if (once == 0)
                {
                    Console.WriteLine("5. Leer Archivo de Contactos");
                    once++;
                }
                
                Console.WriteLine("0. Salir");
                key = Console.ReadKey(true).KeyChar;
                switch (key)
                {
                    
                    case '0':
                        break;
                    case '1':
                        MostrarAgendaConOpcion(Agenda);
                        break;
                    case '2':
                        empleado = CrearEmpleado(i);
                        AgregarEmpleado(Agenda, empleado);
                        i++;
                        break;
                    case '3':
                        Console.WriteLine(CalcularMonto(Agenda));
                        break;
                    case '4':
                        i = leerarchivo(archivo,i,Agenda);
                        break;
                    default:
                        Console.WriteLine("Error: Dato Ingresado no coincide con las opciones predefinidas.");
                        break;
                }
            } while (key != '0');
        }
        public static int leerarchivo(string archivo,int i,List<Empleado> Agenda)
        {
            if (File.Exists(archivo))
            {
                string[] prueba = File.ReadAllLines(archivo);
                string[] registro;
                int cont = prueba.Length;
                for(int f=0;f<cont;f++)
                {
                    registro = prueba[f].Split(';');
                    Empleado nuevo = new Empleado();
                    nuevo.nombre = registro[0];
                    nuevo.apellido = registro[1];
                    nuevo.FechadeNac = registro[2];
                    nuevo.Edad = CalcularEdad(registro[2]);
                    nuevo.EC = registro[3];
                    nuevo.Sexo = registro[4];
                    nuevo.FIngreso = registro[5];
                    nuevo.Antig = CalcularEdad(registro[5]);
                    nuevo.SB = registro[6];
                    nuevo.hijos = RandomNumber(0, 6);
                    if (nuevo.Sexo == "femenino" || nuevo.Sexo == "Femenino") nuevo.Jubil = 60 - nuevo.Edad;
                    else nuevo.Jubil = 65 - nuevo.Edad;
                    if (nuevo.Jubil < 0) nuevo.Jubil = 0;
                    nuevo.Cargo = Randomblow();
                    nuevo.id = i;
                    nuevo.Sueldo = int.Parse(registro[6]) + CalcularAdicional(nuevo.Antig, registro[6], nuevo.Cargo, nuevo.EC, nuevo.hijos);
                    AgregarEmpleado(Agenda, nuevo);
                    i++;
                }
            }
            else
            {
                Console.WriteLine("El archivo no existe.");
            }
            return i;
        }
        public static int CalcularEdad(string _fecha)
        {
            int edad;
            string ano, mes, dia;
            int d1, m1, a1, d2, m2, a2;
            string fechayhora;
            fechayhora = DateTime.Now.ToString("yyyyMMdd");
            char[] aano = { fechayhora[0], fechayhora[1], fechayhora[2], fechayhora[3] };
            char[] _mes = { fechayhora[4], fechayhora[5] };
            char[] _dia = { fechayhora[6], fechayhora[7] };
            string[] charles;//fecha actual
            char[] splitter = { ' ' };
            charles = _fecha.Split(splitter);
            ano = new string(aano);//año de nac
            mes = new string(_mes);//mes de nac
            dia = new string(_dia);//dia de nac
            d1 = int.Parse(dia);
            d2 = int.Parse(charles[0]);
            m1 = int.Parse(mes);
            m2 = int.Parse(charles[1]);
            a1 = int.Parse(ano);
            a2 = int.Parse(charles[2]);
            edad = a1 - a2;
            if (m2 >= m1)
            {
                if (d2 < d1) edad--;
            }
            else edad--;
            return edad;
        }
        public static void Buscarid(List<Empleado> Agenda, int idd)
        {
            int i = 0;
            foreach (Empleado emp in Agenda)
            {
                if (emp.id == idd)
                {
                    i = 1;
                    emp.MostrarEmpleado();
                }
            }
            if (i == 0) Console.WriteLine("El ID especificado no existe en la lista.");
        }
        public static double CalcularAdicional(int Ant, string _SB, string cargo, string EC, int hijos)
        {
            double Adic;
            int SB;
            SB = int.Parse(_SB);
            double porano = 0.02;
            if (Ant == 0) Adic = 0;
            else if (Ant >= 20) Adic = SB * (porano * 25);
            else
            {
                Adic = SB * (porano * Ant);
            }
            if (cargo == "Ingeniero" || cargo == "Especialista") Adic *= 1.5;
            if ((EC == "Casado" || EC == "Casada") && hijos > 2)
            {
                Adic += 5000;
            }
            return Adic;
        }
        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public static Empleado CrearEmpleado(int i)
        {
            string _nombre, _apellido, _FechadeNac, _EC, _Sexo, _FIngreso, _SB;
            Empleado nuevo= new Empleado();
            Console.Write("Ingrese Nombre del Empleado/a: ");
            _nombre = Console.ReadLine();
            Console.Write("Ingrese Apellido del Empleado/a: ");
            _apellido = Console.ReadLine();
            Console.Write(@"Ingrese Fecha de Nacimiento del Empleado/a(Formato: 12 05 1912): ");
            _FechadeNac = Console.ReadLine();
            Console.Write("Estado Civil del Empleado/a(Formato: Soltero/a, Casado/a): ");
            _EC = Console.ReadLine();
            Console.Write("Sexo del Empleado/a(Formato: Femenino o Masculino): ");
            _Sexo = Console.ReadLine();
            Console.Write("Fecha de Ingreso del Empleado/a(Formato: 02 05 1912): ");
            _FIngreso = Console.ReadLine();
            Console.Write("Ingrese el Sueldo Basico del Empleado/a: ");
            _SB = Console.ReadLine();
            nuevo.nombre = _nombre;
            nuevo.apellido = _apellido;
            nuevo.FechadeNac = _FechadeNac;
            nuevo.Edad = CalcularEdad(_FechadeNac);
            nuevo.EC = _EC;
            nuevo.Sexo = _Sexo;
            nuevo.FIngreso = _FIngreso;
            nuevo.Antig = CalcularEdad(_FIngreso);
            nuevo.SB = _SB;
            nuevo.hijos = RandomNumber(0, 6);
            if (nuevo.Sexo == "femenino" || nuevo.Sexo == "Femenino") nuevo.Jubil = 60 - nuevo.Edad;
            else nuevo.Jubil = 65 - nuevo.Edad;
            if (nuevo.Jubil < 0) nuevo.Jubil = 0;
            nuevo.Cargo = Randomblow();
            nuevo.id = i;
            nuevo.Sueldo = int.Parse(_SB) + CalcularAdicional(nuevo.Antig, _SB, nuevo.Cargo, nuevo.EC, nuevo.hijos);
            return nuevo;
        }
        public static void AgregarEmpleado(List<Empleado> Agenda, Empleado contacto)
        {
            StreamWriter log;
            string pub = contacto.nombre + ";" + contacto.apellido + ";" + contacto.FechadeNac + ";" + contacto.EC + ";" + contacto.Sexo + ';' + contacto.FIngreso + ";" + contacto.SB + "\n";
            if (!File.Exists(archivo)) File.WriteAllText(archivo, pub);
            else
            {
                log = File.AppendText(archivo);
                log.WriteLine(pub);
                log.Close();
            }
            Agenda.Add(contacto);
        }
        public static string Randomblow()
        {
            var value = RandomEnumValue<cargo>();
            return value.ToString();
        }
        public static T RandomEnumValue<T>()
        {
            var v = Enum.GetValues(typeof(T));
            return (T)v.GetValue(new Random().Next(v.Length));
        }
        public static void MostrarAgendaConOpcion(List<Empleado> Agenda)
        {

            int i = CantidaddeEmpleados(Agenda);
            int a = 1;
            if(i>0) Console.WriteLine("\n----------------Agenda---------------");
            foreach (Empleado emp in Agenda)
            {
                if (i>0)
                    {
                        Console.Write("{0}.",a);
                        emp.MostrarTag(Agenda);
                    }
                a++;
            }
            string key;
            int key2;
            do
            {
                Console.WriteLine("A Qué Empleado quiere mostrar?");
                Console.WriteLine("0. Volver al menu principal");
                key = Console.ReadLine();
                key2 = int.Parse(key);
                if(key2!=0) Buscarid(Agenda, key2);
            } while (key != "0");
        }
        public static int CantidaddeEmpleados(List<Empleado> Agenda)
        {
            int i = 0;
            Agenda.ForEach(x => i++);
            return i;
        }
        public static double CalcularMonto(List<Empleado> Agenda)
        {
            double Monto = 0;
            Agenda.ForEach(x => Monto += x.Sueldo);
            return Monto;
        }
        public static void MostrarAgendaConForeach(List<Empleado> Agenda)
        {
            Agenda.ForEach(x => x.MostrarEmpleado());
        }
    }
}
