using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
public class SingletonMonoBase<T> : MonoBehaviour
    where T : SingletonMonoBase<T>
{
    public static object _lock = new object();
    public static T instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<T>();
                    _instance.Initialize();
                }
            }
            
            return _instance;
        }
    }
    private static T _instance;

    public  virtual void Initialize()
    {

    }
}
