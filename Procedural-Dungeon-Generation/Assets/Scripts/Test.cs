using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace paper
{
    public class Test : MonoBehaviour
    {

     /*   public DungeonManager manager;
        public List<string> connectionNeeded = new List<string>();
        Room roomTest;

        public List<string> FIndEst(Room roomOld)
        {
            //Est
            if (manager.cells[roomOld.indexX + 2, roomOld.indexY] > 0)
            {
                foreach (Room room in manager.createdRooms)
                {
                    if (room.indexX == roomOld.indexX + 2)
                    {
                        roomTest = room;
                        break;
                    }
                }
                for (int i = 0; i < roomTest.connections.Length; i++)
                {
                    if (roomTest.connections[i].connection.name == "Ovest")
                    {
                        roomTest.connections[i].connectionCompleted = true;
                        connectionNeeded.Add("Est");
                    }
                }
            }
            //Nord
            if (manager.cells[roomOld.indexX + 1, roomOld.indexY + 1] > 0)
            {
                foreach (Room room in manager.createdRooms)
                {
                    if (room.indexX == roomOld.indexX + 1 && room.indexY == roomOld.indexY + 1)
                    {
                        roomTest = room;
                        break;
                    }
                }
                for (int i = 0; i < roomTest.connections.Length; i++)
                {
                    if (roomTest.connections[i].connection.name == "Sud")
                    {
                        roomTest.connections[i].connectionCompleted = true;
                        connectionNeeded.Add("Nord");
                    }
                }
            }
            //Sud
            if (manager.cells[roomOld.indexX + 1, roomOld.indexY - 1] > 0)
            {
                foreach (Room room in manager.createdRooms)
                {
                    if (room.indexX == roomOld.indexX + 1 && room.indexY == roomOld.indexY - 1)
                    {
                        roomTest = room;
                        break;
                    }
                }
                for (int i = 0; i < roomTest.connections.Length; i++)
                {
                    if (roomTest.connections[i].connection.name == "Nord")
                    {
                        roomTest.connections[i].connectionCompleted = true;
                        connectionNeeded.Add("Sud");
                    }
                }
            }


            return connectionNeeded;
        }



        public List<string> FindOvest(Room roomOld)
        {
            //Ovest
            if (manager.cells[roomOld.indexX - 2, roomOld.indexY] > 0)
            {
                foreach (Room room in manager.createdRooms)
                {
                    if (room.indexX == roomOld.indexX - 2)
                    {
                        roomTest = room;
                        break;
                    }
                }
                for (int i = 0; i < roomTest.connections.Length; i++)
                {
                    if (roomTest.connections[i].connection.name == "Est")
                    {
                        roomTest.connections[i].connectionCompleted = true;
                        connectionNeeded.Add("Ovest");
                    }
                }
            }
            //Nord
            if (manager.cells[roomOld.indexX - 1, roomOld.indexY + 1] > 0)
            {
                foreach (Room room in manager.createdRooms)
                {
                    if (room.indexX == roomOld.indexX - 1 && room.indexY == roomOld.indexY + 1)
                    {
                        roomTest = room;
                        break;
                    }
                }
                for (int i = 0; i < roomTest.connections.Length; i++)
                {
                    if (roomTest.connections[i].connection.name == "Sud")
                    {
                        roomTest.connections[i].connectionCompleted = true;
                        connectionNeeded.Add("Nord");
                    }
                }
            }
            //Sud
            if (manager.cells[roomOld.indexX - 1, roomOld.indexY - 1] > 0)
            {
                foreach (Room room in manager.createdRooms)
                {
                    if (room.indexX == roomOld.indexX - 1 && room.indexY == roomOld.indexY - 1)
                    {
                        roomTest = room;
                        break;
                    }
                }
                for (int i = 0; i < roomTest.connections.Length; i++)
                {
                    if (roomTest.connections[i].connection.name == "Nord")
                    {
                        roomTest.connections[i].connectionCompleted = true;
                        connectionNeeded.Add("Sud");
                    }
                }
            }


            return connectionNeeded;



        }



        public List<string> FindNord(Room roomOld)
        {

            //Nord
            if (manager.cells[roomOld.indexX, roomOld.indexY + 2] > 0)
            {
                foreach (Room room in manager.createdRooms)
                {
                    if (room.indexX == roomOld.indexX && room.indexY == roomOld.indexY + 2)
                    {
                        roomTest = room;
                        break;
                    }
                }
                for (int i = 0; i < roomTest.connections.Length; i++)
                {
                    if (roomTest.connections[i].connection.name == "Sud")
                    {
                        roomTest.connections[i].connectionCompleted = true;
                        connectionNeeded.Add("Nord");
                    }
                }
            }
            //Est
            if (manager.cells[roomOld.indexX + 1, roomOld.indexY + 1] > 0)
            {
                foreach (Room room in manager.createdRooms)
                {
                    if (room.indexX == roomOld.indexX + 1 && room.indexY == roomOld.indexY + 1)
                    {
                        roomTest = room;
                        break;
                    }
                }
                for (int i = 0; i < roomTest.connections.Length; i++)
                {
                    if (roomTest.connections[i].connection.name == "Ovest")
                    {
                        roomTest.connections[i].connectionCompleted = true;
                        connectionNeeded.Add("Est");
                    }
                }
            }
            //Ovest
            if (manager.cells[roomOld.indexX - 1, roomOld.indexY + 1] > 0)
            {
                foreach (Room room in manager.createdRooms)
                {
                    if (room.indexX == roomOld.indexX - 1 && room.indexY == roomOld.indexY + 1)
                    {
                        roomTest = room;
                        break;
                    }
                }
                for (int i = 0; i < roomTest.connections.Length; i++)
                {
                    if (roomTest.connections[i].connection.name == "Est")
                    {
                        roomTest.connections[i].connectionCompleted = true;
                        connectionNeeded.Add("Ovest");
                    }
                }
            }


            return connectionNeeded;
        }

        public List<string> FindSud(Room roomOld)
        {
            //Nord
            if (manager.cells[roomOld.indexX, roomOld.indexY - 2] > 0)
            {
                foreach (Room room in manager.createdRooms)
                {
                    if (room.indexX == roomOld.indexX && room.indexY == roomOld.indexY - 2)
                    {
                        roomTest = room;
                        break;
                    }
                }
                for (int i = 0; i < roomTest.connections.Length; i++)
                {
                    if (roomTest.connections[i].connection.name == "Nord")
                    {
                        roomTest.connections[i].connectionCompleted = true;
                        connectionNeeded.Add("Sud");
                    }
                }
            }
            //Est
            if (manager.cells[roomOld.indexX + 1, roomOld.indexY - 1] > 0)
            {
                foreach (Room room in manager.createdRooms)
                {
                    if (room.indexX == roomOld.indexX + 1 && room.indexY == roomOld.indexY - 1)
                    {
                        roomTest = room;
                        break;
                    }
                }
                for (int i = 0; i < roomTest.connections.Length; i++)
                {
                    if (roomTest.connections[i].connection.name == "Ovest")
                    {
                        roomTest.connections[i].connectionCompleted = true;
                        connectionNeeded.Add("Est");
                    }
                }
            }
            //Ovest
            if (manager.cells[roomOld.indexX - 1, roomOld.indexY - 1] > 0)
            {
                foreach (Room room in manager.createdRooms)
                {
                    if (room.indexX == roomOld.indexX - 1 && room.indexY == roomOld.indexY - 1)
                    {
                        roomTest = room;
                        break;
                    }
                }
                for (int i = 0; i < roomTest.connections.Length; i++)
                {
                    if (roomTest.connections[i].connection.name == "Est")
                    {
                        roomTest.connections[i].connectionCompleted = true;
                        connectionNeeded.Add("Ovest");
                    }
                }
            }


            return connectionNeeded;
        }

        public void Clear()
        {
            connectionNeeded.Clear();
        }
     */
    }
}
