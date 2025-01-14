using _project.Scripts.GameEntities;
using _project.Scripts.Services.SavingService;
using UnityEngine;
using Zenject;

namespace _project.Scripts.Services.Factories
{
    public class CubeFactory : ICubeFactory
    {
        private readonly DiContainer _container;
        private readonly Cube _cubePrefab;
        private readonly PlayerPrefsManager _prefsManager;

        public CubeFactory(DiContainer container, Cube cubePrefab, PlayerPrefsManager prefsManager)
        {
            _container = container;
            _cubePrefab = cubePrefab;
            _prefsManager = prefsManager;

            _prefsManager.LoadCubes(this);
        }

        public Cube CreateCube(Color color, Vector2 position)
        {
            Cube cube = _container.InstantiatePrefabForComponent<Cube>(_cubePrefab);
            cube.transform.position = position;
            SpriteRenderer spriteRenderer = cube.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = color;
            }

            return cube;
        }
    }
}