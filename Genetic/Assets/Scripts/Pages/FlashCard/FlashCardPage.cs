using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public bool SetNewFlashCard(Flashcard flashcard, Definition definition)
    {
        if (flashcard == null || definition == null) return false;

        GameObject obj = Instantiate(flashCardPrefab);
        obj.transform.SetParent(contentView.gameObject.transform);
        obj.transform.localScale = new Vector3(1, 1, 1);
        FlashCardMaster flashCardMaster = obj.GetComponent<FlashCardMaster>();
        flashCardMaster.FlashCard = flashcard.text;
        flashCardMaster.Definition = definition.text;
        return true;
    }

    public List<Tag> SearchTag(string tag) {
        List<Tag> currTag = new List<Tag>();
        currTag = manager.GetItems<Tag>(q => q.tag == tag);
        return currTag;
    }

    public List<FlashcardTag> getFlashCardTags(Tag tag)
    {
        return manager.GetItems<FlashcardTag>(q => q.tag_id == tag.id);
    }

    public FlashcardDefinition getFlashcardDefinition(FlashcardTag flashCardTag)
    {
        return manager.GetItem<FlashcardDefinition>(q => q.id == flashCardTag.flashCardDefinition_id);
    }

    public Flashcard getFlashcard(FlashcardDefinition flashcardDefinition)
    {
        return manager.GetItem<Flashcard>(q => q.id == flashcardDefinition.flashCard_id);
    }

    public Definition getDefinition(FlashcardDefinition flashcardDefinition)
    {
        return manager.GetItem<Definition>(q => q.id == flashcardDefinition.definition_id);
    }
    public void Search()
    {
        string input = inputSearch.text;
        tag_list = SearchTag(input);
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
            FlashcardDefinition flashcardDefinition = getFlashcardDefinition(flashcardTag);
            Flashcard flashcard = getFlashcard(flashcardDefinition);
            Definition definition = getDefinition(flashcardDefinition);
            SetNewFlashCard(flashcard, definition);
        }
    }
}
