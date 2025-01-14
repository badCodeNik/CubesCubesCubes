using _project.Scripts.GameEntities;
using UnityEngine;

namespace _project.Scripts.Services.Factories
{
    public interface ICubeFactory
    {
        Cube CreateCube(Color color, Vector2 position);
    }
}