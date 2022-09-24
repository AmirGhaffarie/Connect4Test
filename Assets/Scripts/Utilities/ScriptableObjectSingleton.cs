using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObjectSingleton<T>
{
    protected abstract string Path();
    static T instance;
    public static T Instance
    {
        get
        {
            if (!instance)
            {
                var objs = Resources.FindObjectsOfTypeAll<T>();
                if (objs.Length < 1)
                {
                    throw new System.Exception($"Couldn't find the singleton Scriptable of {typeof(T).Name}");
                }
                else
                {
                    if (objs.Length > 1)
                    {
                        Debug.LogWarning($"Multiple singleton Scriptables of {typeof(T).Name}");
                    }
                    instance = objs[0];
                }

                instance.OnInitialize();
            }

            return instance;
        }
    }
    protected virtual void OnInitialize() { }
}

