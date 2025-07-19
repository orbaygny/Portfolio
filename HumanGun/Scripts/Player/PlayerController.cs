using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Orbay.Gameplay.SwerveSystem;
using DG.Tweening;

public class PlayerController : MonoBehaviour,IStartGame
{
    

    [Header("Movement")]
    public float moveSpeed;
    private Animator _animator;
    private bool isGameStart,isGameEnd;

    private BoxCollider _boxCollider;

    private SkinnedMeshRenderer _renderer;
    public Material[] materials;
    private void Awake()
    {
        EventHandler.triggerPlayer += OnPlayerTriggered;
        _animator = GetComponentInChildren<Animator>();
        _renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        StickContainer.AddList(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        EventHandler.startGame += OnGameStart;
        EventHandler.endGame += OnGameEnd;
        EventHandler.obstacleStickM += OnObstacleHit;
        EventHandler.removeStickM += OnRemovedFromList;
        EventHandler.gunFired += OnGunFired;
        _boxCollider = GetComponentInChildren<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameStart || isGameEnd) return;

        SwerveSystem.SwerveInput(transform, -1.7f, 1.7f, 0.25f);
        Movement();
    }


    private void Movement()
    {
        var fixedSpeed = moveSpeed*Time.deltaTime;
        transform.Translate(Vector3.forward * fixedSpeed);
    }

    private void OnPlayerTriggered(int count)
    {

        switch (count)
        {
            case 2:
               
                _animator.SetTrigger("1");
                _renderer.material = materials[0];

                _boxCollider.size = new Vector3(0.25f, 1, 0.2f);
                _boxCollider.center = new Vector3(0, 0, 0.2f);
                break;

            case 5:      
                
                _boxCollider.size = new Vector3(0.25f, 1, 1.2f);
                _boxCollider.center = new Vector3(0, 0, 0.2f);
                break;

            case 9:
                _animator.SetTrigger("next");
                _renderer.material = materials[1];

                _boxCollider.center = new Vector3(0, 0, 0.5f);
                _boxCollider.size = new Vector3(0.25f, 1, 1.7f);
                break;
        }
    }

    private void OnObstacleHit()
    {
        
        _animator.SetTrigger("back");
        _renderer.material = materials[0];

    }
    public void OnGameStart()
    {
        isGameStart = true;
        _animator.SetBool("run", true);
        EventHandler.startGame -= OnGameStart;
    }
    public void OnGameEnd()
    {
        isGameEnd = true;
        EventHandler.endGame -= OnGameEnd;
    }
    public void OnRemovedFromList(int id)
    {
        if (id != gameObject.GetInstanceID()) return;
        EventHandler.endGame.Invoke();
        transform.parent = null;
        var tmp = transform.position;
        tmp.y = -3;
        tmp.x += Random.Range(-2, +3);
        transform.DOJump(tmp, 2, 1, 1);
    }


   private void OnGunFired()
    {
       transform.GetChild(0).DOLocalRotate(new Vector3(-10, 0, 0), 0.05f).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
        if (StickContainer.ListCount() > 8)
        {
            transform.GetChild(0).DOLocalMoveZ(-1,0.025f).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
        }
    }
    private void OnDisable()
    {
        EventHandler.startGame -= OnGameStart;
        EventHandler.triggerPlayer -= OnPlayerTriggered;
        EventHandler.obstacleStickM -= OnObstacleHit;
        EventHandler.gunFired -= OnGunFired;
        EventHandler.removeStickM -= OnRemovedFromList;
    }


  
}
