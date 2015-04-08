using System;
using System.Text;

namespace Utility.Common
{
   public class PhoneVCode
    {
       public static string GenerateCheckCode()
       {
           string[] cArray = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" }; //随机字符集合
           string checkCode = ""; //产生的随机数
           int temp = -1;    //记录上次随机数值，尽量避免生产几个一样的随机数
           Random rand = new Random();
           for (int i = 1; i < 5; i++)
           {
               if (temp != -1)
               {
                   rand = new Random(i * temp * unchecked((int)DateTime.UtcNow.Ticks));
               }
               int t = rand.Next(33);
               if (temp != -1 && temp == t)
               {
                   return GenerateCheckCode();
               }
               temp = t;
               checkCode += cArray[t];
           }
           return checkCode;
       }
    }
}
