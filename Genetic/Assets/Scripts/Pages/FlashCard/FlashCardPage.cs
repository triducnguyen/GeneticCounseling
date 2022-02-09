using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashCardPage : PageController
{
    static DBManager manager { get => DBManager.instance; }
    public int numFlashCard;
    public GameObject flashCardPrefab;
    public List<Question> questionList;
    public GameObject contentView;

    private HashSet<Question> questionSet;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        questionList = new List<Question>();
        questionSet = new HashSet<Question>();

        for(int i = 0; i < numFlashCard; i++)
        {
            Question new_question = GetNewQuestion();
            if(new_question != null && !questionSet.Contains(new_question))
            {
                questionList.Add(new_question);
                questionSet.Add(new_question);
                SetNewFlashCard(new_question);
            }
        }

        ExpandContenView();
    }

    public void ExpandContenView()
    {
        RectTransform rect = contentView.GetComponent<RectTransform>();
        if(numFlashCard * 400 > rect.sizeDelta.y)
        {
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, (numFlashCard + 1) * 400);
        }
    }

    public void SetNewFlashCard(Question question)
    {
        if (question == null) return;

        GameObject obj = Instantiate(flashCardPrefab);
        obj.transform.SetParent(contentView.gameObject.transform);
        obj.transform.localScale = new Vector3(1, 1, 1);
        FlashCardMaster flashCard = obj.GetComponent<FlashCardMaster>();
        flashCard.currentQuestion = question;

    }

    public Question GetNewQuestion()
    {
        Question curr = new Question();
        int count = manager.db.Table<Question>().Count();
        int id = UnityEngine.Random.Range(1, count + 1);
        curr = manager.GetItem<Question>(q => q.id == id);
        return curr;
    }
}
