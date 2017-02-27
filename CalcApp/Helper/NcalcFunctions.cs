using System;
using NCalc;

namespace CalcApp.Helper
{
    public partial class NCalcFunctions
    {
        // Calc Parameters
        public static void Parameters(string name, ParameterArgs parameterArgs)
        {
            // add Pi (Kreiszahl) 
            if (name == "Pi")
                parameterArgs.Result = Math.PI;
            // add e (Eulerzahl) 
            if (name == "e")
                parameterArgs.Result = Math.E;           
        }
        // Calc Extensions
        public static void Extensions(string name, FunctionArgs functionArgs)
        {
            // add In (Logarithmus Naturalis)
            if (name == "In")
            {
                var param1 = Convert.ToDouble(functionArgs.Parameters[0].Evaluate());
                functionArgs.Result = Math.Log((double)param1);
            }            

            //add Fac(a) (Fakultät)
            if (name == "Fac")
            {
                var param1 = Convert.ToDouble(functionArgs.Parameters[0].Evaluate());
                double counter = param1;
                double fact = 1;
                while (counter > 1)
                {
                    fact *= counter--;
                }
                functionArgs.Result = fact;
            }
        }
        // Calc Extensions
        public static void ExtensionsDegree(string name, FunctionArgs functionArgs)
        {
            if (name == "Sin")
            {
                var param1 = Convert.ToDouble(functionArgs.Parameters[0].Evaluate());
                functionArgs.Result = Math.Sin((Math.PI / 180) * param1);
            }
            if (name == "Cos")
            {
                var param1 = Convert.ToDouble(functionArgs.Parameters[0].Evaluate());
                functionArgs.Result = Math.Cos((Math.PI / 180) * param1);
            }
            if (name == "Tan")
            {
                var param1 = Convert.ToDouble(functionArgs.Parameters[0].Evaluate());
                functionArgs.Result = Math.Tan((Math.PI / 180) * param1);
            }
        }
    }
}