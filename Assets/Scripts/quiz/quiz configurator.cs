using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz/Create Game Mode")]

public class quizconfig : ScriptableObject
{
    public List<questionmodel> Categories;
}
