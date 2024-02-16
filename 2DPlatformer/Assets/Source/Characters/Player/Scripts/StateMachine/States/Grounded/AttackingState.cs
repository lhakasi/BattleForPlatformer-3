using AnimationStates;
using HealthImpact;
using System.Collections.Generic;

public class AttackingState : GroundedState
{
    private readonly AttackingStateConfig _config;

    public AttackingState(IPlayerStateSwitcher stateSwitcher, PlayerStateMachineData data, Player player)
        : base(stateSwitcher, data, player) => _config = player.Config.AttackingStateConfig;

    public override void Enter()
    {
        base.Enter();

        Data.Speed = _config.Speed;
        
        View.StartAnimation(States.Attack);

        Attack(MeleeAttackZone.GetHitEnemies());
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (View.IsAnimationComplete)
            StateSwitcher.SwitchState<IdlingState>();
    }

    private void Attack(List<Enemy> enemies)
    {
        if (enemies == null)
            return;

        foreach (Enemy enemy in enemies)
        {
            enemy.HealthChanger.ChangeHealth(HealthImpactTypes.Damage, _config.MeleeDamage);
        }
    }
}