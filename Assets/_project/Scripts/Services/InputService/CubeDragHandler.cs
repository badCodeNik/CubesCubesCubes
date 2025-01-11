using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;
using Object = UnityEngine.Object;

namespace _project.Scripts.Services.InputService
{
    public class CubeDragHandler : IDisposable
    {
        private bool _isDragging = false;
        private GameObject _draggedCube;
        private ScrollRect _scrollRect;
        private UIRoot _uiRoot;
        private IInput _input;
        private GraphicRaycaster _graphicRaycaster;

        [Inject]
        public CubeDragHandler(UIRoot uiRoot, IInput input)
        {
            _uiRoot = uiRoot;
            _input = input;
            _scrollRect = _uiRoot.ScrollRect;
            _graphicRaycaster = _uiRoot.GraphicRaycaster;
            _input.OnClickDown += ClickDown;
            _input.OnClickUp += ClickUp;
            _input.OnDrag += Drag;
        }

        private void ClickDown(Vector3 position)
        {
            var eventData = new PointerEventData(EventSystem.current){position = position};

            var results = new System.Collections.Generic.List<RaycastResult>();
            _graphicRaycaster.Raycast(eventData, results);
            
            foreach (var result in results)
            {
                var cube = result.gameObject.GetComponent<Cube>();
                Debug.Log(cube.transform.position);
                if (cube != null)
                {
                    _isDragging = true;
                    _scrollRect.enabled = false;

                    _draggedCube = Object.Instantiate(cube.gameObject);
                    _draggedCube.transform.position = cube.transform.position;

                    cube.OnDragStart(position);
                    break;
                }
            }
        }


        private void Drag(Vector3 position)
        {
            if (_draggedCube != null)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _scrollRect.content,
                    position,
                    _uiRoot.Canvas.worldCamera,
                    out Vector2 localPoint);

                _draggedCube.transform.localPosition = localPoint;
            }
        }

        private void ClickUp(Vector3 position)
        {
            if (_isDragging)
            {
                _isDragging = false;
                _scrollRect.enabled = true;

                // Логика дропа кубика
                HandleDrop(_draggedCube, position);

                // Уничтожаем копию кубика
                Object.Destroy(_draggedCube);
            }
        }

        private void HandleDrop(GameObject cube, Vector3 position)
        {
            // Создаем PointerEventData
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = position;

            // Список результатов рейкаста
            var results = new System.Collections.Generic.List<RaycastResult>();

            // Выполняем рейкаст
            _graphicRaycaster.Raycast(eventData, results);

            // Проверяем результаты
            foreach (var result in results)
            {
                if (result.gameObject.CompareTag("Tower"))
                {
                    var tower = result.gameObject.GetComponent<Tower>();
                    if (tower != null)
                    {
                        tower.AddCube(cube);
                        return;
                    }
                }
            }

            Debug.Log("Cube missed!");
            Object.Destroy(cube);   
        }


        public void Dispose()
        {
            _input.OnClickDown -= ClickDown;
            _input.OnClickUp -= ClickUp;
            _input.OnDrag -= Drag;
        }
    }
}