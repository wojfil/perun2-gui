/*
    This file is part of Perun2.
    Perun2 is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.
    Perun2 is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
    GNU General Public License for more details.
    You should have received a copy of the GNU General Public License
    along with Perun2. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perun2Gui.Hint
{
    class HintGenerator
    {
        const int MAX_RECENT_YEARS = 25;

        private HintArray A_StackForce;
        private HintArray A_CommandsSF;
        private HintArray A_Commands;
        private HintArray A_MultiCoreVariables;
        private HintArray A_Functions;
        private HintArray A_AllVariables;
        private HintArray A_BCEE;
        private HintArray A_Semicolon;
        private HintArray A_String;
        private HintArray A_CoreCommands;
        private HintArray A_To;
        private HintArray A_With;
        private HintArray A_CoreCommandsTo;
        private HintArray A_CoreCommandsWith;
        private HintArray A_OneSecond;
        private HintArray A_NameFullname;
        private HintArray A_SingleVariables;
        private HintArray A_Extensionless;
        private HintArray A_Extension;
        private HintArray A_Name;
        private HintArray A_AllAttrVariables;
        private HintArray A_Not;
        private HintArray A_BoolAttrVariables;
        private HintArray A_One;
        private HintArray A_By;
        private HintArray A_Filters;
        private HintArray A_FiltersWithSpace;
        private HintArray A_TAFW;
        private HintArray A_AndOrXor;
        private HintArray A_AscDesc;
        private HintArray A_Times;
        private HintArray A_PeriodSingle;
        private HintArray A_PeriodMulti;
        private HintArray A_Months;
        private HintArray A_DaysOfTheWeek;
        private HintArray A_RecentYears;
        private HintArray A_Plus;
        private HintArray A_BoolOperator_FirstLike;
        private HintArray A_BoolOperator_FirstIn;
        private HintArray A_Equals;
        private HintArray A_MultiVariables;
        private HintArray A_Operators;
        private HintArray A_BoolFunctions;
        private HintArray A_Limit;
        private HintArray A_Brackets;
        private HintArray A_CurlyBrackets;
        private HintArray A_DirectoriesRec;
        private HintArray A_TimeMember;
        private HintArray A_If;
        private HintArray A_Perun2;
        private HintArray A_AfterNot;
        private HintArray A_DirectoryFunctions;

        private string RecentValue;
        private HintType RecentHintType;
        private string OriginWord;
        private string CurrentWord;

        public HintGenerator()
        {
            Init();
        }

        private void Init()
        {
            A_StackForce = new HintArray(new string[] { "stack", "force" });
            A_CommandsSF = new HintArray(new string[] { "copy", "create", "createDirectory", "createFile", "createDirectories", "createFiles", "move", "rename" });
            A_Commands = new HintArray(new string[] { "print", "copy", "create", "createDirectory", "createFile", "createDirectories", "createFiles",
                "delete", "drop", "hide", "lock", "move", "open", 
                "select", "sleep", "rename", "recreate", "remodify", "reaccess", "rechange", "run", "unhide", "unlock", "inside", "if", "while", "else", "foreach", "popup" });
            A_MultiCoreVariables = new HintArray(new string[] { "files", "directories", "recursiveFiles", "recursiveDirectories",
                "images", "videos", "recursiveImages", "recursiveVideos"
            });
            A_Functions = new HintArray(new string[] { "substring()", "anyInside()", "after()", "absolute()", "afterDigits()", "afterLetters()", 
                "any()", "average()", "before()", "beforeDigits()", "beforeLetters()", "binary()", "contains()", "capitalize()", "ceil()",
                "characters()", "christmas()", "concatenate()", "countInside()", "count()", "digits()",
                "date()", "endsWith()", "easter()", "existsInside()", "exists()", "existInside()", "exist()", 
                "floor()", "fill()", "first()", "fromBinary()", "fromHex()", "hex()", "isDigit()", 
                "isBinary()", "isHex()", "isLetter()", "isLower()", "isNumber()",
                "isUpper()", "join()", "length()", "last()", "left()", "letters()", "lower()", "monthName()", "max()", "median()",
                "min()", "numbers()", "newYear()", "number()", "path()", "parent()", "power()", "replace()","random()",
                "repeat()", "reverse()", "right()", "round()", "roman()", "substring()", "sign()", "size()", "split()", "sqrt()", "findText()",
                "startsWith()", "string()", "sum()", "trim()", "truncate()", "time()", "upper()", "words()", "weekDayName()", "shiftMonth()", 
                "shiftWeekDay()", "isNaN()", "isNever()", "clock()", "raw()", "resemblance()", "duration()" }, HintType.Retreating);
            A_AllVariables = new HintArray(new string[] {
                "now", "today", "yesterday", "tomorrow", "desktop", "Perun2", "origin", "access",  "alphabet", "ascii", "arguments",
                "files", "recursiveFiles", "directories", "recursiveDirectories", "index", "this", "location", "success",
                "archive", "creation", "empty", "encrypted", "exists", "hidden", "isDirectory", "isFile", "readonly",
                "change", "compressed", "modification", "lifetime", "size", "depth",
                "drive", "extension", "fullName", "name", "parent", "path",
                "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December",
                "Monday", "Tuesday", "Wednesday", "Thursday", "Friday",  "Saturday", "Sunday", "true", "false", "never", "nothing",
                "pendrive", "pendrives", "downloads", "NaN", "images", "videos", "recursiveImages", "recursiveVideos",
                "isImage", "isVideo", "width", "height", "duration"
            }.Add(ConstantsKeywords.PROGRAMS));
            A_BCEE = new HintArray(new string[] { "break", "continue", "exit", "error" });
            A_Semicolon = new HintArray(new string[] { ";" });
            A_String = new HintArray(new string[] { "''" }, HintType.Retreating);
            A_CoreCommands = new HintArray(new string[] { "print", "copy", "create", "delete", "drop", "hide", "lock", "select", "unhide", "unlock" });
            A_To = new HintArray(new string[] { "to" });
            A_With = new HintArray(new string[] { "with" });
            A_CoreCommandsTo = new HintArray(new string[] { "copy", "move", "rename", "recreate", "reaccess", "rechange", "remodify" });
            A_CoreCommandsWith = new HintArray(new string[] { "open", "run" });
            A_OneSecond = new HintArray(new string[] { "1 second" });
            A_NameFullname = new HintArray(new string[] { "name", "fullName" });
            A_SingleVariables = new HintArray(new string[] {
                "extension", "now", "today", "yesterday", "tomorrow", "desktop", "perun2", "origin", "index", "this", "location", "success",
                "access", "creation", "empty", "encrypted", "exists", "hidden", "isDirectory", "isFile", "readonly",
                "archive", "change", "compressed", "modification", "lifetime", "size", "depth", "drive", "fullName", "name", "parent", "path",
                "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December",
                "Monday", "Tuesday", "Wednesday", "Thursday", "Friday",  "Saturday", "Sunday", "true", "false", "never", "nothing",
                "pendrive", "downloads", "NaN", "isImage", "isVideo", "width", "height", "duration"
            }.Add(ConstantsKeywords.PROGRAMS));
            A_Extensionless = new HintArray(new string[] { "extensionless" });
            A_Extension = new HintArray(new string[] { "extension" });
            A_Name = new HintArray(new string[] { "name" });
            A_AllAttrVariables = new HintArray(new string[] {
                "extension", "index", "this", "location", "success",
                "access", "creation", "empty", "encrypted", "exists", "hidden", "isDirectory", "isFile", "readonly",
                "archive", "change", "compressed", "modification", "lifetime", "size", "depth",
                "drive", "fullName", "name", "parent", "path", 
                "isImage", "isVideo", "width", "height", "duration"
            });
            A_Not = new HintArray(new string[] { "not" });
            A_BoolAttrVariables = new HintArray(new string[] { "empty", "archive", "compressed", "encrypted", "exists",
                "hidden", "isFile", "isDirectory", "readonly", "success", "true", "false",
                "isImage", "isVideo" });
            A_One = new HintArray(new string[] { "1" });
            A_By = new HintArray(new string[] { "by" });
            A_Filters = new HintArray(new string[] { "where", "every", "limit", "order by", "skip", "final" });
            A_FiltersWithSpace = new HintArray(new string[] { " where", " every", " limit", " order by", " skip", " final" });
            A_TAFW = new HintArray(new string[] { "to", "as", "with" });
            A_AndOrXor = new HintArray(new string[] { "and", "or", "xor" });
            A_AscDesc = new HintArray(new string[] { "asc", "desc" });
            A_Times = new HintArray(new string[] { "times" });
            A_PeriodSingle = new HintArray(new string[] { "day", "year", "week", "hour", "month", "minute", "second" });
            A_PeriodMulti = new HintArray(new string[] { "days", "years", "weeks", "hours", "months", "minutes", "seconds" });
            A_Plus = new HintArray(new string[] { "+" });
            A_BoolOperator_FirstLike = new HintArray(new string[] { "like", "not like", "in", "not in", "resembles", "not resembles",
                "between", "not between", "regexp", "not regexp" });
            A_BoolOperator_FirstIn = new HintArray(new string[] { "in", "not in", "like", "not like", "resembles", "not resembles",
                "between", "not between", "regexp", "not regexp" });
            A_Equals = new HintArray(new string[] { "=" });
            A_MultiVariables = new HintArray(new string[] { "files", "alphabet", "arguments", "ascii",
                "directories", "recursiveFiles", "recursiveDirectories", "pendrives",
                "images", "videos", "recursiveImages", "recursiveVideos"
            });
            A_Operators = new HintArray(new string[] { ",", "[", ":", "-", "+", "%", "?" });
            A_BoolFunctions = new HintArray(new string[] { "startsWith()", "anyInside()", "any()", "contains()",
                "endsWith()", "empty()", "existsInside()", "exists()", "existInside()", "exist()", "hidden()", "isDigit()", "isBinary()", "isHex()",
                "isLetter()", "isLower()", "isNumber()", "isUpper()", "random()", "findText()", "isNaN()", "isNever()" }, HintType.Retreating);
            A_Limit = new HintArray(new string[] { "limit" });
            A_Brackets = new HintArray(new string[] { "()" }, HintType.Retreating);
            A_CurlyBrackets = new HintArray(new string[] { "{}" }, HintType.Retreating);
            A_DirectoriesRec = new HintArray(new string[] { "directories", "recursiveDirectories" });
            A_TimeMember = new HintArray(new string[] { "day", "year", "weekDay", "hour", "month", "minute", "second" });
            A_If = new HintArray(new string[] { "if" });
            A_Perun2 = new HintArray(new string[] { "Perun2" });
            A_AfterNot = new HintArray(new string[] { "empty", "like", "in", "true", "false" });
            A_DirectoryFunctions = new HintArray(new string[] { "anyInside()", "countInside()", "existsInside()", "existInside()" }, HintType.Retreating);

            InitTimeWords();
        }

        private void InitTimeWords()
        {
            DateTime now = DateTime.Now;
            int m = now.Month;

            List<string> months = new List<string>();
            foreach (var s in Constants.MONTH_NAMES)
            {
                months.Add(s.ToLower());
            }
            string month = months[m - 1];
            months.RemoveAt(m - 1);
            months.Insert(0, month);

            // and days of the week
                
            int w = (int)now.DayOfWeek;
            if (w == 0)
            {
                w = 7;
            }

            List<string> wdays = new List<string>();
            foreach (var s in Constants.WEEK_DAY_NAMES)
            {
                wdays.Add(s.ToLower());
            }
            string wday = wdays[w - 1];
            wdays.RemoveAt(w - 1);
            wdays.Insert(0, wday);

            // and years

            int year = now.Year;
            List<string> years = new List<string>();

            for (int i = 0; i < MAX_RECENT_YEARS; i++)
            {
                years.Add((year - i).ToString());
            }

            // finally init arrays

            A_Months = new HintArray(months.ToArray());
            A_DaysOfTheWeek = new HintArray(wdays.ToArray());
            A_RecentYears = new HintArray(years.ToArray());
        }

        private bool CheckMatch(HintArray array)
        {
            if (array.Matches(CurrentWord))
            {
                RecentValue = array.GetRecentValue(OriginWord);
                RecentHintType = array.GetHintType();
                return true;
            }
            return false;
        }

        public string GetRecentValue()
        {
            return RecentValue;
        }

        public HintType GetRecentHintType()
        {
            return RecentHintType;
        }

        public bool Matches(string prevWord, string currentWord)
        {
            OriginWord = currentWord;
            CurrentWord = currentWord.ToLower();
            string prev = prevWord.ToLower();

            if (prev.IsEmpty())
            {
                return CheckMatch(A_Commands)
                    || CheckMatch(A_MultiCoreVariables)
                    || CheckMatch(A_StackForce)
                    || CheckMatch(A_AllAttrVariables)
                    || CheckMatch(A_Functions)
                    || CheckMatch(A_AllVariables);
            }
            else if (prev.Equals("stack") || prev.Equals("force"))
            {
                return CheckMatch(A_CommandsSF);
            }
            else if (prev.Equals("{") || prev.Equals("}") || prev.Equals(";"))
            {
                return CheckMatch(A_Commands)
                    || CheckMatch(A_StackForce)
                    || CheckMatch(A_MultiCoreVariables)
                    || CheckMatch(A_AllAttrVariables)
                    || CheckMatch(A_Functions)
                    || CheckMatch(A_AllVariables)
                    || CheckMatch(A_BCEE);
            }
            else if (A_BCEE.ContainsValue(prev))
            {
                return CheckMatch(A_Semicolon);
            }
            else if (A_CoreCommands.ContainsValue(prev))
            {
                return CheckMatch(A_MultiCoreVariables)
                    || CheckMatch(A_AllAttrVariables)
                    || CheckMatch(A_Functions)
                    || CheckMatch(A_AllVariables);
            }
            else if (prev.Equals("foreach"))
            {
                return CheckMatch(A_MultiCoreVariables);
            }
            else if (A_CoreCommandsTo.ContainsValue(prev))
            {
                return CheckMatch(A_MultiCoreVariables)
                    || CheckMatch(A_To)
                    || CheckMatch(A_AllAttrVariables)
                    || CheckMatch(A_Functions)
                    || CheckMatch(A_AllVariables);
            }
            else if (A_CoreCommandsWith.ContainsValue(prev))
            {
                return CheckMatch(A_String)
                    || CheckMatch(A_MultiCoreVariables)
                    || CheckMatch(A_With)
                    || CheckMatch(A_AllAttrVariables)
                    || CheckMatch(A_Functions)
                    || CheckMatch(A_AllVariables);
            }
            else if (prev.Equals("sleep"))
            {
                return CheckMatch(A_OneSecond);
            }
            else if (prev.Equals("create"))
            {
                return CheckMatch(A_MultiCoreVariables)
                    || CheckMatch(A_AllAttrVariables)
                    || CheckMatch(A_Functions)
                    || CheckMatch(A_AllVariables);
            }
            else if (prev.Equals("with"))
            {
                return CheckMatch(A_Perun2)
                    || CheckMatch(A_String)
                    || CheckMatch(A_NameFullname)
                    || CheckMatch(A_AllAttrVariables)
                    || CheckMatch(A_Functions)
                    || CheckMatch(A_AllVariables);
            }
            else if (prev.Equals("as") || prev.Equals("to"))
            {
                return CheckMatch(A_String)
                    || CheckMatch(A_NameFullname)
                    || CheckMatch(A_Extensionless)
                    || CheckMatch(A_AllAttrVariables)
                    || CheckMatch(A_Functions)
                    || CheckMatch(A_SingleVariables)
                    || CheckMatch(A_AllVariables);
            }
            else if (prev.Equals("extensionless"))
            {
                return CheckMatch(A_Name)
                    || CheckMatch(A_AllAttrVariables)
                    || CheckMatch(A_Functions)
                    || CheckMatch(A_AllVariables);
            }
            else if (prev.Equals("where"))
            {
                return CheckMatch(A_Extension)
                    || CheckMatch(A_Not)
                    || CheckMatch(A_AllAttrVariables)
                    || CheckMatch(A_DirectoryFunctions)
                    || CheckMatch(A_AllVariables)
                    || CheckMatch(A_Functions);
            }
            else if (prev.Equals("limit") || prev.Equals("skip") || prev.Equals("every") || prev.Equals("final"))
            {
                return CheckMatch(A_One)
                    || CheckMatch(A_AllAttrVariables)
                    || CheckMatch(A_Functions);
            }
            else if (prev.Equals("order"))
            {
                return CheckMatch(A_By);
            }
            else if (A_MultiCoreVariables.ContainsValue(prev))
            {
                return CheckMatch(A_Filters)
                    || CheckMatch(A_TAFW);
            }
            else if (A_BoolAttrVariables.ContainsValue(prev))
            {
                return CheckMatch(A_AndOrXor)
                    || CheckMatch(A_Filters)
                    || CheckMatch(A_AscDesc)
                    || CheckMatch(A_TAFW);
            }
            else if (prev.Equals("1"))
            {
                return CheckMatch(A_PeriodSingle)
                    || CheckMatch(A_Months)
                    || CheckMatch(A_Filters)
                    || CheckMatch(A_AndOrXor)
                    || CheckMatch(A_Times)
                    || CheckMatch(A_AscDesc)
                    || CheckMatch(A_TAFW);
            }
            else if (A_Months.ContainsValue(prev))
            {
                return CheckMatch(A_RecentYears)
                    || CheckMatch(A_Filters)
                    || CheckMatch(A_AndOrXor)
                    || CheckMatch(A_AscDesc)
                    || CheckMatch(A_TAFW);
            }
            else if (prev.Equals("'"))
            {
                return CheckMatch(A_Plus)
                    || CheckMatch(A_Filters)
                    || CheckMatch(A_AndOrXor)
                    || CheckMatch(A_AscDesc)
                    || CheckMatch(A_TAFW);
            }
            else if (prev.Equals("extension"))
            {
                return CheckMatch(A_Equals)
                    || CheckMatch(A_BoolOperator_FirstIn)
                    || CheckMatch(A_AndOrXor)
                    || CheckMatch(A_AscDesc)
                    || CheckMatch(A_TAFW);
            }
            else if (prev.Equals("name") || prev.Equals("fullname"))
            {
                return CheckMatch(A_BoolOperator_FirstLike)
                    || CheckMatch(A_AndOrXor)
                    || CheckMatch(A_AscDesc)
                    || CheckMatch(A_TAFW);
            }
            else if (A_SingleVariables.ContainsValue(prev))
            {
                return CheckMatch(A_Equals)
                    || CheckMatch(A_AndOrXor)
                    || CheckMatch(A_AscDesc)
                    || CheckMatch(A_BoolOperator_FirstLike)
                    || CheckMatch(A_TAFW);
            }
            else if (A_MultiVariables.ContainsValue(prev))
            {
                return CheckMatch(A_Equals)
                    || CheckMatch(A_AndOrXor)
                    || CheckMatch(A_Filters)
                    || CheckMatch(A_AscDesc)
                    || CheckMatch(A_TAFW);
            }
            else if (prev.Equals("="))
            {
                return CheckMatch(A_String)
                    || CheckMatch(A_NameFullname)
                    || CheckMatch(A_AllAttrVariables)
                    || CheckMatch(A_Functions)
                    || CheckMatch(A_Not)
                    || CheckMatch(A_AllVariables);
            }
            else if (prev.Equals("by") || A_Operators.ContainsValue(prev))
            {
                return CheckMatch(A_NameFullname)
                    || CheckMatch(A_AllAttrVariables)
                    || CheckMatch(A_Functions)
                    || CheckMatch(A_Not)
                    || CheckMatch(A_AllVariables);
            }
            else if (prev.Equals("*"))
            {
                return CheckMatch(A_FiltersWithSpace)
                    || CheckMatch(A_NameFullname)
                    || CheckMatch(A_AllAttrVariables)
                    || CheckMatch(A_Functions)
                    || CheckMatch(A_Not)
                    || CheckMatch(A_AllVariables);
            }
            else if (prev.Equals("("))
            {
                return CheckMatch(A_MultiCoreVariables)
                    || CheckMatch(A_NameFullname)
                    || CheckMatch(A_AllAttrVariables)
                    || CheckMatch(A_Functions)
                    || CheckMatch(A_Not)
                    || CheckMatch(A_AllVariables);
            }
            else if (prev.Equals("/"))
            {
                return CheckMatch(A_NameFullname)
                    || CheckMatch(A_AllAttrVariables)
                    || CheckMatch(A_Functions)
                    || CheckMatch(A_Not)
                    || CheckMatch(A_AllVariables)
                    || CheckMatch(A_Commands);
            }
            else if (prev.Equals("!") || prev.Equals("<") || prev.Equals(">"))
            {
                return CheckMatch(A_Equals)
                    || CheckMatch(A_NameFullname)
                    || CheckMatch(A_AllAttrVariables)
                    || CheckMatch(A_Functions)
                    || CheckMatch(A_Not)
                    || CheckMatch(A_AllVariables);
            }
            else if (prev.Equals("not"))
            {
                return CheckMatch(A_AfterNot)
                    || CheckMatch(A_AllAttrVariables)
                    || CheckMatch(A_BoolFunctions)
                    || CheckMatch(A_Functions)
                    || CheckMatch(A_Not)
                    || CheckMatch(A_AllVariables);
            }
            else if (prev.Equals("or") || prev.Equals("xor") ||
                prev.Equals("and") || prev.Equals("if") || prev.Equals("while"))
            {
                return CheckMatch(A_AllAttrVariables)
                    || CheckMatch(A_BoolFunctions)
                    || CheckMatch(A_Functions)
                    || CheckMatch(A_Not)
                    || CheckMatch(A_AllVariables);
            }
            else if (prev.Equals(")") || prev.Equals("]"))
            {
                return CheckMatch(A_Plus)
                    || CheckMatch(A_AndOrXor)
                    || CheckMatch(A_AscDesc)
                    || CheckMatch(A_TAFW);
            }
            else if (A_AllVariables.ContainsValue(prev))
            {
                return CheckMatch(A_Plus)
                    || CheckMatch(A_BoolOperator_FirstLike)
                    || CheckMatch(A_AndOrXor)
                    || CheckMatch(A_AscDesc)
                    || CheckMatch(A_TAFW);
            }
            else if (prev.Equals("asc") || prev.Equals("desc"))
            {
                return CheckMatch(A_Limit)
                    || CheckMatch(A_Filters)
                    || CheckMatch(A_TAFW);
            }
            else if (prev.Equals("like") || prev.Equals("resembles") 
                || prev.Equals("between") || prev.Equals("popup") || prev.Equals("regexp"))
            {
                return CheckMatch(A_String);
            }
            else if (prev.Equals("in"))
            {
                return CheckMatch(A_Brackets);
            }
            else if (prev.Equals("else"))
            {
                return CheckMatch(A_If);
            }
            else if (prev.Equals("times"))
            {
                return CheckMatch(A_CurlyBrackets);
            }
            else if (prev.Equals("inside"))
            {
                return CheckMatch(A_String)
                    || CheckMatch(A_DirectoriesRec)
                    || CheckMatch(A_AllAttrVariables)
                    || CheckMatch(A_Functions)
                    || CheckMatch(A_AllVariables);
            }
            else if (prev.Equals("."))
            {
                return CheckMatch(A_TimeMember);
            }

            int n;
            if (int.TryParse(prev, out n))
            {
                if (n == 0)
                {
                    return CheckMatch(A_Equals);
                }
                else if (n <= 31)
                {
                    return CheckMatch(A_Months)
                        || CheckMatch(A_PeriodMulti)
                        || CheckMatch(A_Filters)
                        || CheckMatch(A_AndOrXor)
                        || CheckMatch(A_Times)
                        || CheckMatch(A_AscDesc)
                        || CheckMatch(A_TAFW);
                }
                else if (n < 1950 || n > 2100)
                {
                    return CheckMatch(A_PeriodMulti)
                        || CheckMatch(A_Filters)
                        || CheckMatch(A_AndOrXor)
                        || CheckMatch(A_Times)
                        || CheckMatch(A_AscDesc)
                        || CheckMatch(A_TAFW);
                }
                else
                {
                    return CheckMatch(A_AndOrXor)
                        || CheckMatch(A_Filters)
                        || CheckMatch(A_Times)
                        || CheckMatch(A_AscDesc)
                        || CheckMatch(A_TAFW)
                        || CheckMatch(A_PeriodMulti);
                }

            }

            if (IsVariableName(prev))
            {
                return CheckMatch(A_Equals)
                    || CheckMatch(A_AscDesc)
                    || CheckMatch(A_Filters)
                    || CheckMatch(A_TAFW)
                    || CheckMatch(A_BoolOperator_FirstLike);
            }

            return false;
        }

        public bool IsVariableName(string prev)
        {
            return prev.All(ch => ch.IsPerun2WordChar());
        }

    }
}
