using System;
using System.Net;
using System.Threading;
using System.Reflection;
// using Microsoft.Extensions.Logging;


namespace NightBlueEngine
{
    public class Program
    {

        #region Welcome

        static float codeVersion = 0.1f;
        static string hello = $"Hello, this is the NightBlueEngine version {codeVersion}";
        static string info = "For more info, please read our documentation";
        static string doubt = "For a list of commands, type 'help'";
        static string commands = "";
        static string badRequest = "Hello there, i've seen that you made a request, " +
                                   "but you didn't specified the route, so i'll help you." +
                                   "Here's a list of the available routes:";
        #endregion






        static void Main()
        {

            // string lista = ListMethods();
            // System.Console.WriteLine($"alo {lista}");



            // System.Console.WriteLine("test");

            // System.Console.WriteLine("test");
            // ListMethods();
            #region Start
            Console.WriteLine(hello);
            Console.WriteLine(info);
            Console.WriteLine(doubt);
            #endregion


            // System.Console.WriteLine($"This is the body {test}");
            // System.Console.WriteLine($"test {Api.LastUrl}");
            apiConnect();

            static string ListMethods()
            {
                var methods = typeof(Simulation).GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly);
                // foreach (var method in methods)
                // {
                //     Console.WriteLine(method.Name);
                // }
                return string.Join(", ", methods.Select(m => m.Name));
            }

            #region Routes
            /// This gets the url from the api and redirects to the correspinding method
            switch (Api.LastUrl)
            {
                case "/":
                    System.Console.WriteLine(badRequest);
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine(ListMethods());
                    Console.ResetColor();

                    break;
                case "/simulatespeed":
                    Simulation.simulateSpeed(Api.LastRequestbody.Power, Api.LastRequestbody.Weight,
                    Api.LastRequestbody.AerodynamicCoefficient);
                    break;
                case "/simulatepower":
                    Simulation.simulatePower(Api.LastRequestbody.CurrentPower, Api.LastRequestbody.PsiTurbo,
                     Api.LastRequestbody.Displacement, Api.LastRequestbody.TurboEfficiency,
                     Api.LastRequestbody.AirFuelRatio);
                    break;

                case "/simulatetts":
                    Simulation.timeToGet100(Api.LastRequestbody.Power, Api.LastRequestbody.Weight,
                    Api.LastRequestbody.Tork);
                    break;
            }
            #endregion

        }


        // static string redirect(string path)
        // {
        //     switch (path)
        //     {
        //         case "/":
        //             return "";
        //         case "/simulatespeed":
        //         simulateSpeed(Api.LastRequestbody.Power, Api.LastRequestbody.Weight, Api.LastRequestbody.AerodynamicCoefficient);
        //             return "simulateSpeed";
        //         case "/test":
        //             return "test";
        //         case "/sexo":
        //             return "sexo";
        //         default:
        //             return "404.html";
        //     }
        // }

        static void Test()
        {
            Console.WriteLine("HOJE É SECSU NA RAVE");
        }

        static void sexo()
        {
            Console.WriteLine("sexo");
        }


        /// <summary>
        /// connects to the api 
        /// </summary>
        /// <param name="name">diozrshgdjne</param>
        public static string apiConnect()
        {
            Api.Server();
            if (Api.Server())
            {
                Console.Clear();
                System.Console.WriteLine("Server is stopping");
                // System.Environment.Exit(0);
            }
            string test = Api.body;
            return test;
        }



    }
}