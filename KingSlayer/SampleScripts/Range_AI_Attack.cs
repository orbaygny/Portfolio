/// <summary>
/// Handles AI attack logic based on AI type and player distance.
/// Melee enemies attack when in range.
/// Ranged enemies shoot arrows if player is in sight and within attack range.
/// </summary>
void AttackPlayer()
{
    // Melee AI attack logic
    if (aiType == AiType.Non_Range_Idle || aiType == AiType.Non_Range_Patrol)
    {
        if (distance <= attackRange)
        {
            playerDmg.GetDamage(DamageType.Melee);
        }
    }
    else // Ranged AI attack logic
    {
        if (distance <= attackRange && controller.IsPlayerOnSight())
        {
            if (!canFire) return;

            canFire = false;

            // Get an arrow from the object pool and shoot it
            var arrow = ObjectPool.Instance.GetFromPool(POOL_TAGS.CROSSBOW_ARROW);
            var arrowController = arrow.GetComponent<ArrowController>();
            arrowController.ResetArrow();
            arrow.transform.position = controller.ArrowPlace.position;
            arrowController.Fire();

            // Play reload animation
            controller.Animator.SetTrigger("Reload");
        }
        else
        {
            // Player is not in sight or out of range, switch AI to follow mode
            controller.Animator.SetTrigger("ChangeFollow");
        }
    }
}
