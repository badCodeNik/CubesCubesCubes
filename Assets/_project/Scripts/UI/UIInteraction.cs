using System.Collections.Generic;
using _project.Scripts.GameEntities;
using _project.Scripts.Services.Factories;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _project.Scripts.UI
{
    public class UIInteraction
    {
        private readonly ICubeFactory _cubeFactory;
        private readonly Camera _mainCamera;
        private readonly GraphicRaycaster _graphicRaycaster;
        private readonly ScrollRect _scrollRect;

        public UIInteraction(ICubeFactory cubeFactory, UIRoot uiRoot)
        {
            _cubeFactory = cubeFactory;
            _mainCamera = Camera.main;
            _graphicRaycaster = uiRoot.GraphicRaycaster;
            _scrollRect = uiRoot.ScrollRect;
        }

        public Cube TryDragFromUI(Vector3 position)
        {
            _scrollRect.enabled = true;
            var eventData = new PointerEventData(EventSystem.current) { position = position };

            var results = new List<RaycastResult>();
            _graphicRaycaster.Raycast(eventData, results);

            foreach (var result in results)
            {
                if (!result.gameObject.CompareTag("Draggable")) continue;
                var image = result.gameObject.GetComponent<Image>();
                Vector2 worldPosition = _mainCamera.ScreenToWorldPoint(position);
                _scrollRect.enabled = false;
                return _cubeFactory.CreateCube(image.color, worldPosition);
            }

            return null;
        }
    }
}