using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 move
    {
        get => _move;
        set
        {
            _move = value;
            onMoveChanged?.Invoke(value);
        }
    }
    private Vector3 _move;
    public event Action<Vector3> onMoveChanged;
}
