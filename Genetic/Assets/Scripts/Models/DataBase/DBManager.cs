using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class DBManager : Singleton<DBManager>
{

    public string dbName
    {
        get { return $"local.db"; }
    }
    public string dbPath
    {
        get
        {
            var path = new Uri(Application.dataPath+@"/Scripts/Models/DataBase/"+dbName);
            return path.AbsolutePath;
        }
    }

    public SQLite.SQLiteConnection db;

    protected override void Awake()
    {
        base.Awake();
        ConnectDB();
    }


    public void ConnectDB()
    {
        //ensure previous db connection is disconnected
        if (!(db == null))
        {
            db.Close();
            db = null;
        }
        //start new connection to db
        var options = new SQLite.SQLiteConnectionString(dbPath, true, key: "jfkSD0f9&3.dYrv");
        db = new SQLite.SQLiteConnection(options);
        CreateTables();
        Debug.Log("Connected to Local DB");
    }


    public void CreateTables()
    {
        try
        {
            //ensure tables are created
            db.CreateTable<Question>();
            db.CreateTable<Answer>();
            db.CreateTable<CorrectAnswer>();
            db.CreateTable<IncorrectAnswer>();
            db.CreateTable<Tag>();
            db.CreateTable<QuestionTag>();
            db.CreateTable<AnswerTag>();
            db.CreateTable<Hint>();
            Debug.Log("Tables Created/Present");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            Debug.Log(e.StackTrace);
        }
    }

    public bool AddItem<T>(T item) where T : new()
    {
        int rowsAdded = db.Insert(item);
        Debug.Log($"{rowsAdded} rows were added.");
        return rowsAdded == 1;
    }

    public bool AddItems<T>(List<T> items) where T : new()
    {

        int rowsAdded = db.InsertAll(items);
        Debug.Log($"{rowsAdded} rows were added.");
        return rowsAdded == items.Count;
    }

    public bool UpdateItem<T>(T item) where T : new()
    {
        int rowsUpdated = db.Update(item);
        Debug.Log($"{rowsUpdated} rows were updated.");
        return rowsUpdated >= 1;
    }

    public bool UpdateItems<T>(List<T> items) where T : new()
    {
        int rowsUpdated = db.UpdateAll(items);
        Debug.Log($"{rowsUpdated} rows were updated.");
        return rowsUpdated >= items.Count;
    }

    public List<T> GetAll<T>() where T : new()
    {
        return db.Table<T>().Where(x => 1 == 1).ToList();
    }
    public bool DeleteAll<T>() where T : new()
    {
        int count = db.Table<T>().Count();
        int rowsDeleted = db.DeleteAll<T>();
        Debug.Log($"{rowsDeleted} rows were deleted.");
        return rowsDeleted >= count;
    }

    public List<T> GetItems<T>(Expression<Func<T, bool>> condition) where T : new()
    {
        return db.Table<T>().Where(condition).ToList();
    }

    public T GetItem<T>(Expression<Func<T, bool>> condition) where T : new()
    {
        return db.Table<T>().FirstOrDefault(condition);
    }

    public bool DeleteItem<T>(T item) where T : new()
    {
        int rowsRemoved = db.Delete(item);
        return rowsRemoved >= 1;
    }

    public bool DeleteItems<T>(List<T> items) where T : new()
    {
        int success = 0;
        foreach (var item in items)
        {
            success += db.Delete(item);
        }
        return success >= items.Count;
    }
}
