using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.ConstrainedExecution;


namespace NightBlueEngine
{
    public class Simulation
    {

            /// This method lists all the methods in the class Program
        /// it is used to build the routes for the api, so we dont need to write every single route manually
    

        public static double simulatePower(double currentPower, double psiTurbo, double displacement, double turboEfficiency, double airFuelRatio)
        {
            // Check if the parameters are valid
            if (currentPower < 0 || psiTurbo < 0 || displacement <= 0 || turboEfficiency <= 0 || airFuelRatio <= 0)
            {
                throw new ArgumentException("All parameters must be positive values greater than 0");
            }
            // Average pressure of an engine in PSI
            const double averageEnginePressure = 14.7;

            // Calculate the total pressure
            double TotalPsi = ((psiTurbo * 0.3) + averageEnginePressure);

            // Calculate the power gain from the turbo
            double turboPowerGain = (TotalPsi * currentPower) / averageEnginePressure;

            // Calculate the power gain from the increased displacement
            double displacementPowerGain = displacement * 0.5;

            // Calculate the power gain from the turbo's efficiency
            double efficiencyPowerGain = turboPowerGain * turboEfficiency;

            // Calculate the power loss from the air/fuel ratio
            double airFuelPowerLoss = (airFuelRatio > 14.7) ? currentPower * 0.1 : 0;

            // Calculate the final power
            double finalPower = currentPower + turboPowerGain + displacementPowerGain + efficiencyPowerGain - airFuelPowerLoss;

            System.Console.WriteLine($"The final power is {finalPower}");

            return finalPower;
        }

        public static double simulateSpeed(double power, double weight, double aerodynamicCoefficient)
        {
            // Check if the parameters are valid
            if (power < 0 || weight <= 0 || aerodynamicCoefficient <= 0)
            {
                throw new ArgumentException("All parameters must be positive values greater than 0");
            }

            // Calculate the power to weight ratio
            double PowerToWeight = power / weight;

            // Calculate the aerodynamic drag
            double AerodynamicDrag = aerodynamicCoefficient * 0.5;

            // Calculate the final speed
            double FinalSpeed = PowerToWeight * 1000 / AerodynamicDrag;

            System.Console.WriteLine($"The final speed is {FinalSpeed}");

            return FinalSpeed;
        }

        public static double simulateTimeToSpeed(double power, double weight, double aerodynamicCoefficient, double targetSpeed)
        {
            // Check if the parameters are valid
            if (power < 0 || weight <= 0 || aerodynamicCoefficient <= 0 || targetSpeed <= 0)
            {
                throw new ArgumentException("All parameters must be positive values greater than 0");
            }

            // Calculate the final speed
            double finalSpeed = simulateSpeed(power, weight, aerodynamicCoefficient);

            // Calculate the acceleration (assuming the power is constant)
            double acceleration = power / weight;

            // Calculate the time to reach the target speed
            double timeToSpeed = targetSpeed / acceleration;

            System.Console.WriteLine($"The time to reach a speed of {targetSpeed} is {timeToSpeed}");

            return timeToSpeed;
        }

        //this method calculates how much time a car takes to arrive to 
        // 100 kilometers per hour, starting from 0, of course
        public static double timeToGet100(double power, double tork, double weight)
        {
            double time = (weight / power * tork) / 10;
            System.Console.WriteLine($"The time to get to 100 kilometers per hour is {time} seconds");
            return time;
        }
    }
}