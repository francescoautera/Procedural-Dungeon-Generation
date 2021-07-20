using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace paper {
    public class NeighBorManager : MonoBehaviour
    {

        public DungeonManager manager;

        public List<string> connectionNeeded=new List<string>();
        public List<string> connectionBanned = new List<string>();

        //check the  Neighbors' new Room
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


       

    }
}
