using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
public class SingletonBase<T>
    where T : SingletonBase<T>
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
                    //Type type = typeof(T);
                    //ConstructorInfo constructorInfo = type.GetConstructor(new Type[] { });
                    //constructorInfo.Invoke(null);

                    _instance = Activator.CreateInstance<T>();
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
