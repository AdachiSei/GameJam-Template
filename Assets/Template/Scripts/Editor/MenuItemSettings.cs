using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Template.Plugins
{
    static internal class MenuItemSettings
    {
        private const int SCRIPT_PRIOPRITY = 10;

        private const string PLUGIN_PATH = "Assets/Template/Files/";

        private const string MENU_ITEM_ROOT = "Assets/Create/Add Script Files/";

        private const string NEW_FILENAME = "NewScript.cs";
        private const string NEW_MANAGER = "Manager.cs";
        private const string NEW_DATA = "Data.cs";

        private const string MONOBEHAVIOUR = "MonoBehaviour";
        private const string MONOBEHAIVOUR_WITH_UI = "MonoBehaviour (UI)";
        private const string SINGLETON = "SingletonMonoBehaviour";
        private const string SCRIPTABLEOBJECT = "ScriptableObject";
        private const string SERIALIZABLE = "Serializable";

        [MenuItem(MENU_ITEM_ROOT + MONOBEHAVIOUR, priority = SCRIPT_PRIOPRITY)]
        private static void CreateMonoBehaviour()=>
            CreateScript("TemplateMonoBehaviour.txt", NEW_FILENAME);

        [MenuItem(MENU_ITEM_ROOT + MONOBEHAIVOUR_WITH_UI, priority = SCRIPT_PRIOPRITY)]
        private static void CreateMonoBehaviourWithUI()=>
            CreateScript("TemplateMonoBehaviourWithUI.txt", NEW_FILENAME);

        [MenuItem(MENU_ITEM_ROOT + SINGLETON, priority = SCRIPT_PRIOPRITY)]
        private static void CreateSingletonMonoBehaviour()=>
            CreateScript("TemplateSingletonMonoBehaviour.txt", NEW_MANAGER);

        [MenuItem(MENU_ITEM_ROOT + SCRIPTABLEOBJECT, priority = SCRIPT_PRIOPRITY)]
        private static void CreateScriptableObject()=>
            CreateScript("TemplateScriptableObject.txt", NEW_DATA);

        [MenuItem(MENU_ITEM_ROOT + SERIALIZABLE, priority = SCRIPT_PRIOPRITY)]
        private static void CreateSerizalizable() =>
            CreateScript("TemplateSerializable.txt", NEW_DATA);

        private static void CreateScript
            (string templateFileName, string newFileName)
        {
            var path = Path.Combine(PLUGIN_PATH, templateFileName);
            ProjectWindowUtil
                .CreateScriptAssetFromTemplateFile
                    (path, newFileName);
        }
    }
}