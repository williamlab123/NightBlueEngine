using System;
using System.Net;
using System.Threading;
using System.IO;
using System.Text;
using Newtonsoft.Json;


// This class stores the data the program use to make the simulations
// Also, these are the allowed parameters for the body request 
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
    public double targetSpeed { get; set; }
}

public class Api
{
    public static string? body { get; set; }
    public static string? LastUrl { get; set; }
    public static string RequestBody { get; private set; }
    public static Requestbody LastRequestbody { get; private set; }

    public static bool Server()
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
                        ProcessRequest(context);
                    }
                    catch (HttpListenerException)
                    {
                        break;
                    }
                }
            });

            Console.WriteLine("Press Enter to stop the server.");
            // Console.ReadLine();

            var keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.Enter)
            {
                listener.Stop();
                // System.Console.WriteLine("sexo");
                return true;
            }
            return false;

        }
    }

    public static bool hasSimulationParameters(string lastRequestBody)
    {
        var parameters = JsonConvert.DeserializeObject<Dictionary<string, object>>(lastRequestBody);

        return parameters.ContainsKey("Power") ||
               parameters.ContainsKey("Tork") ||
               parameters.ContainsKey("Weight") ||
               parameters.ContainsKey("AerodynamicCoefficient") ||
               parameters.ContainsKey("CurrentPower") ||
               parameters.ContainsKey("PsiTurbo") ||
               parameters.ContainsKey("Displacement") ||
               parameters.ContainsKey("TurboEfficiency") ||
               parameters.ContainsKey("AirFuelRatio") ||
               parameters.ContainsKey("targetSpeed");
    }

    public static string ProcessRequest(HttpListenerContext context)
    {
        string body;
        LastUrl = context.Request.RawUrl;

        try
        {
            using (var reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
            {
                body = reader.ReadToEnd();
                LastRequestbody = JsonConvert.DeserializeObject<Requestbody>(body);


                if (!hasSimulationParameters(LastRequestbody.ToString()))
                {
                    System.Console.WriteLine("The request body does not contain the required parameters");
                }
                else System.Console.WriteLine("test");
            }

            // Console.WriteLine($"Received request with body: {body}");
            byte[] responseBytes = Encoding.UTF8.GetBytes("Request received successfully.");
            context.Response.ContentType = "text/plain";
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
            body = body;

            return body;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing request: {ex.Message}");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return null;
        }
        finally
        {
            context.Response.Close();
        }

    }
}