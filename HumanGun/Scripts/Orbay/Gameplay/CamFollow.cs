using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orbay.Gameplay.Camera
{
    public class CamFollow : MonoBehaviour,IStartGame
    {
        public bool lookAtTarget;
        public Transform target;
        public float smootheSpeed = 0.125f;
        public Vector3 offset;
        private Vector3 velocity = Vector3.zero;

        private bool isGameStarted, isGameEnded;

        private void Start()
        {
            EventHandler.startGame += OnGameStart;
            EventHandler.endGame += OnGameEnd;
            target = GameObject.FindGameObjectWithTag("Player").transform;


            CanvasController.Instance.startUpPanel.SetActive(false);
            CanvasController.Instance.fixedPanel.SetActive(true);

        }
        void LateUpdate()
        {
            if (!isGameStarted || isGameEnded) return;


            Vector3 desiredPosition = target.position + offset;
            //  desiredPosition.x = 0;
            Vector3 smoothedPosition =  Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity,smootheSpeed);
            transform.position = smoothedPosition;

            if (lookAtTarget)
                transform.LookAt(target);
          
        }

        public void OnGameStart()
        {
            isGameStarted = true;
            EventHandler.startGame -= OnGameStart;
        }

        public void OnGameEnd()
        {
            isGameEnded = true;
        }

        private void OnDisable()
        {
            EventHandler.startGame -= OnGameStart;
            EventHandler.endGame -= OnGameEnd;
        }
    }
}

