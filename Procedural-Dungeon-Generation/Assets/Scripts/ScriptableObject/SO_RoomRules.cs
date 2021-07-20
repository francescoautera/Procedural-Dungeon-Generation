using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace paper
{
    [CreateAssetMenu(fileName = "Paper", menuName = "Paper/RoomRules", order = 1)]
    public class SO_RoomRules : ScriptableObject
    {
        public Room[] rooms;
        public Material paint;
        public int roomRange;
        public int maxRoom;
        public int EnemySpawned;
        public GameObject enemy;
        public GameObject cowboy;


        public bool GetRoom(Connection connection, Room rooms)
        {

            foreach (ConnectionPoints c in rooms.connections)
            {
                if (connection.isOpposite(c.connection))
                {

                    return true;
                }

            }
            return false;
        }

        public bool GetLittleRoom(Connection connection, Room rooms)
        {
            if (rooms.connections.Count > 1) { return false; }
            foreach (ConnectionPoints c in rooms.connections)
            {
                if (connection.isOpposite(c.connection))
                {

                    return true;
                }

            }
            return false;
        }

        [System.Obsolete]
        public void AddElementinFinalRoom(Room room)
        {
            var en = Instantiate(cowboy, room.center.transform.position, Quaternion.identity);
            var trs = en.transform.GetComponentInChildren<Animator>();
            
            trs.gameObject.transform.LookAt(room.connections[0].transform);
            //en.transform.localScale=new Vector3(1.5f,1.5f,1.5f);
        }

       

      
    }
}


