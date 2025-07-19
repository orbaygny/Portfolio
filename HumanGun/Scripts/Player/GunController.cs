using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    [System.Serializable]
    public class Gun
    {
        public string tag;
        public float fireRate;
        public float range;
        public float gunPointPos;
    }
   
    public List<Gun> guns;

    [Header("Variables")]
    public LayerMask layerMask;
    public Transform gunPoint;

    private float coolDown;
    private float fireRate;
    private float range;
    private string currentProjectile;

    bool isGameStarted, isGameEnded;
    // Start is called before the first frame update
    void Start()
    {
        EventHandler.startGame += OnGameStart;
        EventHandler.endGame += OnGameEnd;
        EventHandler.triggerPlayer += OnGunUpdate;
       
    }

    void OnGameStart() { 
        isGameStarted = true;
    }
    public void OnGameEnd() { isGameEnded = true; }
    // Update is called once per frame
    void Update()
    {
        if (!isGameStarted || isGameEnded) return;

        if (coolDown > 0) coolDown -= Time.deltaTime;
        float maxDistance = range + (0.025f * (PlayerPrefs.GetInt("RangeLevel", 1) - 1));

        bool isHit = Physics.SphereCast(transform.position, 0.2f, Vector3.forward, out RaycastHit hit, maxDistance, layerMask);
        if (isHit)
        {
            var hitPos = hit.transform.position;
            hitPos.y = 0;
            transform.LookAt(hitPos);
            Fire();
           
        }
        else
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }

    void OnGunUpdate(int count)
    {
        if (count < 5)
        {
            currentProjectile = "Bullet";
            fireRate = guns[0].fireRate;
            range = guns[0].range;
            var tmp = gunPoint.localPosition;
            tmp.z = guns[0].gunPointPos;
            gunPoint.localPosition = tmp;
        }
        else if (count < 9)
        {
            currentProjectile = "Bullet";
            fireRate = guns[1].fireRate;
            range = guns[1].range;
            var tmp = gunPoint.localPosition;
            tmp.z = guns[1].gunPointPos;
            gunPoint.localPosition = tmp;
        }
        else
        {
            currentProjectile = "Shell";
            fireRate = guns[2].fireRate;
            range = guns[2].range;
            var tmp = gunPoint.localPosition;
            tmp.z = guns[2].gunPointPos;
            gunPoint.localPosition = tmp;
        }
    }

    void Fire()
    {
        if (coolDown >0) return;
        EventHandler.gunFired.Invoke();
        var  spawnedObject = ObjectPool.Instance.GetFromPool(currentProjectile);
        spawnedObject.transform.position = gunPoint.position;
        coolDown = fireRate -(0.02f * (PlayerPrefs.GetInt("DmgLevel", 1)-1));
        
    }

    private void OnDisable()
    {
        EventHandler.startGame -= OnGameStart;
        EventHandler.endGame -= OnGameEnd;
        EventHandler.triggerPlayer -= OnGunUpdate;
    }


}
