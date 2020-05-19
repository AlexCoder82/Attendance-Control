using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AttendanceControl.API.DataAccess.Utils
{
    /// <summary>
    ///     Ojeto que gestiona las conversiones de cadenas en MD5
    /// </summary>
    public static class MD5handler
    {
        /// <summary>
        ///     Genera un md5 a partir de un string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GenerateMD5(string data)
        {

            var hashvalue = string
                .Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(data))
                    .Select(s => s.ToString("x2")));

            return hashvalue;

        }

    }
}
