using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    protected bool Initialized { get; set; }
    private static volatile T instance;

    public static T Ins
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;

                if (instance == null)
                {
                    GameObject go = new GameObject(typeof(T).Name);
                    instance = go.AddComponent<T>();
                    DontDestroyOnLoad(go);
                }

                if (!instance.Initialized)
                {
                    instance.Initialize();
                    instance.Initialized = true;
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            InitializeOnAwake();
        }
    }

    protected virtual void Initialize()
    { }

    protected virtual void InitializeOnAwake()
    { }

}
