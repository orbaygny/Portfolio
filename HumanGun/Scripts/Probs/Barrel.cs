using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Barrel : MonoBehaviour
{
    private TextMeshPro hitCntText;
    public int hitCount;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private LayerMask layerMask;

    public Collider[] hitColliders = new Collider[10];
    int numColliders;
    // Start is called before the first frame update
    void Start()
    {
        EventHandler.gateTrigger += OnGateTrigger;
        EventHandler.stoneTrigger += OnBarrelTrigger;
        hitCntText = GetComponentInChildren<TextMeshPro>();
        hitCntText.text = hitCount.ToString();
        numColliders = Physics.OverlapSphereNonAlloc(transform.position, 2f, hitColliders,layerMask);


    }

    void OnGateTrigger(int id)
    {
        if (id != gameObject.GetInstanceID()) return;

        OnExpolode();
       
      
    }

    void OnBarrelTrigger(int id, int damage)
    {
        if (id != gameObject.GetInstanceID()) return;
        hitCount -= damage;
        if (hitCount <= 0) { OnExpolode(); } 
        hitCntText.text = hitCount.ToString();
    }

    private void OnDisable()
    {
        EventHandler.gateTrigger -= OnGateTrigger;
        EventHandler.stoneTrigger -= OnBarrelTrigger;
    }

    public void OnExpolode()
    {
        for(int i = 0; i< numColliders; i++)
        {
            if (hitColliders[i]!= null)
            {
                if (hitColliders[i].transform.parent.gameObject.GetInstanceID() == gameObject.GetInstanceID()) continue;
                EventHandler.gateTrigger.Invoke(hitColliders[i].transform.root.gameObject.GetInstanceID());
            }
        }
        Instantiate(explosionPrefab, transform.position, Quaternion.identity); 
        gameObject.SetActive(false);
    }
}
