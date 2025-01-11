using UnityEngine;

public class Tower : MonoBehaviour
{
    private Vector3 _lastCubePosition;

    public void AddCube(GameObject cube)
    {
        cube.transform.SetParent(transform);
        cube.transform.localPosition =
            _lastCubePosition + new Vector3(Random.Range(-0.5f, 0.5f), 1, 0); // Случайное смещение

        _lastCubePosition = cube.transform.localPosition;

        Debug.Log("Cube added to the tower!");
    }
}