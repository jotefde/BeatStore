namespace BeatStore.API.Helpers
{
    public static class MD5Hasher
    {
        public static string Make(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes).Replace("-", string.Empty).ToLower();
            }
        }
    }
}
