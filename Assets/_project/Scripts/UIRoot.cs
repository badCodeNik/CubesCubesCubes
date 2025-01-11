using UnityEngine;
using UnityEngine.UI;

namespace _project.Scripts
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private Canvas canvas;
        [SerializeField] private GraphicRaycaster graphicRaycaster;
        public ScrollRect ScrollRect => scrollRect;
        public Canvas Canvas => canvas;

        public GraphicRaycaster GraphicRaycaster => graphicRaycaster;
    }
}