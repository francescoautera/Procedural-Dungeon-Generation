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
        public AiEnemy enemy;
        public bool rangeAlpha;
        public bool stop;
        CameraManager manager;
        private void Start()
        {
            Renderer[] renderer = GetComponentsInChildren<Renderer>();
            renderers = renderer.ToList();
            rangeAlpha = true;
            manager = FindObjectOfType<CameraManager>();
            foreach (Renderer rend in renderers)
            {
                if (manager.isChanged)
                {
                    rend.material.SetFloat("_Radius", 500);
                }
                else
                {
                    rend.material.SetFloat("_Radius", 5);
                }
            }
        }

        private void Update()
        {
            if (player != null && enemy != null)
            {
                dist = MinPosEnemyPlayer();
                /*if (dist < 5)
                {
                    if (rangeAlpha)
                    {
                        rangeAlpha = false;
                        dist = 1;
                        StartCoroutine(ChangeAlpha(dist));
                    }
                }
                else
                {
                    rangeAlpha = true;
                    if (!stop)
                    {
                        foreach (Renderer rend in renderers)
                        {
                            rend.material.SetFloat("_Radius", dist);
                        }
                    }
                }*/
            }
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
        float MinPosEnemyPlayer()
        {
            var dist = 0f;
            var dist2 = 0f;
            float minDistance = float.MaxValue;
            dist = Vector3.Distance(center.transform.position, player.transform.position);
            dist2 = Vector3.Distance(center.transform.position, enemy.transform.position);
            if (dist >= dist2)
            {
                foreach (Renderer rend in renderers)
                {
                    rend.material.SetVector("_PosPlayer", enemy.transform.position);
                }
                minDistance = dist2;
            }
            else
            {
                foreach (Renderer rend in renderers)
                {
                    rend.material.SetVector("_PosPlayer", player.transform.position);
                }
                minDistance = dist;
            }
            return minDistance;

        
        }



    }
}