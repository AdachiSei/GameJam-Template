using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public static class PoolObjectNameCreator
{
    #region Private Member

    /// <summary>
    /// ファイル名
    /// </summary>
    private static readonly string FILENAME =
		Path.GetFileNameWithoutExtension(EXPORT_PATH);

    #endregion

    #region Constants

    /// <summary>
    /// コマンド名
    /// </summary>
    private const string COMMAND_NAME = "Tools/CreateConstants/Pool Name";

	/// <summary>
	/// 作成したスクリプトを保存するパス
	/// </summary>
	private const string EXPORT_PATH = "Assets/Scripts/Constants/PoolName.cs";

    #endregion

    #region MenuItem Methods

    /// <summary>
    /// 定数で管理する構造体を作成する関数
    /// </summary>
    [MenuItem(COMMAND_NAME)]
	private static void Create()
	{
		if (!CanCreate()) return;

		CreateScriptPoolName();

		Debug.Log("Pool Nameを作成完了");
		//EditorUtility.DisplayDialog(FILENAME, "作成が完了しました", "OK");
	}

	/// <summary>
	/// プールオブジェクト名を定数で管理する構造体を作成できるかどうかを取得します
	/// </summary>
	[MenuItem(COMMAND_NAME, true)]
	private static bool CanCreate()
	{
		var isPlayingEditor = !EditorApplication.isPlaying;
		var isPlaying = !Application.isPlaying;
		var isCompiling = !EditorApplication.isCompiling;
		return isPlayingEditor && isPlaying && isCompiling;
	}

    #endregion

    #region Private Method

    /// <summary>
    /// プールオブジェクト名を定数で管理する構造体を作成する関数
    /// </summary>
    private static void CreateScriptPoolName()
	{
		StringBuilder builder = new StringBuilder();

		builder.AppendLine("/// <summary>");
		builder.AppendLine("/// プールオブジェクトで管理するオブジェクトプール");
		builder.AppendLine("/// </summary>");
		builder.AppendFormat("public struct {0}", FILENAME).AppendLine();
		builder.AppendLine("{");
		builder.Append("\t").AppendLine("#region Constants");
		builder.AppendLine("\t");

		//エディター
		foreach (var guid in AssetDatabase.FindAssets("t:Prefab"))
		{
			var path = AssetDatabase.GUIDToAssetPath(guid);
			var pathObject = AssetDatabase.LoadMainAssetAtPath(path);	
			var poolPrefab = pathObject as GameObject;

			if (poolPrefab.TryGetComponent(out PoolObjectBase poolObject))
			{
				builder
                    .Append("\t")
                    .AppendFormat
                        (@"  public const string {0} = ""{1}"";",
                        pathObject.name.Replace(" ", "_").ToUpper(),
                        pathObject.name)
                    .AppendLine();
			}
        }

		builder.AppendLine("\t");
		builder.Append("\t").AppendLine("#endregion");
		builder.AppendLine("}");

		string directoryName = Path.GetDirectoryName(EXPORT_PATH);
		if (!Directory.Exists(directoryName))
		{
			Directory.CreateDirectory(directoryName);
		}

		File.WriteAllText(EXPORT_PATH, builder.ToString(), Encoding.UTF8);
		AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
	}

    #endregion
}
