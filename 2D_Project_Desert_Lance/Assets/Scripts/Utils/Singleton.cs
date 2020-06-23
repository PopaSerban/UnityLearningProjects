using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;

    public static T Instance
    {
        get{ return instance;}
    }
    /// <summary>
    /// This function returns where there is already an instance or not
    /// </summary>
    public static bool IsInitialised
    {
        get{ return instance != null; }
    }
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected virtual void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("[Singleton] Trying to instanciate a second instance of the singleton class.");
        }else
        {
            instance = (T) this;
        }   
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    protected virtual void OnDestroy()
    {
        if(instance == this)
        {
            instance = null;
        }
    }

}
