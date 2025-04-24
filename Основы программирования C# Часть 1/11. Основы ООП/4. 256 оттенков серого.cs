using Avalonia.Media;
using GeometryTasks;
using System.Collections.Generic;

namespace GeometryPainting
{
    public static class SegmentExtensions
    {
        public static Dictionary<Segment, Color> SegmentColors = new Dictionary<Segment, Color>();

        public static Color GetColor(this Segment segment)
        {
            if (segment != null && SegmentColors.ContainsKey(segment))
                if (SegmentColors.TryGetValue(segment, out Color value))
                    return SegmentColors[segment];
            return Colors.Black;
        }

        public static void SetColor(this Segment segment, Color color)
        {
            if (SegmentColors.Count != 0 && SegmentColors.ContainsKey(segment))
                SegmentColors[segment] = color;
            else
                SegmentColors.Add(segment, color);
        }
    }
}