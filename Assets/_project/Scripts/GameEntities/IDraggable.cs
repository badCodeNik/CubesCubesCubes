using UnityEngine;

namespace _project.Scripts.GameEntities
{
    public interface IDraggable
    {
        float GetWidth();
        float GetHeight();
        bool Raycast();
        Vector2 GetPosition();
        void SetPosition(Vector2 position);
        void SetParent(Transform parent);
        
    }
}