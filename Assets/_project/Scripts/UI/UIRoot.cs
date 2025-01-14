using UnityEngine;
using UnityEngine.UI;

namespace _project.Scripts.UI
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private Canvas canvas;
        [SerializeField] private GraphicRaycaster graphicRaycaster;
        [SerializeField] private RectTransform cubeContainer;
        [SerializeField] private UIActionLog uiActionLog;

        public UIActionLog UIActionLog => uiActionLog;
        public ScrollRect ScrollRect => scrollRect;
        public Canvas Canvas => canvas;
        public GraphicRaycaster GraphicRaycaster => graphicRaycaster;
        public RectTransform CubeContainer => cubeContainer;
    }
}