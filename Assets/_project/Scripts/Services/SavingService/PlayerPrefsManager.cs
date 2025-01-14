using System;
using System.Globalization;
using _project.Scripts.GameEntities;
using _project.Scripts.Services.Factories;
using _project.Scripts.Tools;
using UnityEngine;

namespace _project.Scripts.Services.SavingService
{
    public class PlayerPrefsManager
    {
        private const string CubesKey = "SavedCubes";
        private readonly Signal _signal;
        private readonly Tower _tower;

        public PlayerPrefsManager(Signal signal, Tower tower)
        {
            _signal = signal;
            _tower = tower;

            _signal.Subscribe<Signals.OnCubeAdded>(OnCubeAdded);
            _signal.Subscribe<Signals.OnCubeRemoved>(OnCubeRemoved);
        }

        private void OnCubeAdded(Signals.OnCubeAdded data)
        {
            var cubeData = string.Format(CultureInfo.InvariantCulture, "{0},{1},{2},{3},{4},{5}",
                data.CubeId, data.X, data.Y, data.Color.r, data.Color.g, data.Color.b);
            var savedCubes = PlayerPrefs.GetString(CubesKey, "");
            savedCubes += $"{cubeData};";
            PlayerPrefs.SetString(CubesKey, savedCubes);
            PlayerPrefs.Save();
        }

        private void OnCubeRemoved(Signals.OnCubeRemoved data)
        {
            var savedCubes = PlayerPrefs.GetString(CubesKey, "");
            var cubes = savedCubes.Split(';');
            var newCubes = "";

            foreach (var cube in cubes)
            {
                if (!string.IsNullOrEmpty(cube))
                {
                    var parts = cube.Split(',');
                    if (parts.Length == 6)
                    {
                        var cubeId = parts[0];

                        if (cubeId != data.CubeId)
                        {
                            newCubes += $"{cube};";
                        }
                    }
                }
            }

            PlayerPrefs.SetString(CubesKey, newCubes);
            PlayerPrefs.Save();
        }

        public void LoadCubes(ICubeFactory cubeFactory)
        {
            var savedCubes = PlayerPrefs.GetString(CubesKey, "");
            var cubes = savedCubes.Split(';');

            foreach (var cube in cubes)
            {
                if (!string.IsNullOrEmpty(cube))
                {
                    var parts = cube.Split(',');

                    if (parts.Length == 6)
                    {
                        try
                        {
                            var cubeId = parts[0];
                            var x = float.Parse(parts[1], CultureInfo.InvariantCulture);
                            var y = float.Parse(parts[2], CultureInfo.InvariantCulture);
                            var color = new Color(
                                float.Parse(parts[3], CultureInfo.InvariantCulture),
                                float.Parse(parts[4], CultureInfo.InvariantCulture),
                                float.Parse(parts[5], CultureInfo.InvariantCulture)
                            );

                            var cubeInstance = cubeFactory.CreateCube(color, new Vector2(x, y));
                            cubeInstance.SetId(cubeId);
                            _tower.AddCube(cubeInstance);
                        }
                        catch (FormatException ex)
                        {
                            Debug.LogWarning($"Failed to parse cube data: {cube}. Error: {ex.Message}");
                        }
                    }
                    else
                    {
                        Debug.LogWarning(
                            $"Invalid cube data in PlayerPrefs (expected 6 parts, got {parts.Length}): {cube}");
                    }
                }
            }
        }
    }
}