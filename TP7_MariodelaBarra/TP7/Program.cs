using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP7
{
    class Program
    {
        public enum Cargo { Auxiliar, Administrativo, Ingeniero, Especialista, Investigador };
        public enum Genero { Masculino, Femenino };
        struct Empleado
        {
            public string Nombre, Apellido;
            public Genero genero;
            public Cargo cargo;
            public int estadoCivil, sueldo, cantHijos;
            public DateTime fechaIngreso, fechaNac;

            public Empleado(string _Nombre, string _Apellido, DateTime _fechaNac, int _estadoCivil, int _cantHijos, Genero _genero, int _sueldo, Cargo _cargo, DateTime _fechaIngreso)
            {
                Nombre = _Nombre;
                Apellido = _Apellido;
                fechaNac = _fechaNac;
                estadoCivil = _estadoCivil;
                cantHijos = _cantHijos;
                genero = _genero;
                sueldo = _sueldo;
                cargo = _cargo;
                fechaIngreso = _fechaIngreso;

            }

            //Calculo de edad

            public static int edadEmpleado(Empleado empleado)
            {
                DateTime fechaActual = DateTime.Now;
                int edad = 0;
                if (empleado.fechaNac.Month <= fechaActual.Month)
                {
                    edad = fechaActual.Year - empleado.fechaNac.Year;
                }
                else if (empleado.fechaNac.Month == fechaActual.Month)
                {
                    if (empleado.fechaNac.Day <= fechaActual.Day)
                    {
                        edad = fechaActual.Year - empleado.fechaNac.Year;
                    }
                    else
                    {
                        edad = fechaActual.Year - empleado.fechaNac.Year - 1;
                    }
                }
                else
                {
                    edad = fechaActual.Year - empleado.fechaNac.Year - 1;
                }
                return edad;
            }

            //Calculo de Antiguedad
            public static double antiguedad(Empleado empleado)
            {
                DateTime fechaActual = DateTime.Now;
                double antiguedadEmp;

                antiguedadEmp = (fechaActual.Year - empleado.fechaIngreso.Year);

                return antiguedadEmp;
            }

            //Calculo de jubilacion

            public static int calculoJubilacion(Empleado empleado)
            {
                DateTime fechaActual = DateTime.Now;
                int edadAct = edadEmpleado(empleado);
                if (empleado.genero == Genero.Femenino)
                {
                    if (edadAct >= 60) return 0; else return 60 - edadAct;
                }
                else
                {
                    if (edadAct >= 65) return 0; else return 65 - edadAct;
                }
            }

            //Calculo del Salario
            public static double salario(Empleado empleado)
            {
                double Salario, Adicional, antiguedad;
                antiguedad = (Empleado.antiguedad(empleado)) / 10;

                if (antiguedad < 20)
                {
                    Adicional = empleado.sueldo * (0.2 * antiguedad);
                }
                else
                {
                    Adicional = (empleado.sueldo * 0.25) * antiguedad;
                }
                if (empleado.cargo == Cargo.Ingeniero || empleado.cargo == Cargo.Especialista)
                {
                    Adicional = Adicional + (Adicional * 0.50);
                }
                if (empleado.estadoCivil == 1 && empleado.cantHijos>2) Adicional = Adicional + 5000;

                Salario = empleado.sueldo + Adicional;

                return Salario;
            }

            public static void mostrarEmpleado(Empleado empleado)
            {
                Console.WriteLine("Nombre y apellido: {0} {1}", empleado.Nombre, empleado.Apellido);
                Console.WriteLine("Fecha de nacimiento: {0}", empleado.fechaNac.ToShortDateString());
                if (empleado.estadoCivil == 1) Console.WriteLine("Estado Civil: Casado"); else Console.WriteLine("Estado Civil: Soltero");
                Console.WriteLine("Genero: {0}", empleado.genero);
                Console.WriteLine("Sueldo: {0}", empleado.sueldo);
                Console.WriteLine("Cargo: {0}", empleado.cargo);
                Console.WriteLine("Fecha de ingreso: {0}", empleado.fechaIngreso.ToShortDateString());
            }
            public static void busquedaEmpleado(List<Empleado> empleadoLista, string[] delimitador)
            {
                int indice = empleadoLista.FindIndex(x => x.Nombre.Contains(delimitador[0]) && x.Apellido.Contains(delimitador[1]));
                Console.WriteLine("\nEmpleado n. {0}\n", indice+1);
                Empleado.mostrarEmpleado(empleadoLista[indice]);
                Console.WriteLine("La edad del empleado es de: {0}", edadEmpleado(empleadoLista[indice]));
                Console.WriteLine("El empleado tiene {0} años de antiguedad.", Empleado.antiguedad(empleadoLista[indice]));
                Console.WriteLine("Al empleado le faltan {0} años para jubilarse.\n", calculoJubilacion(empleadoLista[indice]));
            }

        }

        static void Main(string[] args)
        {
            List<Empleado> empleadoLista = new List<Empleado>();
            Empleado empleado = new Empleado();
            Random rnd = new Random();
            int opc=0;
            string busqueda;
            string[] delimitador;

            //Creando el empleado
            for(int i=0; i<20; i++)
            {
                Console.WriteLine("|====Empleado n. {0}====|", i+1);
                empleado = crearEmpleado(ref empleado, rnd);
                empleadoLista.Add(empleado);
            }
            do
            {
                Console.WriteLine("\n|===== Buscador de empleados =====|");
                Console.WriteLine("\nIngrese el nombre del empleado que desea buscar: ");
                busqueda = Console.ReadLine();
                delimitador = busqueda.Split(' ');

                Empleado.busquedaEmpleado(empleadoLista, delimitador);
                Console.WriteLine("\n¿Desea continuar con la busqueda?: (1- Si ; 0- No)");
                opc = int.Parse(Console.ReadLine());
            } while (opc != 0);


            Console.ReadKey();

        }

        private static Empleado crearEmpleado(ref Empleado empleado, Random rnd)
        {
            int varAux;
            DateTime fechaIngreso;
            //Nombre y apellido
            Console.WriteLine("Ingrese el nombre del empleado: ");
            empleado.Nombre = Console.ReadLine();
            Console.WriteLine("Ingrese el apellido del empleado: ");
            empleado.Apellido = Console.ReadLine();
            //Fecha de nacimiento
            empleado.fechaNac = new DateTime(rnd.Next(1950, 1990), rnd.Next(1, 12), rnd.Next(1, 28));
            //Estado civil, genero, sueldo, cargo
            varAux = rnd.Next(1, 2);
            empleado.estadoCivil = varAux;
            Console.WriteLine("Ingrese el genero del empleado: (1- Masculino; 2- Femenino)");
            varAux = int.Parse(Console.ReadLine());
            if (varAux == 1) empleado.genero = Genero.Masculino; else empleado.genero = Genero.Femenino;
            Console.WriteLine("Ingrese el sueldo del empleado: ");
            varAux = int.Parse(Console.ReadLine());
            empleado.sueldo = varAux;
            //cant de hijos
            varAux = rnd.Next(0, 10);
            empleado.cantHijos = varAux;

            //Cargando el cargo del empleado
            varAux = rnd.Next(1,5);
            switch (varAux)
            {
                case 1: empleado.cargo = Cargo.Auxiliar; break;
                case 2: empleado.cargo = Cargo.Administrativo; break;
                case 3: empleado.cargo = Cargo.Ingeniero; break;
                case 4: empleado.cargo = Cargo.Especialista; break;
                case 5: empleado.cargo = Cargo.Investigador; break;
            }

            //Inicializo la fecha de ingreso
            fechaIngreso = new DateTime(rnd.Next(1980, 2018), rnd.Next(1, 12), rnd.Next(1, 28));
            //================================
            empleado.fechaIngreso = fechaIngreso;

            return empleado;
        }
    }
}
