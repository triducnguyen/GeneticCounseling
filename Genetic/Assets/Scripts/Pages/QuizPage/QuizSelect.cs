using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuizSelect : MonoBehaviour
{
    [System.Serializable]
    public class ToggleEvent : UnityEvent { }
    [SerializeField]
    public ToggleEvent onActiveTogglesChanged;

    DBManager manager => AppController.instance.manager;

    public QuizPage page;

    public GameObject quizTest;
    public QuizMaster qmaster;

    public GameObject quizList;
    public GameObject quizItem;

    public List<Button> buttons;

    public SavedQuiz selected => GetSelecetedQuiz();

    public ToggleGroup group;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        //reload all quiz items
        //manager.DeleteAll<SavedQuiz>();

        LoadItems();
    }
    

    Toggle GetSelectedToggle()
    {
        if (group.ActiveToggles().ToList().Count == 1)
        {
            return group.ActiveToggles().ToList()[0];
        }
        else
        {
            return null;
        }
    }

    GameObject GetSelectedObject()
    {
        if (group.ActiveToggles().ToList().Count == 1)
        {
            return group.ActiveToggles().ToList()[0].gameObject;
        }
        else
        {
            return null;
        }
    }

    SavedQuiz GetSelecetedQuiz()
    {
        if (group.ActiveToggles().ToList().Count == 1)
        {
            var toggle = group.ActiveToggles().ToList()[0];
            var qItem = toggle.gameObject.GetComponent<QuizItem>();
            return qItem.quiz;
        }
        else
        {
            return null;
        }
    }
    void RegisterToggle(Toggle toggle)
    {
        toggle.group = group;
        toggle.onValueChanged.AddListener(HandleToggleValueChanged);
    }

    void HandleToggleValueChanged(bool isOn)
    {
        onActiveTogglesChanged?.Invoke();
    }

    void UnregisterToggle(Toggle toggle)
    {
        toggle.onValueChanged.RemoveListener(HandleToggleValueChanged);
        toggle.group = null;
    }

    private void RemoveItems()
    {
        foreach (Transform t in quizList.transform)
        {
            UnregisterToggle(t.GetComponent<QuizItem>().toggle);
            Destroy(t.gameObject);
        }
    }

    public void RemoveItem()
    {
        var item = GetSelectedObject();
        var toggle = GetSelectedToggle();
        var quiz = GetSelecetedQuiz();
        UnregisterToggle(toggle);
        Debug.Log("Delteted quiz "+quiz.name+": "+manager.DeleteItem(quiz));
        Destroy(item);
        OnSelectedChanged();
    }

    private void LoadItems()
    {
        //remove all items from list
        RemoveItems();
        //get all quizes
        var quizes = manager.GetAll<SavedQuiz>();
        //add them to quizlist
        foreach (var t in quizes)
        {
            var item = Instantiate(quizItem, quizList.transform);
            var qItem = item.GetComponent<QuizItem>();
            qItem.quiz = t;
            RegisterToggle(qItem.toggle);
        }
    }

    public void StartQuiz()
    {
        
        if (selected != null)
        {
            //set up quiz master
            qmaster.NewQuiz(selected);
            //start selected quiz
            page.ShowView(quizTest);
        }
    }
    
    public void OnSelectedChanged()
    {
        if (selected != null)
        {
            //enable buttons
            foreach (var b in buttons)
            {
                b.interactable = true;
            }
        }
        else
        {
            //disable buttons
            foreach (var b in buttons)
            {
                b.interactable = false;
            }
        }
    }
}
