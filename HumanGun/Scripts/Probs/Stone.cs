using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stone : MonoBehaviour
{
    [HideInInspector]public TextMeshPro hitCntText;
    public int hitCount;
    // Start is called before the first frame update
    void Start()
    {
        EventHandler.gateTrigger += OnGateTrigger;
        EventHandler.stoneTrigger += OnStoneTrigger;    
        hitCntText= GetComponentInChildren<TextMeshPro>();
        hitCntText.text = hitCount.ToString();
    }

    void OnGateTrigger(int id)
    {
        if (id != gameObject.GetInstanceID()) return;

        gameObject.SetActive(false);
    }

    void OnStoneTrigger(int id, int damage)
    {
        if (id != gameObject.GetInstanceID()) return;

     
            hitCount-= damage;
            if (hitCount <= 0) gameObject.SetActive(false);
            hitCntText.text = hitCount.ToString();
        
       
     

    }

    private void OnDisable()
    {
        EventHandler.gateTrigger -= OnGateTrigger;
        EventHandler.stoneTrigger -= OnStoneTrigger;
    }
}
