using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Orbay.Core.Singelton;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

namespace Orbay.Core.GameManager
{
    public class GameManager : Singelton<GameManager>
    {
      

        public override void Awake()
        {
            base.Awake();
            Application.targetFrameRate = 60;
        }
        // Start is called before the first frame update
   
        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0) && EventHandler.startGame != null && !EventSystem.current.IsPointerOverGameObject()) {
                EventHandler.startGame.Invoke();
                CanvasController.Instance.startUpgPanel.SetActive(false);
            }
        }


    }
}

