using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public static class ObjectPoolCreator
{
	// �R�}���h��
	private const string COMMAND_NAME = "Tools/ObjectPool";

	//�쐬�����X�N���v�g��ۑ�����p�X
	private const string EXPORT_PATH = "Assets/Scripts/Manager/ObjectPool.cs";

	//�쐬�����C�[�i����ۑ�����p�X
	private const string EXPORT_PATH_ENUM = "Assets/Scripts/Enum/PoolType.cs";

	// �t�@�C����(�g���q����A�Ȃ�)
	private static readonly string FILENAME = Path.GetFileName(EXPORT_PATH);
	private static readonly string FILENAME_WITHOUT_EXTENSION =
		Path.GetFileNameWithoutExtension(EXPORT_PATH);

	private static readonly string ENUM_FILENAME = Path.GetFileName(EXPORT_PATH_ENUM);
	private static readonly string ENUM_FILENAME_WITHOUT_EXTENSION =
		Path.GetFileNameWithoutExtension(EXPORT_PATH_ENUM);

	/// <summary>
	/// �V�[���̃t�@�C������萔�ŊǗ�����N���X���쐬���܂�
	/// </summary>
	[MenuItem(COMMAND_NAME)]
	public static void Create()
	{
		if (!CanCreate()) return;

		CreateScript();
		CreateEnum();

		Debug.Log("ObjectPool���쐬����");
		//EditorUtility.DisplayDialog(FILENAME, "�쐬���������܂���", "OK");
	}

	/// <summary>
	/// �X�N���v�g���쐬���܂�
	/// </summary>
	public static void CreateScript()
	{
		StringBuilder builder = new StringBuilder();

		builder.AppendLine("/// <summary>");
		builder.AppendLine("/// �v�[���I�u�W�F�N�g�ŊǗ�����I�u�W�F�N�g�v�[��");
		builder.AppendLine("/// </summary>");
		builder.AppendFormat("public class {0}", FILENAME_WITHOUT_EXTENSION).AppendLine();
		builder.AppendLine("{");
		builder.Append("\t").AppendLine("#region Constants");
		builder.AppendLine("\t");

		//�G�f�B�^�[
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
	/// �v�[���I�u�W�F�N�g�̃C�[�i�����쐬����֐�
	/// </summary>
	public static void CreateEnum()
    {
		StringBuilder builder = new StringBuilder();

		builder.AppendLine("using System.Collections;");
		builder.AppendLine("using System.Collections.Generic;");
		builder.AppendLine("using UnityEngine;");
		builder.AppendLine("\t");
		builder.AppendLine("/// <summary>");
		builder.AppendLine("/// �v�[���I�u�W�F�N�g�ŊǗ�����I�u�W�F�N�g�v�[��");
		builder.AppendLine("/// </summary>");
		builder.AppendFormat("public enum {0}", ENUM_FILENAME_WITHOUT_EXTENSION).AppendLine();
		builder.AppendLine("{");

		//�G�f�B�^�[
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
	/// �v�[���I�u�W�F�N�g�ŊǗ�����I�u�W�F�N�g�v�[�����쐬�ł��邩�ǂ������擾���܂�
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
