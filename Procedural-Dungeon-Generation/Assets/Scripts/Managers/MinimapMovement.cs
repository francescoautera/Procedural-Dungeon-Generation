using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace paper {
    public class MinimapMovement : MonoBehaviour
    {

        public PlayerMovement playertransform;
        bool startmove;

        private void Start()
        {
            StartCoroutine(findPlayer());   
        }

        IEnumerator findPlayer() {
            yield return new WaitForSeconds(3f);
            startmove = true;
            playertransform = FindObjectOfType<PlayerMovement>();
        }

        // Update is called once per frame
        void Update()
        {
            if (startmove)
            {
                transform.position = new Vector3(playertransform.transform.position.x, transform.position.y, playertransform.transform.position.z);
            }
            if (Input.GetKeyDown(KeyCode.E)) {
                transform.position = new Vector3(transform.position.x, transform.position.y + 30, transform.position.z);
            }
            if (Input.GetKeyUp(KeyCode.E)) { 
                transform.position= new Vector3(transform.position.x, transform.position.y - 30, transform.position.z);
            }
        }

    }
}