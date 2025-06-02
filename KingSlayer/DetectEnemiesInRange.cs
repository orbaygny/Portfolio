// DetectEnemiesInRange detects enemies around the player within a specified radius and angle,
// then applies damage or other effects depending on the enemy type and player's skills.
// Uses Physics.OverlapSphereNonAlloc for performance optimization.
// This method supports chaining attacks and special skill interactions.
public void DetectEnemiesInRange()
{
    if (isPlayerDead)
        return;

    int maxColliders = 5;
    Collider[] hitColliders = new Collider[maxColliders];
    var rad = radius + skillCtrl.ChainKill();
    int numColliders = Physics.OverlapSphereNonAlloc(transform.position, rad, hitColliders, AttackMask);

    for (int i = 0; i < numColliders; i++)
    {
        Vector3 direction = (hitColliders[i].transform.position - transform.position).normalized;
        direction.y = 0;
        var dis = Vector3.Distance(transform.position, hitColliders[i].transform.position);
        var angleBetween = Vector3.Angle(transform.forward, direction);

        if (angleBetween <= angle / 2 || dis <= 0.5f)
        {
            if (hitColliders[i].TryGetComponent(out IDamageable getDmg))
            {
                getDmg.GetDamage(DamageType.Melee);
                skillCtrl.Chaining();
            }
            // Additional interactions for absorbable or deflectable projectiles could be added here
        }
    }
}
