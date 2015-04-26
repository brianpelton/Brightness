using System;
using System.Management;

namespace Brightness
{
    /// <summary>
    ///     Functions to modify system values using WMI.
    /// </summary>
    internal static class WmiFunctions
    {

        /// <summary>
        ///     Query the brightness setting of the display.
        /// </summary>
        internal static int GetBrightnessLevel()
        {
            try
            {
                var s = new ManagementScope("root\\WMI");
                var q = new SelectQuery("WmiMonitorBrightness");
                var mos = new ManagementObjectSearcher(s, q);
                var moc = mos.Get();

                foreach (var managementBaseObject in moc)
                {
                    foreach (var o in managementBaseObject.Properties)
                    {
                        if (o.Name == "CurrentBrightness")
                            return Convert.ToInt32(o.Value);
                    }
                }

                moc.Dispose();
                mos.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return 0;
        }


        /// <summary>
        ///     Change the brightness setting of the display.
        /// </summary>
        /// <param name="brightnessLevel">
        ///     The brightness value to apply. Should
        ///     be between 0 and 100.
        /// </param>
        internal static void SetBrightnessLevel(int brightnessLevel)
        {
            if (brightnessLevel < 0 ||
                brightnessLevel > 100)
                throw new ArgumentOutOfRangeException("brightnessLevel");

            try
            {
                var s = new ManagementScope("root\\WMI");
                var q = new SelectQuery("WmiMonitorBrightnessMethods");
                var mos = new ManagementObjectSearcher(s, q);
                var moc = mos.Get();

                foreach (var managementBaseObject in moc)
                {
                    var o = (ManagementObject) managementBaseObject;
                    o.InvokeMethod("WmiSetBrightness", new object[]
                    {
                        UInt32.MaxValue,
                        brightnessLevel
                    });
                }

                moc.Dispose();
                mos.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}