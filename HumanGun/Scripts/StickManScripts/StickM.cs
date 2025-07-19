using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StickM : MonoBehaviour, ITriggerByPlayer
{
    private Transform target;
    private Animator animator;
    private SkinnedMeshRenderer _renderer;
    private CapsuleCollider _col;
    
    public Material[] materials;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        _renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        EventHandler.collectStickM += OnPlayerTrigger;
        EventHandler.colorChange += OnColorChange;

    }
    // Start is called before the first frame update
    void Start()
    {
       
       
        EventHandler.removeStickM += OnRemovedFromList;
        EventHandler.obstacleStickM += OnObstacleHit;
        EventHandler.gunRecoil += OnGunFired;
        _col = GetComponentInChildren<CapsuleCollider>();
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnObstacleHit()
    {
        animator.SetTrigger("back");   
    }

    public void OnPlayerTrigger(int id)
    {
       
        if (id != gameObject.GetInstanceID()) return;
       
        if (StickContainer.IsContains(gameObject))
        {
            animator.SetTrigger("next");
        }
        else
        {
            _col.enabled = false;
            target = GameObject.FindGameObjectWithTag("StickMParent").transform;
            transform.parent = target;
            transform.localPosition = Vector3.zero;
            var tmp = StickContainer.ListCount() + 1;
            animator.SetTrigger(tmp.ToString());
            
        }
    }

    public void OnColorChange(int id,int colorId)
    {
        if (id != gameObject.GetInstanceID()) return;
        _renderer.material = materials[colorId];
    }

    public void OnRemovedFromList(int id)
    {
        if(id != gameObject.GetInstanceID()) return;
       
        transform.parent = null;
        var tmp = transform.position;
        tmp.y = -3;
        tmp.x += Random.Range(-2,+3);
        transform.DOJump(tmp , 2, 1, 1);

        if (ObjectPool.Instance.poolDictionary["StickM"].Contains(gameObject))
        {
            transform.parent = ObjectPool.Instance.transform.Find("StickMPool");
        }

        
    }

    void OnGunFired(int id)
    {
      if(id != gameObject.GetInstanceID()) return;
        transform.DOLocalMoveZ(-0.35f, 0.05f).SetLoops(2, LoopType.Yoyo);
    }

    private void OnDisable()
    {  
            EventHandler.collectStickM -= OnPlayerTrigger;
            EventHandler.colorChange -= OnColorChange;
            EventHandler.removeStickM -= OnRemovedFromList;
            EventHandler.obstacleStickM -= OnObstacleHit;
            EventHandler.gunRecoil -= OnGunFired;
             _col.enabled = true;
    }
}
