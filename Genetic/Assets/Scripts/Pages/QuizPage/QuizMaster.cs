using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class QuizMaster : View
{
    static DBManager manager { get => DBManager.instance; }

    public SavedQuiz currentQuiz;
    public ImageDownloader downloader;

    public RawImage questionImage;
    public Text questionText;
    public Image QuestionBackground;
    public Text[] buttonTexts;

    //list of buttons
    public GameObject[] buttons;
    //exit button
    public Button StopBtn;
    //feedback for answer
    public GameObject feedbackFrame;
    public Text feedbackText;

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
            if (value.imageURL != null && value.imageURL != "")
            {
                downloader.GetImage(value.imageURL, questionImage);
            }
            else
            {
                questionImage.gameObject.SetActive(false);
            }
            answers = GetAnswers();
            //Update button text
            for (int i = 0; i < buttonTexts.Length; i++)
            {
                var btn = buttonTexts[i];
                try
                {
                    btn.text = answers[i].text;
                }
                catch (ArgumentOutOfRangeException e)
                {
                    btn.text = "";
                }
            }
        }
    }
    Question _question;

    //list of shuffled questions
    List<int> QuestionOrder = new List<int>();
    //current question in list
    int questionIndex = 0;
    //list of answers given by user for this question list
    List<int> givenAnswers = new List<int>();

    //list of shuffled answers
    List<Answer> answers = new List<Answer>();

    //the correct answer
    Answer correctAnswer
    {
        get => GetAnswer();
    }

    int currentAttempt = 0;

    public void SetQuestion(int id)
    {
        currentQuestion = manager.GetItem<Question>(q => q.id == id);
    }

    public void NextQuestion()
    {
        questionIndex++;
        if (questionIndex >= QuestionOrder.Count)
        {
            questionIndex =  questionIndex % QuestionOrder.Count;
        }
        currentAttempt = 0;
        var nextQuestion = QuestionOrder[questionIndex];
        currentQuestion = manager.GetItem<Question>(q => q.id == nextQuestion);
        answered = false;
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

    public void StartQuiz(SavedQuiz quiz)
    {
        currentQuiz = quiz;
        tagList = quiz.tags.Split(',').ToList();
        tagList.Remove("");
        if (quiz.inProgress)
        {
            LoadQuiz(quiz);
        }
        else
        {
            GenerateQuestionOrder();
        }
    }

    void LoadQuiz(SavedQuiz quiz)
    {
        List<int> order = JsonHandler.Deserialize<List<int>>(quiz.questionOrder);
        QuestionOrder = order == null ? new List<int>() : order;
        answered = false;
        questionIndex = quiz.currentQuestion;
        if (QuestionOrder.Count > 0)
        {
            int question = QuestionOrder[quiz.currentQuestion];
            currentQuestion = manager.GetItem<Question>((q) => q.id == question);
        }
        else
        {
            GenerateQuestionOrder();
        }
        currentAttempt = quiz.currentAttempt;
        List<int> given = JsonHandler.Deserialize<List<int>>(quiz.givenAnswers);
        givenAnswers = given == null ? new List<int>() : given;
    }

    void GenerateQuestionOrder()
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
        List<Question> shuffled = new List<Question>();
        //shuffle question list
        while (questions.Count >= 1)
        {
            int idx = UnityEngine.Random.Range(0, questions.Count);
            shuffled.Add(questions[idx]);
            questions.RemoveAt(idx);
        }
        //set question order
        QuestionOrder = shuffled.Select((i) => i.id).ToList();
        answered = false;
        //set current question to first question
        var first = QuestionOrder[0];
        currentQuestion = manager.GetItem<Question>((q) => q.id == first);
        //set quiz as in progress
        currentQuiz.inProgress = true;
        //update quiz info
        
        manager.UpdateItem(currentQuiz);
    }

    void UpdateQuiz()
    {
        currentQuiz.questionOrder = JsonHandler.Serialize(QuestionOrder);
        currentQuiz.currentQuestion = questionIndex;
        currentQuiz.currentAttempt = currentAttempt;
        currentQuiz.givenAnswers = JsonHandler.Serialize(givenAnswers);
        manager.UpdateItem(currentQuiz);
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
                currentAttempt++;
                
                answered = true;
                bool correct = choice.id == correctAnswer.id;
                var img = button.GetComponent<Image>();
                var txt = button.transform.GetChild(0).GetComponentInChildren<Text>();
                if (correct)
                {
                    //correct
                    Debug.Log("Correct!");
                    //highlight button positively momentarily
                    img.color = controller.currentPalette.AnswerBackgroundCorrect;
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
                    img.color = controller.currentPalette.AnswerBackgroundIncorrect;
                    //show feedback
                    feedbackText.text = "Incorrect";
                    feedbackText.color = Color.red;
                    feedbackFrame.SetActive(true);
                }
                await Task.Delay(2000);
                //remove feedback and switch to next question
                if (currentAttempt >= 4 || correct)
                {
                    //set given answer
                    givenAnswers.Add(choice.id);
                    NextQuestion();
                }
                else
                {
                    answered = false;
                }
                UpdateQuiz();
                feedbackFrame.SetActive(false);
                img.color = controller.currentPalette.QuestionBackground;
            }
        }
        
    }

    protected override void ViewAppearing()
    {
        base.ViewAppearing();
        StopBtn.gameObject.SetActive(true);
    }

    protected override void ViewDisappearing()
    {
        base.ViewDisappearing();
        StopBtn.gameObject.SetActive(false);
    }

    public void ExitQuiz()
    {
        UpdateQuiz();
        page.GotoView(page.views.Find((v) => v.GetType() == typeof(QuizSelect)));
    }

    public override void ColorsChanged(ColorPaletteChangedEventArgs args)
    {
        base.ColorsChanged(args);
        QuestionBackground.color = args.palette.QuestionBackground;
        questionText.color = args.palette.QuestionText;
        foreach (var b in buttons)
        {
            b.GetComponent<Image>().color = args.palette.AnswerBackground;
            b.GetComponentInChildren<Text>().color = args.palette.AnswerText;
        }

    }
}
