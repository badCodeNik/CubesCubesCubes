using UnityEngine;

namespace _project.Data
{
    [CreateAssetMenu(fileName = "GameConfiguration", menuName = "GameConfiguration", order = 0)]
    public class GameConfiguration : ScriptableObject
    {
        public int numberOfDraggableObjects;
        public Color[] colorsOfDraggableObjects;
        
        
    }
}