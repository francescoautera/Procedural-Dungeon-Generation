using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.SceneManagement;

namespace paper
{
    public class DungeonManager : MonoBehaviour
    {
        public List<Room> createdRooms = new List<Room>();
        public SO_RoomRules rules;
        public List<Room> validateRoom = new List<Room>();
        public FindNeighBors neighBors;
        [SerializeField] List<Room> trueValidateRoom = new List<Room>();
        public List<HallWay> hallWays=new List<HallWay>();
        List<Room> finalValidateRoom = new List<Room>();
        Room instanciate;
        public GameObject Hallway;
        public GameObject InvertedHallway;
        public int index;
        [SerializeField] int counter = 0;
        [SerializeField] float delayCreateRoom;
        private bool isDone = false;
        public const int maxX = 100;
        public const int maxY = 100;
        public Material lastRoom;
        public Room[,] cells = new Room[maxX, maxY];
        [SerializeField] float timer;
        public GameObject player;
        GameObject play;
        public bool useRange;
        public bool maxRoom;
     
        public ResizeManager ChangeRooms;
        public Transform roomParent;
        public Transform hallwayParent;
        [SerializeField]PlayerMovement _playerMovement;
        UIManager managerUi;
        int rangeRoom;
        bool finishDungeon;
        [SerializeField] float firstProb;
        [SerializeField] float secondProb;
        [SerializeField] int maxRange;
        
       




        private void Start()
        {
            index = 0;
            managerUi = FindObjectOfType<UIManager>();
            var instanciate = Instantiate(rules.rooms[Random.Range(rules.rooms.Length/3, rules.rooms.Length)], Vector3.zero, Quaternion.identity, roomParent);
            createdRooms.Add(instanciate);
            instanciate.firstRoom = true;
            instanciate.indexX = maxX / 2;
            instanciate.indexY = maxY / 2;
            cells[maxX / 2, maxY / 2] = instanciate;
            var pos = new Vector3(cells[maxX / 2, maxY / 2].center.transform.position.x, cells[maxX / 2, maxY / 2].center.transform.position.y + 0.5f, cells[maxX / 2, maxY / 2].center.transform.position.z);
            play = Instantiate(player, pos, Quaternion.identity);
            _playerMovement = play.GetComponent<PlayerMovement>();
            instanciate.player = _playerMovement;
            finishDungeon = false;

        }

        [System.Obsolete]
        private void Update()
        {
            if (maxRoom)
            {
                if (createdRooms.Count < rules.maxRoom)
                {
                   
                    if ((createdRooms.Count +createdRooms[index].connections.Count)>=rules.maxRoom && createdRooms[index].connections.Count > 1)
                    {
                        Debug.Log("ultima Stanza");
                        InstanceLastRoom();
                    }
                    else
                    {
                        CreateDungeon();
                    }
                }
                else
                {
                    if (createdRooms.Count == rules.maxRoom + 1) {
                        createdRooms[createdRooms.Count - 1].gameObject.SetActive(false);
                        createdRooms.RemoveAt(createdRooms.Count - 1);
                    }
                    if (!finishDungeon)
                    {
                        finishDungeon = true;
                        StartCoroutine(WaitBeforeResize());
                    }

                }
            }

            if (useRange)
            {
                if (index < createdRooms.Count && createdRooms.Count < (2 * rules.roomRange) + 1)
                {
                    
                    CreateDungeon();
                }
                else
                {
                    if (createdRooms.Count >= (2 * rules.roomRange) + 1)
                    {
                        if (!ChangeRooms.isDone)
                        {
                            ChangeRooms.isDone = true;
                            StartCoroutine(WaitBeforeResize());
                            index = createdRooms.Count - 1;
                        }

                    }
                    else
                    {
                        if (index >= createdRooms.Count - 1 && !ChangeRooms.isDone)
                        {
                            ChangeRooms.isDone = true;
                            StartCoroutine(waitBeforeBake());

                        }
                    }
                }
            }


        }

       



       
      

       
        public void CompleteHallway(Connection connection, List<Room> rooms)
        {
            var room = cells[rooms[index].indexX, rooms[index].indexY];
            if (connection.name == "Est")
            {
                room = cells[rooms[index].indexX + 1, rooms[index].indexY];
            }
            if (connection.name == "Ovest")
            {
                room = cells[rooms[index].indexX - 1, rooms[index].indexY];
            }
            if (connection.name == "Nord")
            {
                room = cells[rooms[index].indexX, rooms[index].indexY - 1];
            }
            if (connection.name == "Sud")
            {
                room = cells[rooms[index].indexX, rooms[index].indexY + 1];
            }
            foreach (ConnectionPoints connectionPoints in room.connections)
            {
                if (connectionPoints.connection.isOpposite(connection)) { connectionPoints.AddHallway = true; }
            }
        }


        //====CHECK ROOM FOR INSTANCE===
        private void AddRoom(ConnectionPoints connection)
        {
            foreach (Room room in rules.rooms)
            {
                if (rules.GetRoom(connection.connection, room))
                {
                    validateRoom.Add(room);
                }

            }



        }

        //check if the rooms have connections required
        public bool ValidatesRoom(Room room, Connection con)
        {
            if (room.connections.Count != neighBors.connectionNeeded.Count + 1)
            {
                return false;
            }
            foreach (ConnectionPoints connectionPoints in room.connections)
            {
                if (connectionPoints.connection.isOpposite(con))
                {
                    continue;
                }
                if (!neighBors.connectionNeeded.Contains(connectionPoints.connection.name))
                {
                    return false;
                }

            }
            return true;

        }
        public bool ValidatesRoomforChangeRoom(Room room, Connection con)
        {
            if (room.connections.Count != neighBors.connectionNeeded.Count)
            {
                return false;
            }
            foreach (ConnectionPoints connectionPoints in room.connections)
            {
                if (connectionPoints.connection.isOpposite(con))
                {
                    continue;
                }
                if (!neighBors.connectionNeeded.Contains(connectionPoints.connection.name))
                {
                    return false;
                }

            }
            return true;

        }
        void ResizeList() {
            foreach (Room room in rules.rooms)
            {
                foreach (ConnectionPoints connection in room.connections) {
                    if (ValidatesRoomforChangeRoom(room,connection.connection))
                    {
                        validateRoom.Add(room);
                    }
                }

            }
        }
        void TrueResizeList() {
            foreach (Room room in validateRoom)
            {
                if (BannedRooms(room))
                {

                    trueValidateRoom.Add(room);
                }
            }
        }
        public bool BannedRoomsforChangeRoom(Room room)
        {

            foreach (ConnectionPoints connection in room.connections)
            {
                if (neighBors.connectionBanned.Contains(connection.connection.name))
                {
                    return false;
                }

            }
            return true;

        }


        //check if the rooms have connections banned
        public bool BannedRooms(Room room)
        {

            foreach (ConnectionPoints connection in room.connections)
            {
                if (neighBors.connectionBanned.Contains(connection.connection.name))
                {
                    return false;
                }

            }
            return true;

        }
        //add room that have all the connections required
        private void ValidateRoom(Connection connection)
        {

            foreach (Room room in validateRoom)
            {

                if (ValidatesRoom(room, connection))
                {

                    trueValidateRoom.Add(room);
                }
            }


        }

        // add room that not have the connection banneds
        private void BannedRoom()
        {
            foreach (Room room in trueValidateRoom)
            {
                if (BannedRooms(room))
                {

                    finalValidateRoom.Add(room);
                }
            }
        }

        //=======INSTANCE ROOM & HALLWAY====
        private void Instanciate(ConnectionPoints connection, int max)
        {
            instanciate = Instantiate(finalValidateRoom[Random.Range(0, max)], createdRooms[index].checkPosforRoom(connection, createdRooms[index]), Quaternion.identity, roomParent);

            AddMatrix(connection.connection, createdRooms[index], instanciate);
            cells[instanciate.indexX, instanciate.indexY] = instanciate;
            InstanciateHallway(connection);
            connection.AddHallway = true;
            createdRooms.Add(instanciate);
            connection.isIstanciate = true;
            connection.connectionCompleted = true;
            isDone = true;

            if (neighBors.connectionNeeded.Count > 0)
            {
                foreach (ConnectionPoints conn in instanciate.connections)
                {

                    if (neighBors.connectionNeeded.Contains(conn.connection.name))
                    {
                        conn.isIstanciate = true;

                    }

                }
            }

            foreach (ConnectionPoints conn in instanciate.connections)
            {
                if (conn.connection.isOpposite(connection.connection))
                {
                    conn.isIstanciate = true;
                    conn.AddHallway = true;
                }

            }

          




        }
        //instance Hallway based on connection position
        public void InstanciateHallway(ConnectionPoints connection)
        {
            if (connection.connection.name == "Est" || connection.connection.name == "Ovest")
            {
                GameObject inst = Instantiate(Hallway, createdRooms[index].CheckPosforHallway(connection, createdRooms[index]), Quaternion.identity, hallwayParent);
                inst.transform.Rotate(0, 90, 0);
                inst.GetComponent<HallWay>().player = _playerMovement;
                hallWays.Add(inst.GetComponent<HallWay>());
                


            }
            else if (connection.connection.name == "Nord" || connection.connection.name == "Sud")
            {

                GameObject insta = Instantiate(Hallway, createdRooms[index].CheckPosforHallway(connection, createdRooms[index]), Quaternion.identity, hallwayParent);
                insta.GetComponent<HallWay>().player = _playerMovement;
                hallWays.Add(insta.GetComponent<HallWay>());
                

            }



        }
        
        //finish the connection;
        void TerminateConnection()
        {

            createdRooms[index].roomInstanciate++;
            isDone = false;
            validateRoom.Clear();
            trueValidateRoom.Clear();
            finalValidateRoom.Clear();
            neighBors.Clear();


        }

        //add Room in matrix depends on it's creator
        void AddMatrix(Connection c, Room oldRoom, Room newRoom)
        {
            if (c.name == "Est")
            {
                newRoom.indexX = oldRoom.indexX + 1;
                newRoom.indexY = oldRoom.indexY;
            }
            if (c.name == "Ovest")
            {
                newRoom.indexX = oldRoom.indexX - 1;
                newRoom.indexY = oldRoom.indexY;
            }
            if (c.name == "Nord")
            {
                newRoom.indexX = oldRoom.indexX;
                newRoom.indexY = oldRoom.indexY - 1;
            }
            if (c.name == "Sud")
            {
                newRoom.indexX = oldRoom.indexX;
                newRoom.indexY = oldRoom.indexY + 1;
            }
        }

        //====DUNGEON CREATION===
        //set the creation mode
        public void SetMaxRoom() { maxRoom = true; useRange = false; 
                                   managerUi.maxRoomButton.SetActive(false);
                                   managerUi.roomRangeButton.SetActive(false);
            managerUi.maxRoom.enabled = false;
            managerUi.roomRange.enabled = false;
        }
        public void SetUseRange() { 
            useRange = true;
            maxRoom = false;
            managerUi.maxRoomButton.SetActive(false);
            managerUi.roomRangeButton.SetActive(false);
            managerUi.maxRoom.enabled = false;
            managerUi.roomRange.enabled = false;
            rangeRoom = rules.roomRange + Random.Range(0, (int)rules.roomRange / 4);

        }

        //create the dungeon
        public void CreateDungeon()
        {
            foreach (ConnectionPoints connection in createdRooms[index].connections)
            {

                if (connection.isIstanciate && !connection.connectionCompleted)
                {
                    if (!connection.AddHallway)
                    {
                        InstanciateHallway(connection);
                        CompleteHallway(connection.connection, createdRooms);

                    }
                    createdRooms[index].roomInstanciate++;
                    connection.connectionCompleted = true;
                    continue;
                }




                if (!isDone)
                {


                    AddRoom(connection);


                    if (connection.connection.name == "Est")
                    {

                        neighBors.FIndEst(createdRooms[index]);
                    }
                    if (connection.connection.name == "Ovest")
                    {

                        neighBors.FindOvest(createdRooms[index]);
                    }
                    if (connection.connection.name == "Sud")
                    {

                        neighBors.FindSud(createdRooms[index]);
                    }
                    if (connection.connection.name == "Nord")
                    {

                        neighBors.FindNord(createdRooms[index]);
                    }


                    if (neighBors.connectionNeeded.Count == 0 && neighBors.connectionBanned.Count == 0)
                    {
                        finalValidateRoom.Clear();
                        finalValidateRoom.AddRange(validateRoom);
                        //Instanciate(connection);
                    }
                    else
                    {
                        if (neighBors.connectionNeeded.Count > 0)
                        {
                            ValidateRoom(connection.connection);

                        }
                        else if (neighBors.connectionNeeded.Count == 0)
                        {
                            trueValidateRoom.Clear();
                            trueValidateRoom.AddRange(validateRoom);

                        }

                        if (neighBors.connectionBanned.Count > 0)
                        {
                            BannedRoom();
                            //Instanciate(connection);

                        }
                        else if (neighBors.connectionBanned.Count == 0)
                        {
                            finalValidateRoom.Clear();
                            finalValidateRoom.AddRange(trueValidateRoom);
                            // Instanciate(connection);
                        }

                    }

                    if (useRange)
                    {

                        if (createdRooms.Count < rangeRoom)
                        {
                            finalValidateRoom.Reverse();
                            
                        }
                        Instanciate(connection, finalValidateRoom.Count / 2);



                    }
                    else
                    {
                        if (createdRooms.Count < rules.maxRoom)
                         {
                             var x = Random.Range(0, maxRange);
                             if (x < firstProb*maxRange)
                             {

                                 finalValidateRoom.Reverse();
                             }

                         }
                         else {
                             var x = Random.Range(0, maxRange);
                             if (x < secondProb*maxRange)
                             {

                                 finalValidateRoom.Reverse();
                             }
                         }
                        
                        Instanciate(connection, finalValidateRoom.Count/2);
                    }
                    TerminateConnection();

                }




            }




            if (createdRooms[index].completedRoomsCreation = createdRooms[index].CheckCreationRoom())
            {

                index++;
               

            }
            if (index >= createdRooms.Count && maxRoom)
            {
                index = createdRooms.Count - 1;
                if (createdRooms.Count < rules.maxRoom)
                {
                    ReplaceRoom();
                    //ChangeRoom(index);
                }
            }


        }
        //created lastRoom
        void InstanceLastRoom() {
            foreach (ConnectionPoints connection in createdRooms[index].connections)
            {
                if (connection.isIstanciate )
                {
                    if (!connection.AddHallway)
                    {
                        InstanciateHallway(connection);
                        CompleteHallway(connection.connection, createdRooms);

                    }
                    createdRooms[index].roomInstanciate++;
                    
                    continue;
                }

                if (!connection.isIstanciate && createdRooms[index].lastConnection) { continue; }

                if (!connection.isIstanciate && !createdRooms[index].lastConnection)
                {
                    createdRooms[index].lastConnection = true;
                    AddRoom(connection);


                    if (connection.connection.name == "Est")
                    {

                        neighBors.FIndEst(createdRooms[index]);
                    }
                    if (connection.connection.name == "Ovest")
                    {

                        neighBors.FindOvest(createdRooms[index]);
                    }
                    if (connection.connection.name == "Sud")
                    {

                        neighBors.FindSud(createdRooms[index]);
                    }
                    if (connection.connection.name == "Nord")
                    {

                        neighBors.FindNord(createdRooms[index]);
                    }


                    if (neighBors.connectionNeeded.Count == 0 && neighBors.connectionBanned.Count == 0)
                    {
                        finalValidateRoom.Clear();
                        finalValidateRoom.AddRange(validateRoom);
                        //Instanciate(connection);
                    }
                    else
                    {
                        if (neighBors.connectionNeeded.Count > 0)
                        {
                            ValidateRoom(connection.connection);

                        }
                        else if (neighBors.connectionNeeded.Count == 0)
                        {
                            trueValidateRoom.Clear();
                            trueValidateRoom.AddRange(validateRoom);

                        }

                        if (neighBors.connectionBanned.Count > 0)
                        {
                            BannedRoom();
                            //Instanciate(connection);

                        }
                        else if (neighBors.connectionBanned.Count == 0)
                        {
                            finalValidateRoom.Clear();
                            finalValidateRoom.AddRange(trueValidateRoom);
                            // Instanciate(connection);
                        }
                        
                    }
                    Instanciate(connection, finalValidateRoom.Count);
                    validateRoom.Clear();
                    trueValidateRoom.Clear();
                    finalValidateRoom.Clear();
                    neighBors.Clear();
                   

                }
                   
            }
            if (createdRooms[index].roomInstanciate > createdRooms[index].connections.Count)
            {
                index++;
                
            }

            if (createdRooms.Count<rules.maxRoom) {
                createdRooms[index].lastConnection = false;
            }

            
            
        }
        //replace a room  with 4 exit room
        public void ChangeRoom(int index)
        {
            Room instanciate=null;
            Debug.Log("Allora funziona");
            validateRoom.Clear();
            trueValidateRoom.Clear();
            neighBors.CheckNeighBour(createdRooms[index]);
            if (neighBors.connectionNeeded.Count == 0 && neighBors.connectionBanned.Count == 0)
            {
                instanciate = Instantiate(rules.rooms[rules.rooms.Length - 1], createdRooms[index].transform.position, Quaternion.identity, roomParent);
            }
            else {
                if (neighBors.connectionNeeded.Count > 0)
                {

                    ResizeList();
                }
                else {
                    validateRoom.AddRange(rules.rooms);
                }
                if (neighBors.connectionBanned.Count > 0)
                {
                    TrueResizeList();
                }
                else {
                    trueValidateRoom.AddRange(validateRoom);
                }
                instanciate = Instantiate(trueValidateRoom[trueValidateRoom.Count - 1], createdRooms[index].transform.position, Quaternion.identity, roomParent);
            }

            instanciate.indexX = createdRooms[index].indexX;
            instanciate.indexY = createdRooms[index].indexY;
            cells[instanciate.indexX, instanciate.indexY] = instanciate;
            List<string> connectionInstanciated = new List<string>();
            foreach (ConnectionPoints connection in createdRooms[index].connections)
            {
                if (connection.isIstanciate)
                {
                    connectionInstanciated.Add(connection.connection.name);
                }
            }
            foreach (ConnectionPoints connectionPoints in instanciate.connections)
            {

                if (connectionInstanciated.Contains(connectionPoints.connection.name))
                {
                    connectionPoints.isIstanciate = true;
                    connectionPoints.AddHallway = true;
                    
                }
            }
            createdRooms[index].gameObject.SetActive(false);
            createdRooms[index] = instanciate;
            validateRoom.Clear();
            trueValidateRoom.Clear();
            neighBors.Clear();

        }

        void ReplaceRoom() {

            
            Debug.Log("funziona");
            var indx = 0;
            for (int i = 0; i < createdRooms.Count; i++) {
                if (CountNeighBors(createdRooms[i])) {
                   
                    indx = i;
                    break;
                }
            }
            var instanciate = Instantiate(rules.rooms[rules.rooms.Length - 1], createdRooms[indx].transform.position, Quaternion.identity, roomParent);
            instanciate.indexX = createdRooms[indx].indexX;
            instanciate.indexY = createdRooms[indx].indexY;
            createdRooms.Add(instanciate);
           

            for (int i = 0; i < instanciate.connections.Count; i++) {
                for (int j = 0; j < createdRooms[indx].connections.Count; j++) {
                    if (instanciate.connections[i].connection.name == createdRooms[indx].connections[j].connection.name) {
                        instanciate.connections[i].isIstanciate = true;
                        instanciate.connections[i].AddHallway = true;
                        break;
                    }
                }
            
            }

            createdRooms[indx].gameObject.SetActive(false);

        }

        bool CountNeighBors(Room room) {

            if (room.connections.Count > 1) { return false; }

            int count=0;
            if (cells[room.indexX + 1, room.indexY] != null)
            {
                count++;
            }
            if (cells[room.indexX - 1, room.indexY] != null)
            {
                count++;
            }
            if (cells[room.indexX, room.indexY+1] != null)
            {
                count++;
            }
            if (cells[room.indexX, room.indexY-1] != null)
            {
                count++;
            }

            if (count > 1)
            {
                return false;
            }
            else {
                return true;
            }
        }
            
        
        //====COROUTINE===
        IEnumerator waitBeforeBake()
        {
            
            _playerMovement.rotationVelocity = 80;
            yield return new WaitForSeconds(2.5f);
           
            rules.AddElementinFinalRoom(createdRooms[createdRooms.Count - 1]);
            
            
            
            
        }

        [System.Obsolete]
        IEnumerator WaitBeforeResize()
        {
            _playerMovement.rotationVelocity = 80;
            yield return new WaitForSeconds(0.5f);
            ChangeRooms.ResizeRoom(_playerMovement);
            
           
           

        }


    }



}