using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton.Application.Common.Extensions
{
    public static class StringExtensions
    {
        public static string TruncateLongString(this string str, int maxLength)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            return str.Substring(0, Math.Min(str.Length, maxLength));
        }
    }
}
