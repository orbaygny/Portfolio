public class StickMan : MonoBehaviour, ITriggerByPlayer
{
      // Awake and variables here...

    public void OnPlayerTrigger(int id)
    {
       
        if (id != gameObject.GetInstanceID()) return;
       
        if (StickContainer.IsContains(gameObject))
        {
            animator.SetTrigger("next");
        }
        else
        {
            _col.enabled = false;
            target = GameObject.FindGameObjectWithTag("StickMParent").transform;
            transform.parent = target;
            transform.localPosition = Vector3.zero;
            var tmp = StickContainer.ListCount() + 1;
            animator.SetTrigger(tmp.ToString());
        }
    }

    public void OnColorChange(int id,int colorId)
    {
        if (id != gameObject.GetInstanceID()) return;
        _renderer.material = materials[colorId];
    }

    public void OnRemovedFromList(int id)
    {
        if(id != gameObject.GetInstanceID()) return;
       
        transform.parent = null;
        var tmp = transform.position;
        tmp.y = -3;
        tmp.x += Random.Range(-2,+3);
        transform.DOJump(tmp , 2, 1, 1);

        if (ObjectPool.Instance.poolDictionary["StickM"].Contains(gameObject))
        {
            transform.parent = ObjectPool.Instance.transform.Find("StickMPool");
        }

        
    }
      // Rest of script
}
