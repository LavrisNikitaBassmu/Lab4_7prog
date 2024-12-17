using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        // Треклист
        List<string> tracklist = new List<string>
        {
            "Gentle Giant – Free Hand [6:15]",
            "Supertramp – Child Of Vision [07:27]",
            "Camel – Lawrence [10:46]",
            "Yes – Don’t Kill The Whale [3:55]",
            "10CC – Notell Hotel [04:58]",
            "Nektar – King Of Twilight [4:16]",
            "The Flower Kings – Monsters & Men [21:19]",
            "Focus – Le Clochard [1:59]",
            "Pendragon – Fallen Dream And Angel [5:23]",
            "Kaipa – Remains Of The Day (08:02)"
        };

        TimeSpan totalTime = TimeSpan.Zero;
        string longestSong = "";
        TimeSpan longestDuration = TimeSpan.Zero;
        string shortestSong = "";
        TimeSpan shortestDuration = TimeSpan.MaxValue;
        TimeSpan minDifference = TimeSpan.MaxValue;
        string closestPair1 = "";
        string closestPair2 = "";

        // Регулярное выражение для извлечения названия и времени
        Regex regex = new Regex(@"– (.+?) \[(\d{1,2}:\d{2})\]|– (.+?) \((\d{1,2}:\d{2})\)");

        // Обработка треклиста
        for (int i = 0; i < tracklist.Count; i++)
        {
            Match match = regex.Match(tracklist[i]);
            if (match.Success)
            {
                string title = match.Groups[1].Success ? match.Groups[1].Value : match.Groups[3].Value;
                string timeStr = match.Groups[2].Success ? match.Groups[2].Value : match.Groups[4].Value;
                TimeSpan trackTime = TimeSpan.Parse(timeStr);

                // Суммируем общее время
                totalTime += trackTime;

                // Проверяем на самую длинную и короткую песню
                if (trackTime > longestDuration)
                {
                    longestDuration = trackTime;
                    longestSong = title;
                }
                if (trackTime < shortestDuration)
                {
                    shortestDuration = trackTime;
                    shortestSong = title;
                }

                // Сравниваем песни на минимальную разницу
                for (int j = 0; j < tracklist.Count; j++)
                {
                    if (i != j)
                    {
                        Match otherMatch = regex.Match(tracklist[j]);
                        if (otherMatch.Success)
                        {
                            string otherTitle = otherMatch.Groups[1].Success ? otherMatch.Groups[1].Value : otherMatch.Groups[3].Value;
                            string otherTimeStr = otherMatch.Groups[2].Success ? otherMatch.Groups[2].Value : otherMatch.Groups[4].Value;
                            TimeSpan otherTrackTime = TimeSpan.Parse(otherTimeStr);
                            TimeSpan difference = TimeSpan.FromTicks(Math.Abs(trackTime.Ticks - otherTrackTime.Ticks));

                            if (difference < minDifference)
                            {
                                minDifference = difference;
                                closestPair1 = title;
                                closestPair2 = otherTitle;
                            }
                        }
                    }
                }
            }
        }

        // Вывод результатов
        Console.WriteLine($"Общее время звучания: {totalTime}");
        Console.WriteLine($"Самая длинная песня: {longestSong} [{longestDuration}]");
        Console.WriteLine($"Самая короткая песня: {shortestSong} [{shortestDuration}]");
        Console.WriteLine($"Пара песен с минимальной разницей во времени: {closestPair1} и {closestPair2} (разница: {minDifference})");
    }
}