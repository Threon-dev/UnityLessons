using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataManager : MonoBehaviour,IGameManager
{
    public ManagerStatus status { get; private set; }
    private string _filename;
    private NetworkSerivce _network;
    
    public void Startup(NetworkSerivce serivce)
    {
        Debug.Log("Data manager starting...");

        _network = serivce;
        _filename = Path.Combine(Application.persistentDataPath, "game.dat");
        status = ManagerStatus.Started;
    }
    public void SaveGameState()
    {
        Dictionary<string, object> gamestate = new Dictionary<string, object>();

        gamestate.Add("inventory", Managers.Inventory.GetData());
        gamestate.Add("Health", Managers.Player.health);
        gamestate.Add("maxHealth", Managers.Player.maxHealth);
        gamestate.Add("curLevel", Managers.Mission.curLevel);
        gamestate.Add("maxLevel", Managers.Mission.maxLevel);

        FileStream stream = File.Create(_filename);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, gamestate);
        stream.Close();
    }
    public void LoadGameState()
    {
        if (!File.Exists(_filename))
        {
            Debug.Log("No saved game");
            return;
        }
        Dictionary<string, object> gamestate;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = File.Open(_filename, FileMode.Open);
        gamestate = formatter.Deserialize(stream) as Dictionary<string, object>;
        stream.Close();

        Managers.Inventory.UpdateData((Dictionary<string, int>)gamestate["inventory"]);
        Managers.Player.UpdateData((int)gamestate["Health"], (int)gamestate["maxHealth"]);
        Managers.Mission.UpdateData((int)gamestate["curLevel"], (int)gamestate["maxLevel"]);
        Managers.Mission.RestartCurrent();
    }
}