using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class DeepCopyUtilities
    {
        /// <summary>
        /// convert a instance of one type to an instance with properties with the same names
        /// </summary>
        /// <typeparam name="T">output type</typeparam>
        /// <typeparam name="S">input type</typeparam>
        /// <param name="from">input parameter</param>
        /// <param name="to">output parameter</param>
        public static void CopyPropertiesTo<T, S>(this S from, T to)
        {
            foreach (PropertyInfo propTo in to.GetType().GetProperties())
            {
                PropertyInfo propFrom = typeof(S).GetProperty(propTo.Name);
                if (propFrom == null)
                    continue;
                var value = propFrom.GetValue(from, null);
                if (value is ValueType || value is string)
                    propTo.SetValue(to, value);
            }
        }

        /// <summary>
        /// creates a new instance of type type
        /// </summary>
        /// <typeparam name="S">input type</typeparam>
        /// <param name="from">input param</param>
        /// <param name="type">output param Type</param>
        /// <returns></returns>
        public static object CopyPropertiesToNew<S>(this S from, Type type)
        {
            object to = Activator.CreateInstance(type); // new object of Type
            from.CopyPropertiesTo(to);
            return to;
        }
        
    }
}
