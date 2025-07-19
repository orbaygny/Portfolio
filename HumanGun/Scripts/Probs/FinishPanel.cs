using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FinishPanel : MonoBehaviour
{

    [SerializeField] private TextMeshPro bestText;
    // Start is called before the first frame update
    void Start()
    {
        EventHandler.finishTrigger += OnFinishTrigger;
        var tmp = transform.position;
        tmp.z = PosZ;
        transform.position = tmp;
        bestText.text = PlayerPrefs.GetInt("BestDis", 10)+"M";
    }

    void OnFinishTrigger(float z)
    {
        if (transform.position.z < z)
        {   
            var tmp = transform.position;
            tmp.z = z;
            transform.position = tmp;
            PosZ = tmp.z;
            PlayerPrefs.SetInt("BestDis", PlayerPrefs.GetInt("BestDis", 10)+10);
            bestText.text = PlayerPrefs.GetInt("BestDis", 10) + "M";
        }
    }

    private void OnDisable()
    {
        EventHandler.finishTrigger -= OnFinishTrigger;
    }

    float PosZ
    {
        get {

            return PlayerPrefs.GetFloat("tableZ") == 0 ? 50 : PlayerPrefs.GetFloat("tableZ");
        }
        set
        {
            PlayerPrefs.SetFloat("tableZ", value);
            
        }
    }
   
}
