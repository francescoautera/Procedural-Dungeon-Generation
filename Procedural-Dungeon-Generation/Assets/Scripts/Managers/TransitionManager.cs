using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace paper
{
    public class TransitionManager : MonoBehaviour
    {
        [SerializeField]Image Fade;

        private void Start()
        {
            Fade.CrossFadeAlpha(0, 1f, false);
        }

        public void Restart() {
            Fade.CrossFadeAlpha(1, 1f, false);
            StartCoroutine(ChangeScene());
        }

        IEnumerator ChangeScene() {
            yield return new WaitForSeconds(1.2f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}
