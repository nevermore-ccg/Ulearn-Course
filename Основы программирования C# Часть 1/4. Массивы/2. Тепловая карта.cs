namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            var yMonths = new string[12];
            for (int i = 0; i < yMonths.Length; i++)
                yMonths[i] = (i + 1).ToString();
            var xDays = new string[30];
            for (int i = 1; i <= xDays.Length; i++)
                xDays[i - 1] = (i + 1).ToString();
            var heat = new double[30, 12];
            foreach (var nameData in names)
                if (nameData.BirthDate.Day != 1)
                    heat[nameData.BirthDate.Day - 2, nameData.BirthDate.Month - 1]++;
            return new HeatmapData("Пример карты интенсивностей", heat, xDays, yMonths);
        }
    }
}