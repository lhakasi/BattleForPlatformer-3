using Abilities;
using AnimationStates;
using HealthImpact;
using System.Collections;
using UnityEngine;

public class PlayerAbilitiesEngine : MonoBehaviour
{
    private AttackingStateConfig _config;

    private Player _player;
    private Coroutine _ability;

    PlayerView View => _player.View;
    HealthChanger HealthChanger => _player.HealthChanger;
    VampirismAbilityZone VampirismAbilityZone => _player.VampirismAbilityZone;

    void Awake()
    {
        _player = GetComponent<Player>();
        _config = _player.Config.AttackingStateConfig;
    }

    public void UseAbility(PlayerAbilities ability)
    {
        switch (ability)
        {
            case PlayerAbilities.Vampirism:
                Vampirism(VampirismAbilityZone.GetEnemy());
                break;

            default:
                Debug.Log("Отсутствует способность");
                break;
        }
    }

    public void StopAbility() => StopCoroutine(_ability);

    private void Vampirism(Enemy enemy)
    {
        if (enemy == null)
            return;

        View.StartAnimation(States.Vampirism);

        _ability = StartCoroutine(DrainEnemyHealth(enemy));
    }

    private IEnumerator DrainEnemyHealth(Enemy enemy)
    {
        float second = 1f;
        WaitForSeconds oneSecond = new WaitForSeconds(second);

        for (int i = 0; i < _config.VampirismDuration; i++)
        {
            if (enemy.Health.CurrentHealth > 0)
            {
                enemy.HealthChanger.ChangeHealth(HealthImpactTypes.Damage, _config.VampirismPower);

                HealthChanger.ChangeHealth(HealthImpactTypes.Healing, _config.VampirismPower);

                yield return oneSecond;
            }
        }
    }
}

namespace Abilities
{
    public enum PlayerAbilities
    {
        Vampirism
    }
}
