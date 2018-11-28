using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverBullet.General
{
    public class SaveLoadVariable
    {
        public string name;
        public Func<string> write;
        public Action<string> read;

        public SaveLoadVariable(string name, Func<string> save, Action<string> load)
        {
            this.name = name;
            this.write = save;
            this.read = load;
        }
    }
    public class SaveLoadVariable<T>
    {
        public string name;
        public Func<T, string> write;
        public Action<T, string> read;

        public SaveLoadVariable(string name, Func<T, string> save, Action<T, string> load)
        {
            this.name = name;
            this.write = save;
            this.read = load;
        }
    }
}
