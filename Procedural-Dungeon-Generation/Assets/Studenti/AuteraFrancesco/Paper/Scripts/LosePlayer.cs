using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace paper {
    public class LosePlayer : MonoBehaviour
    {
        public AiEnemy enemyAI;


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) {
                if (!enemyAI.loser)
                {
                    enemyAI.Lose();
                }
            }
        }
    }
}