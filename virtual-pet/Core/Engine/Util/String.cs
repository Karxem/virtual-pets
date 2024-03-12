using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace virtual_pet.Core.GameEngine.Util
{
    public class String
    {

        public static Dictionary<string, object> ConstantVars = new Dictionary<string, object>();
        public static Dictionary<string, string> Strings = new Dictionary<string, string>();

        public static void Init()
        {
            Strings.Add("ACTION_USE", "Press [&&ACTION_BUTTON] to use [&0]");
            ConstantVars.Add("ACTION_BUTTON", "E");
        }

        public static void SetStringConstant(string name, object var)
        {
            ConstantVars[name] = var;
        }

        public static string GetString(string name, params object[] param)
        {
            string s = Strings[name];
            foreach (KeyValuePair<string, object> kvp in ConstantVars)
            {
                s = s.Replace("&&" + kvp.Key, kvp.Value.ToString());
            }
            for (int i = 0; i < param.Length; i++)
            {
                s = s.Replace("&" + i, param[i].ToString());
            }
            return s;
        }
    }
}
