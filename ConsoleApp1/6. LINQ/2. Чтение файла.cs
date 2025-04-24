using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace linq_slideviews;

public class ParsingTask
{
    public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
    {
        return lines
            .Select(line => line.Split(';'))
            .Select(data =>
            {
                if (data.Length == 3
                && int.TryParse(data[0], out int id)
                && Enum.TryParse(data[1], true, out SlideType slideType))
                    return new SlideRecord(id, slideType, data[2]);
                else
                    return null;
            })
            .Where(record => record != null)
            .ToDictionary(record => record.SlideId);
    }

    public static IEnumerable<VisitRecord> ParseVisitRecords(
        IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
    {
        return lines
            .Skip(1)
            .Select(line => line.Split(';'))
            .Select(data =>
            {
                if (data.Length == 4
                && int.TryParse(data[0], out int userId)
                && int.TryParse(data[1], out int slideId)
                && DateTime.TryParse(data[2], out DateTime date)
                && DateTime.TryParse(data[3], out DateTime time)
                && slides.TryGetValue(slideId, out SlideRecord slideRecord))
                    return new VisitRecord(userId, slideId, date.Add(time.TimeOfDay), slideRecord.SlideType);
                else throw new FormatException($"Wrong line [{string.Join(';', data)}]");
            });
    }
}