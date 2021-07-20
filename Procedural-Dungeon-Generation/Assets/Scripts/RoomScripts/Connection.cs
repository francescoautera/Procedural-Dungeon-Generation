using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace paper {
    [CreateAssetMenu(fileName = "Paper", menuName = "Paper/ConnectionPoint", order = 1)]
    public class Connection : ScriptableObject
    {
        public Connection OppositeConnection;
       
        public bool isOpposite(Connection con) {
            
            return con == OppositeConnection;
        }
    }
}