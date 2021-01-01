using UnityEngine;

public abstract class SingletonBehaviour : MonoBehaviour
{
    public static SingletonBehaviour Instance
    {
        get
        {
            if (_instance == null)
            { 
                _instance = new GameObject().AddComponent<SingletonBehaviour>();
            } 
            return _instance;
        }
    }
    private static SingletonBehaviour _instance;
    protected void Awake()
    {
        if (_instance != null && _instance != this)
        {
            
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }
}
