using System;

namespace Brightness
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: Pass in brightness change amount or value.");
                return;
            }

            var value = args[0];
            var currentBrightness = WmiFunctions.GetBrightnessLevel();

            Console.WriteLine("Current Brightness:  {0}", currentBrightness);
            if (value.Contains("+"))
            {
                // incremental change
                try
                {
                    var x = int.Parse(value.Replace("+", string.Empty));
                    WmiFunctions.SetBrightnessLevel(Math.Min(currentBrightness + x, 100));
                }
                catch (Exception)
                {
                }
            }
            else if (value.Contains("-"))
            {
                // decremental change
                try
                {
                    var x = int.Parse(value.Replace("-", string.Empty));
                    WmiFunctions.SetBrightnessLevel(Math.Max(currentBrightness - x, 1));
                }
                catch (Exception)
                {
                }
            }
            else
            {
                try
                {
                    var x = int.Parse(value);
                    WmiFunctions.SetBrightnessLevel(x);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}