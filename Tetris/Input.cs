using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    internal class Input
    {
        //zaladowanie listy dostepnych przyciskow klawiatury
        private static Hashtable keyTable = new Hashtable();

        //sprawdz czy jakis przycisk zostal nacisniety
        public static bool KeyPressed(Keys key)
        {
            if(keyTable[key] == null)
            {
                return false;
            }
            return (bool) keyTable[key];
        }
        //wykryj nacisniecie
        public static void ChangeState(Keys key,bool state)
        {
            keyTable[key] = state;
        }
    }
}
