using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace NightBlueEngine
{

    // These are the allowed body parameters
    public class Requestbody
    {
        public double Power { get; set; }
        public double Tork { get; set; }
        public double Weight { get; set; }
        public double AerodynamicCoefficient { get; set; }
        public double CurrentPower { get; set; }
        public double PsiTurbo { get; set; }
        public double Displacement { get; set; }
        public double TurboEfficiency { get; set; }
        public double AirFuelRatio { get; set; }
        public double TargetSpeed { get; set; }
    }

    public class Api
    {
        // public string? Body { get; set; }
        public string? LastUrl { get; set; }
        public Requestbody? LastRequestbody { get; private set; }

        public bool server()
        {
            string url = "http://localhost:8080/";

            using (HttpListener listener = new HttpListener())
            {
                listener.Prefixes.Add(url);
                listener.Start();
                Console.WriteLine($"Listening for requests at {url}");

                ThreadPool.QueueUserWorkItem((state) =>
                {
                    while (listener.IsListening)
                    {
                        try
                        {
                            HttpListenerContext context = listener.GetContext();
                            processRequest(context);
                        }
                        catch (HttpListenerException)
                        {
                            break;
                        }
                    }
                });

                Console.WriteLine("Press Enter to stop the server.");

                var keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    listener.Stop();
                    return true;
                }

                return false;
            }
        }

        public void processRequest(HttpListenerContext context)
        {
            double? result_simulation;
            try
            {
                string body;
                LastUrl = context.Request.RawUrl;
                System.Console.WriteLine(LastUrl);

                using (var reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
                {
                    body = reader.ReadToEnd();
                    LastRequestbody = JsonConvert.DeserializeObject<Requestbody>(body);
                }

                #region Routes
                switch (LastUrl)
                {
                    case "/":
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.ResetColor();
                        result_simulation = null;
                        break;
                    case "/simulatespeed":
                        result_simulation = Simulation.simulateSpeed(LastRequestbody.Power,
                        LastRequestbody.Weight, LastRequestbody.AerodynamicCoefficient);
                        System.Console.WriteLine("test");
                        break;
                    case "/simulatepower":
                        result_simulation = Simulation.simulatePower(LastRequestbody.CurrentPower,
                        LastRequestbody.PsiTurbo,
                             LastRequestbody.Displacement, LastRequestbody.TurboEfficiency,
                             LastRequestbody.AirFuelRatio);
                        break;
                    case "/simulatetts":
                        result_simulation = Simulation.timeToGet100(LastRequestbody.Power,
                        LastRequestbody.Weight, LastRequestbody.Tork);
                        break;

                    default:
                        result_simulation = 404;
                        break;
                }
                #endregion

                string? responseBody = result_simulation.ToString()[0] != '0' ? result_simulation.ToString()
                : "There were no simulations";

                byte[] responseBytes = Encoding.UTF8.GetBytes(responseBody);
                context.Response.ContentType = "text/plain";
                // context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing request: {ex.Message}");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            finally
            {
                context.Response.Close();
            }


            
            
        }
    }
}
