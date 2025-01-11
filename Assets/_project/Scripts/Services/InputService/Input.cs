using System;
using UnityEngine;
using Zenject;

namespace _project.Scripts
{
    public class Input : IInput, ITickable
    {
        
        public event Action<Vector3> OnClickDown;
        public event Action<Vector3> OnClickUp;
        public event Action<Vector3> OnDrag;
        private bool _isSwiping;
        private Vector3 _initialPosition;
        private Vector3 _previousPosition;

        public Input()
        {
            Debug.Log("kek");
        }

        public void Tick()
        {
            ProcessClickDown();
            ProcessClickUp();
            ProcessSwipe();
        }

        private void ProcessSwipe()
        {
            if (!_isSwiping) return;
            
            if(GetMousePosition() != _previousPosition) 
                OnDrag?.Invoke(GetMousePosition());
            _previousPosition = GetMousePosition();
        }

        private Vector3 GetMousePosition()
        {
            return UnityEngine.Input.mousePosition;
        }

        private void ProcessClickUp()
        {
            if (UnityEngine.Input.GetKeyUp(KeyCode.Mouse0))
            {
                _isSwiping = false;
                OnClickUp?.Invoke(GetMousePosition());
            }
        }

        private void ProcessClickDown()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Mouse0))
            {
                _isSwiping = true;
                _previousPosition = GetMousePosition();
                OnClickDown?.Invoke(GetMousePosition());
            }
        }
    }
}