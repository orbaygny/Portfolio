using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] private int damage;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(transform.forward * 30f, ForceMode.VelocityChange);
    }

  
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.CompareTag("Stone"))
        {
            EventHandler.stoneTrigger.Invoke(other.transform.root.gameObject.GetInstanceID(),damage);
            transform.localPosition = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}
