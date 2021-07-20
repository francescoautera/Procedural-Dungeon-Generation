using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace paper
{
    public class PlayerMovement : MonoBehaviour
    {
        public  float velocity;
        public float rotationVelocity;
        Rigidbody rb;
        KeyCode code;
        Vector3 aimDirection;
        ResizeManager ChangeRooms;
        //public TMP_Text win;
        
       

        private void Start()
        {
            ChangeRooms = FindObjectOfType<ResizeManager>();
            rb = GetComponent<Rigidbody>();
            rotationVelocity = 0;
            //win = FindObjectOfType<TMP_Text>();
        }
        private void Update()
        {
            if (ChangeRooms.isDone )
            {
                if (Input.GetKey(KeyCode.W))
                {
                    code = KeyCode.W;
                    rb.velocity = Vector3.RotateTowards(transform.forward, aimDirection, 0.1f, 0.0f) * velocity * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    code = KeyCode.S;
                    rb.velocity = Vector3.RotateTowards(-transform.forward, aimDirection, 0.1f, 0.0f) * velocity/2 * Time.deltaTime;
                }


                if (Input.GetKey(KeyCode.D))
                {
                    code = KeyCode.D;
                    rb.velocity = Vector3.RotateTowards(transform.right, aimDirection, 0.1f, 0.0f) * velocity * Time.deltaTime;
                }

                if (Input.GetKey(KeyCode.A))
                {
                    code = KeyCode.A;
                    rb.velocity = Vector3.RotateTowards(-transform.right, aimDirection, 0.1f, 0.0f) * velocity * Time.deltaTime;
                }

                
                if (Input.GetKeyUp(code)) { rb.velocity = Vector3.zero; }
                transform.GetComponentInChildren<Animator>().SetFloat("Speed", rb.velocity.magnitude);
            }

            transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * Time.deltaTime * rotationVelocity, 0));
        }


    }
}
