using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class FlashCardPage : PageController
{
    static DBManager manager { get => DBManager.instance; }
    public GameObject flashCardPrefab;

    public GameObject contentView;
    public TMP_InputField inputSearch;

    private List<Tag> tag_list;
    private List<FlashcardTag> flashcardTag_list;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        flashcardTag_list = new List<FlashcardTag>();
        ExpandContenView();
    }

    public void ExpandContenView()
    {
        RectTransform rect = contentView.GetComponent<RectTransform>();
        if(flashcardTag_list.Count * 400 > rect.sizeDelta.y)
        {
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, (flashcardTag_list.Count + 1) * 400);
        }
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
        return manager.GetItems<FlashcardTag>(q => q.tag_id == tag.id);
    }

    public Flashcard getFlashcard(FlashcardTag flashcardTag)
    {
        return manager.GetItem<Flashcard>(q => q.id == flashcardTag.flashCard_id);
    }
    public void Search()
    {
        string input = inputSearch.text.ToLower();

        List<string> inputAr = input.Split(',').ToList();
        foreach(string user_input in inputAr)
        {
            tag_list.Add(SearchTag(user_input));
        }
        Intialize(tag_list);
    }
    public void Intialize(List<Tag> tag_list)
    {
        foreach (Tag tag in tag_list)
        {
            flashcardTag_list = getFlashCardTags(tag);
        }

        foreach(FlashcardTag flashcardTag in flashcardTag_list)
        {
            Flashcard flashCard = getFlashcard(flashcardTag);
            SetNewFlashCard(flashCard.text, flashCard.definition);
        }
    }
}
