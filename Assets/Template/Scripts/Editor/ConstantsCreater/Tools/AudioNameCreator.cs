using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

/// <summary>
/// オーディオクリップのファイル名を定数で管理するクラスを作成するスクリプト
/// </summary>
public static class AudioNameCreator
{
    #region Member Variables

    // ファイル名(拡張子あり、なし)
    private static readonly string FILENAME_AUDIO =
		Path.GetFileNameWithoutExtension(EXPORT_PATH_AUDIO);

	// BGM用ファイル名(拡張子あり、なし)
	private static readonly string FILENAME_BGM =
		Path.GetFileNameWithoutExtension(EXPORT_PATH_BGM);

	// SFX用ファイル名(拡張子あり、なし)
	private static readonly string FILENAME_SFX =
		Path.GetFileNameWithoutExtension(EXPORT_PATH_SFX);

    #endregion

    #region Constants

	/// <summary>
	/// コマンド名
	/// </summary>
	private const string COMMAND_NAME = "Tools/CreateConstants/Audio Name";

	/// <summary>
	/// 作成したスクリプトを保存するパス(全て)
	/// </summary>
	private const string EXPORT_PATH_AUDIO = "Assets/Template/Scripts/Constants/AudioName.cs";
	/// <summary>
	/// 作成したスクリプトを保存するパス(BGM)
	/// </summary>
	private const string EXPORT_PATH_BGM = "Assets/Template/Scripts/Constants/BGMName.cs";
	/// <summary>
	/// 作成したスクリプトを保存するパス(SFX)
	/// </summary>
	private const string EXPORT_PATH_SFX = "Assets/Template/Scripts/Constants/SFXName.cs";

    #endregion

    #region MenuItem Methods

    /// <summary>
    /// 定数で管理する構造体を作成します
    /// </summary>
    [MenuItem(COMMAND_NAME)]
	private static void Create()
	{
		if (!CanCreate())return;

		CreateScriptAll();
		CreateScriptBGM();
		CreateScriptSFX();

		Debug.Log("AudioNameを作成完了");
		//EditorUtility.DisplayDialog(FILENAME, "作成が完了しました", "OK");
	}

    /// <summary>
    /// オーディオのファイル名を定数で管理するクラスを作成できるかどうかを取得します
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

    #region Private Methods

    /// <summary>
    /// オーディオ名を定数で管理する構造体を作成します
    /// </summary>
    private static void CreateScriptAll()
	{
		StringBuilder builder = new StringBuilder();

		builder.AppendLine("/// <summary>");
		builder.AppendLine("/// オーディオクリップ名を定数で管理するクラス");
		builder.AppendLine("/// </summary>");
		builder.AppendFormat("public static class {0}", FILENAME_AUDIO).AppendLine();
		builder.AppendLine("{");
		builder.Append("\t").AppendLine("#region BGM Constants");
		builder.AppendLine("\t");

		var bGMList = new List<AudioClip>();
		var sFXList = new List<AudioClip>();
		//エディター
		foreach (var guid in AssetDatabase.FindAssets("t:AudioClip"))
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var pathName = AssetDatabase.LoadMainAssetAtPath(path);
			var audioClip = pathName as AudioClip;
			var isLong = audioClip.length >= BGMLengthData.BGMLength;
			if (isLong) bGMList.Add(audioClip);
			else sFXList.Add(audioClip);
        }

		foreach (AudioClip bgm in bGMList)
		{
			builder
				.Append("\t")
				.AppendFormat
					(@"  public const string BGM_{0} = ""{1}"";",
						bgm.name.Replace(" ", "_").ToUpper(),
						bgm.name)
				.AppendLine();
		}

		builder.AppendLine("\t");
		builder.Append("\t").AppendLine("#endregion");

		builder.AppendLine("\t");

		builder.Append("\t").AppendLine("#region SFX Constants");
		builder.AppendLine("\t");

		foreach (AudioClip sfx in sFXList)
		{
			builder
				.Append("\t")
				.AppendFormat
					(@"  public const string SFX_{0} = ""{1}"";",
						sfx.name.Replace(" ", "_").ToUpper(),
						sfx.name)
				.AppendLine();
		}

		builder.AppendLine("\t");
		builder.Append("\t").AppendLine("#endregion");
		builder.AppendLine("}");

		string directoryName = Path.GetDirectoryName(EXPORT_PATH_AUDIO);
		if (!Directory.Exists(directoryName))
		{
			Directory.CreateDirectory(directoryName);
		}

		File.WriteAllText(EXPORT_PATH_AUDIO, builder.ToString(), Encoding.UTF8);
		AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
	}

	/// <summary>
	/// BGM名を定数で管理する構造体を作成します
	/// </summary>
	private static void CreateScriptBGM()
	{
		StringBuilder builder = new StringBuilder();

		builder.AppendLine("/// <summary>");
		builder.AppendLine("/// 音楽名を定数で管理するクラス");
		builder.AppendLine("/// </summary>");
		builder.AppendFormat("public static class {0}", FILENAME_BGM).AppendLine();
		builder.AppendLine("{");
		builder.Append("\t").AppendLine("#region Constants");
		builder.AppendLine("\t");

		var bGMList = new List<AudioClip>();
		//エディター
		foreach (var guid in AssetDatabase.FindAssets("t:AudioClip"))
		{
			var path = AssetDatabase.GUIDToAssetPath(guid);
			var pathName = AssetDatabase.LoadMainAssetAtPath(path);
			var audioClip = pathName as AudioClip;
			var isLong = audioClip.length >= BGMLengthData.BGMLength;
			if (isLong) bGMList.Add(audioClip);
		}

		foreach (AudioClip bgm in bGMList)
		{
			builder
				.Append("\t")
				.AppendFormat
					(@"  public const string {0} = ""{1}"";",
						bgm.name.Replace(" ", "_").ToUpper(),
						bgm.name)
				.AppendLine();
		}

		builder.AppendLine("\t");
		builder.Append("\t").AppendLine("#endregion");
		builder.AppendLine("}");

		string directoryName = Path.GetDirectoryName(EXPORT_PATH_BGM);
		if (!Directory.Exists(directoryName))
		{
			Directory.CreateDirectory(directoryName);
		}

		File.WriteAllText(EXPORT_PATH_BGM, builder.ToString(), Encoding.UTF8);
		AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
	}

	/// <summary>
	/// SFX名を定数で管理する構造体を作成します
	/// </summary>
	private static void CreateScriptSFX()
	{
		StringBuilder builder = new StringBuilder();

		builder.AppendLine("/// <summary>");
		builder.AppendLine("/// 効果音名を定数で管理するクラス");
		builder.AppendLine("/// </summary>");
		builder.AppendFormat("public static class {0}", FILENAME_SFX).AppendLine();
		builder.AppendLine("{");
		builder.Append("\t").AppendLine("#region Constants");
		builder.AppendLine("\t");

		var sFXList = new List<AudioClip>();
		//エディター
		foreach (var guid in AssetDatabase.FindAssets("t:AudioClip"))
		{
			var path = AssetDatabase.GUIDToAssetPath(guid);
			var pathName = AssetDatabase.LoadMainAssetAtPath(path);
			var audioClip = pathName as AudioClip;
			var isShort = audioClip.length < BGMLengthData.BGMLength;
			if (isShort) sFXList.Add(audioClip);
		}

		foreach (AudioClip sfx in sFXList)
		{
			builder
				.Append("\t")
				.AppendFormat
					(@"  public const string {0} = ""{1}"";",
						sfx.name.Replace(" ", "_").ToUpper(),
						sfx.name)
				.AppendLine();
		}

		builder.AppendLine("\t");
		builder.Append("\t").AppendLine("#endregion");
		builder.AppendLine("}");

		string directoryName = Path.GetDirectoryName(EXPORT_PATH_SFX);
		if (!Directory.Exists(directoryName))
		{
			Directory.CreateDirectory(directoryName);
		}

		File.WriteAllText(EXPORT_PATH_SFX, builder.ToString(), Encoding.UTF8);
		AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
	}

	#endregion
}
