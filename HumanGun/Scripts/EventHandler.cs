using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventHandler 
{
    public static UnityAction startGame;
    public static UnityAction endGame;


    public static UnityAction<int> triggerPlayer;
    public static UnityAction obstacleHit;
    public static UnityAction<int> gateTrigger;
    public static UnityAction<int,int> stoneTrigger;
    public static UnityAction gunFired;
    public static UnityAction<int> gunRecoil;

    public static UnityAction<GameObject> updateList;
    public static UnityAction<int> collectStickM;
    public static UnityAction obstacleStickM;
    public static UnityAction<int> removeStickM;
    public static UnityAction<int, int> colorChange;

    public static UnityAction<float> finishTrigger;
}
