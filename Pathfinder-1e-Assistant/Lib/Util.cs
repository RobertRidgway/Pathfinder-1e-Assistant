using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinder_1e_Assistant.Lib
{
    public static class Util
    {

        private readonly static Random rnd = new();

        private readonly static DataTable dataTable = new();

        public static int GetRandomInt(int min, int max) {  return rnd.Next(min, max); } // Exclusive, ofc

        public static int ComputeString(string str) { return Convert.ToInt32(dataTable.Compute(str, null)); }

        public static string ToLiteral(string valueTextForCompiler)
        {
            return Microsoft.CodeAnalysis.CSharp.SymbolDisplay.FormatLiteral(valueTextForCompiler, false);
        }
    }
}
