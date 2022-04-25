using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NewQuiz : View
{
    [System.Serializable]
    public class ToggleEvent : UnityEvent { }
    [SerializeField]
    public ToggleEvent onActiveTogglesChanged;
    DBManager manager => AppController.instance.manager;
    public GameObject quizSelect;

    public GameObject tagList;

    public GameObject tagItem;

    public InputField quizName;
    public Text search;

    public Button saveButton;

    private List<TagItem> items => tagList.GetComponentsInChildren<TagItem>().ToList();

    private List<TagItem> selected => items.FindAll((t) => t.toggle.isOn);
    private string tags
    {
        get
        {
            if(selected.Count > 0){
                string allTags = "";
                foreach (var s in selected)
                {
                    allTags += s.tagText.text + ",";
                }
                //remove trailing comma
                return allTags.Substring(0, allTags.Length - 1);
            }
            else
            {
                return "";
            }

        }
    }

    string defaultName
    {
        get
        {
            if (selected.Count > 0)
            {
                string allTags = "";
                foreach (var s in selected)
                {
                    allTags += s.tagText.text + ", ";
                }
                //remove trailing comma and space
                return allTags.Substring(0, allTags.Length - 2);
            }
            else
            {
                return "";
            }
        }
    }

    //create name check regex
    Regex r;
    MatchCollection m;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Deleted all saved quizes: "+manager.DeleteAll<SavedQuiz>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        //clear text input only when enabled again
        quizName.text = "";
        search.text = "";
        //load tags
        LoadTags();
    }
    void RegisterToggle(Toggle toggle)
    {
        toggle.onValueChanged.AddListener(HandleToggleValueChanged);
    }

    void HandleToggleValueChanged(bool isOn)
    {
        onActiveTogglesChanged?.Invoke();
    }

    void UnregisterToggle(Toggle toggle)
    {
        toggle.onValueChanged.RemoveListener(HandleToggleValueChanged);
    }

    private void RemoveTags()
    {
        foreach(Transform t in tagList.transform)
        {
            UnregisterToggle(t.GetComponent<TagItem>().toggle);
            Destroy(t.gameObject);
        }
    }

    private void LoadTags()
    {
        //remove all items from list
        RemoveTags();
        //get all tags
        var tags = manager.GetAll<Tag>();
        //add them to taglist
        foreach (var t in tags)
        {
            var item = Instantiate(tagItem, tagList.transform);
            var tItem = item.GetComponent<TagItem>();
            tItem.tagText.text = t.tag;
            RegisterToggle(tItem.toggle);
        }
    }

    public void SaveQuiz()
    {
        //check if quiz info is valid
        if (ValidateInfo())
        {
            SavedQuiz quiz = new SavedQuiz() { name = quizName.text, tags=tags, currentAttempt=0, currentQuestion=0, inProgress=false, questionOrder="", givenAnswers=""};
            //save new quiz to db
            manager.AddItem(quiz);
            //go back to quiz select
            page.GotoView(quizSelect);
        }
    }

    private bool ValidateInfo(int count = 1)
    {
        //check if at least one tag is selected
        if (selected.Count > 0)
        {
            //check if name exists
            if (string.IsNullOrEmpty(quizName.text))
            {
                //give item a default name
                quizName.text = defaultName;
            }
            //check if name exists already
            var matches = manager.GetItems<SavedQuiz>((q) => q.name == quizName.text);

            if (matches != null && matches.Count > 0)
            {
                if (quizName.text.EndsWith($" {count-1}"))
                {
                    int digits = Mathf.FloorToInt(Mathf.Log10(count-1) + 1);
                    quizName.text = quizName.text.Substring(0, quizName.text.Length - digits - 1)+" "+count;
                }
                else
                {
                    quizName.text = quizName.text + " " + count;
                }
                return ValidateInfo(count+1);
            }
            else
            {
                return true;
            }
        }
        
        return false;
    }

    public bool RegexCallback(SavedQuiz q)
    {
        r = new Regex(@$"{q.name} (\d+)(?!.*\d)", RegexOptions.IgnoreCase);
        m = r.Matches(q.name);
        return m.Count > 0;
    }

    public void OnToggleUpdate()
    {
        if(selected.Count > 0)
        {
            //enable save button
            saveButton.interactable = true;
        }
        else
        {
            //disable save button
            saveButton.interactable = false;
        }
    }
}
