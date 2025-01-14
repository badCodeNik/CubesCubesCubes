using UnityEngine;

namespace _project.Scripts.GameEntities
{
    [RequireComponent(typeof(Collider2D))]
    public class Hole : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<IDestroyable>(out var destroyable)) return;
            destroyable.Destroy();
        }

        public void HandleCube(Cube cube)
        {
            Debug.Log("Destroyed");
            cube.Destroy();
            
        }
    }
}