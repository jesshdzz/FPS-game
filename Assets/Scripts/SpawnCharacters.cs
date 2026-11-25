using UnityEngine;
using Photon.Pun;
public class SpawnCharacters : MonoBehaviour
{
    public GameObject character;
    public Transform[] spawnPoints;

    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Instantiate(character.name, spawnPoints[PhotonNetwork.CountOfPlayers - 1].position,
            spawnPoints[PhotonNetwork.CountOfPlayers - 1].rotation);
        }
    }
    void Update()
    {
        
    }
}