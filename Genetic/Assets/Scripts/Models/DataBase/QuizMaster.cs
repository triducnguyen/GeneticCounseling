using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuizMaster : MonoBehaviour
{
    static DBManager manager { get => DBManager.instance; }

    public Text questionText;
    public Text[] buttonTexts;

    //determines whether to use random questions or a list of tags/questions
    public bool isCustomList = false;
    //determines whether to get questions by id or by tag
    public bool useQuestionList = false;
    //list of question ids to use
    public int[] questionList;
    //list of tags to use
    public string[] tagList;

    //current question being displayed
    Question currentQuestion
    {
        get
        {
            return _question;
        }
        set
        {
            _question = value;
            //set question text
            questionText.text = value.text;
            answers = GetAnswers();
            //Update button text
            for (int i = 0; i < buttonTexts.Length; i++)
            {
                var btn = buttonTexts[i];
                try
                {
                    btn.text = answers[i].text;
                }
                catch(IndexOutOfRangeException e)
                {
                    btn.text = "";
                }
            }
        }
    }
    Question _question;

    //list of shuffled answers
    List<Answer> answers = new List<Answer>();

    //the correct answer
    Answer correctAnswer
    {
        get => GetAnswer();
    }

    //Finds a random question
    public bool GetNewQuestion()
    {
        int count = manager.db.Table<Question>().Count();
        int id = UnityEngine.Random.Range(1, count + 1);
        currentQuestion = manager.GetItem<Question>(q => q.id == id);
        return currentQuestion == null ? false : true;
    }

    //Finds a random question that has any of the tags provided. Returns true if successful, fals if not.
    public bool GetNewQuestionAnyMatch(List<string> newTags)
    {
        //get all tags in list
        var tags = manager.GetItems<Tag>(t => newTags.Contains(t.tag));
        var ids = tags.Select(t => t.id);
        //get all questions that share this tag
        var qtRelations = manager.GetItems<QuestionTag>(qt => ids.Contains(qt.tagID));
        ids = qtRelations.Select(qtr => qtr.questionID);
        //find all questions that are related to the tags
        var questions = manager.GetItems<Question>(q => ids.Contains(q.id));
        if (questions != null && questions.Count > 0)
        {
            //select random question if there are any
            currentQuestion = questions[UnityEngine.Random.Range(0, questions.Count)];
            return true;
        }
        else
        {
            return false;
        }
    }
    //Finds a random question that has all of the tags provided. Returns true if successful, false if not.
    //public bool GetNewQuestionAllMatch(List<string> tags)
    //{

    //}
    //Finds a random question that has only the tags provided. Returns true if succesful, false if not.

    public void SetQuestion(int id)
    {
        currentQuestion = manager.GetItem<Question>(q => q.id == id);
    }

    Answer GetAnswer()
    {
        if (currentQuestion == null)
        {
            return null;
        }
        else
        {
            var aRelation = manager.GetItem<CorrectAnswer>(ca => ca.questionID == currentQuestion.id);
            return manager.GetItem<Answer>(a => a.id == aRelation.answerID);
        }
    }

    //retuns a list of all answers shuffled up
    List<Answer> GetAnswers()
    {
        if (currentQuestion == null)
        {
            return null;
        }
        else
        {
            var badAnswers = GetIncorrectAnswers();
            badAnswers.Insert(UnityEngine.Random.Range(0, badAnswers.Count), correctAnswer);
            return badAnswers;
        }
    }
    
    //returns a list of 3 random incorrect answers
    List<Answer> GetIncorrectAnswers()
    {
        if (currentQuestion == null)
        {
            return null;
        }
        else
        {
            //get all incorrect answers to this question
            var incorrectRelations = manager.GetItems<IncorrectAnswer>(ia => ia.questionID == currentQuestion.id);
            List<Answer> badAnswers = new List<Answer>();
            foreach (var relation in incorrectRelations)
            {
                badAnswers.Add(manager.GetItem<Answer>(a => a.id == relation.answerID));
            }
            //get 3 random bad answers
            List<Answer> result = new List<Answer>();
            for (int i = 0; i < 2; i++)
            {
                if (result.Count > 0)
                {
                    var random = UnityEngine.Random.Range(0, badAnswers.Count);
                    result.Add(badAnswers[random]);
                    badAnswers.Remove(badAnswers[random]);
                }
            }
            return badAnswers;
        }
    }

    private void Start()
    {
        //get a random question
        GetNewQuestion();
    }
    public void Answer1()
    {
        //user chose answer 1, check if it is correct
        CheckAnswer(answers[0]);
    }

    public void Answer2()
    {
        //user chose answer 2, check if it is correct
        CheckAnswer(answers[1]);
    }

    public void Answer3()
    {
        //user chose answer 3, check if it is correct
        CheckAnswer(answers[2]);
    }

    public void Answer4()
    {
        //user chose answer 4, check if it is correct
        CheckAnswer(answers[3]);
    }

    public bool CheckAnswer(Answer choice)
    {
        bool correct = choice.id == correctAnswer.id;
        if (correct)
        {
            //correct
            Debug.Log("Correct!");
        }
        else
        {
            //incorrect
            Debug.Log("Incorrect!");
        }
        //get new question
        GetNewQuestion();
        return correct;
    }
}
