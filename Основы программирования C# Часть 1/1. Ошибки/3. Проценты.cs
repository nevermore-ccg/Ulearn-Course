public static double Calculate(string userInput)
{
    var parts = userInput.Split();
    double initialAmount = Convert.ToDouble(parts[0]);
    double rate = Convert.ToDouble(parts[1]);
    double depositPeriod = Convert.ToDouble(parts[2]);
    return initialAmount * Math.Pow(1 + ((rate / 100) / 12), depositPeriod);
}