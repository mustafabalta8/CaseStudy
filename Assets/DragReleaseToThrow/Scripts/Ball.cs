using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Simsoft.DragReleaseToThrow 
{ 
    public class Ball : MonoBehaviour
    {
        [SerializeField] private GameObject winUI;
        [SerializeField] private GameObject loseUI;
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Hole>())
            {
                StartCoroutine(HandleWinning());
            }
            if (other.GetComponent<LoseCollider>())
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        IEnumerator HandleWinning()
        {
            winUI.SetActive(true);
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }





    }
}
