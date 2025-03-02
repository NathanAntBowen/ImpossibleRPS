/// <summary>
/// Result probabilities structure to calculate result of game based on streak level.
/// </summary>
public struct ResultProbabilities
{
    public int WinProbability;
    public int DrawProbability;
    public int LossProbability;

    /// <summary>
    /// Initialize a new instance of the <see cref="ResultProbabilities"/>.
    /// </summary>
    /// <param name="winProbability">The probability of winning.</param>
    /// <param name="drawProbability">The probability of drawing.</param>
    /// <param name="lossProbability">The probability of losing.</param>
    public ResultProbabilities(int winProbability, int drawProbability, int lossProbability)
    {
        WinProbability = winProbability;
        DrawProbability = drawProbability;
        LossProbability = lossProbability;
    }
}