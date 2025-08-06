using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

[RequireComponent(typeof(AudioSource))]
public class Quizmanager : MonoBehaviour
{
    public GameObject[] answersButton;
    public TextMeshProUGUI quiztext;
    public GameObject quiz;

    public List<questionmodel> Questions;
    public int currentQuestion;
    public AudioSource correctAudio;
    public AudioSource wrongAudio;

    public void QuizStart()
    {

        GenerateQuestion();
    }
    void SetAnswers()
    {
        for (int i = 0; i < answersButton.Length; i++)
        {
            answersButton[i].SetActive(true);
            answersButton[i].GetComponent<quiztrigger>().isCorrect = false;
            answersButton[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Questions[currentQuestion].Answer[i];

            if (Questions[currentQuestion].correctAnswer == i + 1)
                answersButton[i].GetComponent<quiztrigger>().isCorrect = true;
        }
    }
    void GenerateQuestion()
    {
        if (Questions.Count == 0)
        {
            FindObjectOfType<einsteinMenager>().StartEndDialogue();
            return;
        }
        currentQuestion = Random.Range(0, Questions.Count);

        quiztext.text = Questions[currentQuestion].Question;
        SetAnswers();

        Questions.RemoveAt(currentQuestion);
    }
    public void correct()
    {
        correctAudio.Play();
        GenerateQuestion();
    }

    public void wrong()
    {
        wrongAudio.Play();
    }
}
