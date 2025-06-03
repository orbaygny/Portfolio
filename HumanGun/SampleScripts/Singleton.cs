/// <summary>
/// A generic MonoBehaviour-based Singleton class for Unity.
/// Ensures a single instance of any component type T across scenes.
/// </summary>
public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;

    /// <summary>
    /// Global access point to the singleton instance.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    CreateInstance();
                }
            }
            return instance;
        }
    }

    /// <summary>
    /// Sets up the singleton instance or destroys duplicates.
    /// </summary>
    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Creates a new GameObject if no instance is found.
    /// </summary>
    private static void CreateInstance()
    {
        GameObject singletonObj = new GameObject(typeof(T).Name);
        instance = singletonObj.AddComponent<T>();
        DontDestroyOnLoad(singletonObj);
    }
}
