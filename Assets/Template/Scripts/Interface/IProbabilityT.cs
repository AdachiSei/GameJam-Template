
/// <summary>
/// Serializable‚âScriptableObject‚ÉŒp³‚³‚¹‚Ä‚­‚¾‚³‚¢
/// </summary>
/// <typeparam name="TScriptName">Script‚Ì–¼‘O</typeparam>
/// <typeparam name="T">’lŒ^</typeparam>
public interface IProbabilityT<TScriptName, T>
    where T: struct
    where TScriptName : IProbabilityT<TScriptName, T>
{
    public T[] AllValue(TScriptName[] num);
    //return num.Select(e => e.TŒ^).ToArray();
}
