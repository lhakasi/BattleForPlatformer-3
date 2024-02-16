using Abilities;

public class VampirismState : GroundedState
{
    private readonly AttackingStateConfig _config;

    public VampirismState(IPlayerStateSwitcher stateSwitcher, PlayerStateMachineData data, Player player)
        : base(stateSwitcher, data, player) => _config = player.Config.AttackingStateConfig;

    public override void Enter()
    {
        base.Enter();

        Data.Speed = _config.Speed;

        PlayerAbilitiesEngine.UseAbility(PlayerAbilities.Vampirism);
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
}
