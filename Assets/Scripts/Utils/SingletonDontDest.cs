using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonDontDest<T> : MonoBehaviour where T : SingletonDontDest<T> {

    public static T Instance { get; private set; }

    protected virtual void Awake() {
        if (Instance)
            Destroy(gameObject);
        else {
            DontDestroyOnLoad(gameObject);
            Instance = (T)this;
        }
    }
}