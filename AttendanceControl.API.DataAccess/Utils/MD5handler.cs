using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AttendanceControl.API.DataAccess.Utils
{
    public static class MD5handler
    {
        //Create and return the MD5 hash value of a string value
        public static string GenerateMD5(string data)
        {
            var hashvalue = string
                .Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(data))
                    .Select(s => s.ToString("x2")));

            return hashvalue;
        }
    }
}
