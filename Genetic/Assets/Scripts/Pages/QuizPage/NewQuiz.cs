using App;
using App.Models.DataBase;
using App.Pages;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace App.Pages.QuizPage
{
/// <summary>Represents a new quiz view.</summary>
public class NewQuiz : View
{
    /// <summary>An event for handling toggle value changes.</summary>
    [SerializeField]
    public UnityEvent onActiveTogglesChanged;
    /// <summary>Gets the database manager.</summary>
    /// <value>The database manager.</value>
    DBManager manager => AppController.instance.manager;
    /// <summary>The gameobject containing the quiz select
    /// view.</summary>
    public GameObject quizSelect;

    /// <summary>The gameobject that will parent the tag list items.</summary>
    public GameObject tagList;

    /// <summary>The tag list item prefab.</summary>
    public GameObject tagItem;

    /// <summary>The quiz name.</summary>
    public InputField quizName;

    /// <summary>The save button.</summary>
    public Button saveButton;

    /// <summary>Gets the TagItem components from the tag list.</summary>
    /// <value>The TagItem list.</value>
    private List<TagItem> items => tagList.GetComponentsInChildren<TagItem>().ToList();

    /// <summary>Finds selected toggles from items.</summary>
    /// <value>The selected items.</value>
    private List<TagItem> selected => items.FindAll((t) => t.toggle.isOn);
    /// <summary>Gets the tags selected and turns them into a string.</summary>
    /// <value>The tags as a string.</value>
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

    /// <summary>Gets a default name.</summary>
    /// <value>The default name.</value>
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
    /// <summary>Regex placeholder.</summary>
    Regex r;
    /// <summary>MatchCollection placeholder.</summary>
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
        //load tags
        LoadTags();
    }
    /// <summary>Registers the toggle to report value changes.</summary>
    /// <param name="toggle">The toggle to register.</param>
    void RegisterToggle(Toggle toggle)
    {
        toggle.onValueChanged.AddListener(HandleToggleValueChanged);
    }

    /// <summary>Handles the toggle value changed event.</summary>
    /// <param name="isOn">if set to <c>true</c> [is on].</param>
    void HandleToggleValueChanged(bool isOn)
    {
        onActiveTogglesChanged?.Invoke();
    }

    /// <summary>Unregisters the toggle from reporting value changes.</summary>
    /// <param name="toggle">The toggle to unregister.</param>
    void UnregisterToggle(Toggle toggle)
    {
        toggle.onValueChanged.RemoveListener(HandleToggleValueChanged);
    }

    /// <summary>Removes all tags in the tag list.</summary>
    private void RemoveTags()
    {
        foreach(Transform t in tagList.transform)
        {
            UnregisterToggle(t.GetComponent<TagItem>().toggle);
            Destroy(t.gameObject);
        }
    }

    /// <summary>Repopulates the tag list.</summary>
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

    /// <summary>Saves the quiz.</summary>
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

    /// <summary>Validates the quiz information.</summary>
    /// <param name="count">The number of saved sets with the same name.
    /// Starts at 1, is used to create a default name.</param>
    /// <returns><c>True</с> when quiz info is valid; otherwise <c>False</с></returns>
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

    /// <summary>Called when a toggle's value has changed.</summary>
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

}

