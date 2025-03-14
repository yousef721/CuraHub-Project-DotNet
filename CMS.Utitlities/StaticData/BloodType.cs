using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Utitlities.StaticData
{
    public static class BloodType
    {
        public const string A_Positive =   "A+";
        public const string A_Negative =   "A-";
        public const string B_Positive =   "B+";
        public const string B_Negative =   "B-";
        public const string AB_Positive =  "AB+";
        public const string AB_Negative = "AB-";
        public const string O_Positive =   "O+";
        public const string O_Negative =   "O-";

        public static List<string> BloodTypes = ["A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-"];
    }
}
