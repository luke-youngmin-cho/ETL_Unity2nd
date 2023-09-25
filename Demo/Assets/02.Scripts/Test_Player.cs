using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    [Flags]
    public enum State
    {
        None = 0 << 0, // 0   // ... 00000000
        Idle = 1 << 0, // 1   // ... 00000001
        Move = 1 << 1, // 2   // ... 00000010
        Jump = 1 << 2, // 4   // ... 00000100
        Fall = 1 << 3, // 8   // ... 00001000
        Attack = 1 << 4, // 16  // ... 00010000
    }

    public enum State2
    {
        None = 0, // ... 00000000
        Idle = 1, // ... 00000001
        Move = 2, // ... 00000010
        Jump = 3, // ... 00000011
        Fall = 4, // ... 00000100
        Attack = 5, // ... 00000101
    }

    public class Test_Player : MonoBehaviour
    {
        // _groundMask = 1 << 8 | 1 << 13
        [SerializeField] private LayerMask _groundMask;

        private void OnCollisionEnter(Collision collision)
        {
            Console.WriteLine(State.Move | State.Attack); // [Flags] X -> 18 출력
            Console.WriteLine(State.Move | State.Attack); // [Flags] O -> Move, Attack 출력


            if ((1 << collision.gameObject.layer & _groundMask) > 0)
            {
                Debug.Log("Is Grounded !!");
            }
        }
    }
}