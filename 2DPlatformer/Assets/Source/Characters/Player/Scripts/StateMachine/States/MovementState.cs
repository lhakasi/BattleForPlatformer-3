using UnityEngine;

public class MovementState : IPlayerState
{
    protected readonly IPlayerStateSwitcher StateSwitcher;
    protected readonly PlayerStateMachineData Data;

    private readonly Player _player;

    public MovementState(IPlayerStateSwitcher stateSwitcher, PlayerStateMachineData data, Player player)
    {
        StateSwitcher = stateSwitcher;
        Data = data;
        _player = player;
    }

    protected PlayerInput Input => _player.Input;
    protected Rigidbody2D Rigidbody => _player.Rigidbody;
    protected PlayerView View => _player.View;
    protected MeleeAttackZone MeleeAttackZone => _player.MeleeAttackZone;
    protected PlayerAbilitiesEngine PlayerAbilitiesEngine  => _player.PlayerAbilitiesEngine;
    protected VampirismAbilityZone VampirismAbilityZone => _player.VampirismAbilityZone;
    protected HealthChanger HealthChanger => _player.HealthChanger;

    public virtual void Enter()
    {
        Debug.Log(GetType());

        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallback();
    }

    public virtual void HandleInput()
    {
        Data.XInput = ReadHorizontalInput();
        Data.XVelocity = Data.XInput * Data.Speed;
    }

    public virtual void Update()
    {
        Vector3 velocity = GetConvertedVelocity();

        Move();

        if (GetType() == typeof(AttackingState))
            return;

        View.Flip(velocity);
    }

    protected virtual void AddInputActionsCallbacks() { }

    protected virtual void RemoveInputActionsCallback() { }

    protected bool IsHorizontalInputZero() => Data.XInput == 0;

    private void Move()
    {        
        Vector2 currentPosition = Rigidbody.position;        
        Vector2 newPosition = currentPosition + new Vector2(Data.XVelocity, Data.YVelocity) * Time.fixedDeltaTime;
        
        Rigidbody.MovePosition(newPosition);
    }

    private Vector3 GetConvertedVelocity() => new Vector3(Data.XVelocity, Data.YVelocity, 0);
    private float ReadHorizontalInput() => Input.Movement.Move.ReadValue<float>();
}