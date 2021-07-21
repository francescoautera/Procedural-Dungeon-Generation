using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
namespace paper
{

    public class UIManager : MonoBehaviour
    {

        public List<Button> difficultButtons;
        public List<SO_RoomRules> rules;
        public DungeonManager manager;
        public GameObject difficult;
        public GameObject Action;
        public int index;
        public List<TMP_Text> maxRooms;
        public List<TMP_Text> roomRanges;
        public GameObject blocks;
        public TMP_Text maxRoom;
        public TMP_Text roomRange;
        bool showText;
        public TMP_Text roomNumber;
        public GameObject maxRoomButton;
        public GameObject roomRangeButton;
        public GameObject restartButton;


        private void Start()
        {
            showText = false;
            for (int i = 0; i < rules.Count; i++)
            {
                maxRooms[i].text = "fixed value Dungeon :" + rules[i].maxRoom;

                roomRanges[i].text = "variable Dungeon : " + "(" + (rules[i].roomRange + 1).ToString() + "-" + (2 * rules[i].roomRange + 1).ToString() + ")";
            }
            maxRoom.enabled = false;
            roomRange.enabled = false;
            roomNumber.enabled = false;
            restartButton.SetActive(false);
            ActiveObject();
        }

        private void Update()
        {
            if (manager.ChangeRooms.isDone)
            {
                restartButton.SetActive(true);
             
                if (!showText)
                {

                    showText = true;

                    StartCoroutine(ShowText());
                }
            }
            
        }

        IEnumerator ShowText()
        {

            roomNumber.enabled = true;
            
            roomNumber.text = "Room istanciated: " + manager.createdRooms.Count;
            yield return new WaitForSeconds(4.5f);
           
            roomNumber.enabled = false;
        }
        public void OnButtonClick(Button button)
        {

            index = difficultButtons.IndexOf(button);
            manager.rules = rules[index];

            difficult.SetActive(false);
            Action.SetActive(true);
            blocks.SetActive(false);
            maxRoom.enabled = true;
            roomRange.enabled = true;
            maxRoom.text = "fixed value Dungeon :" + rules[index].maxRoom;
            roomRange.text = "variable Dungeon: " + "(" + (rules[index].roomRange + 1).ToString() + "-" + (2 * rules[index].roomRange + 1).ToString() + ")";
            for (int i = 0; i < rules.Count; i++)
            {
                maxRooms[i].enabled = false;
                roomRanges[i].enabled = false;
            }

        }




        public void ActiveObject()
        {
            difficult.SetActive(true);

            Action.SetActive(false);
        }
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void Quit()
        {

            Application.Quit();
        }
    }
}
