using System;

namespace MyGame.Gameplay
{
    public class RandomWordGenerator
    {
        private String[] words =
        {
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m",
            "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
        };
        static readonly Random _random = new Random();
        public string generateWord(int len)
        {
            String result = "";
            for (int i = 0; i < len; i++)
            {
                result += RandomChoice(words);
            }
            return result;
        }
        
        public static T RandomChoice<T>(T[] items)
        {
            if (items == null || items.Length == 0)
                throw new ArgumentException("The collection must not be null or empty.");

            int index = _random.Next(items.Length); 
            return items[index];
        }
    }
}
