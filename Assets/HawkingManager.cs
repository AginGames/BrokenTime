using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Collections;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class HawkingManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI currentText;
    public TMP_InputField input;
    public GameObject dwarfManager;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public AudioSource correctAudio;
    public AudioSource wrongAudio;
    public AudioSource gameOverAudio;
    private int number1;
    private int number2;
    private double result;
    private int hearts = 3;
    private int current = 0;
    private readonly char[] operators = { '+', '-', '*', '^' };

    // Start is called before the first frame update
    void Start()
    {
        GenerateTask();
    }

    void GenerateTask()
    {
        if (current == 10) // TODO: Set to 10
        {
            dwarfManager.GetComponent<dwarfmanager>().Endthisshit();
            return;
        }
        current++;

        currentText.text = current + "/10";

        char randomOperator = operators[UnityEngine.Random.Range(0, operators.Length)];
        Debug.Log("Random Operator: " + randomOperator);

        number1 = UnityEngine.Random.Range(1, 15 + 1);
        number2 = UnityEngine.Random.Range(1, randomOperator == '^' ? 3 : 15 + 1);



        // Perform operations based on the randomly selected operator
        switch (randomOperator)
        {
            case '+':
                int additionResult = number1 + number2;
                text.text = number1 + " + " + number2;
                result = additionResult;
                Debug.Log(text.text);
                break;
            case '-':
                int subtractionResult = number1 - number2;
                text.text = number1 + " - " + number2;
                result = subtractionResult;
                Debug.Log(text.text);
                break;
            case '*':
                int multiplicationResult = number1 * number2;
                text.text = number1 + " * " + number2;
                result = multiplicationResult;
                Debug.Log(text.text);
                break;
            case '^':
                double powerResult = Mathf.Pow(number1, number2);
                text.text = number1 + " ^ " + number2;
                result = powerResult;
                Debug.Log(text.text);
                break;
        }
    }

    void RemoveHeart()
    {
        hearts--;

        if (hearts >= 1)
        {
            wrongAudio.Play();
        }

        if (hearts == 2)
        {
            heart3.SetActive(false);
        }
        else if (hearts == 1)
        {
            heart2.SetActive(false);
        }
        else if (hearts < 1)
        {
            gameOverAudio.Play();

            current = 0;
            hearts = 3;

            heart2.SetActive(true);
            heart3.SetActive(true);

            GenerateTask();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckResult(string r)
    {
        try
        {
            double parsedDoubleValue = double.Parse(r);
            if (parsedDoubleValue == result)
            {
                correctAudio.Play();
                GenerateTask();
            }
            else
            {
                RemoveHeart();
            }

        }
        catch (FormatException)
        {
        }

        input.text = ""; // Clear the text input field
    }
}
