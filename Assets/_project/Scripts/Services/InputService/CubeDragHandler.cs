using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _project.Scripts.Services.InputService
{
    public class CubeDragHandler : ITickable
    {
        private IInput _input;
        private bool _isDragging = false;
        private Cube _draggedCube;
        private ScrollRect _scrollRect;
        private RectTransform _scrollContent;
        private Canvas _canvas;

        [Inject]
        public CubeDragHandler(IInput input)
        {
            _input = input;
            _input.OnDrag += Drag;
            _input.OnClickDown += ClickDown;
            _input.OnClickUp += ClickUp;
        }

        private void ClickDown(Vector3 position)
        {
            var hit = Physics2D.Raycast(position, Vector2.zero);
            if (!hit.collider.TryGetComponent<Cube>(out var cube)) return;
            
            _isDragging = true;
            _scrollRect.enabled = false; 

            _draggedCube = Object.Instantiate(cube, _scrollContent);
            _draggedCube.transform.position = hit.collider.transform.position;
            _draggedCube.transform.SetAsLastSibling();

        }
        
        public void SetScrollRect(ScrollRect scrollRect)
        {
            _scrollRect = scrollRect;
            _scrollContent = scrollRect.content;
        }

        private void ClickUp(Vector3 position)
        {
            if (_isDragging)
            {
                _isDragging = false;
                _scrollRect.enabled = true;
                HandleDrop();
            }   
        }

        private void HandleDrop()
        {
            
        }

        private void Drag(Vector3 position)
        {
            if (_isDragging && _draggedCube != null)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _scrollContent,
                    position,
                    Camera.main,
                    out Vector2 localPoint);

                _draggedCube.transform.localPosition = localPoint;
            }
        }

        public void Tick()
        {
            
        }
    }
}