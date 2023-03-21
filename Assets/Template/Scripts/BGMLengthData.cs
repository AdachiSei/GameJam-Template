/// <summary>
/// BGMの長さ
/// </summary>
public static class BGMLengthData
{
    public static float BGMLength { get; private set; } = 10f;

    public static void SetBGMLength(float length)
    {
        BGMLength = length;
    }
}