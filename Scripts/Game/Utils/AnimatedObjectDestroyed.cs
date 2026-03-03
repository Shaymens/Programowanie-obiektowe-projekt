using UnityEngine;

public class AnimatedObjectDestroyed : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void Start()
    {
        float clipLength = animator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, clipLength);
    }
}