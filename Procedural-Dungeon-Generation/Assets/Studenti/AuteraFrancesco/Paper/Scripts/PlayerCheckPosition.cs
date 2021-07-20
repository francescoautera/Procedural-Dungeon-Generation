using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace paper
{
    public class PlayerCheckPosition : MonoBehaviour
    {
        Room lastCreatedRoom;
        TransitionManager manager;
        public So_Sfx win;
        public So_Sfx far;
        public So_Sfx near;
        bool winner;
        SfxManager sfxManager;
        [SerializeField]AiEnemy enemyAi;
        bool changeMusic;
        [SerializeField]float distance;

        private void Start()
        {

            changeMusic = false;
            winner = false;
            manager = FindObjectOfType<TransitionManager>();
            sfxManager = FindObjectOfType<SfxManager>();
            sfxManager.source.mute = false;
            sfxManager.PlayMusic(far, 1f);
        }

        private void Update()
        {
            if (lastCreatedRoom != null) {
                if(Mathf.Abs(transform.position.x-lastCreatedRoom.center.transform.position.x)<1f
                    && Mathf.Abs(transform.position.z - lastCreatedRoom.center.transform.position.z) < 0.5f){
                    if (!winner)
                    {
                        winner = true;
                        AudioManager.Instance.playSFX(win);
                        sfxManager.PlayMusic(far, 2f);
                        sfxManager.source.mute = true;
                        manager.Restart();
                    }
                }
            }

           
        }

        public void SetLastRoom(Room lst) {
            lastCreatedRoom = lst;
        }

        public void SetEnemy(AiEnemy enemy) {
            enemyAi = enemy;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {

                sfxManager.PlayMusic(near, 2f);

            }
        }
       
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {

                sfxManager.PlayMusic(far, 2f);

            }
        }
    }
}