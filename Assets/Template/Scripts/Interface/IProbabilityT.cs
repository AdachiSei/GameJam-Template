
/// <summary>
/// Serializable��ScriptableObject�Ɍp�������Ă�������
/// </summary>
/// <typeparam name="TScriptName">Script�̖��O</typeparam>
/// <typeparam name="T">�l�^</typeparam>
public interface IProbabilityT<TScriptName, T>
    where T: struct
    where TScriptName : IProbabilityT<TScriptName, T>
{
    public T[] AllValue(TScriptName[] num);
    //return num.Select(e => e.T�^).ToArray();
}
