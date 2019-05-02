using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace TheGreatPatrioticWar
{
	public static class Global
	{
        
        public static void AssignFieldWithCast(object target,FieldInfo joj,string data)
        {


            var invariant = System.Globalization.CultureInfo.InvariantCulture;
            switch (joj.FieldType.Name)
            {
                case "Boolean": joj.SetValue(target, bool.Parse(data)); break;
                case "Int32": joj.SetValue(target, int.Parse(data, invariant)); break;
                case "Int64": joj.SetValue(target, long.Parse(data, invariant)); break;
                case "Single": joj.SetValue(target, float.Parse(data, invariant)); break;
                case "Double": joj.SetValue(target, double.Parse(data, invariant)); break;

                default: joj.SetValue(target, data); break;
            }
        }
	}
}
