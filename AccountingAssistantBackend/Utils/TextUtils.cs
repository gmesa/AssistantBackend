using SharpToken;
using System.Linq;
using System.Text;


namespace AccountingAssistantBackend.Utils
{
    public static class TextUtils
    {

        /// <summary>
        /// Splits the given text into chunks of a specified size.
        /// </summary>
        /// <param name="text">The text to be split into chunks.</param>
        /// <param name="shunkSize">The size of each chunk.</param>
        /// <returns>A List<string> containing the chunks of the text.</returns>
        public static List<string> SplitTextIntoShunks(string text, int shunkSize, string model)
        {
            var encoding = GptEncoding.GetEncodingForModel(model);
            var words = encoding.Encode(text);
            var chunks = new List<string>();
            for (int i = 0; i < words.Count; i += shunkSize)
                chunks.Add(encoding.Decode(words.Skip(i).Take(shunkSize).ToList()));
            return chunks;
        }

        /// <summary>
        /// Returns a list of tokens from the given text
        /// </summary>
        /// <param name="text">The text to retrieve the tokens </param>
        /// <param name="model">The encoding model</param>
        /// <returns></returns>
        public static List<int> GetTokensFromText(string text, string model)
        {
            var encoding = GptEncoding.GetEncodingForModel(model);
            return encoding.Encode(text);            
        }

        /// <summary>
        /// Takes a list of integers representing tokens and a string representing
        /// a model. It returns a string that represents the decoded text from the tokens using the 
        /// provided model.
        /// </summary>
        /// <param name="text">A list of integers representing tokens. </param>
        /// <param name="model">A string representing the model</param>
        /// <returns></returns>
        public static string GetTextFromTokens(List<int> tokens, string model)
        {
            var encoding = GptEncoding.GetEncodingForModel(model);
            return encoding.Decode(tokens);
        }
    }
}
