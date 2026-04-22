using System;

namespace MyGame.Gameplay
{
    public class RandomWordGenerator
    {
        private static String stage1Strings = "qwertyuiopasdfghjklzxcvbnm";
        private static String stage2Strings = "qwertyuiopasdfghjkl";
        private static String stage3Strings = "asdfghjkl";
        private static String stage4Strings = "awsd";
        private static Char[] stage1Words = stage1Strings.ToCharArray();
        private static Char[] stage2Words = stage2Strings.ToCharArray();
        private static Char[] stage3Words = stage3Strings.ToCharArray();
        private static Char[] stage4Words = stage4Strings.ToCharArray();
        private static Char[][] words =
        {
            stage1Words,
            stage2Words,
            stage3Words,
            stage4Words
        };
        static readonly Random _random = new Random();
        public string generateWord(int len, int stage)
        {
            String result = "";
            for (int i = 0; i < len; i++)
            {
                result += RandomChoice(words[stage]);
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
