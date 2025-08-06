using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quiztrigger : MonoBehaviour
{
    public bool isCorrect = false;
    public Quizmanager quizmanager;

    public void Answer()
    {
        if (isCorrect)
        {
            quizmanager.correct();
        }
        else
        {
            this.gameObject.SetActive(false);
            quizmanager.wrong();
        }
    }
}

