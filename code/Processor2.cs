
namespace code;
public class Processor2
{
    public static long Process(string input)
    {

        var sections = input.Split(new string[] { "seeds:", "seed-to-soil map:", "soil-to-fertilizer map:", "fertilizer-to-water map:", "water-to-light map:", "light-to-temperature map:", "temperature-to-humidity map:", "humidity-to-location map:" }, StringSplitOptions.RemoveEmptyEntries);
        List<(long, long)> seedsPair = ParseSeedPairs(sections[0]);
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
        //iterate List<(long, long)> seeds, generate new seeds for pairs
        foreach (var seedRange in seedsPair)
        {
            List<(long, long)> locationRanges = GetLocationRangeBulk(maps, seedRange);
            foreach (var locationRange in locationRanges)
            {
                result.Add(locationRange.Item1);
            }
        }

        return result.Min();
    }

    public static List<(long, long)> GetLocationRangeBulk(List<List<(long destStart, long srcStart, long length)>> maps, (long seedStart, long seedLength) seedRange)
    {
        List<(long, long)> replacebleRanges = [seedRange];
        foreach (var map in maps)
        {  
            replacebleRanges = GetNextRangesForAMapType(map, replacebleRanges);
        }
        return replacebleRanges;
    }
    public static List<(long, long)> GetNextRangesForAMapType(List<(long destStart, long srcStart, long length)> rangeMappings, List<(long, long)> currentRanges)
    {
        List<(long, long)> nextRanges = [];

        foreach (var (currentStart, currentLength) in currentRanges)
        {
            List<(long, long)> unCoveredRanges = [(currentStart, currentLength)];

            foreach (var (destStart, srcStart, length) in rangeMappings)
            {
                unCoveredRanges = GetUncoveredRangesForASingleRange(unCoveredRanges, (destStart, srcStart, length), nextRanges);
            }
            nextRanges.AddRange(unCoveredRanges);
        }

        return nextRanges;
    }
    public static List<(long, long)> GetUncoveredRangesForASingleRange(List<(long, long)> rangesWaiting, (long destStart, long srcStart, long length) rangeMappings, List<(long, long)> coveredRanges)
    {
        var (destStart, srcStart, length) = rangeMappings;
        List<(long, long)> unCoveredRanges = [];

        foreach (var rangeWaiting in rangesWaiting)
        {
            var (currentStart0, currentLength0) = rangeWaiting;

            // If the seed range is fully not covered, return a same range
            if ((currentStart0 + currentLength0 <= srcStart) || (currentStart0 >= srcStart + length))
            {
                unCoveredRanges.Add((currentStart0, currentLength0));
            }
            else
            {
                // find the seed range being covered partly and return the uncovered ranges
                long uncoveredLeftPartStart =  currentStart0;
                long uncoveredLeftPartEnd = srcStart;
                long uncoveredRightPartStart = srcStart + length;
                long uncoveredRightPartEnd =  currentStart0 + currentLength0;
                if (uncoveredLeftPartStart < uncoveredLeftPartEnd)
                {
                    unCoveredRanges.Add((uncoveredLeftPartStart, uncoveredLeftPartEnd - uncoveredLeftPartStart));
                }
                if (uncoveredRightPartStart < uncoveredRightPartEnd)
                {
                    unCoveredRanges.Add((uncoveredRightPartStart, uncoveredRightPartEnd - uncoveredRightPartStart));
                }
                // find the dest range for the covered range
                long coveredPartStart = Math.Max(srcStart, currentStart0);
                long coveredPartEnd = Math.Min(srcStart + length, currentStart0 + currentLength0);
                if (coveredPartStart < coveredPartEnd)
                {
                    coveredRanges.Add((destStart + coveredPartStart - srcStart, coveredPartEnd - coveredPartStart));
                }
            }
        }
        return unCoveredRanges;
    }

    static List<(long, long)> ParseSeedPairs(string input)
    {
        var result = new List<(long, long)>();
        var numbers = input.Trim()
                            .Split(' ')
                            .Select(long.Parse)
                            .ToArray();

        for (long i = 0; i < numbers.Length; i += 2)
        {
            if (i + 1 < numbers.Length)
            {
                result.Add((numbers[i], numbers[i + 1]));
            }
        }

        return result;
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
