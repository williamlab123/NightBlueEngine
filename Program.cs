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

        static float  codeVersion = 0.1f;
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


            // HttpListener listener = new HttpListener();
            // listener.Prefixes.Add("http://localhost:8080/");
            // listener.Start();
            // HttpListenerContext context = listener.GetContext();



            // System.Console.WriteLine("test");

            // System.Console.WriteLine("test");
            // ListMethods();
            #region Start
            Console.WriteLine(hello);
            Console.WriteLine(info);
            Console.WriteLine(doubt);
            #endregion



            apiConnect(); //static method

            static string listMethods()
            {
                var methods = typeof(Simulation).GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly);
                // foreach (var method in methods)
                // {
                //     Console.WriteLine(method.Name);
                // }
                return string.Join(", ", methods.Select(m => m.Name));
            }




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

        static void test()
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
        public static void apiConnect()
        {
            Api api = new Api();

            
            // api.server();
            if (api.server())
            {
                Console.Clear();
                System.Console.WriteLine("Server is stopping");
                // System.Environment.Exit(0);
            }
            // string test = Api.body;
            
        }



    }
}