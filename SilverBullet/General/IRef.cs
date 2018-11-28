using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverBullet.General
{
    public interface IRef
    {
        object GetValue();
        void SetValue(object v);
    }
    /// <summary>
    /// https://gist.github.com/svermeulen/a6929e6e26f2de2cc697d24f108c5f85
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Ref<T> : IRef where T : struct
    {
        T _value;

        public Ref(T value)
        {
            _value = value;
        }

        public T Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public object GetValue()
        {
            return Value;
        }

        public void SetValue(object v)
        {
            Value = (T)v;
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public static implicit operator T(Ref<T> wrapper)
        {
            return wrapper.Value;
        }

        public static implicit operator Ref<T>(T value)
        {
            return new Ref<T>(value);
        }
    }
}
