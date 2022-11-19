
/// <summary>
/// Serializable‚âScriptableObject‚ÉŒp³‚³‚¹‚Ä‚­‚¾‚³‚¢
/// </summary>
/// <typeparam name="TScriptName">Script‚Ì–¼‘O</typeparam>
/// <typeparam name="T">’lŒ^</typeparam>
public interface IProbabilityInT<TScriptName, T>
    where T : struct
    where TScriptName : IProbabilityInT<TScriptName, T>
{
    public T[] AllValue(TScriptName num);
    //return num.TŒ^.ToArray();
}
