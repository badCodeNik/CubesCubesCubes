using _project.Scripts.Tools;
using UnityEngine;
using Zenject;

namespace _project.Scripts.UI
{
    public class ActionLogListener : MonoBehaviour
    {
        private Signal _signal;
        private UIActionLog _uiActionLog;

        [Inject]
        public void Construct(UIActionLog uiActionLog, Signal signal)
        {
            _uiActionLog = uiActionLog;
            _signal = signal;
            _signal.Subscribe<Signals.OnActionPerformed>(OnActionPerformed);
        }

        private void OnActionPerformed(Signals.OnActionPerformed data)
        {
            _uiActionLog.AddLogEntry(data.ActionDescription);
        }

        private void OnDestroy()
        {
            _signal.Unsubscribe<Signals.OnActionPerformed>(OnActionPerformed);
        }
    }
}