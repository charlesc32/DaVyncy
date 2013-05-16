using System;
using System.Collections.Concurrent;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        var input = new Dictionary<int, string>();
        var output = new ConcurrentDictionary<int, string>();
        int order = 0;
        using (StreamReader reader = File.OpenText(args[0]))
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            if (null == line)
                continue;
            input.Add(order, line);
            order++;
        }

        Parallel.ForEach(input, d => output.TryAdd(d.Key, DaVyncyDecoder.Decode(d.Value)));


        for (int i = 0; i < output.Count; i++)
        {
            Console.WriteLine(output[i]);
        }
    }
}

public static class DaVyncyDecoder
{
    public static string Decode(string input)
    {
        var fragments = input.Split(';').ToList();

        var output = CombineFragments(fragments);

        return output;
    }

    private static string CombineFragments(List<string> fragments)
    {
        var findMatchFor = fragments[0];
        var bestMatchIndex = -1;
        var bestMatch = String.Empty;

        for (int i = 1; i < fragments.Count; i++)
        {
            var stringToSearch = fragments[i];
            string combinedString = findMatchFor.FindOverlap(stringToSearch);
            if (combinedString.Length > bestMatch.Length)
            {
                bestMatch = combinedString;
                bestMatchIndex = i;
            }
        }

        if (bestMatch == findMatchFor) fragments.RemoveAt(bestMatchIndex);
        if (bestMatch.Length > 0)
        {
            fragments[0] = bestMatch;
        }

        return (fragments.Count == 1) ? fragments[0] : CombineFragments(fragments);
    }
}

public static class StringExtension
{
     public static string FindOverlap(this String str1, String str2)
     {
         if (str1.Contains(str2)) return str1;
         
         string match = str1[0].ToString();
         for (int i = 1; i < str1.Length; i++)
         {
             if (str2.StartsWith(match))
             {
                 match = match + str1[i];
             }
             else
             {
                 match = str1[i].ToString();
             }
         }
         if (!str2.StartsWith(match) && match.Length > 1) match = match.Substring(0, match.Length - 1);

         int startingMatchLength = match.Length;

         match = str1[str1.Length - 1].ToString();
         for (int i = str1.Length - 1; i >= 0; i--)
         {
             if (str2.EndsWith(match))
             {
                 match = str1[i] + match;
             }
             else
             {
                 match = str1[i].ToString();
             }
         }
         if (!str2.EndsWith(match) && match.Length > 1) match = match.Substring(1);

         string ret = string.Empty;
         if (startingMatchLength > match.Length)
         {
             ret = str1 + str2.Substring(startingMatchLength, str2.Length - startingMatchLength);
         }
         else if (match.Length > startingMatchLength)
         {
             ret = str2 + str1.Substring(match.Length, str1.Length - match.Length);
         }

         return ret;
     }
}