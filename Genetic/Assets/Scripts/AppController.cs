using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AppController : Singleton<AppController>
{
    public TextAsset[] question_bank;
    public CSVIngress csvIngress;

    public List<QuestionCSV> questionList;
    protected override void Awake()
    {
        base.Awake();
        //Ensure object is not destroyed
        DontDestroyOnLoad(gameObject);
        //Load questions
        foreach (var csv in question_bank)
        {
            csvIngress.Import(csv);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
