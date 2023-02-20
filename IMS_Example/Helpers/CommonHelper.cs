using System.Text;

namespace IMS_Example.Helpers
{
    public static class CommonHelper
    {
        public static string RandomString(int length, string projectCode, int maxTaskCode)
        {
            StringBuilder code = new StringBuilder();
            code.Append(projectCode);
            code.Insert(code.Length, "0", length - maxTaskCode.ToString().Length);
            code.Append(maxTaskCode.ToString());
            return code.ToString();
        }
    }
}
