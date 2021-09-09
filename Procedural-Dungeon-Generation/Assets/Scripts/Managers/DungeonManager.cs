using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace paper
{
    public class DungeonManager : MonoBehaviour
    {
        //====List===
        public List<Room> createdRooms = new List<Room>(); // list of created rooms
        List<Room> validateRoom = new List<Room>(); // list of room with opposite connections
        List<Room> trueValidateRoom = new List<Room>(); // list of room that pass the open needed check
        List<Room> finalValidateRoom = new List<Room>(); // list of room that pass the open blocked check
        public List<HallWay> hallWays = new List<HallWay>(); // list of created Hallways
        public Room[,] cells = new Room[maxX, maxY]; //  Rooms matrix

        //==References==
        public SO_RoomRules rules; // rules of dungeon 
        public ResizeManager ChangeRooms;
        public NeighBorManager neighBors;
        UIManager managerUi;
        Room instanciate; // current instaciated room 
        public GameObject prefabHallway;
        public GameObject prefabPlayer;
        public Transform roomParent;
        public Transform hallwayParent;
        [SerializeField] PlayerMovement _playerMovement;

        //==Variables==
        public int index; // index of createdRooms 
        const int maxX = 100;// max  number of matrix row 
        const int maxY = 100; // max number of matrix columns 
        int rangeRoom;// number after that change behaviur of Room choice
        int maxBoundRange=10; // bounds for range
        //number that divided rules RangeRoom number; this is a shift of rangeRoom from rules RangeRoom number
        int divisorRangeRoom=3;
        int tmp;
        int divisorBoundInstanciateList;//number that limit the sup Bound of finalValidateRoom
        [SerializeField] float firstProb; // probability of choose room with 3 o more opens
        [SerializeField] float secondProb; // probability of choose room with 3 o more opens after maxRange
        public bool useRange; // check if using range Creation
        public bool maxRoom; // check if using  fixed Creation
        [SerializeField] bool randomCreation; // check if you want a creation more flexible
        private bool isDone = false;
        bool finishDungeon;//check if number of rooms=  rules'room number
        

        private void Start()
        {
            index = 0;
            managerUi = FindObjectOfType<UIManager>();
            //instance the first Room
            var instanciate = Instantiate(rules.rooms[Random.Range(rules.rooms.Length / 3, rules.rooms.Length)], Vector3.zero, Quaternion.identity, roomParent);
            createdRooms.Add(instanciate);
            //insert the room in the middle of matrix
            instanciate.indexX = maxX / 2;
            instanciate.indexY = maxY / 2;
            cells[maxX / 2, maxY / 2] = instanciate;
            //instance Player
            var pos = new Vector3(cells[maxX / 2, maxY / 2].center.transform.position.x, cells[maxX / 2, maxY / 2].center.transform.position.y + 0.5f, cells[maxX / 2, maxY / 2].center.transform.position.z);
            var play = Instantiate(prefabPlayer, pos, Quaternion.identity);
            _playerMovement = play.GetComponent<PlayerMovement>();
            instanciate.player = _playerMovement;
            finishDungeon = false;
            tmp = divisorBoundInstanciateList;

        }

        [System.Obsolete]
        private void Update()
        {
            if (randomCreation)
            {
                
                divisorBoundInstanciateList = 1;
                
            }
            else {
                divisorBoundInstanciateList = tmp;
            }
            //check if creation is fixed or variable
            if (maxRoom)
            {
                //check if room number reached goal room number
                if (createdRooms.Count < rules.maxRoom)
                {
                    //case of room number - rules.maxRooms<2;we need that the geneted room number 
                    //don't pass the maxRoom 
                    if ((createdRooms.Count + createdRooms[index].connections.Count) >= rules.maxRoom && createdRooms[index].connections.Count > 1)
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
                    //eliminate exceeded Room
                    if (createdRooms.Count == rules.maxRoom + 1)
                    {
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
                //check if index reached the end of list or if room number reached 2 * room goal number +1  
                if (index < createdRooms.Count && createdRooms.Count < (2 * rules.roomRange) + 1)
                {

                    CreateDungeon();
                }
                else
                {
                    //case reached the goal number
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
                        //case index reached the end of matrix
                        if (index >= createdRooms.Count - 1 && !ChangeRooms.isDone)
                        {
                            ChangeRooms.isDone = true;
                            StartCoroutine(waitBeforeBake());

                        }
                    }
                }
            }


        }

        //create the dungeon
        public void CreateDungeon()
        {
            
            foreach (ConnectionPoints connection in createdRooms[index].connections)
            {
                //check if this connection has instanciated already
                if (connection.isIstanciate && !connection.connectionCompleted)
                {
                    //check if this connection has instanciated Hallway already
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


                    AddRoom(connection);//Insert Room on ValidateRoom

                    //check neighBors
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

                    //case no NeighBors
                    if (neighBors.connectionNeeded.Count == 0 && neighBors.connectionBanned.Count == 0)
                    {
                        
                        finalValidateRoom.Clear();
                        finalValidateRoom.AddRange(validateRoom);
                        //Instanciate(connection);
                    }
                    //case with NeighBors
                    else
                    {
                        //case with open Needed
                        if (neighBors.connectionNeeded.Count > 0)
                        {
                            ValidateRoom(connection.connection);

                        }
                        //case with no open Needed; in that case true validateRoom=validateRoom
                        else if (neighBors.connectionNeeded.Count == 0)
                        {
                            trueValidateRoom.Clear();
                            trueValidateRoom.AddRange(validateRoom);

                        }
                        //case with open Banned
                        if (neighBors.connectionBanned.Count > 0)
                        {
                            BannedRoom();
                            //Instanciate(connection);

                        }
                        //case with no open Banned;in that case finalvalidateRoom=trueValidateRoom
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
                            var x = Random.Range(0, maxBoundRange);
                            if (x < firstProb * maxBoundRange)
                            {

                                finalValidateRoom.Reverse();
                            }

                        }
                        else
                        {
                            var x = Random.Range(0, maxBoundRange);
                            if (x < secondProb * maxBoundRange)
                            {

                                finalValidateRoom.Reverse();
                            }
                        }

                        Instanciate(connection, finalValidateRoom.Count / divisorBoundInstanciateList);
                    }
                    TerminateConnection();

                }




            }
            //check if number the room completed the Creation
            if (createdRooms[index].completedRoomsCreation = createdRooms[index].CheckCreationRoom())
            {

                index++;
            }
            //check if index reached the end of list but the Creation not end
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
        //add room that have all the connections required
        private void ValidateRoom(Connection connection)
        {

            foreach (Room room in validateRoom)
            {

                if (CheckConnectionNeeded(room, connection))
                {

                    trueValidateRoom.Add(room);
                }
            }


        }

        //check if the rooms have connections required
        public bool CheckConnectionNeeded(Room room, Connection con)
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
        
      

        //check if the rooms have connections banned
        public bool CheckConnectionBanned(Room room)
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
        

        // add room that not have the connection banneds
        private void BannedRoom()
        {
            foreach (Room room in trueValidateRoom)
            {
                if (CheckConnectionBanned(room))
                {

                    finalValidateRoom.Add(room);
                }
            }
        }

        //=======INSTANCE ROOM & HALLWAY====
        private void Instanciate(ConnectionPoints connection, int max)
        {
            //istance Room
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
                GameObject inst = Instantiate(prefabHallway, createdRooms[index].CheckPosforHallway(connection, createdRooms[index]), Quaternion.identity, hallwayParent);
                inst.transform.Rotate(0, 90, 0);
                inst.GetComponent<HallWay>().player = _playerMovement;
                hallWays.Add(inst.GetComponent<HallWay>());



            }
            else if (connection.connection.name == "Nord" || connection.connection.name == "Sud")
            {

                GameObject insta = Instantiate(prefabHallway, createdRooms[index].CheckPosforHallway(connection, createdRooms[index]), Quaternion.identity, hallwayParent);
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
        public void SetMaxRoom()
        {
            maxRoom = true; useRange = false;
            managerUi.maxRoomButton.SetActive(false);
            managerUi.roomRangeButton.SetActive(false);
            managerUi.maxRoom.enabled = false;
            managerUi.roomRange.enabled = false;
        }
        public void SetUseRange()
        {
            useRange = true;
            maxRoom = false;
            managerUi.maxRoomButton.SetActive(false);
            managerUi.roomRangeButton.SetActive(false);
            managerUi.maxRoom.enabled = false;
            managerUi.roomRange.enabled = false;
            rangeRoom = rules.roomRange + Random.Range(0, (int)rules.roomRange/divisorRangeRoom);

        }

        

        //created lastRoom
        void InstanceLastRoom()
        {
            foreach (ConnectionPoints connection in createdRooms[index].connections)
            {
                if (connection.isIstanciate)
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

            if (createdRooms.Count < rules.maxRoom)
            {
                createdRooms[index].lastConnection = false;
            }



        }



        //replace a room  with 4 exit room
        void ReplaceRoom()
        {


            Debug.Log("funziona");
            var indx = 0;
            for (int i = 0; i < createdRooms.Count; i++)
            {
                if (CountNeighBors(createdRooms[i]))
                {

                    indx = i;
                    break;
                }
            }
            var instanciate = Instantiate(rules.rooms[rules.rooms.Length - 1], createdRooms[indx].transform.position, Quaternion.identity, roomParent);
            instanciate.indexX = createdRooms[indx].indexX;
            instanciate.indexY = createdRooms[indx].indexY;
            createdRooms.Add(instanciate);


            for (int i = 0; i < instanciate.connections.Count; i++)
            {
                for (int j = 0; j < createdRooms[indx].connections.Count; j++)
                {
                    if (instanciate.connections[i].connection.name == createdRooms[indx].connections[j].connection.name)
                    {
                        instanciate.connections[i].isIstanciate = true;
                        instanciate.connections[i].AddHallway = true;
                        break;
                    }
                }

            }

            createdRooms[indx].gameObject.SetActive(false);

        }
        //Count if room have 1 connection and no more of 1 neighBor
        bool CountNeighBors(Room room)
        {

            if (room.connections.Count > 1) { return false; }

            int count = 0;
            if (cells[room.indexX + 1, room.indexY] != null)
            {
                count++;
            }
            if (cells[room.indexX - 1, room.indexY] != null)
            {
                count++;
            }
            if (cells[room.indexX, room.indexY + 1] != null)
            {
                count++;
            }
            if (cells[room.indexX, room.indexY - 1] != null)
            {
                count++;
            }

            if (count > 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }



        //====COROUTINE===
        IEnumerator waitBeforeBake()
        {

            _playerMovement.rotationVelocity = 80;
            yield return new WaitForSeconds(0.5f);
            
        }

        [System.Obsolete]
        IEnumerator WaitBeforeResize()
        {
            _playerMovement.rotationVelocity = 80;
            yield return new WaitForSeconds(0.5f);
            ChangeRooms.ResizeRoom(_playerMovement,rules);




        }


    }



}