using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class StoneWiithObj : Stone
{

    [SerializeField] private Transform obj;
    private MeshRenderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        EventHandler.gateTrigger += OnGateTrigger;
        EventHandler.stoneTrigger += OnStoneTrigger;
        hitCntText = GetComponentInChildren<TextMeshPro>();
        hitCntText.text = hitCount.ToString();
        _renderer = GetComponent<MeshRenderer>();

    }

    void OnGateTrigger(int id)
    {
        if (id != gameObject.GetInstanceID()) return;

        _renderer.enabled = false;
        transform.GetChild(1).gameObject.SetActive(false);
        hitCntText.gameObject.SetActive(false);
        var pos = obj.localPosition;
        pos.y = -0.37f;
        pos.z = 1;
        obj.DOLocalJump(pos,1,1,0.5f).OnComplete(() => OnDoComplete());
        EventHandler.stoneTrigger -= OnStoneTrigger;

    }

    void OnStoneTrigger(int id, int damage)
    {
        if (id != gameObject.GetInstanceID()) return;

        hitCount -= damage;
       
        if (hitCount <= 0)
        {
            _renderer.enabled = false;
            transform.GetChild(1).gameObject.SetActive(false);
            hitCntText.gameObject.SetActive(false);
            var pos = obj.localPosition;
            pos.y = -0.37f;
            pos.z = 1;
            obj.DOLocalJump(pos, 1, 1, 0.5f).OnComplete(() => OnDoComplete());
            EventHandler.stoneTrigger -= OnStoneTrigger;
            
        }
        
        hitCntText.text = hitCount.ToString();



    }
    private void OnDoComplete()
    {
        obj.parent = null;
        gameObject.SetActive(false);

    }
    private void OnDisable()
    {
        EventHandler.gateTrigger -= OnGateTrigger;
        EventHandler.stoneTrigger -= OnStoneTrigger;
    }
}
