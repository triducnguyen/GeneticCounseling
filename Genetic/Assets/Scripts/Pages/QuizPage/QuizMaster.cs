using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class QuizMaster : View
{
    static PaletteController controller { get => AppController.instance.controller; }
    static DBManager manager { get => DBManager.instance; }

    public SavedQuiz currentQuiz;

    public Text questionText;
    public Text[] buttonTexts;

    //list of buttons
    public GameObject[] buttons;
    //feedback for answer
    public GameObject feedbackFrame;
    public Text feedbackText;

    //determines whether to use random questions or a list of tags/questions
    public bool isCustomList = false;
    //determines whether to get questions by id or by tag
    public bool useQuestionList = false;
    //list of question ids to use
    public int[] questionList;
    //list of tags to use
    public List<string> tagList;

    //keeps track of whether this question has been answered already. Prevents multiple answers from being pressed
    bool answered = false;

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
                catch(ArgumentOutOfRangeException e)
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
    public bool GetNewQuestion(int lastPressed = -1)
    {
        if (lastPressed != -1)
        {
            var img = buttons[lastPressed].GetComponent<Image>();

        }
        int count = manager.db.Table<Question>().Count();
        int id = UnityEngine.Random.Range(1, count + 1);
        currentQuestion = manager.GetItem<Question>(q => q.id == id);
        answered = false;
        return currentQuestion == null ? false : true;
    }

    //Finds a random question that has any of the tags provided. Returns true if successful, false if not.
    public bool GetNewQuestionAnyMatch()
    {
        //get all tags in list
        var tags = manager.GetItems<Tag>(t => tagList.Contains(t.tag));
        //get ids of tags
        var ids = tags.Select(t => t.id);
        //get all questions that have this tag id
        var qtRelations = manager.GetItems<QuestionTag>(qt => ids.Contains(qt.tagID));
        //replace tag ids with question ids that have this tag
        ids = qtRelations.Select(qtr => qtr.questionID);
        //get all questions with given ids
        var questions = manager.GetItems<Question>(q => ids.Contains(q.id));
        if (questions != null && questions.Count > 0)
        {
            //select random question if there are any
            currentQuestion = questions[UnityEngine.Random.Range(0, questions.Count)];
            answered = false;
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

    public void NewQuiz(SavedQuiz quiz = null)
    {
        currentQuiz = quiz;
        isCustomList = true;
        tagList = quiz.tags.Split(',').ToList();
        tagList.Remove("");
        GetNewQuestionAnyMatch();
    }
    public void NewQuiz()
    {
        isCustomList = false;
    }
    public void Answer1()
    {
        //user chose answer 1, check if it is correct
        CheckAnswer(0);
    }

    public void Answer2()
    {
        //user chose answer 2, check if it is correct
        CheckAnswer(1);
    }

    public void Answer3()
    {
        //user chose answer 3, check if it is correct
        CheckAnswer(2);
    }

    public void Answer4()
    {
        //user chose answer 4, check if it is correct
        CheckAnswer(3);
    }

    public async void CheckAnswer(int answer)
    {
        if (answer <= answers.Count-1)
        {
            Answer choice = answers[answer];
            GameObject button = buttons[answer];

            if (answered != true)
            {
                answered = true;
                bool correct = choice.id == correctAnswer.id;
                var img = button.GetComponent<Image>();
                var txt = button.transform.GetChild(0).GetComponentInChildren<Text>();
                if (correct)
                {
                    //correct
                    Debug.Log("Correct!");
                    //highlight button positively momentarily
                    img.color = controller.currentPalette.QuestionBackgroundCorrect;
                    //Show feedback
                    feedbackText.text = "Correct";
                    feedbackText.color = Color.green;
                    feedbackFrame.SetActive(true);
                }
                else
                {
                    //incorrect
                    Debug.Log("Incorrect!");
                    //highlight button negatively momentarily
                    img.color = controller.currentPalette.QuestionBackgroundIncorrect;
                    //show feedback
                    feedbackText.text = "Incorrect";
                    feedbackText.color = Color.red;
                    feedbackFrame.SetActive(true);
                }
                await Task.Delay(2000);
                //remove feedback and switch to next question
                GetNewQuestionAnyMatch();
                feedbackFrame.SetActive(false);
                img.color = controller.currentPalette.QuestionBackground;
            }
        }
        
    }

    
}
