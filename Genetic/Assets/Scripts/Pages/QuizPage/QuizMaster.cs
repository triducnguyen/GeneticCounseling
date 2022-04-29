using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>Controls the behavior of the quiz view.</summary>
public class QuizMaster : View
{
    /// <summary>Gets the database manager.</summary>
    /// <value>The database manager.</value>
    static DBManager manager { get => DBManager.instance; }

    /// <summary>The current quiz.</summary>
    public SavedQuiz currentQuiz;
    /// <summary>The image downloader.</summary>
    public ImageDownloader downloader;

    /// <summary>The question image.</summary>
    public RawImage questionImage;
    /// <summary>The question text.</summary>
    public Text questionText;
    /// <summary>The question background.</summary>
    public Image QuestionBackground;
    /// <summary>The answer button texts.</summary>
    public Text[] buttonTexts;

    //list of buttons
    /// <summary>The answer buttons.</summary>
    public GameObject[] buttons;
    //exit button
    /// <summary>The exit button</summary>
    public Button StopBtn;
    //feedback for answer
    /// <summary>The feedback frame.</summary>
    public GameObject feedbackFrame;
    /// <summary>The feedback text.</summary>
    public Text feedbackText;

    //list of tags to use
    /// <summary>The tag list
    /// being used.</summary>
    public List<string> tagList;

    //keeps track of whether this question has been answered already. Prevents multiple answers from being pressed
    /// <summary>Whether the user has given an answer yet. Becomes false after feedback is displayed.</summary>
    bool answered = false;

    //current question being displayed
    /// <summary>Gets or sets the current question.</summary>
    /// <value>The current question.</value>
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
    /// <summary>The question
    /// private value.</summary>
    Question _question;

    //list of shuffled questions
    /// <summary>The question order.</summary>
    List<int> QuestionOrder = new List<int>();
    //current question in list
    /// <summary>The current question index.</summary>
    int questionIndex = 0;
    //list of answers given by user for this question list
    /// <summary>The answers the user has given thus far.</summary>
    List<int> givenAnswers = new List<int>();

    //list of shuffled answers
    /// <summary>List of shuffled answers.</summary>
    List<Answer> answers = new List<Answer>();

    //the correct answer
    /// <summary>Gets the correct answer.</summary>
    /// <value>The correct answer.</value>
    Answer correctAnswer
    {
        get => GetAnswer();
    }
    /// <summary>The maximum attempts
    /// on the current question before moving to the next question.</summary>
    [SerializeField]
    int maxAttempts = 4;
    /// <summary>The current attempt for the current question.</summary>
    [SerializeField]
    int currentAttempt = 0;

    /// <summary>Sets the current question.</summary>
    /// <param name="id">The question identifier.</param>
    public void SetQuestion(int id)
    {
        currentQuestion = manager.GetItem<Question>(q => q.id == id);
    }

    /// <summary>Goes to the next question.</summary>
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

    /// <summary>Gets the answer to current question.</summary>
    /// <returns>Answer to current question.</returns>
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
    /// <summary>Gets a shuffled list of all possible answers to current question, correct and incorrect.</summary>
    /// <returns>Shuffled list of answers to the current question.</returns>
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
    /// <summary>Gets the incorrect answers for the current question.</summary>
    /// <returns>Incorrect answers for current question.</returns>
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

    /// <summary>Starts the given saved quiz.</summary>
    /// <param name="quiz">The saved quiz to start.</param>
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

    /// <summary>Loads the given saved quiz.</summary>
    /// <param name="quiz">The saved quiz to load.</param>
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

    /// <summary>Generates the question order.</summary>
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

    /// <summary>Updates the current quiz in the database.</summary>
    void UpdateQuiz()
    {
        currentQuiz.questionOrder = JsonHandler.Serialize(QuestionOrder);
        currentQuiz.currentQuestion = questionIndex;
        currentQuiz.currentAttempt = currentAttempt;
        currentQuiz.givenAnswers = JsonHandler.Serialize(givenAnswers);
        manager.UpdateItem(currentQuiz);
    }

    /// <summary>Checks answer 1.</summary>
    public void Answer1()
    {
        //user chose answer 1, check if it is correct
        CheckAnswer(0);
    }

    /// <summary>Checks answer 2.</summary>
    public void Answer2()
    {
        //user chose answer 2, check if it is correct
        CheckAnswer(1);
    }

    /// <summary>Checks answer 3.</summary>
    public void Answer3()
    {
        //user chose answer 3, check if it is correct
        CheckAnswer(2);
    }

    /// <summary>Checks answer 4.</summary>
    public void Answer4()
    {
        //user chose answer 4, check if it is correct
        CheckAnswer(3);
    }

    /// <summary>Checks the given answer.</summary>
    /// <param name="answer">The answer given.</param>
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
                if (currentAttempt >= maxAttempts || correct)
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

    /// <summary>Called by default when the view appears.</summary>
    protected override void ViewAppearing()
    {
        base.ViewAppearing();
        StopBtn.gameObject.SetActive(true);
    }

    /// <summary>Called by default when the view disappears.</summary>
    protected override void ViewDisappearing()
    {
        base.ViewDisappearing();
        StopBtn.gameObject.SetActive(false);
    }

    /// <summary>Exits the quiz.</summary>
    public void ExitQuiz()
    {
        UpdateQuiz();
        page.GotoView(page.views.Find((v) => v.GetType() == typeof(QuizSelect)));
    }

    /// <summary>Event handler for when colors change.</summary>
    /// <param name="args">The <see cref="ColorPaletteChangedEventArgs" /> instance containing the color palette data.</param>
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
