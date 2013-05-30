using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YedideyChabad.Models;

namespace YedideyChabad.Code
{
    public static class Globals
    {
        //public static int OwnerId { get; set; }
        public static string SelectedMemberId { get; set; }
        public static string SelectedMemberFullName { get; set; }
        public static string SelectedChildFullName { get; set; }

        public static ObjectId OwnerId
        {
            get
            {
                return LoggedInUser._id;  
            }
        }

        public static User LoggedInUser
        {
            get
            {
                object o = HttpContext.Current.Session["_LoggedInUser"];
                if (o != null)
                {
                    return (User)o;
                }
                return null;
            }
        }

        public static ObjectId CreateId()
        {
            return new ObjectId();

            //string allowedChars = "0123456789";
            //char[] chars = new char[8];
            //Random rd = new Random();

            //for (int i = 0; i < 8; i++)
            //{
            //    chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            //}

            //return new string(chars);
        }
    }
}