using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinder_1e_Assistant.Lib
{
    public class CharacterSheetUtils
    {

        public static int StatModifier(int stat)
        {
            return Math.Max(-5, (stat / 2 - 5));
        }

    }
}
