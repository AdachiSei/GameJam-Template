using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// インプット名を定数で管理するクラスを作成するスクリプト
/// </summary>
public class InputNameCreator : AssetPostprocessor
{
    //作成したスクリプトを保存するパス
    private const string EXPORT_PATH = "Assets/Scripts/Constants/InputName.cs";

    // ファイル名(拡張子あり、なし)
    private static readonly string FILENAME = Path.GetFileName(EXPORT_PATH);
    private static readonly string FILENAME_WITHOUT_EXTENSION =
        Path.GetFileNameWithoutExtension(EXPORT_PATH);

    /// <summary>
    /// InputManagerを変更したら作成する
    /// </summary>
    private static void OnPostprocessAllAssets
        (string[] importedAssets,string[] deletedAssets,
            string[] movedAssets, string[] movedFromPath)
    {
        // InputManagerの変更チェック
        var inputManagerPath = 
            Array
                .Find(importedAssets,
                    path => Path.GetFileName(path) == "InputManager.asset");

        if (inputManagerPath == null)return;　

        // InputManagerの設定情報読み込み
        var serializedObjects =
            AssetDatabase
                .LoadAllAssetsAtPath(inputManagerPath);

        var serializedObject =
            new SerializedObject
                (AssetDatabase.LoadAllAssetsAtPath(inputManagerPath)[0]);

        var axesProperty = serializedObject.FindProperty("m_Axes");

        var axesSize = axesProperty.arraySize;

        StringBuilder builder = new StringBuilder();

        builder.AppendLine("/// <summary>");
        builder.AppendLine("/// インプット名を定数で管理するクラス");
        builder.AppendLine("/// </summary>");
        builder.AppendFormat("public struct {0}", FILENAME_WITHOUT_EXTENSION).AppendLine();
        builder.AppendLine("{");
        builder.Append("\t").AppendLine("#region Constants");
        builder.AppendLine("\t");

        List<string> inputNames = new();
        //全部取ってくる
        for (int i = 0; i < axesSize; ++i)
        {
            foreach (var serialized in serializedObjects)
            {
                var newSerialize = new SerializedObject(serialized);
                var newAxesProperty = newSerialize.FindProperty("m_Axes");
                var newAxisProperty = newAxesProperty.GetArrayElementAtIndex(i);
                var inputName = GetChildProperty(newAxisProperty, "m_Name").stringValue;
                inputNames.Add(inputName);
            } 
        }
        //重複する要素を消す
        inputNames = new(inputNames.Distinct());
        foreach (var name in inputNames)
        {
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
        Debug.Log("InputNamesを作成完了");
    }

    private static SerializedProperty GetChildProperty(SerializedProperty parent, string name)
    {
        SerializedProperty child = parent.Copy();
        child.Next(true);
        do
        {
            if (child.name == name) return child;
        }
        while (child.Next(false));
        return null;
    }
}