using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>Controls the behavior of the QuizSelect View.</summary>
public class QuizSelect : View
{
    /// <summary>Event for when a toggle's value changes.</summary>
    [SerializeField]
    public UnityEvent onActiveTogglesChanged;

    /// <summary>Gets the database manager.</summary>
    /// <value>The database manager.</value>
    DBManager manager => AppController.instance.manager;

    /// <summary>The QuizMaster view.</summary>
    public QuizMaster qmaster;

    /// <summary>The quiz list.</summary>
    public GameObject quizList;
    /// <summary>The quiz item
    /// prefab.</summary>
    public GameObject quizItem;

    /// <summary>The quiz list buttons.</summary>
    public List<Button> buttons;
    /// <summary>The start button text.</summary>
    public Text startBtnTxt;
    /// <summary>Gets the selected quiz.</summary>
    /// <value>The selected quiz.</value>
    public SavedQuiz selected => GetSelectedQuiz();

    /// <summary>A toggle group for the quiz list.</summary>
    public ToggleGroup group;

    private void OnEnable()
    {
        //reload all quiz items
        LoadItems();
    }


    /// <summary>Gets the selected toggle.</summary>
    /// <returns>Selected toggle.</returns>
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

    /// <summary>Gets the selected object.</summary>
    /// <returns>Selected object.</returns>
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

    /// <summary>Gets the selected quiz.</summary>
    /// <returns>Selected quiz.</returns>
    SavedQuiz GetSelectedQuiz()
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
    /// <summary>Registers the toggle to report when its value changes.</summary>
    /// <param name="toggle">The toggle to register.</param>
    void RegisterToggle(Toggle toggle)
    {
        toggle.group = group;
        toggle.onValueChanged.AddListener(HandleToggleValueChanged);
    }

    /// <summary>Handles the toggle value changed event.</summary>
    /// <param name="isOn">Whether the toggle is on or off.</param>
    void HandleToggleValueChanged(bool isOn)
    {
        onActiveTogglesChanged?.Invoke();
    }

    /// <summary>Unregisters the toggle from reporting when its value changes.</summary>
    /// <param name="toggle">The toggle to unregister.</param>
    void UnregisterToggle(Toggle toggle)
    {
        if (toggle.onValueChanged != null)
        {
            toggle.onValueChanged.RemoveListener(HandleToggleValueChanged);
        }
        toggle.group = null;
    }

    /// <summary>Removes all items from quiz list.</summary>
    private void RemoveItems()
    {
        foreach (Transform t in quizList.transform)
        {
            UnregisterToggle(t.GetComponent<QuizItem>().toggle);
            Destroy(t.gameObject);
        }
    }

    /// <summary>Removes the currently selected item.</summary>
    public void RemoveItem()
    {
        if(selected != null)
        {
            var item = GetSelectedObject();
            var toggle = GetSelectedToggle();
            var quiz = GetSelectedQuiz();
            UnregisterToggle(toggle);
            Debug.Log("Delteted quiz "+quiz.name+": "+manager.DeleteItem(quiz));
            Destroy(item);
            OnSelectedChanged();
        }
        
    }

    /// <summary>Repopulates the quiz list.</summary>
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

    /// <summary>Starts the selected quiz.</summary>
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

    /// <summary>Navigates to the new quiz view.</summary>
    public void NewQuiz()
    {
        page.GotoView(page.views.Find((v) => v.GetType() == typeof(NewQuiz)));
    }

    /// <summary>Called when selected quiz changes.</summary>
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
