using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System;
using UnityEngine;
using TMPro;


namespace paper
{
    public class CalculateTimer : MonoBehaviour
    {
        
        float timerCreation;
        public TMP_Text timerText;
        ChangeRooms changeRoom;
        DungeonManager dungeonManager;
        bool isShow;
       
       
        // Start is called before the first frame update
        void Start()
        {
            timerCreation = 0;
            changeRoom = FindObjectOfType<ChangeRooms>();
            dungeonManager = FindObjectOfType<DungeonManager>();
            timerText.enabled = false;
            isShow = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (dungeonManager.useRange || dungeonManager.maxRoom)
            {
                if (!changeRoom.isDone)
                {
                    timerCreation+=Time.deltaTime;
                }
                else {
                    if (!isShow)
                    {
                        isShow = true;
                        StartCoroutine(ShowTime());
                    }
                }
            }
        }


        IEnumerator ShowTime() {
            timerText.enabled=true;
            timerText.text = "Tempo di Costruzione : " + timerCreation.ToString("#.00") +"s";
            yield return new WaitForSeconds(4.5f);
            timerText.enabled = false;
        }
    }
}