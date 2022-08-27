using App.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace App.Models.DataBase
{
/// <summary>Manages the local database.</summary>
public class DBManager : Singleton<DBManager>
{

    /// <summary>Gets the name of the database.</summary>
    /// <value>The name of the database. Currently 'local.db'.</value>
    public string dbName
    {
        get { return $"local.db"; }
    }
    /// <summary>Gets the database path.</summary>
    /// <value>The database path dynamically.</value>
    public string dbPath
    {
        get
        {
            var path = new Uri(Path.Combine(Application.persistentDataPath, dbName));
            Debug.Log(path);
            return path.LocalPath;
        }
    }

    /// <summary>The local database.</summary>
    public SQLite.SQLiteConnection db;

    /// <summary>Called when object is instantiated.</summary>
    protected override void Awake()
    {
        base.Awake();
        //connect to local DB
        ConnectDB();
    }

    /// <summary>Connects to the local database. Encryption key is hard coded at the moment.</summary>
    public void ConnectDB()
    {
        //ensure previous db connection is disconnected
        Disconnect();
        //start new connection to db
        var options = new SQLite.SQLiteConnectionString(dbPath, true, key: "jfkSD0f9&3.dYrv");
        db = new SQLite.SQLiteConnection(options);
        CreateTables();
        Debug.Log("Connected to Local DB");
    }

    /// <summary>Disconnects from the local database.</summary>
    public void Disconnect()
    {
        //ensure previous db connection is disconnected
        if (db != null)
        {
            db.Close();
            db = null;
        }
    }

    /// <summary>Creates the tables necessary to store user info.</summary>
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
            db.CreateTable<Flashcard>();
            db.CreateTable<FlashcardTag>();
            db.CreateTable<SavedQuiz>();
            Debug.Log("Tables Created/Present");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            Debug.Log(e.StackTrace);
        }
    }

    /// <summary>Adds an item to the database.</summary>
    /// <typeparam name="T">Type of table to insert data into.</typeparam>
    /// <param name="item">The item to insert.</param>
    /// <returns>If the row was added.</returns>
    public bool AddItem<T>(T item) where T : new()
    {
        int rowsAdded = db.Insert(item);
        Debug.Log($"{rowsAdded} rows were added.");
        return rowsAdded == 1;
    }

    /// <summary>Adds multiple items to the local database.</summary>
    /// <typeparam name="T">Type of table to insert data into.</typeparam>
    /// <param name="items">The items to add.</param>
    /// <returns>True if the items were added.</returns>
    public bool AddItems<T>(List<T> items) where T : new()
    {

        int rowsAdded = db.InsertAll(items);
        Debug.Log($"{rowsAdded} rows were added.");
        return rowsAdded == items.Count;
    }

    /// <summary>Updates an already existing item.</summary>
    /// <typeparam name="T">Type of table to update item in.</typeparam>
    /// <param name="item">The item to update.</param>
    /// <returns>True if the item was updated.</returns>
    public bool UpdateItem<T>(T item) where T : new()
    {
        int rowsUpdated = db.Update(item);
        Debug.Log($"{rowsUpdated} rows were updated.");
        return rowsUpdated >= 1;
    }

    /// <summary>Updates multiple already existing items in database.</summary>
    /// <typeparam name="T">Type of table to update items in.</typeparam>
    /// <param name="items">The items to update.</param>
    /// <returns>True if the items were updated.</returns>
    public bool UpdateItems<T>(List<T> items) where T : new()
    {
        int rowsUpdated = db.UpdateAll(items);
        Debug.Log($"{rowsUpdated} rows were updated.");
        return rowsUpdated >= items.Count;
    }

    /// <summary>Gets all items in a table.</summary>
    /// <typeparam name="T">Type of table to get all items from.</typeparam>
    /// <returns>All items in a list.</returns>
    public List<T> GetAll<T>() where T : new()
    {
        return db.Table<T>().Where(x => 1 == 1).ToList();
    }
    /// <summary>Deletes <em>all</em> items in a table.</summary>
    /// <typeparam name="T">Type of table to delete <em>all</em> items in.</typeparam>
    /// <returns>True if all rows or more were deleted.</returns>
    public bool DeleteAll<T>() where T : new()
    {
        int count = db.Table<T>().Count();
        int rowsDeleted = db.DeleteAll<T>();
        Debug.Log($"{rowsDeleted} rows were deleted.");
        return rowsDeleted >= count;
    }

    /// <summary>Gets a list of items based on a predicate.</summary>
    /// <typeparam name="T">Type of table to get items from.</typeparam>
    /// <param name="condition">The predicate. An anonymous function that returns true when a condition is met. This function is converted into SQL statements.</param>
    /// <returns>A list of objects that met the predicate's condition.</returns>
    public List<T> GetItems<T>(Expression<Func<T, bool>> condition) where T : new()
    {
        return db.Table<T>().Where(condition).ToList();
    }

    /// <summary>Gets the first item that meets the condition, or null if there is none.</summary>
    /// <typeparam name="T">Type of table to get an item from.</typeparam>
    /// <param name="condition">The search condition. An anonymous function that returns true when a condition is met.</param>
    /// <returns>The first match</returns>
    public T GetItem<T>(Expression<Func<T, bool>> condition) where T : new()
    {
        return db.Table<T>().FirstOrDefault(condition);
    }

    /// <summary>Deletes an item from a table.</summary>
    /// <typeparam name="T">Type of table to delete an item from.</typeparam>
    /// <param name="item">The item to delete.</param>
    /// <returns>True if the rows removed were 1 or more.</returns>
    public bool DeleteItem<T>(T item) where T : new()
    {
        int rowsRemoved = db.Delete(item);
        return rowsRemoved >= 1;
    }

    /// <summary>Deletes multiple items from a table.</summary>
    /// <typeparam name="T">Type of table to delete items from.</typeparam>
    /// <param name="items">The items to delete.</param>
    /// <returns>True if the rows deleted were larger than or equal to the number of items deleted.</returns>
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

}

