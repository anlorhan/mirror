using Mirror;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;

public class IP
{
    public string ip;
}

public class MyNetworkManager : NetworkManager
{

    //public static int PickedSkinId;
    //public static SkinHolder skin;
    NetworkManager manager;

    
    public override void Awake()
    {
        //StartServer();
        manager = GetComponent<NetworkManager>();
        GetIp();
    }

    private IP data;
    public static string Ip;



    string jsonURL = "https://anlorhan.github.io/game-json/ip.json";

    public void GetIp()
    {
        StartCoroutine(GetData(jsonURL));
    }

    public IEnumerator GetData(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            // error ...

        }
        else
        {
            // success...
            data = JsonUtility.FromJson<IP>(request.downloadHandler.text);
            // print data in UI
            Ip = data.ip;
            
            manager.networkAddress = Ip;
            //manager.StartClient();
            //NetworkClient.Ready();
            StartClient();
            print(Ip);
        }

        // Clean up any resources it is using.
        request.Dispose();
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        
        
        base.OnServerAddPlayer(conn);
        //var connectedPlayer = conn.identity.GetComponent<MyNetworkPlayer>();
        //connectedPlayer.SetDisplayName(GetInfo.nickname);
        /*
        connectedPlayer.SetDisplayName(MyNetworkPlayer.nickName);
        if (conn.identity.gameObject.GetComponent<MyNetworkPlayer>()!=null)
        {
            print("connection:" + conn);
            var connectedPlayer = conn.identity.GetComponent<MyNetworkPlayer>();
            connectedPlayer.SetDisplayName(CharacterSelect.nickname);
        }
        else
        {
            print("null");
        }*/

        //connectedPlayer.SetDisplayColor(new Color(Random.Range(0F,1F), Random.Range(0F,1F),Random.Range(0F,1F),1F));

        //connectedPlayer.SetDisplayName($"Player {numPlayers}");

        //print(PickedSkinId);
        //skin=conn.identity.GetComponent<SkinHolder>();
        //skin.PickSkin(0);
    }
    
    public override void OnStartClient()
    {
        //InputManager.Add(ActionMapNames.Player);//Hareketi kitle
        //InputManager.Remove(ActionMapNames.Player);
        //InputManager.Controls.Player.Look.Enable();
    }
}