using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GroundScript : MonoBehaviour
{
    private GameObject player;
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnCollisionEnter(Collision collision)
    {
        // Solution over reference
        if (collision.gameObject == player)
        {
            GameObject.Destroy(player);
            player = null;
            SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
            StartCoroutine(RestartLevel());
        }

        // Solution over Tag
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Destroy(player);
        }

        // Solution over Name
        if (collision.gameObject.name == "Player")
        {
            GameObject.Destroy(player);
        }
    }
    
    private IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(3f); // Wait 3 seconds
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

