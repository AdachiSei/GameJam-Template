using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

/// <summary>
/// ビルドセッティングスのシーン名を定数で管理するクラスを作成するスクリプト
/// </summary>
public static class SceneNameCreator
{
    #region Private Member

    // ファイル名
    private static readonly string FILENAME =
		Path.GetFileNameWithoutExtension(EXPORT_PATH);

	#endregion

    #region Constants

    /// <summary>
    /// コマンド名
    /// </summary>
    private const string COMMAND_NAME = "Tools/CreateConstants/Scene Name";

	/// <summary>
	/// 作成したスクリプトを保存するパス
	/// </summary>
	private const string EXPORT_PATH = "Assets/Scripts/Constants/SceneName.cs";

	#endregion

	#region MenuItem Method

	/// <summary>
	/// 定数で管理する構造体を作成する関数
	/// </summary>
	[MenuItem(COMMAND_NAME)]
	private static void Create()
	{
		if (!CanCreate()) return;

		CreateScriptSceneName();

		Debug.Log("SceneNamesを作成完了");
		//EditorUtility.DisplayDialog(FILENAME, "作成が完了しました", "OK");
	}

	/// <summary>
	/// シーン名を定数で管理する構造体を作成できるかどうかを取得する関数
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
	/// シーン名を定数で管理する構造体を作成する関数
	/// </summary>
	private static void CreateScriptSceneName()
	{
		StringBuilder builder = new StringBuilder();

		builder.AppendLine("/// <summary>");
		builder.AppendLine("/// シーン名を定数で管理するクラス");
		builder.AppendLine("/// </summary>");
		builder.AppendFormat("public struct {0}", FILENAME).AppendLine();
		builder.AppendLine("{");
		builder.Append("\t").AppendLine("#region Constants");
		builder.AppendLine("\t");

		//BuildSetingsに入っているSceneの名前を全てとってくる
		foreach (var scene in EditorBuildSettings.scenes)
		{
			var name = Path.GetFileNameWithoutExtension(scene.path);
			builder
				.Append("\t")
				.AppendFormat
					(@"  public const string {0} = ""{1}"";",
						name.Replace(" ","_").ToUpper(), 
						name)
				.AppendLine();
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
