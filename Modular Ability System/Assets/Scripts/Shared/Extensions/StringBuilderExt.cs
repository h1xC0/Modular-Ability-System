using System;
using System.Text;
using UnityEngine;

namespace Shared.Extensions
{
    public static class StringBuilderExt
    {
        public static void SearchAndReplaceLastWord(this StringBuilder stringBuilder, string word, string replaceWord)
        {
            var stringIndex = stringBuilder.ToString().LastIndexOf(word, StringComparison.Ordinal);
            Debug.Log($"Last string index in order {stringIndex}");

            if (stringBuilder.ToString().Contains(word) == false) return;
            stringBuilder.Replace(word, replaceWord, stringIndex, word.Length);
        }
    }
}