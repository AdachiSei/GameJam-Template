
/// <summary>
/// Serializable��ScriptableObject�Ɍp�������Ă�������
/// </summary>
/// <typeparam name="TScriptName">Script�̖��O</typeparam>
/// <typeparam name="T">�l�^</typeparam>
public interface IProbabilityInArray<TScriptName, T>
    where T : struct
    where TScriptName : IProbabilityInArray<TScriptName, T>
{
    public T[] AllValue(TScriptName num);
    //return num.T�^.ToArray();
}
