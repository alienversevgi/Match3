using System.IO;
using UnityEngine;

namespace BaseX.Utils
{
    public static class StringUtil
    {
        public static string ConvertNumberString(this int number)
        {
            return number < 10 ? $"0{number}" : number.ToString();
        }
    }
}