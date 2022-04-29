using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class FlashCardPage : PageController
{
    static DBManager manager { get => DBManager.instance; }
    public GameObject flashCardPrefab;

    public GameObject contentView;
    public TMP_InputField inputSearch;
    public GameObject resButton;
    public GameObject searchView;
    private Trie searchTree;

    private List<Tag> tag_list;
    private List<FlashcardTag> flashcardTag_list;
    // Start is called before the first frame update
    void Start()
    {
        tag_list = new List<Tag>();
        flashcardTag_list = new List<FlashcardTag>();
        flashcardTag_list = new List<FlashcardTag>();
        searchTree = new Trie();
        ExpandContenView(10);
        InitializeFlashCards();
        inputSearch.onValueChanged.AddListener(delegate { SuggestedTags(); });
    }

    public void ExpandContenView(int size)
    {
        RectTransform rect = contentView.GetComponent<RectTransform>();
        if(size * 400 > rect.sizeDelta.y)
        {
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, (size + 1) * 400);
        }
    }

    public void InitializeFlashCards()
    {
        List<Flashcard> cards = manager.GetAll<Flashcard>();
        foreach(var card in cards)
        {
            //if(card.text != null)
            //    searchTree.insert(card.text.ToLower());
            SetNewFlashCard(card.term, card.definition);
        }
        ExpandContenView(cards.Count);
    }

    public bool SetNewFlashCard(string flashcard, string definition)
    {
        if (flashcard == null || definition == null) return false;

        GameObject obj = Instantiate(flashCardPrefab);
        obj.transform.SetParent(contentView.gameObject.transform);
        obj.transform.localScale = new Vector3(1, 1, 1);
        FlashCardMaster flashCardMaster = obj.GetComponent<FlashCardMaster>();
        flashCardMaster.FlashCard = flashcard;
        flashCardMaster.Definition = definition;
        return true;
    }

    public Tag SearchTag(string tag) {
        Tag currTag = new Tag();
        currTag = manager.GetItem<Tag>(q => q.tag == tag);
        return currTag;
    }

    public List<FlashcardTag> getFlashCardTags(Tag tag)
    {
        return manager.GetItems<FlashcardTag>(q => q.tagID == tag.id);
    }

    public Flashcard getFlashcard(FlashcardTag flashcardTag)
    {
        return manager.GetItem<Flashcard>(q => q.id == flashcardTag.flashcardID);
    }
    public void Search()
    {
        string input = inputSearch.text.ToLower();

        List<string> inputAr = input.Split(',').ToList();
        foreach(string user_input in inputAr)
        {
            if (SearchTag(user_input) != null)
            {
                
                tag_list.Add(SearchTag(user_input));
            }
        }
        Intialize(tag_list);
        DestroySuggestion();
    }
    public void Intialize(List<Tag> tag_list)
    {
            foreach (Tag tag in tag_list)
            {
                List<FlashcardTag> listCreatedFromTag = getFlashCardTags(tag);
                Debug.Log(listCreatedFromTag.Count);

            foreach (FlashcardTag fl in listCreatedFromTag)
                {
                    Debug.Log("In here");
                    flashcardTag_list.Add(fl);
                }
                
            }

        if(flashcardTag_list.Count >0)
            foreach(FlashcardTag flashcardTag in flashcardTag_list)
            {
                Flashcard flashCard = getFlashcard(flashcardTag);
                SetNewFlashCard(flashCard.term, flashCard.definition);
            }
    }

    private void DestroySuggestion()
    {
        if (searchView.transform.childCount > 0)
        {
            foreach (Transform child in searchView.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void SuggestedTags()
    {
        string current = inputSearch.text;
        DestroySuggestion();
        foreach (string output in searchTree.GetWordsStartingWith(current))
        {
            if (output == "")
            {
                DestroySuggestion();
                break;
            }
                
            GameObject obj = Instantiate(resButton);
            Button btn = obj.GetComponent<Button>();
            btn.onClick.AddListener(delegate { CreateSearch(output); });
            obj.transform.SetParent(searchView.gameObject.transform);
            obj.transform.localScale = new Vector3(1, 1, 1);
            obj.GetComponentInChildren<Text>().text = output;
            
        }
        
    }

    public void CreateSearch(string word)
    {
        inputSearch.text = word;
        Search();
     
    }
     
}
