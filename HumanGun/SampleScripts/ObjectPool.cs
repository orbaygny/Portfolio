/// <summary>
/// Manages reusable GameObjects using a tag-based dictionary and queues for optimized spawning.
/// </summary>
public class ObjectPool : Singleton<ObjectPool>
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            var parentObj = new GameObject($"{pool.tag}Pool");
            parentObj.transform.SetParent(transform);
            parentObj.transform.localPosition = Vector3.zero;

            var objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                var obj = Instantiate(pool.prefab);
                obj.transform.SetParent(parentObj.transform);
                obj.transform.localPosition = Vector3.zero;
                obj.SetActive(false); // Optional: start inactive
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    /// <summary>
    /// Retrieves an object from the pool and immediately re-enqueues it.
    /// </summary>
    public GameObject GetFromPool(string tag)
    {
        if (!poolDictionary.ContainsKey(tag)) return null;

        var spawnObject = poolDictionary[tag].Dequeue();
        spawnObject.SetActive(true);
        poolDictionary[tag].Enqueue(spawnObject);
        return spawnObject;
    }
}
