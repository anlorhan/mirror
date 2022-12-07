using Mirror;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;



public class GetInfo : NetworkBehaviour
{
    public static string nickname;

    

    public void Login(TextMeshProUGUI nick)
    {
        nickname = nick.text;
        print(nickname);
        //MyNetworkPlayer.nickName=nick.text;
        SceneManager.LoadScene("Game");
    }
}
