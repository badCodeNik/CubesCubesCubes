using UnityEngine;

namespace _project.Scripts
{
    public interface IDraggable
    {
        void OnDragStart(Vector3 position);
        void OnDrag(Vector3 position);
        void OnDragEnd(Vector3 position);
    }
}