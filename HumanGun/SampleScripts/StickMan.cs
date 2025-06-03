/// <summary>
/// StickMan character that reacts to player-triggered events such as collecting, color change, and removal.
/// Handles animations, parenting, and object pooling logic.
/// </summary>
public class StickMan : MonoBehaviour, ITriggerByPlayer
{
    // Assume these are assigned elsewhere
    private Animator animator;
    private Collider _col;
    private Renderer _renderer;
    private Material[] materials;

    private Transform target;

    /// <summary>
    /// Called when the player interacts with this stickman (e.g., collecting it).
    /// </summary>
    public void OnPlayerTrigger(int id)
    {
        if (id != gameObject.GetInstanceID()) return;

        if (StickContainer.IsContains(gameObject))
        {
            animator.SetTrigger("next"); // Play animation for already-collected
        }
        else
        {
            _col.enabled = false;

            // Move stickman to the player's collection root
            target = GameObject.FindGameObjectWithTag("StickMParent").transform;
            transform.parent = target;
            transform.localPosition = Vector3.zero;

            int triggerIndex = StickContainer.ListCount() + 1;
            animator.SetTrigger(triggerIndex.ToString());
        }
    }

    /// <summary>
    /// Changes the material color of the stickman based on provided color ID.
    /// </summary>
    public void OnColorChange(int id, int colorId)
    {
        if (id != gameObject.GetInstanceID()) return;
        _renderer.material = materials[colorId];
    }

    /// <summary>
    /// Handles stickman removal — detaches from parent, jumps, and returns to pool if available.
    /// </summary>
    public void OnRemovedFromList(int id)
    {
        if (id != gameObject.GetInstanceID()) return;

        transform.parent = null;

        Vector3 jumpTarget = transform.position;
        jumpTarget.y = -3;
        jumpTarget.x += Random.Range(-2, +3);

        transform.D
