using TMPro;
using UnityEngine;

namespace _project.Scripts.UI
{
    public class UIActionLog : MonoBehaviour
    {
        [SerializeField] private TMP_Text actionLogText;
        [SerializeField] private int maxLogEntries = 4;

        private void Awake()
        {
            if (actionLogText == null)
            {
                Debug.LogError("ActionLogText is null!");
            }
        }

        public void AddLogEntry(string actionDescription)
        {
            actionLogText.text = $"{actionDescription}\n{actionLogText.text}";

            var logEntries = actionLogText.text.Split('\n');
            if (logEntries.Length > maxLogEntries)
            {
                actionLogText.text = string.Join("\n", logEntries, 0, maxLogEntries);
            }
        }

        public void Clear()
        {
            actionLogText.text = "";
        }
    }
}