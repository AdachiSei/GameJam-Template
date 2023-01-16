
/// <summary>
/// Serializable‚âScriptableObject‚ÉŒp³‚³‚¹‚Ä‚­‚¾‚³‚¢
/// </summary>
/// <typeparam name="TScriptName">Script‚Ì–¼‘O</typeparam>
/// <typeparam name="T">’lŒ^</typeparam>
public interface IProbabilityInElement<TScriptName, T>
    where T : struct
    where TScriptName : IProbabilityInElement<TScriptName, T>
{
    public T[] AllValue(TScriptName num);
    //return num.TŒ^.ToArray();
}
