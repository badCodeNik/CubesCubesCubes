using _project.Data;
using _project.Scripts.Services.Factories;
using _project.Scripts.UI;
using UnityEngine;

namespace _project.Scripts.Spawners
{
    public class UICubeSpawner
    {
        private readonly GameConfiguration _gameConfiguration;
        private readonly UICubeFactory _uiCubeFactory;
        private readonly UIRoot _uiRoot;

        public UICubeSpawner(GameConfiguration gameConfiguration, UICubeFactory uiCubeFactory, UIRoot uiRoot)
        {
            _gameConfiguration = gameConfiguration;
            _uiCubeFactory = uiCubeFactory;
            _uiRoot = uiRoot;
        }

        // По сути неважно какой тут будет конфиг, если это гугл таблицы - можно парсить, если иной источник - иначе обработать,
        // можно в принципе было взять и сделать метод, получить рандомный цвет, вариантов много
        public void SpawnCubes()
        {
            Transform container = _uiRoot.CubeContainer;
            Debug.Log($"Number of cubes: {_gameConfiguration.numberOfDraggableObjects}");

            Color defaultColor = Color.white;

            for (int i = 0; i < _gameConfiguration.numberOfDraggableObjects; i++)
            {
                Color cubeColor = _gameConfiguration.colorsOfDraggableObjects.Length > 0
                    ? _gameConfiguration.colorsOfDraggableObjects[
                        i % _gameConfiguration.colorsOfDraggableObjects.Length]
                    : defaultColor;

                _uiCubeFactory.CreateUICube(cubeColor, container);
            }
        }
    }
}