using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    public GameObject Leech;
    public GameObject BigBoy;
    public PlayerController player;
    public Database db;
    public List<GameObject> keys;

    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
    }

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

    public void saveItems(string saveName)
    {
        foreach (GameObject key in player.keys)
        {
            db.addItem(saveName, key.GetComponent<Key>().name, 1);
        }
    }

    public void Load(string saveName)
    {
        player.loadGame(saveName);
        loadItems(saveName);
        foreach (EnemyClass enemy in GameObject.FindObjectsOfType<EnemyClass>())
        {
            Destroy(enemy);
        }
        List<float[]> enemyArray = SaveGame.LoadEnemies(saveName);
        /* Enemy Data takes the following form
         * position x|y|z| rotation x|y|z|w| health| type|
         *          0|1|2|          3|4|5|6|      7|    8|
         */
        foreach (float[] enemyData in enemyArray)
        {
            //Leech
            if (enemyData[8] == 0)
            {
                GameObject newLeech = Instantiate(Leech, new Vector3(enemyData[0], enemyData[1], enemyData[2]), new Quaternion(enemyData[3], enemyData[4], enemyData[5], enemyData[6]));
                LeechAI newLeechAI = newLeech.GetComponent<LeechAI>();
                newLeechAI.playerHealth = FindObjectOfType<Health>();
                newLeechAI.setHealth(enemyData[7]);
            }
        }
    }

    public void loadItems(string saveName)
    {
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