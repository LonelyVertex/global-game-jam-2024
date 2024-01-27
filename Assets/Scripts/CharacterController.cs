using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public const float CatchDelay = 18f / 60f;
    public const float ThrowDelay = 14f / 60f;
    public const float FailDelay = 15f / 60f;
    const float HeadSwapDelay = 11.5f / 60f;

    [SerializeField] Animator animator;
    [SerializeField] Transform objectPoint;
    [SerializeField] GameObject headHappy;
    [SerializeField] GameObject headSad;
    
    [Header("Debug")] 
    [SerializeField] bool throwAnimation;
    [SerializeField] bool catchAnimation;
    [SerializeField] bool failAnimation;
    [SerializeField] bool successAnimation;

    public Transform ObjectPointTransform => objectPoint;

    static readonly int ThrowTrigger = Animator.StringToHash("Throw");
    static readonly int CatchTrigger = Animator.StringToHash("Catch");
    static readonly int FailTrigger = Animator.StringToHash("Fail");
    static readonly int SuccessTrigger = Animator.StringToHash("Success");

    void Start()
    {
        headHappy.SetActive(false);
        headSad.SetActive(true);
    }

    void Update()
    {
        if (throwAnimation)
        {
            Throw();
            throwAnimation = false;
        }

        if (catchAnimation)
        {
            Catch();
            catchAnimation = false;
        }

        if (failAnimation)
        {
            Fail();
            failAnimation = false;
        }

        if (successAnimation)
        {
            Success();
            successAnimation = false;
        }
    }

    public void Throw()
    {
        animator.SetTrigger(ThrowTrigger);
    }

    public void Catch()
    {
        animator.SetTrigger(CatchTrigger);
    }

    public void Fail()
    {
        animator.SetTrigger(FailTrigger);
    }
    
    public void Success()
    {
        animator.SetTrigger(SuccessTrigger);
        Invoke(nameof(SwapToHappyFace), HeadSwapDelay);
    }

    void SwapToHappyFace()
    {
        headSad.SetActive(false);
        headHappy.SetActive(true);
    }
}