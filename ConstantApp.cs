using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManage
{
    internal class ConstantApp
    {
        public static string columnId = "id";
        public static string columnFullname = "họ và tên";
        public static string columnDOB = "ngày sinh";
        public static string columnClass = "lớp";
        public static string columnSex = "giới tính";
        public static string columnArgScore = "điểm trung bình";
        public static string columnNameSub = "tên môn học";
        public static string columnScore = "số điểm";
        public static List<string> subjects = new List<string> { "Môn học 1", "Môn học 2", "Môn học 3" };
        public static object[] cbSort = new object[] {
            "Mặc định",
            "Sắp xếp theo tên A-Z",
            "Sắp xếp theo điểm giảm dần"};
    }
}
