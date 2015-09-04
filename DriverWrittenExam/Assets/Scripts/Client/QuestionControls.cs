using UnityEngine;
using UnityEngine.UI;
using System;
using Object = System.Object;

public class QuestionControls : MonoBehaviour
{
    public Button BackButton;
    public Button ForwardButton;

    public GameObject ResultGO;
    public Text ResultText;

    public Text Question;
    public Image QuestionPic;
    public Text[] Answers;

    private int correctIdx;

    public void Start()
    {
        DrawControls();
    }

    private void DrawControls()
    {
        DrawQuestion();
        DrawAnswer();
        DrawFlip();
    }

    private void DrawQuestion()
    {
        var db = User.I.GetCurrentQuestion();
        var question = db["Question"] as String;
        this.Question.text = question;

        var value = (Object) null;
        if (db.TryGetValue("Picture", out value))
        {
            var picture = value as String;
            var sprite = Resources.Load(String.Format("Pictures/{0}", picture), typeof(Sprite)) as Sprite;
            this.QuestionPic.sprite = sprite;
        } else
            this.QuestionPic.sprite = null;

        var answers = db["Answers"] as Object[];
        for (int i = 0; i < this.Answers.Length; i++)
            this.Answers[i].text = answers[i] as String;

        this.correctIdx = (int) db["Correct"] - 1;
    }

    private void DrawAnswer()
    {
        if (! User.I.HasAnswered())
        {
            this.ForwardButton.gameObject.SetActive(false);
            this.ResultGO.gameObject.SetActive(false);
            return;
        }

        this.ForwardButton.gameObject.SetActive(true);
        var answerIdx = Int32.Parse(User.I.GetAnswerIdx());
        if (answerIdx == 10)
        {
            this.ResultGO.gameObject.SetActive(false);
            this.Answers[this.correctIdx].color = Color.green;
            return;
        }

        this.ResultGO.gameObject.SetActive(true);
        this.Answers[answerIdx].color = Color.red;
        this.Answers[this.correctIdx].color = Color.green;
        if (answerIdx == this.correctIdx)
        {
            this.ResultText.text = Locale.Text(101);
            return;
        }

        this.ResultText.text = Locale.Text(102);
    }

    private void DrawFlip()
    {
        if (User.I.IsFirstQuestion())
            this.BackButton.gameObject.SetActive(false);
        
        if (User.I.IsFinalQuestion())
            this.ForwardButton.gameObject.SetActive(false);
    }

    public void ShowAnswer()
    {
        if (User.I.HasAnswered())
            return;

        User.I.AnswerQuestion(10);
        DrawControls();
    }

    public void SelectAnswer(int index)
    {
        if (User.I.HasAnswered())
            return;

        User.I.AnswerQuestion(index);
        DrawControls();
    }

    public void FlipBack()
    {
        if (User.I.IsFirstQuestion())
            return;

        User.I.FlipBack();
        Application.LoadLevel(1);
    }

    public void FlipForward()
    {
        if (User.I.IsFinalQuestion())
            return;

        User.I.FlipForward();
        Application.LoadLevel(1);
    }

    public void Exit()
    {
        Application.LoadLevel(0);
    }
}
