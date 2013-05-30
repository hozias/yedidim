using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace YedideyChabad.Code
{
    public class Utils
    {

        public static bool IsValidObjectId(string object_id){
            try 
	        {	        
		        var oid = ObjectId.Parse(object_id);
                return true;
	        }
	        catch (Exception)
	        {
		        return false;
	        }
        }

    }
}