
/// <summary>
/// Serializable��ScriptableObject�Ɍp�������Ă�������
/// </summary>
/// <typeparam name="TScriptName">Script�̖��O</typeparam>
/// <typeparam name="T">�l�^</typeparam>
public interface IProbabilityInArrayOrList<TScriptName, T>
    where T : struct
    where TScriptName : IProbabilityInArrayOrList<TScriptName, T>
{
    public T[] AllValue(TScriptName num);
    //return num.T�^.ToArray();
}
