using System;
using UnityEngine;

namespace _project.Scripts.Services.InputService
{
    public interface IInput
    {
        event Action<Vector3> OnClickDown;
        event Action<Vector3> OnClickUp;
        event Action<Vector3> OnDrag;
    }
}