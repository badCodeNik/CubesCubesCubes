using System.Collections.Generic;
using _project.Scripts.Tools;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _project.Scripts.GameEntities
{
    [RequireComponent(typeof(Collider2D))]
    public class Tower : MonoBehaviour
    {
        private readonly List<IDraggable> _cubes = new();
        private Collider2D _collider2D;
        private Signal _signal;

        [Inject]
        public void Construct(Signal signal)
        {
            _signal = signal;
        }


        private void OnValidate()
        {
            _collider2D ??= GetComponent<Collider2D>();
        }

        public bool TryAdd(IDraggable cube)
        {
            if (CanAddCube(cube))
            {
                AddCube(cube);
                return true;
            }
            return false;
        }

        public void RemoveCube(Cube cube)
        {
            if (!_cubes.Contains(cube)) return;
            var index = _cubes.IndexOf(cube);
            cube.GetFromTower();
            _cubes.RemoveAt(index);
            MoveCubesDown(index);
        }

        public void AddCube(IDraggable cube)
        {
            Vector2 newPosition = GetTopPosition(cube);

            float cubeWidth = cube.GetWidth();
            float randomOffset = Random.Range(-cubeWidth * 0.5f, cubeWidth * 0.5f);
            newPosition += new Vector2(randomOffset, 0);

            cube.SetPosition(newPosition);

            _cubes.Add(cube);
            cube.SetParent(transform);
            var cubeInstance = cube as Cube;
            
            _signal.RegistryRaise(new Signals.OnCubeAdded
            {
                CubeId = cubeInstance!.Id,
                Color = cubeInstance!.GetComponent<SpriteRenderer>().color,
                X = cubeInstance.transform.position.x,
                Y = cubeInstance.transform.position.y,
            });
        }

        private void MoveCubesDown(int removedIndex)
        {
            for (int i = removedIndex; i < _cubes.Count; i++)
            {
                Cube currentCube = _cubes[i] as Cube;
                Vector2 newPosition = currentCube!.GetPosition() - Vector2.up * currentCube.GetHeight();
                currentCube.SetPosition(newPosition);
            }
        }

        private float GetHeight()
        {
            if (_cubes.Count == 0)
            {
                return 0;
            }

            var topCube = _cubes[^1];
            var cubeHeight = topCube.GetHeight();
            return _cubes.Count * cubeHeight;
        }

        private Vector2 GetTopPosition(IDraggable cube)
        {
            if (_cubes.Count == 0)
            {
                return cube.GetPosition();
            }

            IDraggable topCube = _cubes[^1];
            Vector2 topCubePosition = topCube.GetPosition();

            float newCubeHeight = cube.GetHeight();
            return new Vector2(topCubePosition.x, topCubePosition.y + newCubeHeight);
        }

        
        //В целом можно сюда вписывать новые условия, но мне больше нравится ComplexConditions, это библиотека в которой
        // есть класс, мы добавляем туда условия кастомные и проверяем их, но честно говоря времени уже не хватило на 
        //имплементацию
        private bool CanAddCube(IDraggable cube)
        {
            var cubeHeight = cube.GetHeight();
            var towerHeight = GetHeight();

            float screenHeight = Camera.main.orthographicSize * 2;
            if (towerHeight + cubeHeight > screenHeight)
            {
                _signal.RegistryRaise(new Signals.OnActionPerformed()
                {
                    ActionDescription = "Cannot add cube: height limit reached.",
                });
                return false;
            }

            if (_cubes.Count > 0)
            {
                IDraggable topCube = _cubes[^1];
                float topCubeY = topCube.GetPosition().y;
                float newCubeY = cube.GetPosition().y;

                if (newCubeY < topCubeY + cubeHeight * 0.5f)
                {
                    _signal.RegistryRaise(new Signals.OnActionPerformed()
                    {
                        ActionDescription = "Cannot add cube: not placed on top of the previous cube.",
                    });
                    return false;
                }

                if (!cube.Raycast())
                {
                    _signal.RegistryRaise(new Signals.OnActionPerformed
                    {
                        ActionDescription = "Cannot add cube: there is no cube below.",
                    });
                    return false;
                }
            }

            return true;
        }
    }
}