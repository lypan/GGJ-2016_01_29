using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = false;
            float h = 0;
            bool attack = false;
            bool up = false;
            bool down = false;
          
            //crouch = Input.GetKey(KeyCode.LeftControl);
            //if(!m_Character.onAttack)
             h = CrossPlatformInputManager.GetAxis("Horizontal");

           // if (!m_Character.onDamage)
           // {
                attack = Input.GetKeyDown(KeyCode.LeftControl);
                up = Input.GetKey(KeyCode.UpArrow);
                down = Input.GetKey(KeyCode.DownArrow);

           // }
                // Pass all parameters to the character control script.
            m_Character.Move(h, m_Jump,attack , up , down);
            m_Jump = false;
        }
    }
}
