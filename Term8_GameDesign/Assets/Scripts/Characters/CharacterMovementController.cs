using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMovementController : MonoBehaviour
{
    [SerializeField] private LayerMask m_WhatIsGround;      // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;       // A position marking where to check if the player is grounded.
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private float m_fallMultiplier = 5f;
    [SerializeField] protected float jumpForce = 10f;

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;
    private Vector3 m_Velocity = Vector3.zero;

	private AudioSource audioSrc;
	public AudioClip jumpSFX;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;			// allow us to set animator to stop jumping

	private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
		audioSrc = GetComponent<AudioSource>();
		if (OnLandEvent == null) OnLandEvent = new UnityEvent();
	}

    private void FixedUpdate()
    {
		bool wasGrounded = m_Grounded;
		m_Grounded = false;
		
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
			//Debug.Log(m_Rigidbody2D.velocity.y);
			// if y-velocity not at 0 means passing through one-way platformer and not on ground. threshold is set at 1.5f because moving left somehow provides velocity of -1.3f.
			if (colliders[i].gameObject != gameObject && Math.Abs(m_Rigidbody2D.velocity.y) < 1.5f)
            {
				m_Grounded = true;
				if (!wasGrounded) OnLandEvent.Invoke();
            }
        }
		//Debug.Log("im grounded " + m_Grounded);
	}

	public void Move(float move, bool jump)
	{
		// move x-direction
		Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
		m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

		if (move > 0 && !m_FacingRight)
		{
			// moving right but player face left
			Flip();
		}
		else if (move < 0 && m_FacingRight)
		{
			// moving left but player face right
			Flip();
		}

		if (m_Grounded && jump)
		{
			audioSrc.PlayOneShot(jumpSFX);
			//Debug.Log("jump velocity activated!");
			m_Grounded = false;
			/*m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));*/
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, jumpForce);			// use velocity so that we can do mario jump
		}
	}
	private void Flip()
	{
		m_FacingRight = !m_FacingRight;
		transform.Rotate (0f, 180f, 0f);
	}

	public void BetterJump()
	{
		// super mario style jumping when landing, more gravity
		if (m_Rigidbody2D.velocity.y < 0)
		{
			m_Rigidbody2D.velocity += Vector2.up * Physics2D.gravity * (m_fallMultiplier - 1) * Time.deltaTime;
		}
	}
}
