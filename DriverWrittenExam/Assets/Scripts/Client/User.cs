using System;
using System.Collections.Generic;

public class User
{
    private const int SAMPLE_COUNT = 20;

    private static User instance = new User();
    public static User I { get { return User.instance; } }

    private object[] questions;
    private int currentIdx;
    private string[] answerIndices;

    public void LoadAllQuestions()
    {
        var file = ConfigManager.I.GetConfig("Questions");
        this.questions = file["Data"] as Object[];
        this.currentIdx = 0;
        this.answerIndices = new string[this.questions.Length];
    }

    public void LoadSampleExam()
    {
        var file = ConfigManager.I.GetConfig("Questions");
        var allQs = file["Data"] as Object[];

        var rnd = new System.Random();
        var myQs = new List<Object>(User.SAMPLE_COUNT);

        for (int i = 0; i < User.SAMPLE_COUNT; i++)
        {
            var idx = rnd.Next(0, allQs.Length);
            myQs.Add(allQs[idx]);
        }

        this.questions = myQs.ToArray();
        this.currentIdx = 0;
        this.answerIndices = new string[this.questions.Length];
    }

    public Dictionary<String, Object> GetCurrentQuestion()
    {
        return this.questions[this.currentIdx] as Dictionary<String, Object>;
    }

    public void FlipBack()
    {
        Log.Assert(this.currentIdx > 0);
        this.currentIdx--;
    }

    public void FlipForward()
    {
        Log.Assert(this.currentIdx < this.questions.Length - 1);
        this.currentIdx++;
    }
    
    public bool IsFirstQuestion()
    {
        return this.currentIdx == 0;
    }
    
    public bool IsFinalQuestion()
    {
        return this.currentIdx == this.questions.Length - 1;
    }

    public void AnswerQuestion(int answerIndx)
    {
        this.answerIndices[this.currentIdx] = answerIndx.ToString();
    }

    public bool HasAnswered()
    {
        return GetAnswerIdx() != null;
    }

    public string GetAnswerIdx()
    {
        var answerIdx = this.answerIndices[this.currentIdx];
        return answerIdx;
    }
}