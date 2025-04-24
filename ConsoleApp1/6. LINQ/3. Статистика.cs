using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews;

public class StatisticsTask
{
    public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
    {
        return visits
            .GroupBy(visit => visit.UserId)
            .SelectMany(group => group.OrderBy(visit => visit.DateTime).Bigrams())
            .Where(bigram => bigram.First.SlideType == slideType)
            .Select(bigram => bigram.Second.DateTime.Subtract(bigram.First.DateTime).TotalMinutes)
            .Where(time => time >= 1 && time <= 120)
            .DefaultIfEmpty(0)
            .Median();
    }
}