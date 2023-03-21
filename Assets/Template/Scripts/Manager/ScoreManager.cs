/// <summary>
/// ƒXƒRƒA‚ğŠÇ—‚·‚éManager
/// </summary>
public static class ScoreManager
{
    #region Properties

    public static int Score { get; private set; }

    #endregion

    #region Public Methods

    public static void AddScore(int score)
    {
        Score += score;
    }

    public static void Init()
    {
        Score = 0;
    }

    #endregion
}
