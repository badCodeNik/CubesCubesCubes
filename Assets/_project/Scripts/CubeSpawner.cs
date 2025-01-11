using System;
using UnityEngine.Pool;
using Zenject;
using Object = UnityEngine.Object;

namespace _project.Scripts
{
    public class CubeSpawner : ObjectPool<Cube>
    {
        private Cube _cubePrefab;
        private event Action<Cube> _onCubeSpawned; 

        [Inject]
        public void Construct(Cube cube)
        {
            _cubePrefab = cube;
        }
        
        public CubeSpawner(Func<Cube> createFunc, 
            Action<Cube> actionOnGet = null, 
            Action<Cube> actionOnRelease = null, 
            Action<Cube> actionOnDestroy = null, 
            bool collectionCheck = true, 
            int defaultCapacity = 10, 
            int maxSize = 10000) : 
            base(createFunc, actionOnGet, actionOnRelease, actionOnDestroy, collectionCheck, defaultCapacity, maxSize)
        {
            actionOnGet = _ => CreateCube();
            actionOnRelease = _ => ReleaseCube();
        }

        private void ReleaseCube()
        {
            
        }

        public Cube CreateCube()
        {
            var cube = Object.Instantiate(_cubePrefab);
            _onCubeSpawned?.Invoke(cube);
            return cube;
        }
    }
}