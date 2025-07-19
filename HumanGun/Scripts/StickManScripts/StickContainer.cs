using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Orbay.Core.Singelton;
using System.Linq;

public class StickContainer:MonoBehaviour
{
    private static List<GameObject> stickMans;
    [SerializeField] private GameObject stickmanPrefab;
    public void Awake()
    {
        stickMans = new List<GameObject>();
        EventHandler.updateList += OnListUpdate;
        EventHandler.obstacleHit += OnObstacleHit;
        EventHandler.gunFired += OnGunFired;
    }
    private void Start()
    {
        for(int i = 1; i < PlayerPrefs.GetInt("stickMLevel"); i++)
        {
            GameObject tmp = Instantiate(stickmanPrefab);
            EventHandler.collectStickM.Invoke(tmp.GetInstanceID());
            EventHandler.updateList.Invoke(tmp);
        }
    }
    #region List Operations
    public static void AddList(GameObject obj)
    {
       stickMans.Add(obj);
    }

    public static  bool isContainsThis(GameObject obj)
    {
        return stickMans.Contains(obj);
    }
    public static bool IsContains(GameObject obj)
    {
        return stickMans.Contains(obj);
    }

    public static int ListCount() { return stickMans.Count; }

    private void RemoveLast()
    {
        EventHandler.removeStickM.Invoke(stickMans[^1].GetInstanceID());
        stickMans.Remove(stickMans[^1]);
    }
    #endregion


    #region Event Operations
    private void OnListUpdate(GameObject obj)
    {
       
        stickMans.Add(obj);
        ColorChanger(obj);

        if (stickMans.Count == 5)
        {
            EventHandler.collectStickM.Invoke(stickMans[3].GetInstanceID());
        }

       else if (stickMans.Count == 9)
        {
            foreach (GameObject _obj in stickMans)
            {
                EventHandler.collectStickM.Invoke(_obj.GetInstanceID());
            }
        }
        EventHandler.triggerPlayer.Invoke(stickMans.Count);
    }

    private void OnObstacleHit()
    {
        if(stickMans.Count == 0) { EventHandler.obstacleHit -= OnObstacleHit; return; }
        RemoveLast();
        if(stickMans.Count == 4 || stickMans.Count == 8) { EventHandler.obstacleStickM.Invoke(); }
        foreach(GameObject _obj in stickMans) { ColorChanger(_obj); }
        EventHandler.triggerPlayer.Invoke(stickMans.Count);
    }

    private void ColorChanger(GameObject obj)
    {
        

        if (stickMans.Count > 2 && stickMans.Count < 5)
        {
            if(stickMans.IndexOf(obj) >1)
            EventHandler.colorChange.Invoke(obj.GetInstanceID(), 1);
        }

       else if (stickMans.Count == 5 )
        {
            foreach(GameObject _obj in stickMans)
            {
                EventHandler.colorChange.Invoke(_obj.GetInstanceID(), 0);
            }
            EventHandler.colorChange.Invoke(stickMans[1].gameObject.GetInstanceID(), 1);
        }


        else if (stickMans.Count > 5 && stickMans.Count < 9 && stickMans.Count != 6)
        {
            if (stickMans.IndexOf(obj) > 5 || stickMans.IndexOf(obj) == 1)
                EventHandler.colorChange.Invoke(obj.GetInstanceID(), 1);
            else
                EventHandler.colorChange.Invoke(obj.GetInstanceID(), 0);
        }

        else if(stickMans.Count == 9)
        {
            EventHandler.colorChange.Invoke(stickMans[1].GetInstanceID(), 1);

            EventHandler.colorChange.Invoke(stickMans[6].GetInstanceID(), 2);
            EventHandler.colorChange.Invoke(stickMans[7].GetInstanceID(), 2);
            EventHandler.colorChange.Invoke(stickMans[8].GetInstanceID(), 2);
        }

        else
        {
            EventHandler.colorChange.Invoke(obj.GetInstanceID(), 0);
        }
    }

    private void OnGunFired()
    {

        
        if (stickMans.Count >= 5 && stickMans.Count<9)
        {
           
            EventHandler.gunRecoil.Invoke(stickMans[1].GetInstanceID());
            for (int i = 6; i < stickMans.Count; i++)
            {
                EventHandler.gunRecoil.Invoke(stickMans[i].GetInstanceID());
            }
        }
       else if (stickMans.Count >= 3)
        {
            for (int i = 2; i < stickMans.Count; i++)
            {
                EventHandler.gunRecoil.Invoke(stickMans[i].GetInstanceID());
            }
        }
    }
    #endregion

    private void OnDisable()
    {
        stickMans.Clear();
        EventHandler.gunFired -= OnGunFired;
        EventHandler.updateList -= OnListUpdate;
        EventHandler.obstacleHit -= OnObstacleHit;
    }
}
