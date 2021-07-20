using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace paper {
    public class ChangeRooms : MonoBehaviour
    {
        public DungeonManager manager;
        public List<string> connectionNeed;
        public RoomRules rules;
        public Room instanceRoom;
        public List<Room> resizedRooms;
        public bool isDone;
        [SerializeField]PlayerCheckPosition checkPosition;

        [System.Obsolete]
        public void ResizeRoom(PlayerMovement player) {
            checkPosition = FindObjectOfType<PlayerCheckPosition>();
            foreach (Room room in manager.createdRooms)
            {
                if (room.roomInstanciate>=room.connections.Count) { resizedRooms.Add(room); room.player = player; continue; }
                else
                {
                    foreach (ConnectionPoints connection in room.connections)
                    {
                        if (connection.isIstanciate)
                        {
                            connectionNeed.Add(connection.connection.name);
                        }
                    }
                    foreach (Room rooms in rules.rooms)
                    {
                        if (rooms.connections.Count == connectionNeed.Count)
                        {
                            var count = 0;
                            foreach (ConnectionPoints connection in rooms.connections)
                            {
                                if (connectionNeed.Contains(connection.connection.name))
                                {
                                    count++;
                                }
                                else { count = 0; break; }
                            }
                            if (count == rooms.connections.Count)
                            {
                                instanceRoom = rooms;
                                break;
                            }
                        }
                    }
                    var instanciate = Instantiate(instanceRoom, room.transform.position, Quaternion.identity, manager.roomParent);
                    instanciate.indexX = room.indexX;
                    instanciate.indexY = room.indexY;
                    foreach (ConnectionPoints points in instanciate.connections)
                    {
                        points.isIstanciate = true;
                    }
                    resizedRooms.Add(instanciate);
                    instanciate.player = player;
                    room.gameObject.SetActive(false);
                    connectionNeed.Clear();
                }
            }
            foreach (ConnectionPoints connectionPoints in resizedRooms[resizedRooms.Count - 1].connections) {
                if (!connectionPoints.AddHallway)
                {
                    InstanciateHallway(connectionPoints,player);
                    

                }
            }
            DestroyRoomsnotNecessary();
            rules.AddElementinFinalRoom(LastRoomwithoneOpen());
            checkPosition.SetLastRoom(LastRoomwithoneOpen());
            isDone = true;
          
        }
        public void InstanciateHallway(ConnectionPoints connection,PlayerMovement movement)
        {
            if (connection.connection.name == "Est" || connection.connection.name == "Ovest")
            {
                GameObject inst = Instantiate(manager.Hallway, resizedRooms[resizedRooms.Count - 1].CheckPosforHallway(connection, resizedRooms[resizedRooms.Count - 1]), Quaternion.identity, manager.hallwayParent);
                inst.transform.Rotate(0, 90, 0);
                inst.GetComponent<HallWay>().player = movement;
                manager.hallWays.Add(inst.GetComponent<HallWay>());
                


            }
            else if (connection.connection.name == "Nord" || connection.connection.name == "Sud")
            {
                GameObject insta = Instantiate(manager.Hallway, resizedRooms[resizedRooms.Count - 1].CheckPosforHallway(connection, resizedRooms[resizedRooms.Count - 1]), Quaternion.identity, manager.hallwayParent);
                insta.GetComponent<HallWay>().player = movement;
                manager.hallWays.Add(insta.GetComponent<HallWay>());
                

            }



        }

        Room LastRoomwithoneOpen() {

            for (int i = resizedRooms.Count - 1; i > 0; i--) {
                if (resizedRooms[i].connections.Count == 1) {
                    return resizedRooms[i];
                }
            }
            return null;
        }

        void DestroyRoomsnotNecessary()
        {
            foreach (Room room in manager.createdRooms) {
                if (!resizedRooms.Contains(room)) {
                    room.gameObject.SetActive(false);
                }
            }
        }
    }

}
