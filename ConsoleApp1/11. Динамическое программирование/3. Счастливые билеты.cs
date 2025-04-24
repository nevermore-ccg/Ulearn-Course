using System.Numerics;

namespace Tickets;

public static class TicketsTask
{
    public static BigInteger Solve(int halfLen, int totalSum)
    {
        if (totalSum % 2 != 0) return 0;
        var halfSum = totalSum / 2;
        var opt = new BigInteger[halfLen + 1, halfSum + 1];
        for (var i = 1; i <= halfLen; i++)
            opt[i, 0] = 1;
        for (var i = 1; i <= halfLen; i++)
            for (var j = 1; j <= halfSum; j++)
            {
                if (j > i * 9) continue;
                opt[i, j] = opt[i, j - 1] + opt[i - 1, j];
                if (j > 9)
                    opt[i, j] -= opt[i - 1, j - 10];
            }
        return opt[halfLen, halfSum] * opt[halfLen, halfSum];
    }
}