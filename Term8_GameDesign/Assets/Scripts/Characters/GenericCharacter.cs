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

    // attack variables
    protected float timeBtwAttack;          // To prevent spamming skill
    [SerializeField]
    protected float startTimeBtwAttack = 0.3f;
    protected bool wasHurted;         // To prevent issue of getting damaged multiple times by same collider

    // status effect variables
    protected bool isMuddled = false;
    protected bool canMove = true;
    public bool isFast = false;      // used to prevent stacking of swiftness elixirs
    [SerializeField]
    private float speedBoostMultiplier = 1.75f;

    // death respawn stuff
    public float respawnTime = 2f;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    protected Rigidbody2D rigidBody;

    // SFX
    protected AudioSource audioSrc;
    public SpecialPotsSFX specialPotsSFX;   // ScriptableObject that stores all the SFX for special potions
    public AudioClip hurtSFX;
    public AudioClip deathSFX;
    public AudioClip jumpSFX;
    public AudioClip landSFX;

    public GameObject MMSprite;
    public GameObject DDSprite;

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
            horizontalMove *= (isFast ? speedBoostMultiplier : 1);
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (canMove)
        {
            //audioSrc.PlayOneShot(jumpSFX);
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
        //audioSrc.PlayOneShot(landSFX);
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("pickup"))
        {
            Debug.Log("I met some weeds");
            playerScript.AddWeets();
            collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("potion"))
        {
            Debug.Log("I met the potion");
            playerScript.AddSecPotionQty();
            collision.gameObject.SetActive(false);
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

    public void UsePotion3()
    {
        // remember to check if there's any more potions left.
        // if playerScript.UseSpecialPotionIfCanUse() -> use
        if (playerScript.UseSpecialPotionIfCanUse()) {
            Debug.Log("Potion 3!");
            switch (playerScript.GetSpecialPotionType())
            {
                case SpecialPotionType.DreamDust:
                    DreamDust();
                    break;
                case SpecialPotionType.KillerBrew:
                    KillerBrew();
                    break;
                case SpecialPotionType.MuddlingMist:
                    MuddlingMist();
                    break;
                case SpecialPotionType.SwiftnessElixir:
                    SwiftnessElixir();
                    break;
            }
        }
        
    }

    public abstract void OnDeath();

    // implement methods for all the different potion 3s here
    public void SwiftnessElixir()
    {
        Debug.Log("Started speed boost");
        animator.SetTrigger("SE");
        audioSrc.PlayOneShot(specialPotsSFX.SwiftnessElixirSFX);
        isFast = true;
        StartCoroutine(RevertEnhancedSpeed());
    }

    public void KillerBrew()
    {
        Debug.Log("Started Killer Brew");
        animator.SetTrigger("KB");
        audioSrc.PlayOneShot(specialPotsSFX.KillerBrewSFX);
        playerScript.IncreaseDamageDealtTo2();
        StartCoroutine(RevertDamageDealt());
    }

    public void MuddlingMist()
    {
        Debug.Log("Started Muddling Mist");
        animator.SetTrigger("Open");
        MMSprite.SetActive(true);
        Invoke("disableSprite", 1f);
        audioSrc.PlayOneShot(specialPotsSFX.MuddlingMistSFX);
        playerScript.CastMuddlingMist();
    }

    public void disableSprite() {
        MMSprite.SetActive(false);
        DDSprite.SetActive(false);
    }

    public void DreamDust()
    {
        Debug.Log("Started Dream Dust");
        animator.SetTrigger("Open");
        DDSprite.SetActive(true);
        Invoke("disableSprite", 1f);
        audioSrc.PlayOneShot(specialPotsSFX.DreamDustSFX);
        playerScript.CastDreamingDust();
    }

    // Coroutines to end special potion's effect
    IEnumerator RevertEnhancedSpeed()
    {
        yield return new WaitForSeconds(7f);
        isFast = false;
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
        audioSrc.PlayOneShot(deathSFX);
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
