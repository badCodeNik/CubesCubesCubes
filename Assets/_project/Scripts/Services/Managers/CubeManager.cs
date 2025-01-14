using _project.Scripts;
using _project.Scripts.GameEntities;
using _project.Scripts.Services.Factories;
using _project.Scripts.Tools;
using UnityEngine;

public class CubeManager
{
    private readonly ICubeFactory _cubeFactory;
    private readonly Signal _signal;
    private readonly Camera _mainCamera;
    private readonly int _cubeLayerMask;

    private bool _isDragging = false;
    private Cube _draggedCube;

    public CubeManager(ICubeFactory cubeFactory, Signal signal)
    {
        _cubeFactory = cubeFactory;
        _signal = signal;
        _mainCamera = Camera.main;
        _cubeLayerMask = 1 << LayerMask.NameToLayer("Cube");
    }

    public void TryDragFromTower(Vector3 position)
    {
        Vector2 worldPosition = _mainCamera.ScreenToWorldPoint(position);

        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, Mathf.Infinity, _cubeLayerMask);
        if (hit.collider != null && hit.collider.TryGetComponent<Cube>(out var cube))
        {
            if (cube.IsInTower)
            {
                var tower = Object.FindObjectOfType<Tower>();
                if (tower != null)
                {
                    tower.RemoveCube(cube);
                }

                _draggedCube = cube;
                _isDragging = true;

                _signal.RegistryRaise(new Signals.OnActionPerformed
                {
                    ActionDescription = "Cube dragged from the tower!"
                });
            }
        }
    }
    
    public void StartDragging(Cube cube)
    {
        _draggedCube = cube;
        _isDragging = true;
    }

    public void Drag(Vector3 position)
    {
        if (_isDragging && _draggedCube != null)
        {
            Vector2 worldPosition = _mainCamera.ScreenToWorldPoint(position);
            _draggedCube.transform.position = worldPosition;
        }
    }

    public void HandleDrop(Vector3 position)
    {
        if (_isDragging)
        {
            _isDragging = false;

            Vector2 worldPosition = _mainCamera.ScreenToWorldPoint(position);
            RaycastHit2D[] hits = Physics2D.RaycastAll(worldPosition, Vector2.zero, ~_cubeLayerMask);

            foreach (var hit in hits)
            {
                if (hit.collider != null && hit.collider.gameObject != _draggedCube.gameObject)
                {
                    if (hit.collider.TryGetComponent<Tower>(out var tower))
                    {
                        if (tower.TryAdd(_draggedCube))
                        {
                            _signal.RegistryRaise(new Signals.OnActionPerformed
                            {
                                ActionDescription = "Cube has been added to the tower"
                            });
                            return;
                        }

                        _signal.RegistryRaise(new Signals.OnActionPerformed
                        {
                            ActionDescription = "Cube cannot be placed."
                        });
                        PlayExplosionAnimation(_draggedCube);
                        return;
                    }

                    if (hit.collider.TryGetComponent<Hole>(out var hole))
                    {
                        _signal.RegistryRaise(new Signals.OnActionPerformed
                        {
                            ActionDescription = "Cube dropped into the hole!"
                        });
                        hole.HandleCube(_draggedCube);
                        return;
                    }
                }
            }

            PlayExplosionAnimation(_draggedCube);
        }
    }

    private void PlayExplosionAnimation(Cube cube)
    {
        _signal.RegistryRaise(new Signals.OnExplosion { Position = cube.transform.position });
        cube.Destroy();
        
        _signal.RegistryRaise(new Signals.OnActionPerformed
        {
            ActionDescription = "Cube exploded!"
        });
    }
}