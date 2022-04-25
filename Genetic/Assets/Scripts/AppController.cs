using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AppController : Singleton<AppController>
{
    public DBManager manager;

    public PaletteController controller;
    protected override void Awake()
    {
        base.Awake();
        //Ensure object is not destroyed
        DontDestroyOnLoad(gameObject);
        //do other app startup stuff here

    }
}
