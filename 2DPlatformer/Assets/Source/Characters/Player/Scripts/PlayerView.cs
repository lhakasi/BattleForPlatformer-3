using AnimationStates;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Animator _animator;

    public bool IsAnimationComplete;
    private float AnimationFullLenght => _animator.GetCurrentAnimatorStateInfo(0).length;
    
    private Quaternion TurnRight => Quaternion.identity;
    private Quaternion TurnLeft => Quaternion.Euler(0, 180, 0);

    public void StartAnimation(States state)
    {
        IsAnimationComplete = false;

        _animator.Play(state.ToString());

        Debug.Log($"{state} - {AnimationFullLenght}");

        Invoke("CompleteAnimation", AnimationFullLenght);
    }

    public void CompleteAnimation() => IsAnimationComplete = true;

    public void Flip(Vector3 velocity)
    {
        if (velocity.x > 0)
            transform.rotation = TurnRight;

        if (velocity.x < 0)
            transform.rotation = TurnLeft;
    }
}

namespace AnimationStates
{
    public enum States
    {
        Idle,
        Walk,
        Jump,
        Attack,
        Vampirism,
        Hurt,
        Die
    }
}