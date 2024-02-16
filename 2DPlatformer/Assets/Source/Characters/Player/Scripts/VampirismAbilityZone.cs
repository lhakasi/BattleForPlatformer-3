using System.Collections.Generic;
using UnityEngine;

public class VampirismAbilityZone : MonoBehaviour
{
    [SerializeField] private LayerMask _enemies;
    [SerializeField, Range(0, 10)] private float _abilityRange;

    private Vector2 _abilityPoint => transform.position;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_abilityPoint, _abilityRange);
    }

    public Enemy GetEnemy()
    {
        List<Enemy> enemies = new List<Enemy>();

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_abilityPoint, _abilityRange, _enemies);

        foreach (Collider2D enemy in hitEnemies)
            enemies.Add(enemy.GetComponent<Enemy>());

        if (enemies.Count > 0)
            return enemies[0];
        else
            return null;
    }
}
