using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace paper
{
    public class TreasureManager : MonoBehaviour
    {
        PlayerMovement playerMovement;
        public GameObject treasure;

        private void Start()
        {

            playerMovement = FindObjectOfType<PlayerMovement>();
        }

        private void Update()
        {
            if (Vector3.Distance(playerMovement.gameObject.transform.position, treasure.transform.position) > 5f)
            {
                treasure.gameObject.SetActive(false);
            }
            else
            {
                treasure.gameObject.SetActive(true);
            }
        }

    }
}