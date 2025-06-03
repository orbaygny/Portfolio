
public class ObjectPool : Singelton<ObjectPool>
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
  
  
    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            GameObject _gameObject = new GameObject(pool.tag+"Pool");
            _gameObject.transform.SetParent(transform);
            _gameObject.transform.localPosition = Vector3.zero;

            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i <pool.size; i++)
            {
                var obj = Instantiate(pool.prefab);
                obj.transform.parent = _gameObject.transform;
                obj.transform.localPosition = Vector3.zero;
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject GetFromPool(string tag)
    {
        GameObject spawnObject = poolDictionary[tag].Dequeue();

        spawnObject.SetActive(true);
        poolDictionary[tag].Enqueue(spawnObject); 
        return spawnObject;
    }

   
}
