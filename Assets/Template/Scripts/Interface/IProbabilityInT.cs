
/// <summary>
/// Serializable��ScriptableObject�Ɍp�������Ă�������
/// </summary>
/// <typeparam name="TScriptName">Script�̖��O</typeparam>
/// <typeparam name="T">�l�^</typeparam>
public interface IProbabilityInT<TScriptName, T>
    where T : struct
    where TScriptName : IProbabilityInT<TScriptName, T>
{
    public T[] AllValue(TScriptName num);
    //return num.T�^.ToArray();
}
