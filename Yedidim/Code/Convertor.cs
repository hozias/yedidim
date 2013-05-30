using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.IO;
using System.Xml;

namespace YedideyChabad.Code
{
    public class Convertor
    {
        private static HebrewCalendar _heb = new HebrewCalendar();
        private static Dictionary<int, string> gimatria = new Dictionary<int, string>();
        private static Dictionary<string, string> DayOfWeek = new Dictionary<string, string>();

        public Convertor() { }

        public static string[] GregorianToJewish(int day, int month, int year)
        {
            DateTime dt = new DateTime(year, month, day);
            int jDay = _heb.GetDayOfMonth(dt);
            int jMonth = _heb.GetMonth(dt);
            int jYear = _heb.GetYear(dt);

            string sYearJ = ConvertYearToStr(jYear);
            string jMonthHeb = GetJewishMonthName(jMonth, jYear);
            string jDayHeb = Constants.HEB_DAY[jDay];

            if (jDayHeb.Length > 1)
                jDayHeb = jDayHeb.Insert(jDayHeb.Length - 1, "\"");

            //add " before end of string
            sYearJ = sYearJ.Insert(sYearJ.Length - 1, "\"");

            string[] date = { jDayHeb, jMonthHeb, sYearJ };
            return date;
        }

        public static string ConvertDayToStr(string day)
        {
            if (DayOfWeek.Count == 0)
            {
                DayOfWeek.Add("Sunday", "א");
                DayOfWeek.Add("Monday", "ב");
                DayOfWeek.Add("Thusday", "ג");
                DayOfWeek.Add("Wednesday", "ד");
                DayOfWeek.Add("Thursday", "ה");
                DayOfWeek.Add("Friday", "ו");
                DayOfWeek.Add("Saturday", "ש");
            }

            string value;
            string hDay;
            if (DayOfWeek.TryGetValue(day, out value))
                hDay = value;
            else
                hDay = "";

            return hDay;
        }

        public static string ConvertYearToStr(int jYear)
        {
            if (gimatria.Count == 0)
            {
                gimatria.Add(1, "א");
                gimatria.Add(2, "ב");
                gimatria.Add(3, "ג");
                gimatria.Add(4, "ד");
                gimatria.Add(5, "ה");
                gimatria.Add(6, "ו");
                gimatria.Add(7, "ז");
                gimatria.Add(8, "ח");
                gimatria.Add(9, "ט");
                gimatria.Add(10, "י");
                gimatria.Add(20, "כ");
                gimatria.Add(30, "ל");
                gimatria.Add(40, "מ");
                gimatria.Add(50, "נ");
                gimatria.Add(60, "ס");
                gimatria.Add(70, "ע");
                gimatria.Add(80, "פ");
                gimatria.Add(90, "צ");
                gimatria.Add(100, "ק");
                gimatria.Add(200, "ר");
                gimatria.Add(300, "ש");
                gimatria.Add(400, "ת");
                gimatria.Add(500, "'תק");
                gimatria.Add(600, "'תר");
                gimatria.Add(700, "'תש");
                gimatria.Add(800, "'תת");
                gimatria.Add(900, "'תתק");
                gimatria.Add(1000, "'תתר");
            }

            var sYear = jYear.ToString();
            //TODO: while loop >> on each itteration trim the first char
            int segment0 = int.Parse(sYear.Substring(0, sYear.Length));
            int segment1 = int.Parse(sYear.Substring(1, sYear.Length - 1));
            int segment2 = int.Parse(sYear.Substring(2, sYear.Length - 2));
            int segment3 = int.Parse(sYear.Substring(3, sYear.Length - 3));

            int[] arrYear = new int[4];

            arrYear[0] = (segment0 - segment1) / 1000;
            arrYear[1] = (segment1 - segment2);
            arrYear[2] = (segment2 - segment3);
            arrYear[3] = (segment3);

            string strJYear = "";

            for (int i = 0; i < arrYear.Length; i++)
            {
                string value;

                if (gimatria.TryGetValue(arrYear[i], out value))
                {
                    strJYear += value;
                }
            }


            arrYear = null;
            return strJYear;
        }

        public static string GetJewishMonthName(int jMonth, int jYear)
        {
            int leap = jYear;
            string jMonthHeb = string.Empty;

            if (_heb.IsLeapYear(leap))
            {
                if (jMonth == 1)
                    jMonthHeb = Constants.TISHRI;
                else if (jMonth == 2)
                    jMonthHeb = Constants.HESHVAN;
                else if (jMonth == 3)
                    jMonthHeb = Constants.KISLEV;
                else if (jMonth == 4)
                    jMonthHeb = Constants.TEVET;
                else if (jMonth == 5)
                    jMonthHeb = Constants.SHVAT;
                else if (jMonth == 6)
                    jMonthHeb = Constants.ADAR_A;
                else if (jMonth == 7)
                    jMonthHeb = Constants.ADAR_B;
                else if (jMonth == 8)
                    jMonthHeb = Constants.NISAN;
                else if (jMonth == 9)
                    jMonthHeb = Constants.IYAR;
                else if (jMonth == 10)
                    jMonthHeb = Constants.SIVAN;
                else if (jMonth == 11)
                    jMonthHeb = Constants.TAMUZ;
                else if (jMonth == 12)
                    jMonthHeb = Constants.AV;
                else if (jMonth == 13)
                    jMonthHeb = Constants.ELUL;
            }
            else
            {
                if (jMonth == 1)
                    jMonthHeb = Constants.TISHRI;
                else if (jMonth == 2)
                    jMonthHeb = Constants.HESHVAN;
                else if (jMonth == 3)
                    jMonthHeb = Constants.KISLEV;
                else if (jMonth == 4)
                    jMonthHeb = Constants.TEVET;
                else if (jMonth == 5)
                    jMonthHeb = Constants.SHVAT;
                else if (jMonth == 6)
                    jMonthHeb = Constants.ADAR;
                else if (jMonth == 7)
                    jMonthHeb = Constants.NISAN;
                else if (jMonth == 8)
                    jMonthHeb = Constants.IYAR;
                else if (jMonth == 9)
                    jMonthHeb = Constants.SIVAN;
                else if (jMonth == 10)
                    jMonthHeb = Constants.TAMUZ;
                else if (jMonth == 11)
                    jMonthHeb = Constants.AV;
                else if (jMonth == 12)
                    jMonthHeb = Constants.ELUL;
            }

            return jMonthHeb;
        }


    }

    public class HebrewDate
    {
        private static HebrewCalendar hebCal = new HebrewCalendar();
        private static GregorianCalendar gCal;

        public HebrewDate()
        {
            hebCal = new HebrewCalendar();
            DateTime dt = DateTime.Now;
            NextGregMonth = 0;
            CurrentGregDay = dt.Day;
            CurrentGregMonth = dt.Month;
            CurrentGregYear = dt.Year;
            GetGregDateScope();

            NextJewishMonth = 0;
            //NextJewishMonthScope = 0;
            CurrentJewishDay = hebCal.GetDayOfMonth(dt);
            CurrentJewishMonth = hebCal.GetMonth(dt);
            CurrentJewishYear = hebCal.GetYear(dt);
            GetJewishDaysScope();
        }


        #region GetGregDateScope
        private void GetGregDateScope()
        {
            gCal = new GregorianCalendar();

            int _currentGregMonthLength = gCal.GetDaysInMonth(CurrentGregYear, CurrentGregMonth);
            //int[] _scope = new int[7];

            if ((CurrentGregDay + 6) > _currentGregMonthLength)
            {
                //if next month
                if ((CurrentGregMonth + 1) > 12)
                {
                    //if next year
                    NextGregMonth = 1;
                    NextGregYear = CurrentGregYear + 1;
                }
                else
                    NextGregMonth = CurrentGregMonth + 1;

                //create new dates scope
                int[] _scopeInCurrentMonth = Enumerable.Range(CurrentGregDay, (_currentGregMonthLength - CurrentGregDay + 1)).ToArray();
                int[] _scopeInNextMonth = Enumerable.Range(1, (Math.Abs(_currentGregMonthLength - (CurrentGregDay + 6)))).ToArray();

                //_scopeInCurrentMonth.CopyTo(_scope, 0);
                //_scopeInNextMonth.CopyTo(_scope, _scopeInCurrentMonth.Length);
                CurrentGregDayScope = _scopeInCurrentMonth;
                NextGregMonthScope = _scopeInNextMonth;

            }
            else
            {
                //if current month
                //_scope = Enumerable.Range( CurrentGregDay, 7 ).ToArray();
                CurrentGregDayScope = Enumerable.Range(CurrentGregDay, 7).ToArray();
            }

            //return _scope;
        }
        #endregion

        #region GetJewishDaysScope
        private void GetJewishDaysScope()
        {
            DateTime dt = new DateTime(CurrentGregYear, CurrentGregMonth, CurrentGregDay);

            //get nuber of days in jewish month
            int _currentJewishMonthLength = hebCal.GetDaysInMonth(hebCal.GetYear(dt), hebCal.GetMonth(dt));
            int _leapYearMonthes = hebCal.IsLeapYear(CurrentJewishYear) ? 13 : 12; //if IsLeapYear then 13 monthe else 12 months in year

            if ((CurrentJewishDay + 6) > _currentJewishMonthLength)
            {
                if ((CurrentJewishMonth + 1) > _leapYearMonthes)
                {
                    NextJewishMonth = 1;
                    NextJewishYear = CurrentJewishYear + 1;
                }
                else
                    NextJewishMonth = CurrentJewishMonth + 1;

                int[] _scopeInCurrentMonth = Enumerable.Range(CurrentJewishDay, (_currentJewishMonthLength - CurrentJewishDay + 1)).ToArray();
                int[] _scopeInNextMonth = Enumerable.Range(1, (Math.Abs(_currentJewishMonthLength - (CurrentJewishDay + 6)))).ToArray();

                CurrentJewishDayScope = _scopeInCurrentMonth;
                NextJewishMonthScope = _scopeInNextMonth;
            }
            else
            {
                //if current month
                CurrentJewishDayScope = Enumerable.Range(CurrentJewishDay, 7).ToArray();
            }
            //return _scope;
        }
        #endregion


        public int CurrentGregDay { get; set; }
        public int[] CurrentGregDayScope { get; set; } //CurrentDay + 7
        public int CurrentGregMonth { get; set; }
        public int CurrentGregYear { get; set; }
        public int NextGregMonth { get; set; } //if(CurrentDayScope) > 30,31...
        public int[] NextGregMonthScope { get; set; }
        public int NextGregYear { get; set; }

        public int CurrentJewishDay { get; set; }
        public int[] CurrentJewishDayScope { get; set; }
        public int CurrentJewishMonth { get; set; }
        public int CurrentJewishYear { get; set; }
        public int NextJewishMonth { get; set; } //if(CurrentDayScope) > 30,31...
        public int[] NextJewishMonthScope { get; set; }
        public int NextJewishYear { get; set; }

        public string JewishDate
        {
            get
            {
                return string.Join(" ", YedideyChabad.Code.Convertor.GregorianToJewish(CurrentGregDay, CurrentGregMonth, CurrentGregYear));
            }
        }

        public static string ToHebDate(int CurrentGregDay, int CurrentGregMonth, int CurrentGregYear)
        {
            return string.Join(" ", YedideyChabad.Code.Convertor.GregorianToJewish(CurrentGregDay, CurrentGregMonth, CurrentGregYear));
        }
    }

}
