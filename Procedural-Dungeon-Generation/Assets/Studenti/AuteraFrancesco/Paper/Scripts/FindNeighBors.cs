using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace paper {
    public class FindNeighBors : MonoBehaviour
    {

        public DungeonManager manager;
        public List<string> connectionNeeded=new List<string>();
        public List<string> connectionBanned = new List<string>();

       public List<string> FIndEst( Room roomOld) {
            //Est
           
            if (manager.cells[roomOld.indexX+2,roomOld.indexY]!=null) {
                ;
                for (int i = 0; i < manager.cells[roomOld.indexX + 2, roomOld.indexY].connections.Count; i++)
                {
                    if (manager.cells[roomOld.indexX + 2, roomOld.indexY].connections[i].connection.name == "Ovest")
                    {
                        manager.cells[roomOld.indexX + 2, roomOld.indexY].connections[i].isIstanciate = true;
                        connectionNeeded.Add("Est");
                        break;
                    }
                    
                }
                if (!connectionNeeded.Contains("Est")) {
                    connectionBanned.Add("Est");
                }
                
            }
            //Nord
            
            if (manager.cells[roomOld.indexX + 1, roomOld.indexY-1] != null)
            {
                
                for (int i = 0; i < manager.cells[roomOld.indexX + 1, roomOld.indexY - 1].connections.Count; i++)
                {
                    if (manager.cells[roomOld.indexX + 1, roomOld.indexY - 1].connections[i].connection.name == "Sud")
                    {
                        manager.cells[roomOld.indexX + 1, roomOld.indexY - 1].connections[i].isIstanciate = true;
                        connectionNeeded.Add("Nord");
                        break;
                    }
                    
                }
                if (!connectionNeeded.Contains("Nord"))
                {
                    connectionBanned.Add("Nord");
                }
            }
            //Sud
            
            if (manager.cells[roomOld.indexX + 1, roomOld.indexY+1] != null)
            {
               
                for (int i = 0; i < manager.cells[roomOld.indexX + 1, roomOld.indexY + 1].connections.Count; i++)
                {
                    if (manager.cells[roomOld.indexX + 1, roomOld.indexY + 1].connections[i].connection.name == "Nord")
                    {
                        manager.cells[roomOld.indexX + 1, roomOld.indexY + 1].connections[i].isIstanciate = true;
                        connectionNeeded.Add("Sud");
                        break;
                    }
                  
                }
                if (!connectionNeeded.Contains("Sud"))
                {
                    connectionBanned.Add("Sud");
                }
            }


            return connectionNeeded;
        }
       public List<string> FindOvest(Room roomOld)
        {
            //Ovest
            if (manager.cells[roomOld.indexX - 2, roomOld.indexY] != null)
            {
                for (int i = 0; i < manager.cells[roomOld.indexX - 2, roomOld.indexY].connections.Count; i++)
                {
                    if (manager.cells[roomOld.indexX - 2, roomOld.indexY].connections[i].connection.name == "Est")
                    {
                        manager.cells[roomOld.indexX - 2, roomOld.indexY].connections[i].isIstanciate = true;
                        connectionNeeded.Add("Ovest");
                        break;
                    }
                    
                }
                if (!connectionNeeded.Contains("Ovest"))
                {
                    connectionBanned.Add("Ovest");
                }
            }
            //Nord
            if (manager.cells[roomOld.indexX - 1, roomOld.indexY - 1] != null)
            {
                for (int i = 0; i < manager.cells[roomOld.indexX - 1, roomOld.indexY - 1].connections.Count; i++)
                {
                    if (manager.cells[roomOld.indexX - 1, roomOld.indexY - 1].connections[i].connection.name == "Sud")
                    {
                        manager.cells[roomOld.indexX - 1, roomOld.indexY - 1].connections[i].isIstanciate = true;
                        connectionNeeded.Add("Nord");
                        break;
                    }
                   
                }
                if (!connectionNeeded.Contains("Nord"))
                {
                    connectionBanned.Add("Nord");
                }
            }
            //Sud
            if (manager.cells[roomOld.indexX - 1, roomOld.indexY + 1] != null)
            {
                for (int i = 0; i < manager.cells[roomOld.indexX - 1, roomOld.indexY + 1].connections.Count; i++)
                {
                    if (manager.cells[roomOld.indexX - 1, roomOld.indexY + 1].connections[i].connection.name == "Nord")
                    {
                        manager.cells[roomOld.indexX - 1, roomOld.indexY + 1].connections[i].isIstanciate = true;
                        connectionNeeded.Add("Sud");
                        break;
                    }
                    
                }
                if (!connectionNeeded.Contains("Sud"))
                {
                    connectionBanned.Add("Sud");
                }
            }


            return connectionNeeded;



        }
        public List<string> FindSud(Room roomOld)
        {
            //Sud
            if (manager.cells[roomOld.indexX, roomOld.indexY+2] != null)
            {
                for (int i = 0; i < manager.cells[roomOld.indexX, roomOld.indexY + 2].connections.Count; i++)
                {
                    if (manager.cells[roomOld.indexX, roomOld.indexY + 2].connections[i].connection.name == "Nord")
                    {
                        manager.cells[roomOld.indexX, roomOld.indexY + 2].connections[i].isIstanciate = true;
                        connectionNeeded.Add("Sud");
                    }
                    
                }

                if (!connectionNeeded.Contains("Sud"))
                {
                    connectionBanned.Add("Sud");
                }
            }
            //Est
            if (manager.cells[roomOld.indexX + 1, roomOld.indexY + 1] != null)
            {
                for (int i = 0; i < manager.cells[roomOld.indexX + 1, roomOld.indexY + 1].connections.Count; i++)
                {
                    if (manager.cells[roomOld.indexX + 1, roomOld.indexY + 1].connections[i].connection.name == "Ovest")
                    {
                        manager.cells[roomOld.indexX + 1, roomOld.indexY + 1].connections[i].isIstanciate = true;
                        connectionNeeded.Add("Est");
                        break;
                    }
                   
                }
                if (!connectionNeeded.Contains("Est"))
                {
                    connectionBanned.Add("Est");
                }
            }
            //Ovest
            if (manager.cells[roomOld.indexX - 1, roomOld.indexY + 1] != null)
            {
                for (int i = 0; i < manager.cells[roomOld.indexX - 1, roomOld.indexY + 1].connections.Count; i++)
                {
                    if (manager.cells[roomOld.indexX - 1, roomOld.indexY + 1].connections[i].connection.name == "Est")
                    {
                        manager.cells[roomOld.indexX - 1, roomOld.indexY + 1].connections[i].isIstanciate = true;
                        connectionNeeded.Add("Ovest");
                        break;
                    }
                    
                }
                if (!connectionNeeded.Contains("Ovest"))
                {
                    connectionBanned.Add("Ovest");
                }
            }


            return connectionNeeded;
        }
        public List<string> FindNord(Room roomOld)
        {
            //Sud
            if (manager.cells[roomOld.indexX, roomOld.indexY - 2] != null)
            {
                for (int i = 0; i < manager.cells[roomOld.indexX, roomOld.indexY - 2].connections.Count; i++)
                {
                    if (manager.cells[roomOld.indexX, roomOld.indexY - 2].connections[i].connection.name == "Sud")
                    {
                        manager.cells[roomOld.indexX, roomOld.indexY - 2].connections[i].isIstanciate = true;
                        connectionNeeded.Add("Nord");
                        break;
                    }
                   
                }
                if (!connectionNeeded.Contains("Nord"))
                {
                    connectionBanned.Add("Nord");
                }
            }
            //Est
            if (manager.cells[roomOld.indexX + 1, roomOld.indexY - 1] != null)
            {
                for (int i = 0; i < manager.cells[roomOld.indexX + 1, roomOld.indexY - 1].connections.Count; i++)
                {
                    if (manager.cells[roomOld.indexX + 1, roomOld.indexY - 1].connections[i].connection.name == "Ovest")
                    {
                        manager.cells[roomOld.indexX + 1, roomOld.indexY - 1].connections[i].isIstanciate = true;
                        connectionNeeded.Add("Est");
                        break;
                    }
                  
                }
                if (!connectionNeeded.Contains("Est"))
                {
                    connectionBanned.Add("Est");
                }
            }
            //Ovest
            if (manager.cells[roomOld.indexX - 1, roomOld.indexY - 1] != null)
            {
                for (int i = 0; i < manager.cells[roomOld.indexX - 1, roomOld.indexY - 1].connections.Count; i++)
                {
                    if (manager.cells[roomOld.indexX - 1, roomOld.indexY - 1].connections[i].connection.name == "Est")
                    {
                        manager.cells[roomOld.indexX - 1, roomOld.indexY - 1].connections[i].isIstanciate = true;
                        connectionNeeded.Add("Ovest");
                    }
                   
                }
                if (!connectionNeeded.Contains("Ovest"))
                {
                    connectionBanned.Add("Ovest");
                }
            }


            return connectionNeeded;
        }
        public void Clear() {
            connectionNeeded.Clear();
            connectionBanned.Clear();
        }


        public void CheckNeighBour(Room room) {
            if (manager.cells[room.indexX + 1, room.indexY] != null) {
                Room r = manager.cells[room.indexX + 1, room.indexY];
                foreach (ConnectionPoints connection in r.connections) {
                    if (connection.connection.name == "Ovest") {
                        connectionNeeded.Add("Est");
                        break;
                    }
                }
                if (!connectionNeeded.Contains("Est")) {
                    connectionBanned.Add("Est");
                }
            }
            else
            {

                connectionNeeded.Add("Est");
            }

            if (manager.cells[room.indexX - 1, room.indexY] != null)
            {
                Room r = manager.cells[room.indexX - 1, room.indexY];
                foreach (ConnectionPoints connection in r.connections)
                {
                    if (connection.connection.name == "Est")
                    {
                        connectionNeeded.Add("Ovest");
                        break;
                    }
                }
                if (!connectionNeeded.Contains("Ovest"))
                {
                    connectionBanned.Add("Ovest");
                }
            }
            else
            {

                connectionNeeded.Add("Ovest");
            }
            if (manager.cells[room.indexX, room.indexY-1] != null)
            {
                Room r = manager.cells[room.indexX, room.indexY-1];
                foreach (ConnectionPoints connection in r.connections)
                {
                    if (connection.connection.name == "Sud")
                    {
                        connectionNeeded.Add("Nord");
                        break;
                    }
                }
                if (!connectionNeeded.Contains("Nord"))
                {
                    connectionBanned.Add("Nord");
                }
            }
            else
            {

                connectionNeeded.Add("Nord");
            }
            if (manager.cells[room.indexX, room.indexY +1] != null)
            {
                Room r = manager.cells[room.indexX, room.indexY + 1];
                foreach (ConnectionPoints connection in r.connections)
                {
                    if (connection.connection.name == "Nord")
                    {
                        connectionNeeded.Add("Sud");
                        break;
                    }
                }
                if (!connectionNeeded.Contains("Sud"))
                {
                    connectionBanned.Add("Sud");
                }
            }
            else
            {

                connectionNeeded.Add("Sud");
            }

        }

    }
}
