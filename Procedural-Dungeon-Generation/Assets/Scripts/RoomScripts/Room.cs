using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace paper
{
    public class Room : MonoBehaviour
    {
        public List<ConnectionPoints> connections;
        public bool completedRoomsCreation;
        public int roomInstanciate;
        DungeonManager manager;
        public GameObject center;
        public int dir;
        public bool lastConnection;
        public int indexX;
        public int indexY;
        public bool painted;
        public PlayerMovement player;
        [SerializeField] float dist;
        public List<Renderer> renderers = new List<Renderer>();
        public bool rangeAlpha;
        public bool firstRoom;
        public bool stop;
        CameraManager cameraManager;
        
        private void Start()
        {

            roomInstanciate = 0;
            completedRoomsCreation = false;
            rangeAlpha = true;
            Renderer[] rend = GetComponentsInChildren<Renderer>();
            renderers = rend.ToList();
            cameraManager = FindObjectOfType<CameraManager>();
            foreach (Renderer r in renderers)
            {
                if (cameraManager.isChanged)
                {
                    r.material.SetFloat("_Radius", 500);
                }
                else
                {
                    r.material.SetFloat("_Radius", 5);
                }
            }

        }

        private void Update()
        {
            

        }

        IEnumerator ChangeAlpha(float dist) {
            while (dist > 0.01f) {
                dist -= 0.05f;
                foreach (Renderer rend in renderers)
                {
                    rend.material.SetFloat("_Radius", dist);
                }
                yield return null;
            }
            foreach (Renderer rend in renderers)
            {
                rend.material.SetFloat("_Radius", 0);
            }
            
        }
        public Vector3 checkPosforRoom(ConnectionPoints c, Room room)
        {
            if (c.connection.name == "Est")
            {
                return new Vector3(room.transform.position.x + 5, room.transform.position.y, room.transform.position.z);
            }
            if (c.connection.name == "Ovest")
            {
                return new Vector3(room.transform.position.x - 5, room.transform.position.y, room.transform.position.z);
            }
            if (c.connection.name == "Nord")
            {
                return new Vector3(room.transform.position.x, room.transform.position.y, room.transform.position.z + 5);
            }
            if (c.connection.name == "Sud")
            {
                return new Vector3(room.transform.position.x, room.transform.position.y, room.transform.position.z - 5);
            }
            return Vector3.zero;
        }


        public bool CheckCreationRoom()
        {


            if (roomInstanciate >= connections.Count)
            {
                return true;

            }

            return false;
        }

        public Vector3 CheckPosforHallway(ConnectionPoints c, Room room)
        {
            if (c.connection.name == "Est")
            {
                return new Vector3(room.center.transform.position.x + 2, room.center.transform.position.y + 0.24f, room.center.transform.position.z);
            }
            if (c.connection.name == "Ovest")
            {
                return new Vector3(room.center.transform.position.x - 3, room.center.transform.position.y + 0.24f, room.center.transform.position.z);
            }
            if (c.connection.name == "Nord")
            {
                return new Vector3(room.center.transform.position.x, room.center.transform.position.y + 0.24f, room.center.transform.position.z + 2);
            }
            if (c.connection.name == "Sud")
            {
                return new Vector3(room.center.transform.position.x, room.center.transform.position.y + 0.24f, room.center.transform.position.z - 3);
            }
            return Vector3.zero;
        }

        
    }
}