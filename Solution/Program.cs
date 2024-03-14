using System;

using System;
using System.Reflection;

class Program
{

    static long StringToLong(string input)
    {
        long result = 0;
        foreach (char c in input)
        {
            char lowerC = Char.ToLower(c);
            result = result * 52 + (Char.IsLower(c) ? lowerC - 'a' : lowerC - 'a' + 26);
        }
        return result;
    }

    static string LongToString(long number)
    {
        string result = "";
        const string alphabet = "abcdefghijklmnopqrstuvwxyz";
        while (number > 0)
        {
            int index = (int)(number % 52);
            char c = (index < 26) ? alphabet[index] : Char.ToUpper(alphabet[index - 26]);
            result = c + result;
            number /= 52;
        }
        return result.PadLeft(10, 'a');
    }

    static void Main()
    {
        string path = "..\\..\\..\\Data\\";
        var stringCounts = new Dictionary<long, int>();
        var resultCounts = new Dictionary<long, int>();

        int index = 1;

        for (int i = 0; i < 10 - 1; i++)
        {
            for (int j = i + 1; j < 10; j++)
            {
                stringCounts.Clear();
                // Console.WriteLine(i + ":" + j);
                Console.WriteLine(index + " / 45");
                index++;

                using (StreamReader reader = new StreamReader(path + "data" + i + ".dat"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] strings = line.Split(';');
                        foreach (string str in strings)
                        {
                            long intValue = StringToLong(str);
                            if (stringCounts.ContainsKey(intValue))
                            {
                                stringCounts[intValue]++;
                            }
                            else
                            {
                                stringCounts[intValue] = 1;
                            }
                        }
                    }
                }

                using (StreamReader reader = new StreamReader(path + "data" + j + ".dat"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] strings = line.Split(';');
                        foreach (string str in strings)
                        {
                            long intValue = StringToLong(str);
                            if (stringCounts.ContainsKey(intValue))
                            {
                                stringCounts[intValue]++;
                            }
                            else
                            {
                                stringCounts[intValue] = 1;
                            }
                        }
                    }
                }

                foreach (var kv in stringCounts)
                {
                    if (kv.Value > 1)
                    {
                        Console.WriteLine(LongToString(kv.Key));
                        if (resultCounts.ContainsKey(kv.Key))
                        {
                            resultCounts[kv.Key]++;
                        }
                        else { resultCounts[kv.Key] = 1; }
                    }
                }

            }
        }


        var top10 = resultCounts.OrderByDescending(kv => kv.Value).Take(10);

        foreach (var kv in top10)
        {
            Console.WriteLine($"{LongToString(kv.Key)}: {kv.Value} lan");
        }

    }
}
