using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

/// <summary>
/// �I�[�f�B�I�N���b�v�̃t�@�C������萔�ŊǗ�����N���X���쐬����X�N���v�g
/// </summary>
public static class AudioNameCreator
{
    #region Member Variables

    // �t�@�C����(�g���q����A�Ȃ�)
    private static readonly string FILENAME_AUDIO =
		Path.GetFileNameWithoutExtension(EXPORT_PATH_AUDIO);

	// BGM�p�t�@�C����(�g���q����A�Ȃ�)
	private static readonly string FILENAME_BGM =
		Path.GetFileNameWithoutExtension(EXPORT_PATH_BGM);

	// SFX�p�t�@�C����(�g���q����A�Ȃ�)
	private static readonly string FILENAME_SFX =
		Path.GetFileNameWithoutExtension(EXPORT_PATH_SFX);

    #endregion

    #region Constants

    /// <summary>
    /// ���̒���
    /// </summary>
    public const float BGM_LENGTH = 10f;

	/// <summary>
	/// �R�}���h��
	/// </summary>
	private const string COMMAND_NAME = "Tools/CreateConstants/Audio Name";

	/// <summary>
	/// �쐬�����X�N���v�g��ۑ�����p�X(�S��)
	/// </summary>
	private const string EXPORT_PATH_AUDIO = "Assets/Scripts/Constants/AudioName.cs";
	/// <summary>
	/// �쐬�����X�N���v�g��ۑ�����p�X(BGM)
	/// </summary>
	private const string EXPORT_PATH_BGM = "Assets/Scripts/Constants/BGMName.cs";
	/// <summary>
	/// �쐬�����X�N���v�g��ۑ�����p�X(SFX)
	/// </summary>
	private const string EXPORT_PATH_SFX = "Assets/Scripts/Constants/SFXName.cs";

    #endregion

    #region MenuItem Methods

    /// <summary>
    /// �萔�ŊǗ�����\���̂��쐬���܂�
    /// </summary>
    [MenuItem(COMMAND_NAME)]
	private static void Create()
	{
		if (!CanCreate())return;

		CreateScriptAll();
		CreateScriptBGM();
		CreateScriptSFX();

		Debug.Log("AudioName���쐬����");
		//EditorUtility.DisplayDialog(FILENAME, "�쐬���������܂���", "OK");
	}

    /// <summary>
    /// �I�[�f�B�I�̃t�@�C������萔�ŊǗ�����N���X���쐬�ł��邩�ǂ������擾���܂�
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
    /// �I�[�f�B�I����萔�ŊǗ�����\���̂��쐬���܂�
    /// </summary>
    private static void CreateScriptAll()
	{
		StringBuilder builder = new StringBuilder();

		builder.AppendLine("/// <summary>");
		builder.AppendLine("/// �I�[�f�B�I�N���b�v����萔�ŊǗ�����N���X");
		builder.AppendLine("/// </summary>");
		builder.AppendFormat("public struct {0}", FILENAME_AUDIO).AppendLine();
		builder.AppendLine("{");
		builder.Append("\t").AppendLine("#region BGM Constants");
		builder.AppendLine("\t");

		var bGMList = new List<AudioClip>();
		var sFXList = new List<AudioClip>();
		//�G�f�B�^�[
		foreach (var guid in AssetDatabase.FindAssets("t:AudioClip"))
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var pathName = AssetDatabase.LoadMainAssetAtPath(path);
			var audioClip = pathName as AudioClip;
			var isLong = audioClip.length >= BGM_LENGTH;
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
	/// BGM����萔�ŊǗ�����\���̂��쐬���܂�
	/// </summary>
	private static void CreateScriptBGM()
	{
		StringBuilder builder = new StringBuilder();

		builder.AppendLine("/// <summary>");
		builder.AppendLine("/// ���y����萔�ŊǗ�����N���X");
		builder.AppendLine("/// </summary>");
		builder.AppendFormat("public struct {0}", FILENAME_BGM).AppendLine();
		builder.AppendLine("{");
		builder.Append("\t").AppendLine("#region Constants");
		builder.AppendLine("\t");

		var bGMList = new List<AudioClip>();
		//�G�f�B�^�[
		foreach (var guid in AssetDatabase.FindAssets("t:AudioClip"))
		{
			var path = AssetDatabase.GUIDToAssetPath(guid);
			var pathName = AssetDatabase.LoadMainAssetAtPath(path);
			var audioClip = pathName as AudioClip;
			var isLong = audioClip.length >= BGM_LENGTH;
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
	/// SFX����萔�ŊǗ�����\���̂��쐬���܂�
	/// </summary>
	private static void CreateScriptSFX()
	{
		StringBuilder builder = new StringBuilder();

		builder.AppendLine("/// <summary>");
		builder.AppendLine("/// ���ʉ�����萔�ŊǗ�����N���X");
		builder.AppendLine("/// </summary>");
		builder.AppendFormat("public struct {0}", FILENAME_SFX).AppendLine();
		builder.AppendLine("{");
		builder.Append("\t").AppendLine("#region Constants");
		builder.AppendLine("\t");

		var sFXList = new List<AudioClip>();
		//�G�f�B�^�[
		foreach (var guid in AssetDatabase.FindAssets("t:AudioClip"))
		{
			var path = AssetDatabase.GUIDToAssetPath(guid);
			var pathName = AssetDatabase.LoadMainAssetAtPath(path);
			var audioClip = pathName as AudioClip;
			var isShort = audioClip.length < BGM_LENGTH;
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
