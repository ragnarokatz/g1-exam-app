using UnityEngine;
using System.Collections;

public class MenuControls : MonoBehaviour
{
    public void StartAll()
    {
        User.I.LoadAllQuestions();
        Application.LoadLevel(1);
    }

    public void StartExam()
    {
        User.I.LoadSampleExam();
        Application.LoadLevel(1);
    }
}
