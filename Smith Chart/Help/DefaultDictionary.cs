using System.Collections.Generic;

namespace Smith_Chart.Help
{
    public class DefaultDictionary<T1, T2> : Dictionary<T1, T2>
    {
        public new T2 this[T1 key]
        {
            get
            {
                T2 t;
                return TryGetValue(key, out t) ? t : default(T2);
            }
            set
            {
                if (ContainsKey(key)) base[key] = value;
                else Add(key, value);
            }
        }

        public DefaultDictionary()
        {
            
        }

    }
}