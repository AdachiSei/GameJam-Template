
/// <summary>
/// Serializable��ScriptableObject�Ɍp�������Ă�������
/// </summary>
/// <typeparam name="TScriptName">Script�̖��O</typeparam>
/// <typeparam name="T">�l�^</typeparam>
public interface IRandom<TScriptName, T>
{
    public T[] AllValue(TScriptName[] num);
    //return num.Select(e => e.T�^).ToArray();
}

public interface IRandomList<TScriptName, T>
{
    public T[] AllValue(TScriptName num);
    //return num.T�^.ToArray();
}
