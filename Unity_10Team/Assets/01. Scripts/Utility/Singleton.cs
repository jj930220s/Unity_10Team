using System;
using UnityEngine;


// ���̱���
public class Singleton<T> : MonoBehaviour, IDisposable where T : class
{
    protected Singleton()
    {
        Init();
    }

    public static T Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType(typeof(T)) as T;

            return _instance;
        }
    }
    protected static T _instance = null; // ���� �ν��Ͻ� �ڵ�
    protected virtual void Init() { }

    public void Dispose() { }
}
