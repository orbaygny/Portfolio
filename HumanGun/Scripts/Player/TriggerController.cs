using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TriggerController : MonoBehaviour
{
    bool finishTriggerd;
    private void OnTriggerEnter(Collider other)
    {
        string key = other.transform.root.gameObject.tag;

        switch (key)
        {
            case "StickM":

                if (StickContainer.ListCount() >= 11)
                {
                    other.transform.root.gameObject.SetActive(false);
                    CanvasController.Instance.MoneyCollected();
                }
                else
                {
                    EventHandler.collectStickM.Invoke(other.transform.parent.gameObject.GetInstanceID());
                    EventHandler.updateList.Invoke(other.transform.parent.gameObject);
                }
               
                break;

            case "Obstacle":
                EventHandler.obstacleHit.Invoke();
                break ;

            case "Door":
                EventHandler.gateTrigger.Invoke(other.transform.parent.gameObject.GetInstanceID());
                break;

            case "Stone":
                if (finishTriggerd) { EventHandler.endGame.Invoke(); return; }
                EventHandler.obstacleHit.Invoke();
                EventHandler.gateTrigger.Invoke(other.transform.root.gameObject.GetInstanceID());
                break;

            case "Finish":
                finishTriggerd = true;
                EventHandler.finishTrigger(other.transform.parent.position.z);
                break;

            case "Money":
                other.gameObject.SetActive(false);
                CanvasController.Instance.MoneyCollected();
                break;
        }
    }
}
