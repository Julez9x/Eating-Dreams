using UnityEngine;
using UnityEngine.SceneManagement;

public class CollideNextScene1 : MonoBehaviour
{
    [Header("Level Name")]
    public string levelName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(levelName);
        }
    }
}
