using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
            }
        }
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
			m_Grounded = false;
			/*m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));*/
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, jumpForce);			// use velocity so that we can do mario jump
		}
	}
	private void Flip()
	{
		m_FacingRight = !m_FacingRight;
		Vector3 playerLocalScale = transform.localScale;
		playerLocalScale.x *= -1;		// flip
		transform.localScale = playerLocalScale;
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
