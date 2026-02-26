using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static bool _isQuitting = false;
    public static T Instance
    {
        get
        {
            if (_isQuitting) return null;
            if (_instance) return _instance;
            _instance = FindAnyObjectByType<T>();
            DontDestroyOnLoad(_instance.gameObject);
            return _instance;
        }
    }

    protected void SingletonInit()
    {
        _instance = GetComponent<T>();
        DontDestroyOnLoad(gameObject);
    }

    private void OnApplicationQuit()
    {
        _isQuitting = true;
    }
}
