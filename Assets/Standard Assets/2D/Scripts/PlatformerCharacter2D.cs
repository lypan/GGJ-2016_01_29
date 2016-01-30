using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 100f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        [SerializeField] private bool inLight;
        private bool attackable = true;
        public bool onDamage;
        public bool onAttack;

        private bool Damagable = true;
        private float damageCount;
        private float totalDamageTime = 1.5f;
        private Transform m_Attack_Origin;
        private Transform m_Center;

        [SerializeField] GameObject black_Bullet;
        [SerializeField] GameObject white_Bullet;

        [SerializeField] int Spirit = 3;


        Vector2 attackDir;

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Attack_Origin = transform.Find("AttackOrigin");
            m_Center = transform.Find("Center");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            attackDir = new Vector2(1.0f, 0);
        }


        private void FixedUpdate()
        {
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }
            m_Anim.SetBool("Ground", m_Grounded);
            //m_Anim.SetTrigger("goLand");

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

            if (!Damagable)
            {
                Color c = GetComponent<SpriteRenderer>().color;
                damageCount += Time.deltaTime;
                c.a = Mathf.Abs((10.0f*damageCount / totalDamageTime)%2.0f -1.0f);

                GetComponent<SpriteRenderer>().color = c;
              
                if (damageCount >= 1.5f)
                {
                    c = GetComponent<SpriteRenderer>().color;
                    //damageCount += Time.deltaTime;
                    c.a = 1.0f;
                    GetComponent<SpriteRenderer>().color = c;

                    damageCount = 0;
                    Damagable = true;
                }
            }

            if (Spirit >= 1)
                m_Center.Find("Spirit1").gameObject.GetComponent<SpriteRenderer>().enabled = true;
            else if(Spirit<1)
                m_Center.Find("Spirit1").gameObject.GetComponent<SpriteRenderer>().enabled = false;

            if (Spirit >= 2)
                m_Center.Find("Spirit2").gameObject.GetComponent<SpriteRenderer>().enabled = true;
            else if (Spirit < 2)
                m_Center.Find("Spirit2").gameObject.GetComponent<SpriteRenderer>().enabled = false;

            if (Spirit >= 3)
                m_Center.Find("Spirit3").gameObject.GetComponent<SpriteRenderer>().enabled = true;
            else if (Spirit < 3)
                m_Center.Find("Spirit3").gameObject.GetComponent<SpriteRenderer>().enabled = false;

            if (Spirit >= 4)
                m_Center.Find("Spirit4").gameObject.GetComponent<SpriteRenderer>().enabled = true;
            else if (Spirit < 4)
                m_Center.Find("Spirit4").gameObject.GetComponent<SpriteRenderer>().enabled = false;

            if (Spirit >= 5)
                m_Center.Find("Spirit5").gameObject.GetComponent<SpriteRenderer>().enabled = true;
            else if (Spirit < 5)
                m_Center.Find("Spirit5").gameObject.GetComponent<SpriteRenderer>().enabled = false;

            if (Spirit >= 6)
                m_Center.Find("Spirit6").gameObject.GetComponent<SpriteRenderer>().enabled = true;
            else if (Spirit < 6)
                m_Center.Find("Spirit6").gameObject.GetComponent<SpriteRenderer>().enabled = false;

            if (Spirit >= 7)
                m_Center.Find("Spirit7").gameObject.GetComponent<SpriteRenderer>().enabled = true;
            else if (Spirit < 7)
                m_Center.Find("Spirit7").gameObject.GetComponent<SpriteRenderer>().enabled = false;

            if (Spirit >= 8)
                m_Center.Find("Spirit8").gameObject.GetComponent<SpriteRenderer>().enabled = true;
            else if (Spirit < 8)
                m_Center.Find("Spirit8").gameObject.GetComponent<SpriteRenderer>().enabled = false;

            /*
            Vector3 axis;
            if (m_FacingRight)
                axis = new Vector3(0, 0, 1);
            else
                axis = new Vector3(0, 0, -1);
            m_Center.Rotate(n, Time.deltaTime * 180, Space.World);
            */
        }


        public void Move(float move, bool crouch, bool jump , bool attack)
        {
          

            // If crouching, check to see if the character can stand up
           // if (!crouch && m_Anim.GetBool("Crouch"))
           // {
                // If the character has a ceiling preventing them from standing up, keep them crouching
             //   if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
             //   {
              //      crouch = true;
             //   }
          //  }

            // Set whether or not the character is crouching in the animator
            //m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move*m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                Debug.Log("qq");
                m_Anim.SetTrigger("goJump");
                
               m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }

            if (attackable && attack)
            {
                if (!inLight)
                {
                    if(black_Bullet!=null){
                        m_Anim.SetTrigger("goAttack");
                        onAttack = true;
                 }
                }
                else if (inLight)
                {
                    if (white_Bullet != null)
                    {
                        m_Anim.SetTrigger("goAttack");
                        onAttack = true;
                    }

                }
            }
        }

        public void OnBulletOut()
        {
            GameObject temp;
            if (!inLight)
                temp = Instantiate(black_Bullet, m_Attack_Origin.position, Quaternion.identity) as GameObject;
            else
            {
                temp = Instantiate(white_Bullet, m_Attack_Origin.position, Quaternion.identity) as GameObject;
            }
            Vector2 tempVel = (Vector2)m_Attack_Origin.position - (Vector2)m_Center.position;
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(m_Rigidbody2D.velocity.x, 0) + tempVel * 10.0f;
        }

        public void OnAttackEnd()
        {
            m_Anim.SetTrigger("attackEnd");
            onAttack = false;
        }

        public void OnDamage()
        {
            if (!Damagable)
                return;

            m_Anim.SetTrigger("goDamage");
            m_Rigidbody2D.velocity = Vector2.zero;
            onDamage = true;
            Damagable = false;

        }

         public void OnDamage(Vector3 dir)
        {
            if (!Damagable)
                return;

             m_Anim.SetTrigger("goDamage");
             m_Rigidbody2D.velocity =  (Vector2)(-0.3f *dir);
             onDamage = true;
             Damagable = false;

        }

        public void DamageEnd()
        {
            onDamage = false;
        }

        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        public void SetInLight(bool flag)
        {
            inLight = flag;

        }

        public bool GetInLight()
        {
            return inLight;
        }

        public bool GetInputable()
        {
            return !(onAttack || onDamage);
        }

        void OnCollisionEnter(Collision other)
        {
            GameObject otherOBJ = other.gameObject;
            if (otherOBJ.tag == "Monster" || otherOBJ.tag == "Monster_Bullet")
            {
                OnDamage(other.impulse);
            }

        }

    }
}
