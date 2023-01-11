using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public static class PoolObjectNamesCreator
{
	// �R�}���h��
	private const string COMMAND_NAME = "Tools/CreateConstants/Pool Names";

	//�쐬�����X�N���v�g��ۑ�����p�X
	private const string EXPORT_PATH = "Assets/Scripts/Constants/PoolNames.cs";

	// �t�@�C����(�g���q����A�Ȃ�)
	private static readonly string FILENAME = Path.GetFileName(EXPORT_PATH);
	private static readonly string FILENAME_WITHOUT_EXTENSION =
		Path.GetFileNameWithoutExtension(EXPORT_PATH);

	/// <summary>
	/// �V�[���̃t�@�C������萔�ŊǗ�����N���X���쐬���܂�
	/// </summary>
	[MenuItem(COMMAND_NAME)]
	public static void Create()
	{
		if (!CanCreate()) return;

		CreateScript();

		Debug.Log("Pool Names���쐬����");
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
		builder.AppendFormat("public struct {0}", FILENAME_WITHOUT_EXTENSION).AppendLine();
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
