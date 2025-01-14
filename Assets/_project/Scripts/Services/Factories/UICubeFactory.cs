using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _project.Scripts.Services.Factories
{
    public class UICubeFactory
    {
        private readonly DiContainer _container;
        private readonly GameObject _uiCubePrefab;

        public UICubeFactory(DiContainer container, GameObject uiCubePrefab)
        {
            _container = container;
            _uiCubePrefab = uiCubePrefab;
        }

        public GameObject CreateUICube(Color color, Transform parent)
        {
            GameObject uiCube = _container.InstantiatePrefab(_uiCubePrefab, parent);

            Image cubeImage = uiCube.GetComponent<Image>();
            if (cubeImage != null)
            {
                cubeImage.color = color;
            }

            return uiCube;
        }
    }
}