using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateController : MonoBehaviour
{

    private string _sign;
    private int _number;

    public List<GameObject> gates;
    // Start is called before the first frame update
    void Start()
    {
        
        _sign = GetComponentInChildren<TextMeshPro>().text[..1];
        _number = int.Parse(GetComponentInChildren<TextMeshPro>().text[1..]);

        EventHandler.gateTrigger += OnGateTrigger;

    }


    void OnGateTrigger(int id)
    {
        if (id != gameObject.GetInstanceID()) return;
        
       foreach (GameObject go in gates)
        {
            go.SetActive(false);
        }
        switch (_sign)
        {

            case "+":
             
                for (int i = 0; i < _number; i++)
                {
                    
                   var spawnObject = ObjectPool.Instance.GetFromPool("StickM");
                    EventHandler.collectStickM.Invoke(spawnObject.GetInstanceID());
                    EventHandler.updateList.Invoke(spawnObject);
                }
                break;

            case "-":
                for (int i = 0; i < _number; i++)
                {
                    if(EventHandler.obstacleHit == null) return;
                    
                     EventHandler.obstacleHit.Invoke();
                    
                   
                }
                break;

        }

      
    }

    private void OnDisable()
    {
        EventHandler.gateTrigger -= OnGateTrigger;
    }
}
