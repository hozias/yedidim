using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Yedidim.App_Code
{
    public class Dates
    {
        private static HebrewCalendar hebCal = new HebrewCalendar();
        private static GregorianCalendar gCal;

        public Dates()
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
                if ((CurrentGregMonth + 1) > 12) {
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
                CurrentGregDayScope = Enumerable.Range( CurrentGregDay, 7 ).ToArray();
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
                return string.Join(" ", Yedidim.App_Code.Convertor.GregorianToJewish(CurrentGregDay, CurrentGregMonth, CurrentGregYear));
            }
        }

        public static string ToHebDate(int CurrentGregDay, int CurrentGregMonth, int CurrentGregYear)
        {
           return string.Join(" ", Yedidim.App_Code.Convertor.GregorianToJewish(CurrentGregDay, CurrentGregMonth, CurrentGregYear));
        }
        
    }
}
