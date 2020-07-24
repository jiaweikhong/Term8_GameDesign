using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class GenericCharacter : MonoBehaviour
{
    [SerializeField]
    protected ControlsManager controlsManager;
    protected CharacterMovementController movementController;
    protected GenericPlayer playerScript;
    protected Animator animator;

    // movement variables
    public float runSpeed = 40f;
    protected Vector2 inputVector = new Vector2(0, 0);
    protected float horizontalMove = 0f;
    protected bool jump = false;
    protected bool isLeftPressed = false;     // REMOVE THIS once we get the axis controls
    protected bool isRightPressed = false;

    // attack variables
    protected float timeBtwAttack;          // To prevent spamming skill
    [SerializeField]
    protected float startTimeBtwAttack = 0.3f;
    protected bool wasHurted;         // To prevent issue of getting damaged multiple times by same collider

    // status effect variables
    protected bool isMuddled = false;
    protected bool canMove = true;

    // death respawn stuff
    public float respawnTime = 5f;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidBody;

    // SFX
    protected AudioSource audioSrc;
    public SpecialPotsSFX specialPotsSFX;   // ScriptableObject that stores all the SFX for special potions
    public AudioClip hurtSFX;

    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public virtual void Awake()
    {
        playerScript = GetComponentInParent<GenericPlayer>();
        movementController = GetComponent<CharacterMovementController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        timeBtwAttack -= Time.deltaTime;
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        if (canMove)
        {
            //Debug.Log("move input detected");
            inputVector = context.ReadValue<Vector2>();
            horizontalMove = inputVector.x * runSpeed * (isMuddled ? -1 : 1);
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (canMove)
        {
            //Debug.Log("jump input detected");
            jump = true;
            animator.SetBool("IsJumping", true);
        }
    }

    public void PriPotInput(InputAction.CallbackContext context)
    {
        if (timeBtwAttack <= 0 && canMove)
        {
            //Debug.Log("pri pot input detected");
            UseCharacterPotion();
            timeBtwAttack = startTimeBtwAttack;
        }
    }

    public void SecPotInput(InputAction.CallbackContext context)
    {
        if (timeBtwAttack <= 0 && canMove)
        {
            //Debug.Log("sec pot input detected");
            UsePotion2();
            timeBtwAttack = startTimeBtwAttack;
        }
    }

    public void SpecialPotInput(InputAction.CallbackContext context)
    {
        if (timeBtwAttack <= 0 && canMove)
        {
            //Debug.Log("special pot input detected");
            UsePotion3();
            timeBtwAttack = startTimeBtwAttack;
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Damage") && !wasHurted)
        {
            int otherPlayerNum;
            if (other.gameObject.TryGetComponent(out PrimaryPotion primary))
            {
                otherPlayerNum = primary.casterPlayerNum;
            }
            else if (other.gameObject.TryGetComponent(out SecondaryPotion secondary))
            {
                otherPlayerNum = secondary.casterPlayerNum;
            }
            else
            {
                otherPlayerNum = other.gameObject.GetComponent<ProfessorSecondary>().casterPlayerNum;
            }


            int playerNum = gameObject.transform.parent.gameObject.GetComponentInParent<GenericPlayer>().playerNum;
            if (otherPlayerNum != playerNum)
            {
                Debug.Log("Taken damage from Player " + otherPlayerNum);
                audioSrc.PlayOneShot(hurtSFX);
                playerScript.TakeDamage(otherPlayerNum);
                wasHurted = true;
                StartCoroutine(UnhurtPlayer());
            }
        }
    }

    IEnumerator UnhurtPlayer()
    {
        animator.SetLayerWeight(1, 1);
        // during these 0.3s won't get hurt again
        yield return new WaitForSeconds(1.0f);
        animator.SetLayerWeight(1, 0);
        wasHurted = false;
    }

    // abstract methods must be implemented by child classes
    public abstract void UseCharacterPotion();

    public abstract void UsePotion2();

    public abstract void UsePotion3();

    public abstract void OnDeath();

    // implement methods for all the different potion 3s here
    public void SwiftnessElixir()
    {
        Debug.Log("Started speed boost");
        audioSrc.PlayOneShot(specialPotsSFX.SwiftnessElixirSFX);
        float speedMultiplier = 1.75f;   // TODO: refactor to variable later
        runSpeed *= speedMultiplier;
        StartCoroutine(RevertEnhancedSpeed(speedMultiplier));
    }

    public void KillerBrew()
    {
        Debug.Log("Started Killer Brew");
        audioSrc.PlayOneShot(specialPotsSFX.KillerBrewSFX);
        playerScript.IncreaseDamageDealtTo2();
        StartCoroutine(RevertDamageDealt());
    }

    public void MuddlingMist()
    {
        Debug.Log("Started Muddling Mist");
        audioSrc.PlayOneShot(specialPotsSFX.MuddlingMistSFX);
        playerScript.CastMuddlingMist();
    }

    public void DreamDust()
    {
        Debug.Log("Started Dream Dust");
        audioSrc.PlayOneShot(specialPotsSFX.DreamDustSFX);
        playerScript.CastDreamingDust();
    }

    // Coroutines to end special potion's effect
    IEnumerator RevertEnhancedSpeed(float speedMultiplier)
    {
        yield return new WaitForSeconds(7f);
        runSpeed /= speedMultiplier;
        Debug.Log("Ended speed boost");
    }

    IEnumerator RevertDamageDealt()
    {
        yield return new WaitForSeconds(7f);
        playerScript.DecreaseDamageDealtTo1();
        Debug.Log("Ended Killer Brew");
    }

    // Special Potions Status Effect on this player
    public void SetMuddleness(bool isCharacterMuddled)
    {
        isMuddled = isCharacterMuddled;
    }

    public void SetDreaming(bool isCharacterDreaming)
    {
        // character can move if it is not currently dreaming
        horizontalMove = 0f;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        canMove = !isCharacterDreaming;
    }

    protected IEnumerator SetSpawnPosition(float deathAnimLength)
    {
        controlsManager.DisableActionMap(playerScript.playerNum);
        rigidBody.velocity = Vector3.zero;
        rigidBody.bodyType = RigidbodyType2D.Kinematic;
        boxCollider.enabled = false;
        yield return new WaitForSeconds(deathAnimLength);
        spriteRenderer.enabled = false;
        transform.position = new Vector3(Random.Range(-9f, 9f), 5, 0);
        yield return new WaitForSeconds(respawnTime - deathAnimLength);
        boxCollider.enabled = true;
        rigidBody.bodyType = RigidbodyType2D.Dynamic;
        spriteRenderer.enabled = true;
        controlsManager.EnableCharacterActionMap(playerScript.playerNum);
    }
}
