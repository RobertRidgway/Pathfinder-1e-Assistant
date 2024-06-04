using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pathfinder_1e_Assistant.Lib
{
    public partial class DiceParse
    {
        public static string ReplaceFirst(string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0) { return text; }

            string result = text.Remove(pos, search.Length).Insert(pos, replace);
            return result;
        }

        public static (int, int) DiceRoll(int numRolls, int sideNum, int critRange = int.MaxValue)
        {

            int result = 0;
            int critFlag = 0;

            for (int i = 0; i < numRolls; i++)
            {
                int roll = Util.GetRandomInt(1, sideNum + 1);
                result += roll;
                if (roll > critRange & numRolls == 1) { critFlag = 1; }
                else if (roll == 1 & numRolls == 1) { critFlag = -1; }
            }
            return (result, critFlag);
        }


        public static int[][] BracketSearch(string macrostring)
        {
            List<int[]> indicesList = [];

            int bracketCount = 0;
            int tempBracketIndex = 0;
            bool idxUnset = true;
            for (int i = 0; i < macrostring.Length; i++)
            {
                if (macrostring[i] == '[')
                {
                    bracketCount++;
                    if (bracketCount == 2 && idxUnset)
                    {
                        tempBracketIndex = i - 1;
                        idxUnset = false;
                    }
                }
                else if (macrostring[i] == ']')
                {
                    bracketCount--;
                    if (bracketCount == 0 && !idxUnset)
                    {
                        indicesList.Add([tempBracketIndex, i + 1 ]);
                        idxUnset = true;
                    }
                }
            }
            return [.. indicesList];
        }

        // Take [[ XXdYY + ZZZ ]] and return a string with replaced text
        // Along with critcal hit/fail flags for formatting purposes
        public static (string, int) RollParse(string rollString)
        {
            // 1 is a crit, -1 is a crit fail
            int globalCritFlag = 0;
            string outString = new(rollString);

            string modString = rollString.ToLower();
            // [GeneratedRegex("[^(cs>)0-9d+-]")]
            modString = RegexDiceParse().Replace(modString, "");

            string[] stringDelimiters = [ "+", "-" ];
            string[] rollDelimiters = [ "d", "cs>" ];

            var addsOrSubs = modString.Split(stringDelimiters, StringSplitOptions.None);
            //Console.WriteLine(addsOrSubs);

            foreach (string term in addsOrSubs)
            {

                // Splits into XX d YY cs> ZZ
                // Length of 1 means constant, length of 2 means dice roll, length of 3 means diceroll with crit range
                string[] d = term.Split(rollDelimiters, StringSplitOptions.None);
                if (d.Length > 1)
                {
                    if (!int.TryParse(d[0], out int numRolls)) { numRolls = 1; }
                    //int numRolls = int.Parse(d[0]);
                    int faceNum = int.Parse(d[1]);
                    int critRange;
                    bool critsPossible = false;
                    if (d.Length == 3) { critRange = int.Parse(d[2]); }
                    else if (numRolls == 1 && faceNum == 20) { critRange = 19; }
                    else { critRange = int.MaxValue; }

                    (int roll, int critFlag) = DiceRoll(numRolls, faceNum, critRange);

                    critsPossible = false;
                    // Only track crits if a single d20 is being rolled, or whether cs>XX is specified
                    if ((numRolls == 1 && faceNum == 20) | (critRange != int.MaxValue)) { critsPossible = true; }

                    if (critsPossible && critFlag == 1) { globalCritFlag = 1; }
                    else if (critsPossible && critFlag == -1) { globalCritFlag = -1; }
                    outString = ReplaceFirst(outString, term, roll.ToString());
                }

            }
            // [GeneratedRegex(@"[\[\]]")]
            outString = RegexBrackets().Replace(outString, "");

            int totalResult = (int)new DataTable().Compute(outString, null);

            return (totalResult.ToString(), globalCritFlag);
        }

        // Returns a list of strings with flags that comprise a rolled macro
        // flags: 0 is a regular roll, 1 is a critical hit, -1 is a critical fail, 2 is a nonroll 
        public static (string[], int[]) MacroParse(string macro)
        {
            List<string> stringList = [];
            List<int> rollFlags = [];

            int[][] bracketIdxs = BracketSearch(macro);

            if (bracketIdxs.Length == 0)
            {
                return (new string[] { macro }, new int[] { 0 });
            }

            stringList.Add(macro[0..bracketIdxs[0][0]]);
            rollFlags.Add(2);

            // Add rolls and nonroll strings to list
            for (int i = 0; i < bracketIdxs.Length; i++)
            {
                int[] idxs = bracketIdxs[i];
                string subStr = macro[idxs[0]..idxs[1]];
                (string resultString, int critFlag) = RollParse(subStr);
                stringList.Add(resultString);
                rollFlags.Add(critFlag);

                // Add nonroll string between rolls to list, skipped on last roll
                if (i < bracketIdxs.Length - 1) 
                {
                    // Capture string between rolls
                    string strBetween = macro[idxs[1]..bracketIdxs[i + 1][0]];

                    stringList.Add(strBetween);
                    rollFlags.Add(2);
                }
            }
            stringList.Add(macro[bracketIdxs[^1][1]..macro.Length]);
            rollFlags.Add(2);

            Console.WriteLine(stringList);
            return (stringList.ToArray(), rollFlags.ToArray());
        }

        [GeneratedRegex("[^(cs>)0-9d+-]")]
        private static partial Regex RegexDiceParse();

        [GeneratedRegex(@"[\[\]]")]
        private static partial Regex RegexBrackets();
    }
}
