using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace paper
{
    public class HallWay : MonoBehaviour
    {
        public List<Renderer> renderers;
        public PlayerMovement player;
        float dist;
        public GameObject center;
        
        public bool rangeAlpha;
        public bool stop;
        CameraManager manager;
        private void Start()
        {
            
            
            rangeAlpha = true;
            manager = FindObjectOfType<CameraManager>();
           
        }

        private void Update()
        {
            
        }

        IEnumerator ChangeAlpha(float dist)
        {
            while (dist > 0.01f)
            {
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
       



    }
}