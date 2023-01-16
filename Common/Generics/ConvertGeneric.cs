using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Generics
{
    public static class ConvertGeneric
    {
        public static T? ConvertFromString<T>(this string str)
        {
            try
            {
                var convert = TypeDescriptor.GetConverter(typeof(T));
                if (convert.ConvertFromString(str) is T res)
                {
                    return res;
                }
                return default;
            }
            catch (NotSupportedException)
            {
                return default;
            }
        }
    }
}
