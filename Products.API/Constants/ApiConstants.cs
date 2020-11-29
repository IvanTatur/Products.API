using System.Text.RegularExpressions;

namespace Products.API.Constants
{
    public static class ApiConstants
    {
        public const string PathFormat = "in" + "/{0}/{1}";

        public static readonly Regex AllowedExtensionsRegEx = new Regex(@"\.(doc|docx)$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);
    }
}