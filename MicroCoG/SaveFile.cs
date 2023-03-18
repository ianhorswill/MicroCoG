using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCoG
{
    public static class SaveFile
    {
        public static Hashtable Data = (Hashtable)SomaSim.SION.SION.Parse(File.OpenText(SocialInference.PathTo("savedata.sim")));

        public static T Lookup<T>(this Hashtable data, params string[] path)
        {
            object? o = data;
            foreach (var key in path)
            {
                var h = (Hashtable)o;
                if (h.ContainsKey(key))
                    o = h[key];
                else 
                    throw new KeyNotFoundException(key);
            }

            return (T)o;
        }
    }
}
