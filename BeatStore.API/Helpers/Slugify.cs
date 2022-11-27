using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace BeatStore.API.Helpers
{
    public static class Slugify
    {
        public static string Generate(string phrase)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding(0).GetBytes(phrase);
            string str = System.Text.Encoding.ASCII.GetString(bytes).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }
    }
}
