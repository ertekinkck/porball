using UnityEngine;
using UnityEngine.SceneManagement;
public class Portal : MonoBehaviour
{
    public string sceneToLoad = "AI_scene"; // Name of the scene to load
    public string ballTag = "Ball"; // Tag of the ball GameObject
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(ballTag))
        { // Store the ball's position before unloading the current scene
            Vector3 ballPosition = other.transform.position; // Load the new scene
            SceneManager.LoadScene(sceneToLoad); // After the new scene is loaded, find the ball and set its position // (This assumes the ball persists between scenes or is recreated)
            GameObject ball = GameObject.FindGameObjectWithTag(ballTag);
            if (ball != null) { ball.transform.position = ballPosition; }
            else { Debug.LogWarning("Ball not found in the new scene!"); }
        }
    }
}