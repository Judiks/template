using System;

namespace THD.Domain.Extension
{
    public static class CustomExtensions
    {
        public static double? ToDouble(this object value)
        {
            if (value is null)
            {
                return default;
            }
            double result;
            var parseResult = double.TryParse(value.ToString(), out result);
            if (parseResult)
            {
                return result;
            }
            return default;
        }

        public static int? ToInt(this object value)
        {
            if (value is null)
            {
                return default;
            }
            int result;
            var parseResult = int.TryParse(value.ToString(), out result);
            if (parseResult)
            {
                return result;
            }
            return default;
        }

        public static bool TryParse(this string value)
        {
            Guid guid;
            if (Guid.TryParse(value, out guid))
            {
                return true;
            }
            return false;
        }
    }
}
