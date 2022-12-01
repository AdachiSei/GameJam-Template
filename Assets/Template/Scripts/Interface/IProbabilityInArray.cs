
/// <summary>
/// Serializable‚âScriptableObject‚ÉŒp³‚³‚¹‚Ä‚­‚¾‚³‚¢
/// </summary>
/// <typeparam name="TScriptName">Script‚Ì–¼‘O</typeparam>
/// <typeparam name="T">’lŒ^</typeparam>
public interface IProbabilityInArray<TScriptName, T>
    where T : struct
    where TScriptName : IProbabilityInArray<TScriptName, T>
{
    public T[] AllValue(TScriptName num);
    //return num.TŒ^.ToArray();
}
