using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuizSelect : View
{
    [System.Serializable]
    public class ToggleEvent : UnityEvent { }
    [SerializeField]
    public ToggleEvent onActiveTogglesChanged;

    DBManager manager => AppController.instance.manager;

    public QuizMaster qmaster;

    public GameObject quizList;
    public GameObject quizItem;

    public List<Button> buttons;
    public Text startBtnTxt;
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
        if (toggle.onValueChanged != null)
        {
            toggle.onValueChanged.RemoveListener(HandleToggleValueChanged);
        }
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
        foreach (var t in quizes)
        {
            var item = Instantiate(quizItem, quizList.transform);
            //add item to list
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
            qmaster.StartQuiz(selected);
            //start selected quiz
            page.GotoView(page.views.Find((v) => v.GetType() == typeof(QuizMaster)));
        }
    }

    public void NewQuiz()
    {
        page.GotoView(page.views.Find((v) => v.GetType() == typeof(NewQuiz)));
    }

    public void Select()
    {
        page.GotoView(page.views.Find((v) => v == this));
    }
    
    public void OnSelectedChanged()
    {
        if (selected != null)
        {
            if (selected.inProgress)
            {
                //change button text
                startBtnTxt.text = "Resume Set";
            }
            else
            {
                startBtnTxt.text = "Start Set";
            }
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
