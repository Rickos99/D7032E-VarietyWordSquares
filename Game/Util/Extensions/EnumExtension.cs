using System;
using System.ComponentModel;
using System.Reflection;

namespace Game.Util.Extensions
{
    /// <summary>
    /// Extension methods for Enum
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// Get description of enum value
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Description of enum value</returns>
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }
    }
}
