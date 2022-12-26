using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Template.Plugins
{
    static internal class MenuItemSettings
    {
        private const int SCRIPT_PRIOPRITY = 10;

        private const string PLUGIN_PATH = "Assets/Template/Files/";
        private const string NEW_FILENAME = "NewScript.cs";

        private const string MENU_ITEM_ROOT = "Assets/Create/Add Script Files/";

        private const string SimpleScript = "Simple Script";
        private const string MonoBehaviour = "MonoBehaviour";
        private const string MonoBehaviourWithUniRx = "MonoBehaviour (UniRx)";

        [MenuItem(MENU_ITEM_ROOT + SimpleScript, priority = SCRIPT_PRIOPRITY)]
        private static void CreateSimpleScript()
        {
            CreateScriptFile("SimpleScript.cs", NEW_FILENAME);
        }

        [MenuItem(MENU_ITEM_ROOT + MonoBehaviour, priority = SCRIPT_PRIOPRITY)]
        private static void CreateMonoBehaviour()
        {
            CreateScriptFile("TemplateMonoBehaviour.txt", NEW_FILENAME);
        }

        [MenuItem(MENU_ITEM_ROOT + MonoBehaviourWithUniRx, priority = SCRIPT_PRIOPRITY)]
        private static void CreateMonoBehaviourWithUniRx()
        {
            CreateScriptFile("TemplateMonoBehaviourWithUniRx.txt", NEW_FILENAME);
        }

        private static void CreateScriptFile(string templateFileName, string newFileName)
        {
            ProjectWindowUtil
                .CreateScriptAssetFromTemplateFile
                    (System.IO.Path.Combine(PLUGIN_PATH, $"{templateFileName}"), newFileName);
        }
    }
}