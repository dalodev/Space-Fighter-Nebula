using UnityEngine;
using System.Collections;
using UnityEditor;
namespace GameSlyce
{
    public class GSCustomListView : MonoBehaviour
    {

        [MenuItem("Help/GameSlyce/Help")]
        public static void OpenHelp()
        {
            string url = "www.gameslyce.com/GS_CustomList/help";
            Application.OpenURL(url);
        }
        [MenuItem("Help/GameSlyce/About Plugin")]
        public static void ShowAbout()
        {
            EditorUtility.DisplayDialog(sSuccess,
                 sInfo, sOk);
        }
        static string sSuccess = "Information",
            sInfo = "Installed Version of GameSlyce Custom Listview Version is 1.0",
            sOk = "Ok";
    }
}