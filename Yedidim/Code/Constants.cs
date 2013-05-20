using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace YedideyChabad.Code
{
    public class Constants
    {
        private static Dictionary<string, bool> SystemTests = new Dictionary<string, bool>();
        public Constants() { }

        public static string KEY_ADD_NEW = "new";

        public static string TISHRI = "תשרי";
        public static string HESHVAN = "חשוון";
        public static string KISLEV = "כסלו";
        public static string TEVET = "טבת";
        public static string SHVAT = "שבט";
        public static string ADAR = "אדר";
        public static string ADAR_A = "'אדר א";
        public static string ADAR_B = "'אדר ב";
        public static string NISAN = "ניסן";
        public static string IYAR = "אייר";
        public static string SIVAN = "סיוון";
        public static string TAMUZ = "תמוז";
        public static string AV = "אב";
        public static string ELUL = "אלול";
        //public static string BackupPath = "";
        public static string BeitChabadName = "";

        public static bool validEmailSettings = false;
        public static bool validEvents = false;

        public static string[] HEB_DAY = new string[] { "", "א", "ב", "ג", "ד", "ה", "ו", "ז", "ח", "ט", "י", "יא", "יב", "יג", "יד", "טו", "טז", "יז", "יח", "יט", "כ", "כא", "כב", "כג", "כד", "כה", "כו", "כז", "כח", "כט", "ל" };

        public static int TL_ERROR = 1;
        public static int TL_WARNNING = 2;
        public static int TL_INFO = 3;

        //TODO: http://dotnetperls.com/trygetvalue
        public enum EventType
        {
            Birthday = 0,
            Yorzayt = 1,
            Class = 2,
            Holidays = 3,
            ChabadDays = 4
        }

        private static bool IsEmailSettingsValid()
        {
            if (validEmailSettings)
                return true;
            else
                return false;
        }

        private static bool IsValidEventsThisWeek()
        {
            if (validEvents)
                return true;
            else
                return false;
        }
    }
}
