using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

/// <summary>
/// オーディオクリップのファイル名を定数で管理するクラスを作成するスクリプト
/// </summary>
public static class AudioNamesCreator
{
	/// <summary>
	/// 音の長さ
	/// </summary>
	private const float AUDIO_LENGTH = 10f;

	// コマンド名
	private const string COMMAND_NAME = "Tools/CreateConstants/Audio Names";

	//作成したスクリプトを保存するパス(全て)
	private const string EXPORT_PATH = "Assets/Scripts/Constants/AudioNames.cs";
	//作成したスクリプトを保存するパス(BGM)
	private const string EXPORT_PATH_BGM = "Assets/Scripts/Constants/BGMNames.cs";
	//作成したスクリプトを保存するパス(SFX)
	private const string EXPORT_PATH_SFX = "Assets/Scripts/Constants/SFXNames.cs";

	// ファイル名(拡張子あり、なし)
	private static readonly string FILENAME = Path.GetFileName(EXPORT_PATH);
	private static readonly string FILENAME_WITHOUT_EXTENSION =
		Path.GetFileNameWithoutExtension(EXPORT_PATH);

	// BGM用ファイル名(拡張子あり、なし)
	private static readonly string FILENAME_BGM = Path.GetFileName(EXPORT_PATH_BGM);
	private static readonly string FILENAME_BGM_WITHOUT_EXTENSION =
		Path.GetFileNameWithoutExtension(EXPORT_PATH_BGM);

	// SFX用ファイル名(拡張子あり、なし)
	private static readonly string FILENAME_SFX = Path.GetFileName(EXPORT_PATH_SFX);
	private static readonly string FILENAME_SFX_WITHOUT_EXTENSION =
		Path.GetFileNameWithoutExtension(EXPORT_PATH_SFX);

	/// <summary>
	/// オーディオのファイル名を定数で管理するクラスを作成します
	/// </summary>
	[MenuItem(COMMAND_NAME)]
	public static void Create()
	{
		if (!CanCreate())return;

		CreateScriptBGM();
		CreateScriptSFX();

		Debug.Log("AudioNamesを作成完了");
		//EditorUtility.DisplayDialog(FILENAME, "作成が完了しました", "OK");
	}

	/// <summary>
	/// スクリプトを作成します
	/// </summary>
	public static void CreateScriptAll()
	{
		StringBuilder builder = new StringBuilder();

		builder.AppendLine("/// <summary>");
		builder.AppendLine("/// オーディオクリップ名を定数で管理するクラス");
		builder.AppendLine("/// </summary>");
		builder.AppendFormat("public struct {0}", FILENAME_WITHOUT_EXTENSION).AppendLine();
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
			var isLong = audioClip.length >= AUDIO_LENGTH;
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

		string directoryName = Path.GetDirectoryName(EXPORT_PATH);
		if (!Directory.Exists(directoryName))
		{
			Directory.CreateDirectory(directoryName);
		}

		File.WriteAllText(EXPORT_PATH, builder.ToString(), Encoding.UTF8);
		AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
	}

	/// <summary>
	/// BGM用スクリプトを作成します
	/// </summary>
	public static void CreateScriptBGM()
	{
		StringBuilder builder = new StringBuilder();

		builder.AppendLine("/// <summary>");
		builder.AppendLine("/// 音楽名を定数で管理するクラス");
		builder.AppendLine("/// </summary>");
		builder.AppendFormat("public struct {0}", FILENAME_BGM_WITHOUT_EXTENSION).AppendLine();
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
			var isLong = audioClip.length >= AUDIO_LENGTH;
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
	/// SFX用スクリプトを作成します
	/// </summary>
	public static void CreateScriptSFX()
	{
		StringBuilder builder = new StringBuilder();

		builder.AppendLine("/// <summary>");
		builder.AppendLine("/// 効果音名を定数で管理するクラス");
		builder.AppendLine("/// </summary>");
		builder.AppendFormat("public struct {0}", FILENAME_SFX_WITHOUT_EXTENSION).AppendLine();
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
			var isShort = audioClip.length < AUDIO_LENGTH;
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
}
