using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace paper {
    public class AiEnemy : MonoBehaviour
    {
        DungeonManager manager;
        public DirectedAgent agent;
        [SerializeField] List<Room> roomAvaiable;
        TransitionManager managerTransition;
        public So_Sfx lose;
        public bool loser;
        SfxManager sfxManager;
        private void Start()
        {
            loser = false;
            sfxManager = FindObjectOfType<SfxManager>();
            manager = FindObjectOfType<DungeonManager>();
            managerTransition = FindObjectOfType<TransitionManager>();
            StartCoroutine(wait());
            
        }

        IEnumerator wait() {
            yield return new WaitForSeconds(1.5f);
            if (manager.maxRoom)
            {
                roomAvaiable.AddRange(manager.ChangeRooms.resizedRooms);
            }
            if (manager.useRange)
            {
                if (manager.index >= manager.createdRooms.Count - 1)
                {
                    roomAvaiable.AddRange(manager.createdRooms);
                }
                else
                {
                    roomAvaiable.AddRange(manager.ChangeRooms.resizedRooms);
                }
            }
        }
        private void Update()
        {
            if (Mathf.Abs(transform.position.x-agent.targetPosition.x)<0.01f && Mathf.Abs(transform.position.z - agent.targetPosition.z) < 0.01f) {
                
                agent.SetTarget(SetDestination(roomAvaiable));
            }
            //ShootRaycast();
        }

        Vector3 SetDestination(List<Room> rooms) {
            return rooms[Random.Range(0, rooms.Count)].center.transform.position;
        }

        void ShootRaycast() {
            RaycastHit hit;
            
            
            if (Physics.Raycast(transform.position, Vector3.forward, out hit, 2f)
                ||Physics.Raycast(transform.position, Vector3.back, out hit, 2f)
                ||Physics.Raycast(transform.position, Vector3.left, out hit, 2f)
                ||Physics.Raycast(transform.position, Vector3.right, out hit, 2f)
                )
            {
                 if (hit.collider.gameObject.GetComponent<PlayerMovement>()) {
                    if (!loser)
                    {
                        Lose();
                    }
                 }
            }
        }

        public void Lose() {
            
            agent.agent.speed = 0;
            loser = true;
            AudioManager.Instance.playSFX(lose);

            sfxManager.source.mute = true;

            managerTransition.Restart();

        }
    }
}
