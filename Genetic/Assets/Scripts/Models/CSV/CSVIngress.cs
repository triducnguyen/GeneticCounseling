using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using CSVHelper;
using UnityEditor;
using CsvHelper;
using System.Globalization;
using System.Linq;
using System;

public class CSVIngress : Singleton<CSVIngress>
{
    public static DBManager manager { get => DBManager.instance; }

    public void Import(TextAsset csvfile)
    {
        try
        {
            List<QuestionCSV> questions = new List<QuestionCSV>();
            Question question;
            List<Answer> answers;
            List<Tag> tags = new List<Tag>();

            using (var reader = new StringReader(csvfile.text))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                questions = csv.GetRecords<QuestionCSV>().ToList();
                //convert QuestionCSV to DB Records
                foreach (var q in questions)
                {
                    //clear lists
                    answers = new List<Answer>();
                    tags = new List<Tag>();
                    //set question
                    question = new Question() { text = q.Question };
                    //get all answers to this question
                    answers = new List<string>
                    {
                        q.CorrectAnswer,
                        q.Answer1,
                        q.Answer2,
                        q.Answer3,
                        q.Answer4,
                        q.Answer5,
                    }.Select(a => a != null && a != "" ? new Answer() { text = a } : null).ToList();
                    //remove all null and empty answers
                    answers.RemoveAll(a => a == null);
                    //get all tags
                    tags = q.Tags.Split(',').Select(t => new Tag() { tag = t }).ToList();

                    //check if question, answers, and tags already exist
                    var existingQuestion = manager.GetItem<Question>(q => q.text == question.text);
                    if (existingQuestion == null)
                    {
                        //add new question
                        manager.AddItem(question);
                    }
                    else
                    {
                        //make current question the existing one
                        question = existingQuestion;
                    }
                    for (var i = answers.Count - 1; i >= 0; i--)
                    {
                        Answer answ = answers[i];
                        var existingAnsw = manager.GetItem<Answer>(a => a.text == answ.text);
                        if (existingAnsw == null)
                        {
                            //add answer to db
                            manager.AddItem(answ);
                        }
                        else
                        {
                            //update current answer to be existing one
                            answers[i] = existingAnsw;
                        }
                    }
                    for (var i = tags.Count - 1; i >= 0; i--)
                    {
                        Tag t = tags[i];
                        var existingT = manager.GetItem<Tag>(tg => tg.tag.Equals(t.tag));
                        if (existingT == null)
                        {
                            //add tag if it doesn't exist
                            manager.AddItem(t);
                        }
                        else
                        {
                            //update current tag to be the existing one
                            tags[i] = existingT;
                        }
                    }
                    //create relations between question, answer, and tag
                    //check if correct answer relation already exists
                    var aID = answers[0].id;
                    var existingCAnswer = manager.GetItem<CorrectAnswer>(ca => ca.questionID == question.id && ca.answerID == aID);
                    if (existingCAnswer == null)
                    {
                        //add new relation
                        manager.AddItem(new CorrectAnswer() { questionID = question.id, answerID = aID });
                    }
                    //check if incorrect answer relations exist
                    List<IncorrectAnswer> ianswers = new List<IncorrectAnswer>();
                    for (int i = 1; i < answers.Count; i++)
                    {
                        //check if relation exists already
                        var a = answers[i];
                        var existingIAnswer = manager.GetItem<IncorrectAnswer>(ia => ia.questionID == question.id && ia.answerID == a.id);
                        if (existingIAnswer == null)
                        {
                            //add new relation
                            manager.AddItem(new IncorrectAnswer() { questionID = question.id, answerID = a.id });
                        }
                    }
                    //check if question and answers have relations to tags
                    foreach (var t in tags)
                    {
                        //check if question is using this tag
                        var existingQTag = manager.GetItem<QuestionTag>(qt => qt.questionID == question.id && qt.tagID == t.id);
                        if (existingQTag == null)
                        {
                            QuestionTag qtag = new QuestionTag() { questionID = question.id, tagID = t.id };
                            //add new tag relation to db
                            manager.AddItem(qtag);
                        }
                        //check if answers are using this tag
                        foreach (var a in answers)
                        {
                            var existingATag = manager.GetItem<AnswerTag>(at => at.answerID == a.id && at.tagID == t.id);
                            if (existingATag == null)
                            {
                                //add new tag relations
                                manager.AddItem(new AnswerTag() { answerID = a.id, tagID = t.id });
                            }
                        }
                    }

                }
            }
            Debug.Log("Successfully imported CSV Quesitons");
        }
        catch(Exception e){
            //could not parse questions
            Debug.Log($"{e.Message}\nStackTrace:\n{e.StackTrace}\nInncerException:\n{e.InnerException}");
        }
    }
}
