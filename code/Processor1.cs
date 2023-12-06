using System.Text.RegularExpressions;

namespace code;
public class Processor1
{
    public static long Process(string input)
    {
        var sections = input.Split(new string[] { "seeds:", "seed-to-soil map:", "soil-to-fertilizer map:", "fertilizer-to-water map:", "water-to-light map:", "light-to-temperature map:", "temperature-to-humidity map:", "humidity-to-location map:" }, StringSplitOptions.RemoveEmptyEntries);
        long[] seeds = ParseSeeds(sections[0]);
        List<List<(long, long, long)>> maps =
        [
            ParseMap(sections[1]),
            ParseMap(sections[2]),
            ParseMap(sections[3]),
            ParseMap(sections[4]),
            ParseMap(sections[5]),
            ParseMap(sections[6]),
            ParseMap(sections[7]),
        ];
        // store the destination number for each seed
        List<long> result = new List<long>();
        // Output soil numbers for each seed
        foreach (var seed in seeds)
        {
            long locationNumber = GetLocationNumberBulk(maps, seed);

            result.Add(locationNumber);

        }

        // get the min of all numbers in result
        long min = result.Min();

        return min;
    }
    public static long GetLocationNumberBulk(List<List<(long destStart, long srcStart, long length)>> maps, long seedNumber)
    {long replacebleNumber = seedNumber;
        foreach (var map in maps)
        {
         replacebleNumber = GetNextNumber(map, replacebleNumber);
        }
        return replacebleNumber;
    }
    public static long GetNextNumber(List<(long destStart, long srcStart, long length)> rangeMappings, long currentNumber)
    {
        foreach (var (destStart, srcStart, length) in rangeMappings)
        {
            // find the seed number being covered and return the dest number

            if (currentNumber >= srcStart && currentNumber < srcStart + length)
            {
                return destStart + (currentNumber - srcStart);
            }
        }
        // If the seed number isn't in any range, it maps to itself
        return currentNumber;
    }

    static long[] ParseSeeds(string input)
    {
        return input.Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
    }

    static List<(long, long, long)> ParseMap(string input)
    {
        var result = new List<(long, long, long)>();

        var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            var parts = line.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (long.TryParse(parts[0], out long part1) &&
                long.TryParse(parts[1], out long part2) &&
                long.TryParse(parts[2], out long part3))
            {
                result.Add((part1, part2, part3));
            }
            else
            {
                Console.WriteLine($"Unable to parse line: '{line}'");
            }
        }

        return result;
    }

}
