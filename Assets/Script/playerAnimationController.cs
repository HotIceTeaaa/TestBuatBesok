using UnityEngine;

public class playerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void setSpeed(float speed)
    {
        animator.SetFloat("speed", speed);
    }

    public void setJump(bool value)
    {
        animator.SetBool("isJumping", value);
    }

    public void setDash(bool value)
    {
        animator.SetBool("isDashing", value);
    }
}
