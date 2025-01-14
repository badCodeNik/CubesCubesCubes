using System;
using _project.Scripts.UI;
using UnityEngine;

namespace _project.Scripts.Services.InputService
{
    public class InputHandler : IDisposable
    {
        private readonly IInput _input;
        private readonly CubeManager _cubeManager;
        private readonly UIInteraction _uiInteraction;

        public InputHandler(IInput input, CubeManager cubeManager, UIInteraction uiInteraction)
        {
            _input = input;
            _cubeManager = cubeManager;
            _uiInteraction = uiInteraction;

            _input.OnClickDown += OnClickDown;
            _input.OnClickUp += OnClickUp;
            _input.OnDrag += OnDrag;
        }

        private void OnClickDown(Vector3 position)
        {
            var cube = _uiInteraction.TryDragFromUI(position);
            if (cube != null)
            {
                _cubeManager.StartDragging(cube);
            }
            else
            {
                _cubeManager.TryDragFromTower(position);
            }
        }

        private void OnDrag(Vector3 position)
        {
            _cubeManager.Drag(position);
        }

        private void OnClickUp(Vector3 position)
        {
            _cubeManager.HandleDrop(position);
        }

        public void Dispose()
        {
            _input.OnClickDown -= OnClickDown;
            _input.OnClickUp -= OnClickUp;
            _input.OnDrag -= OnDrag;
        }
    }
}