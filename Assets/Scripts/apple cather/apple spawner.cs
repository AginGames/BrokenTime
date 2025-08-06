using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class applespawner : MonoBehaviour
{
    public GameObject apple;
    public GameObject zepsuteJablko;
    public int playerScore = 0;
    public TextMeshProUGUI scoreText;
    public GameObject canvas;
    public float yBound;
    private newtonscript1 newtonscript1;

    void Start()
    {
        newtonscript1 = FindObjectOfType<newtonscript1>();
    }
    public void StartGame()
    {
        playerScore = 0;
        StopAllCoroutines();
        StartCoroutine(SpawnRandomGameObject());
        scoreText.text = playerScore.ToString();
        canvas.SetActive(true);
    }

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();
        if(playerScore >= 20)
        {
            Debug.Log(newtonscript1.czyDrugi);
            newtonscript1.czyDrugi = true;
            StopAllCoroutines();
            canvas.SetActive(false);
            newtonscript1.aktywuj();
            newtonscript1.CameraNaPokoj.Priority = 11;
            FindObjectOfType<PlayerMovement>().EnableControls();
            newtonscript1.StartEndDialogue();

        }
    }
    public void removeScore()
    {
        playerScore = 0;
        scoreText.text = playerScore.ToString();
    }
    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator SpawnRandomGameObject()
    {
        yield return new WaitForSeconds(Random.Range(1, 3));
        if(Random.Range(0f, 1f) < 0.6f)
        Instantiate(apple, new Vector3(Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x), yBound), transform.rotation);
        else
            Instantiate(zepsuteJablko, new Vector3(Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x), yBound), transform.rotation);
        StartCoroutine(SpawnRandomGameObject());
    }
}
