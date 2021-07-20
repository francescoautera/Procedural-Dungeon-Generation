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
        public int roomRange;
        public int maxRoom;
        
      


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

        
        
       

       

      
    }
}


