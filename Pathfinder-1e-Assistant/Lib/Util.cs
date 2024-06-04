using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinder_1e_Assistant.Lib
{
    public static class Util
    {

        private readonly static Random rnd = new();
        public static int GetRandomInt(int min, int max) {  return rnd.Next(min, max); } // Exclusive, ofc
        public static string ToLiteral(string valueTextForCompiler)
        {
            return Microsoft.CodeAnalysis.CSharp.SymbolDisplay.FormatLiteral(valueTextForCompiler, false);
        }
    }
}
