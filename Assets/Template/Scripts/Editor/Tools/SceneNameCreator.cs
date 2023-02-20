using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

/// <summary>
/// �r���h�Z�b�e�B���O�X�̃V�[������萔�ŊǗ�����N���X���쐬����X�N���v�g
/// </summary>
public static class SceneNameCreator
{
    #region Private Member

    // �t�@�C����
    private static readonly string FILENAME =
		Path.GetFileNameWithoutExtension(EXPORT_PATH);

	#endregion

    #region Constants

    /// <summary>
    /// �R�}���h��
    /// </summary>
    private const string COMMAND_NAME = "Tools/CreateConstants/Scene Name";

	/// <summary>
	/// �쐬�����X�N���v�g��ۑ�����p�X
	/// </summary>
	private const string EXPORT_PATH = "Assets/Scripts/Constants/SceneName.cs";

	#endregion

	#region MenuItem Method

	/// <summary>
	/// �萔�ŊǗ�����\���̂��쐬����֐�
	/// </summary>
	[MenuItem(COMMAND_NAME)]
	private static void Create()
	{
		if (!CanCreate()) return;

		CreateScriptSceneName();

		Debug.Log("SceneNames���쐬����");
		//EditorUtility.DisplayDialog(FILENAME, "�쐬���������܂���", "OK");
	}

	/// <summary>
	/// �V�[������萔�ŊǗ�����\���̂��쐬�ł��邩�ǂ������擾����֐�
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
	/// �V�[������萔�ŊǗ�����\���̂��쐬����֐�
	/// </summary>
	private static void CreateScriptSceneName()
	{
		StringBuilder builder = new StringBuilder();

		builder.AppendLine("/// <summary>");
		builder.AppendLine("/// �V�[������萔�ŊǗ�����N���X");
		builder.AppendLine("/// </summary>");
		builder.AppendFormat("public struct {0}", FILENAME).AppendLine();
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

    #endregion
}
