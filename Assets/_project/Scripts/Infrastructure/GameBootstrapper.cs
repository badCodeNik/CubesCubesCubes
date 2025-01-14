using _project.Scripts.Spawners;
using UnityEngine;
using Zenject;


//В целом обычно я использую паттерн единая точка входа, или FSM для входа в приложение, но тут не вижу смысла в этом
namespace _project.Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private UICubeSpawner _uiCubeSpawner;

        [Inject]
        public void Construct(UICubeSpawner uiCubeSpawner)
        {
            _uiCubeSpawner = uiCubeSpawner;
        }

        private void Start()
        {
            _uiCubeSpawner.SpawnCubes();
        }
    }
}