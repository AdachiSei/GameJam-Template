using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public static class ObjectPoolCreator
{
	// コマンド名
	private const string COMMAND_NAME = "Tools/ObjectPool";

	//作成したスクリプトを保存するパス
	private const string EXPORT_PATH = "Assets/Scripts/Manager/ObjectPool.cs";

	//作成したイーナムを保存するパス
	private const string EXPORT_PATH_ENUM = "Assets/Scripts/Enum/PoolType.cs";

	// ファイル名(拡張子あり、なし)
	private static readonly string FILENAME = Path.GetFileName(EXPORT_PATH);
	private static readonly string FILENAME_WITHOUT_EXTENSION =
		Path.GetFileNameWithoutExtension(EXPORT_PATH);

	private static readonly string ENUM_FILENAME = Path.GetFileName(EXPORT_PATH_ENUM);
	private static readonly string ENUM_FILENAME_WITHOUT_EXTENSION =
		Path.GetFileNameWithoutExtension(EXPORT_PATH_ENUM);

	/// <summary>
	/// シーンのファイル名を定数で管理するクラスを作成します
	/// </summary>
	[MenuItem(COMMAND_NAME)]
	public static void Create()
	{
		if (!CanCreate()) return;

		CreateScript();
		CreateEnum();

		Debug.Log("ObjectPoolを作成完了");
		//EditorUtility.DisplayDialog(FILENAME, "作成が完了しました", "OK");
	}

	/// <summary>
	/// スクリプトを作成します
	/// </summary>
	public static void CreateScript()
	{
		StringBuilder builder = new StringBuilder();

		builder.AppendLine("/// <summary>");
		builder.AppendLine("/// プールオブジェクトで管理するオブジェクトプール");
		builder.AppendLine("/// </summary>");
		builder.AppendFormat("public class {0}", FILENAME_WITHOUT_EXTENSION).AppendLine();
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

	/// <summary>
	/// プールオブジェクトのイーナムを作成する関数
	/// </summary>
	public static void CreateEnum()
    {
		StringBuilder builder = new StringBuilder();

		builder.AppendLine("using System.Collections;");
		builder.AppendLine("using System.Collections.Generic;");
		builder.AppendLine("using UnityEngine;");
		builder.AppendLine("\t");
		builder.AppendLine("/// <summary>");
		builder.AppendLine("/// プールオブジェクトで管理するオブジェクトプール");
		builder.AppendLine("/// </summary>");
		builder.AppendFormat("public enum {0}", ENUM_FILENAME_WITHOUT_EXTENSION).AppendLine();
		builder.AppendLine("{");

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
						(@"{0},",
						pathObject.name.Replace(" ", ""))
					.AppendLine();
			}
		}

		builder.AppendLine("}");

		string directoryName = Path.GetDirectoryName(EXPORT_PATH_ENUM);
		if (!Directory.Exists(directoryName))
		{
			Directory.CreateDirectory(directoryName);
		}

		File.WriteAllText(EXPORT_PATH_ENUM, builder.ToString(), Encoding.UTF8);
		AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
	}

	/// <summary>
	/// プールオブジェクトで管理するオブジェクトプールを作成できるかどうかを取得します
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
