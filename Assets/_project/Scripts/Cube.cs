using _project.Scripts;
using UnityEngine;

public class Cube : MonoBehaviour, IDraggable
{
    public void OnDragStart(Vector3 position)
    {
        Debug.Log("Cube drag started!");
    }

    public void OnDrag(Vector3 position)
    {
        Debug.Log("Cube drag started!");
    }

    public void OnDragEnd(Vector3 position)
    {
        Debug.Log("Cube drag ended!");
    }
}