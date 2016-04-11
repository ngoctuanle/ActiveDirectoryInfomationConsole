using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAD2
{
    class User
    {
        /// <summary>
        /// Các giá trị lưu trong lớp User tương ứng
        /// - SAMAccountName: Tên đăng nhập của User
        /// - commonName: tên đầy đủ của User
        /// - ou: OU của User
        /// </summary>
        public string SAMAccountName { get; set; }
        public string commonName { get; set; }
        public string ou { get; set; }
    }
}
