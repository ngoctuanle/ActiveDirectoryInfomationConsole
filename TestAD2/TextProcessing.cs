using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAD2
{
    class TextProcessing
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sResult"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static string getProperty(SearchResult sResult, string propertyName)
        {
            if (sResult.Properties.Contains(propertyName))
                return sResult.Properties[propertyName][0].ToString();
            else
                return string.Empty;
        }

        /// <summary>
        /// Hiển thị các dòng lệnh để đăng nhập vào chương trình
        /// người sử dụng điền vào username và password
        /// </summary>
        /// <returns>
        ///     string[2] với username ([0]) và password ([1])
        /// </returns>
        public static string[] login()
        {
            Console.Write("Please login to Server\nEnter your username: ");
            string username = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = ReadPassword();
            string[] result = new string[2];
            result[0] = username;
            result[1] = password;
            return result;
        }
        
        /// <summary>
        /// Che password trên Console khi nhập liệu
        /// </summary>
        /// <returns>
        ///     string chứa password người dùng nhập vào
        /// </returns>
        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        // remove one character from the list of password characters
                        password = password.Substring(0, password.Length - 1);
                        // get the location of the cursor
                        int pos = Console.CursorLeft;
                        // move the cursor to the left by one character
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        // replace it with space
                        Console.Write(" ");
                        // move the cursor to the left by one character again
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }

            // add a new line because user pressed enter at the end of their password
            Console.WriteLine();
            return password;
        }


        /// <summary>
        ///   
        /// </summary>
        /// <param name="str">Chua CN, DC, OU. Vi du "CN=techlinkvn,DC=techlinkvn.com,OU=BM KTMT" </param>
        /// <returns>
        ///     string chứa OU
        /// </returns>
        public static string getOU(string str)
        {
            string ou = "";
            string[] array = str.Split(',');
            foreach(string s in array)
            {
                if (s.Contains("OU="))
                {
                    ou = s.Substring(3);
                }
            }
            return ou;
        }
    }
}
