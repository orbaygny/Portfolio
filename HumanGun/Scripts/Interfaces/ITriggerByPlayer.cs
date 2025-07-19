using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerByPlayer {
    public void OnPlayerTrigger(int id);
    public void OnColorChange(int id,int colorId);
   
}
