using UnityEngine;

namespace _project.Scripts
{
    public class Cube : MonoBehaviour
    {
        private bool _isDragged;
        public bool IsDragged => _isDragged;

        public void Drag()
        {
            _isDragged = true;
        }

        public void Drop()
        {
            _isDragged = false;
        }
    }
}