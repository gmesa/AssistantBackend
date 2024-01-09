namespace AccountingAssistantBackend.Extensions
{
    public static class CommonExtensions
    {
        public static string ReverseString(this string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
