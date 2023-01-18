using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

/// <summary>
/// �r���h�Z�b�e�B���O�X�̃V�[������萔�ŊǗ�����N���X���쐬����X�N���v�g
/// </summary>
public static class SceneNameCreator
{
	// �R�}���h��
	private const string COMMAND_NAME = "Tools/CreateConstants/Scene Name";

	//�쐬�����X�N���v�g��ۑ�����p�X
	private const string EXPORT_PATH = "Assets/Scripts/Constants/SceneName.cs";

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

		Debug.Log("SceneNames���쐬����");
		//EditorUtility.DisplayDialog(FILENAME, "�쐬���������܂���", "OK");
	}

	/// <summary>
	/// �X�N���v�g���쐬���܂�
	/// </summary>
	public static void CreateScript()
	{
		StringBuilder builder = new StringBuilder();

		builder.AppendLine("/// <summary>");
		builder.AppendLine("/// �V�[������萔�ŊǗ�����N���X");
		builder.AppendLine("/// </summary>");
		builder.AppendFormat("public struct {0}", FILENAME_WITHOUT_EXTENSION).AppendLine();
		builder.AppendLine("{");
		builder.Append("\t").AppendLine("#region Constants");
		builder.AppendLine("\t");

		//BuildSetings�ɓ����Ă���Scene�̖��O��S�ĂƂ��Ă���
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

	/// <summary>
	/// �V�[���̃t�@�C������萔�ŊǗ�����N���X���쐬�ł��邩�ǂ������擾���܂�
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