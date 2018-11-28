using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverBullet.General
{
    public class Textbox
    {
        public Action<string> setText;
        public Func<string> GetText;

        public Textbox(Action<string> setText, Func<string> getText)
        {
            this.setText = setText;
            this.GetText = getText;
        }
    }
}
