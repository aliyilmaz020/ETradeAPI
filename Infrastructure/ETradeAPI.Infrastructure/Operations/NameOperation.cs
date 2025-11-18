namespace ETradeAPI.Infrastructure.Operations
{
    public static class NameOperation
    {
        public static string CharacterRegulatory(string name)
        {
            return name.Replace("I", "i")
                 .Replace("İ", "i")
                 .Replace("ı", "i")
                 .Replace("ğ", "g")
                 .Replace("Ğ", "g")
                 .Replace("ü", "u")
                 .Replace("Ü", "u")
                 .Replace("ş", "s")
                 .Replace("Ş", "s")
                 .Replace("ö", "o")
                 .Replace("Ö", "o")
                 .Replace("ç", "c")
                 .Replace("Ç", "c")
                 .Replace("\"", "")
                 .Replace("'", "")
                 .Replace(" ", "_")
                 .Replace("?", "")
                 .Replace("!", "")
                 .Replace(".", "")
                 .Replace(",", "")
                 .Replace(";", "")
                 .Replace(":", "")
                 .Replace("(", "")
                 .Replace(")", "")
                 .Replace("[", "")
                 .Replace("]", "")
                 .Replace("{", "")
                 .Replace("}", "")
                 .Replace("\\", "")
                 .ToLower();
        }
    }
}
