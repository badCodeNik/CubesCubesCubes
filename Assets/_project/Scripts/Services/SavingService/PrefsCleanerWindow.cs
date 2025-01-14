using UnityEditor;
using UnityEngine;

namespace _project.Scripts.Services.SavingService
{
    public class PrefsCleanerWindow : EditorWindow
    {
        [MenuItem("Tools/Clear PlayerPrefs")]
        public static void ShowWindow()
        {
            GetWindow<PrefsCleanerWindow>("Clear PlayerPrefs");
        }

        private void OnGUI()
        {
            GUILayout.Label("Clear PlayerPrefs", EditorStyles.boldLabel);

            if (GUILayout.Button("Clear All PlayerPrefs"))
            {
                ClearPlayerPrefs();
            }
        }

        private void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll(); 
            PlayerPrefs.Save();
            Debug.Log("PlayerPrefs cleared!");
        }
    }
}