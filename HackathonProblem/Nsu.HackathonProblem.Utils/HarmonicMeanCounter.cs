namespace Nsu.HackathonProblem.Utils;

public static class HarmonicMeanCounter
{
    public static double CountHarmonicMean(List<int> allIndexes)
    {
        double sum = 0.0;
        foreach (int index in allIndexes)
            if (index != 0.0)
                sum += 1.0 / index;
        if (sum == 0.0)
            throw new ArgumentException("All indexes are zero. Harmonic mean is undefined");
        return (allIndexes.Count()) / sum;
    }
}