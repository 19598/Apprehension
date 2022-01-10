using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    public GameObject Leech;
    public PlayerController player;
    public Database db;
    public List<GameObject> keys;

    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
        EmailCrashReport.SendEmailReport("brendeg39@gmail.com");
    }

    /// <summary>
    /// Saves the game
    /// </summary>
    /// <param name="saveName">Name of the save</param>
    public void Save(string saveName)
    {
        SaveGame.SaveEnemies(GameObject.FindObjectsOfType<EnemyClass>(), saveName);
        player.saveGame(saveName);
        //Whenever the game is saved, it has a recent save so the most recent save is always known
        SaveGame.SaveEnemies(GameObject.FindObjectsOfType<EnemyClass>(), "recent");
        player.saveGame("recent");
        saveItems(saveName);
        saveItems("recent");
    }

    /// <summary>
    /// Saves the items in the player's inventory
    /// </summary>
    /// <param name="saveName">Name of the save</param>
    public void saveItems(string saveName)
    {
        db.resetDB(saveName);
        foreach (GameObject key in player.keys)
        {
            db.addItem(saveName, key.GetComponent<Key>().name, 1);
        }
    }

    /// <summary>
    /// Loads desired save
    /// </summary>
    /// <param name="saveName">Name of the save</param>
    public void Load(string saveName)
    {
        player.loadGame(saveName);
        loadItems(saveName);
        //destroy enemies and place new ones
        foreach (EnemyClass enemy in GameObject.FindObjectsOfType<EnemyClass>())
        {
            Destroy(enemy.gameObject);
        }
        List<float[]> enemyArray = SaveGame.LoadEnemies(saveName);
        /* Enemy Data takes the following form
         * position x|y|z| rotation x|y|z|w| health| type|
         *          0|1|2|          3|4|5|6|      7|    8|
         */
        //for every saved leech, create a leech and assign it data
        foreach (float[] enemyData in enemyArray)
        {
            //Leech
            if (enemyData[8] == 0)
            {
                GameObject newLeech = Instantiate(Leech, new Vector3(enemyData[0], enemyData[1], enemyData[2]), new Quaternion(enemyData[3], enemyData[4], enemyData[5], enemyData[6]));
                LeechAI newLeechAI = newLeech.GetComponent<LeechAI>();
                //newLeechAI.playerHealth = FindObjectOfType<Health>();
                newLeechAI.setHealth(enemyData[7]);
            }
        }
    }

    /// <summary>
    /// Loops through all the keys and checks if any were saved. If they were, add them to the player's inventory
    /// </summary>
    /// <param name="saveName">Name of the save</param>
    public void loadItems(string saveName)
    {
        //player.keys.Clear();
        foreach (string[] name in db.getInventory(saveName))
        {
            foreach (GameObject key in keys)
            {
                if (key.GetComponent<Key>().name == name[0] && float.Parse(name[1]) > 0)
                {
                    player.keys.Add(key);
                    key.SetActive(false);
                }
            }
        }
    }
}