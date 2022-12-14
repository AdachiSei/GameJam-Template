using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

/// <summary>
/// �I�[�f�B�I�N���b�v�̃t�@�C������萔�ŊǗ�����N���X���쐬����X�N���v�g
/// </summary>
public static class AudioNamesCreator
{
	// �R�}���h��
	private const string COMMAND_NAME = "Tools/CreateConstants/Audio Name";

	//�쐬�����X�N���v�g��ۑ�����p�X
	private const string EXPORT_PATH = "Assets/Scripts/Constants/AudioNames.cs"; 

	// �t�@�C����(�g���q����A�Ȃ�)
	private static readonly string FILENAME = Path.GetFileName(EXPORT_PATH);
	private static readonly string FILENAME_WITHOUT_EXTENSION =
		Path.GetFileNameWithoutExtension(EXPORT_PATH);

	/// <summary>
	/// �I�[�f�B�I�̃t�@�C������萔�ŊǗ�����N���X���쐬���܂�
	/// </summary>
	[MenuItem(COMMAND_NAME)]
	public static void Create()
	{
		if (!CanCreate())return;

		CreateScript();

		Debug.Log("AudioNames���쐬����");
		//EditorUtility.DisplayDialog(FILENAME, "�쐬���������܂���", "OK");
	}

	/// <summary>
	/// �X�N���v�g���쐬���܂�
	/// </summary>
	public static void CreateScript()
	{
		StringBuilder builder = new StringBuilder();

		builder.AppendLine("/// <summary>");
		builder.AppendLine("/// �I�[�f�B�I�N���b�v����萔�ŊǗ�����N���X");
		builder.AppendLine("/// </summary>");
		builder.AppendFormat("public static class {0}", FILENAME_WITHOUT_EXTENSION).AppendLine();
		builder.AppendLine("{");

		//�w�肵���p�X�̃��\�[�X��S�Ď擾
		object[] bgmList = Resources.LoadAll("Audio/BGM");
		object[] seList = Resources.LoadAll("Audio/SFX");

		foreach (AudioClip bgm in bgmList)
		{
			builder
				.Append("\t")
				.AppendFormat
					(@"  public const string BGM_{0} = ""{1}"";",
						bgm.name.Replace(" ","_").ToUpper(),
						bgm.name)
				.AppendLine();
		}

		builder.AppendLine("\t");

		foreach (AudioClip sfx in seList)
		{
			builder
				.Append("\t")
				.AppendFormat
					(@"  public const string SFX_{0} = ""{1}"";",
						sfx.name.Replace(" ", "_").ToUpper(),
						sfx.name)
				.AppendLine();
		}

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
	/// �I�[�f�B�I�̃t�@�C������萔�ŊǗ�����N���X���쐬�ł��邩�ǂ������擾���܂�
	/// </summary>
	[MenuItem(COMMAND_NAME, true)]
	private static bool CanCreate()
	{
		return !EditorApplication.isPlaying && !Application.isPlaying && !EditorApplication.isCompiling;
	}
}
