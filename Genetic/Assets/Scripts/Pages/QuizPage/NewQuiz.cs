using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public QuizPage page;
    public GameObject quizSelect;

    public GameObject tagList;

    public GameObject tagItem;

    public Text quizName;
    public Text search;

    public Button saveButton;

    private List<TagItem> items => tagList.GetComponentsInChildren<TagItem>().ToList();

    private List<TagItem> selected => items.FindAll((t) => t.toggle.isOn );

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
            Debug.Log("");
            string allTags = "";
            foreach (var s in selected)
            {
                allTags += s.tagText.text+",";
            }
            //remove trailing comma
            allTags.Substring(0, allTags.Length-1);
            SavedQuiz quiz = new SavedQuiz() { name = quizName.text, tags=allTags};
            //save new quiz to db
            manager.AddItem(quiz);
            //go back to quiz select
            page.GotoView(quizSelect);
        }
    }

    private bool ValidateInfo()
    {
        //check if name exists
        if (quizName.text.Length > 0)
        {
            //check if at least one tag is selected
            if (selected.Count > 0)
            {
                return true;
            }
        }
        return false;
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
