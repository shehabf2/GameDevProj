using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    int count;
    AudioSource GameAudio;
    [SerializeField] float speed;
    [SerializeField] Text countText;
    [SerializeField] Text winText;
    [SerializeField] Text loseText;
    [SerializeField] AudioClip winningSound;
    [SerializeField] AudioClip losingSound;
    [SerializeField] int loadingTime = 4;
    [SerializeField] GameObject winFx;
    [SerializeField] GameObject fireFx;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameAudio = GetComponent<AudioSource>();
        count = 0;
        SetCountText();
        winText.text = "";
        loseText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move()
    {
        float moveHorz = Input.GetAxis("Horizontal");
        float moveVert = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorz, 0.0f, moveVert);
        rb.velocity = movement * speed;
    }

    void OnTriggerEnter(Collider other) 
    {
        //Destroy(other.gameObject);
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("Enemy")) {
            // you lose!
            other.gameObject.SetActive(false);
            GameAudio.PlayOneShot(losingSound);
            fireFx.SetActive(true);
            loseText.text = "You Lose!";
            Invoke("LoadCurrentScene", loadingTime);

        }
    }

    void SetCountText() 
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 7) 
        {
            GameAudio.PlayOneShot(winningSound);
            winFx.SetActive(true);
            winText.text = "You Win!";
            Invoke("LoadNextScene", loadingTime);
        }
    }

    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        // turn particle effect off before moving to the next scene
        SceneManager.LoadScene(nextSceneIndex);
    }

    void LoadCurrentScene() 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
