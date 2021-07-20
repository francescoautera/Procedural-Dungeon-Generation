using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace paper {
    public class CameraManager : MonoBehaviour
    {
        public Camera upCamera;
        Camera playerCamera;
        PlayerMovement movementPlayer;
        bool isChanged;
        [SerializeField] float velocity;
        public UIManager manager;

        private IEnumerator Start()
        {
            upCamera.enabled = true;
            isChanged = true;
            yield return new WaitForSeconds(0.5f);
            movementPlayer = FindObjectOfType<PlayerMovement>();
            playerCamera = movementPlayer.GetComponentInChildren<Camera>();
            playerCamera.enabled = false;
            

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M)) {
                if (isChanged)
                {
                    ResetCameraPlayer();
                   
                }
                else {
                    ChangeCamera();
                }
            }

            MoveCamera();

        }

        private void ChangeCamera() {
            
            isChanged = true;
            upCamera.enabled = true;
            playerCamera.enabled = false;
            movementPlayer.velocity = 0;

        }

        private void ResetCameraPlayer() {
           
            isChanged = false;
            upCamera.enabled = false;
            playerCamera.enabled = true;
            movementPlayer.velocity = 150;
        }

        void MoveCamera() {
            if (isChanged) {
                if (Input.GetKey(KeyCode.W)) {
                    transform.position += Vector3.forward*Time.deltaTime*velocity;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    transform.position += Vector3.back * Time.deltaTime * velocity;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.position += Vector3.right * Time.deltaTime * velocity;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    transform.position += Vector3.left * Time.deltaTime * velocity;
                }
                transform.position -= new Vector3(0,Input.mouseScrollDelta.y,0);
            }
        }
    }
}