using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Oppenheimermanager : MonoBehaviour
{
    [SerializeField] private string id = "Oppenheimer";
    public bool czyDrugi = false;
    public TextMeshProUGUI character;
    public TextMeshProUGUI DialogueText;
    public Animator animator;
    public GameObject dialougebox;
    public float textSpeed = 0.05f;
    public CinemachineVirtualCamera bosscamera;
    public GameObject healthcanvas;
    public GameObject retryCanvas;
    public AudioSource audioSource;

    public dialogue dialogue;
    public dialogue2 EndDialogue;
    public Queue<string> sentences;
    private bool dialog = false;
    private PlayerMovement characterController;
    private shooting shooting;
    private GameObject oppenheimer;
    private GameObject player;
    private Animator oppenheimeranimator;
    private Health oppenheimerHealth;
    private Health playerHealth;
    public bool game = false;
    private Vector3 playerStartPosition;
    private Vector3 oppStartPosition;


    public void LoadData(GameData data)
    {
        this.czyDrugi = data.checkpoint.TryGetValue(id, out czyDrugi);
    }
    public void SaveData(ref GameData data)
    {
        if (data.checkpoint.ContainsKey(id))
        {
            data.checkpoint.Remove(id);
        }
        data.checkpoint.Add(id, czyDrugi);
    }
    void Start()
    {
        sentences = new Queue<string>();
        dialog = false;
        player = GameObject.FindGameObjectWithTag("Player");
        characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        shooting = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<shooting>();
        oppenheimer = GameObject.FindGameObjectWithTag("boss");
        playerStartPosition = player.GetComponent<Transform>().position;
        oppStartPosition = oppenheimer.GetComponent<Transform>().position;
        StartDialogue(dialogue, czyDrugi);
        oppenheimeranimator = oppenheimer.GetComponent<Animator>();
        oppenheimerHealth = oppenheimer.GetComponent<Health>();
        playerHealth = GameObject.FindGameObjectWithTag("logic").GetComponent<Health>();


    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && dialog == true)
        {
            DisplayNextScene();

        }
        if(playerHealth.health <= 0)
        {
            dead();
        }
        if(oppenheimerHealth.health <= 0)
        {
            GameObject.FindGameObjectWithTag("levelloader").GetComponent<LevelLoader>().LoadNextLevel(8);
        }
    }
    private void dead()
    {
        Debug.Log("bad ending");
        oppenheimeranimator.SetBool("ruch", false);
        player.GetComponent<Transform>().position = playerStartPosition;
        oppenheimer.GetComponent<Transform>().position = oppStartPosition;
        StopAllCoroutines();
        healthcanvas.SetActive(false);
        characterController.DisableControls();
        retryCanvas.SetActive(true);
        audioSource.Stop();

    }
    public void StartDialogue(dialogue dialogue, bool czyDrugi)
    {
        audioSource.Play();
        bosscamera.Priority = 20;
        characterController.DisableControls();
        shooting.canShoot = false;
        dialog = true;
        dialougebox.SetActive(true);
        animator.SetBool("IsOpen", true);
        character.text = dialogue.name;
        sentences.Clear();
        if (!czyDrugi || dialogue.sentences2.Length == 0)
        {
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            DisplayNextScene();
        }
        else if (czyDrugi)
        {
            foreach (string sentence in dialogue.sentences2)
            {
                sentences.Enqueue(sentence);
            }

            DisplayNextScene();
        }

    }
    public void Retry()
    {
        oppenheimerHealth.SetHealth(10000, false);
        playerHealth.SetHealth(3000, false);
        retryCanvas.SetActive(false);
        Start();
    }
    public void StartEndDialogue()
    {
        characterController.DisableControls();
        shooting.canShoot = false;
        dialog = true;
        game = false;
        dialougebox.SetActive(true);
        animator.SetBool("IsOpen", true);
        character.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in EndDialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextScene();

    }
    public void DisplayNextScene()
    {
        if (sentences.Count == 0)
        {
            if (!czyDrugi)
            {
                StartFight();
            }
            else if (czyDrugi)
            {
                Endthisshit();
            }
            dialog = false;
            bosscamera.Priority = 0;
            return;

        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence(string sentence)
    {
        DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    public void StartFight()
    {
        game = true;
        healthcanvas.SetActive(true);
        animator.SetBool("IsOpen", false);
        dialougebox.SetActive(false);
        dialog = false;
        //Debug.Log(czyDrugi);
        Cursor.visible = true;
        characterController.EnableControls();
        shooting.canShoot = true;
        oppenheimeranimator.SetBool("ruch", true);
        StartCoroutine(Fight());
    }
    void Endthisshit()
    {
        StopAllCoroutines();
        animator.SetBool("IsOpen", false);
        dialougebox.SetActive(false);
        dialog = false;
        FindObjectOfType<LevelLoader>().LoadNextLevel(1);
        FindObjectOfType<PlayerMovement>().EnableControls();
        Cursor.visible = false;
        characterController.EnableControls();
        

    }
    public IEnumerator Fight()
    {
        yield return new WaitForSeconds(5);
        oppenheimeranimator.SetTrigger("attack");
        StopCoroutine(Fight());
        StartCoroutine(Fight());

    }
}
