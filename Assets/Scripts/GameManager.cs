using System;
using System.Collections;

using UnityEngine;

using Photon.Pun;

public enum PlayerColors
{
    Red,
    Blue,
    Green,
    Yellow
}

public enum TileStates
{
    Empty,
    Built,
}

public class GameManager : MonoBehaviour
{
    PhotonView _myPhotonView;

    public TileStates[,] GameBoard = new TileStates[12, 12];
    GameObject[,] GameTiles = new GameObject[12, 12];

    [HideInInspector] public PlayerColors playersTurn = PlayerColors.Red;
    int playersTurnCount = 0;

    [Header("Backend GOs")]
    public GameObject building;
    public GameObject baseTile;

    [Header("Building Materials")]
    public Material buildTileMaterialRed;
    public Material buildTileMaterialBlue;
    public Material buildTileMaterialGreen;
    public Material buildTileMaterialYellow;

    [Header("Backend Materials")]
    public Material baseTileMaterialRed;
    public Material baseTileMaterialBlue;
    public Material baseTileMaterialGreen;
    public Material baseTileMaterialYellow;

    [Space(15)]
    public Material baseMaterial;
    public Material baseHoverMaterial;

    [HideInInspector] public int _boardSize = 12;

    public static GameManager Instance;
    private void Awake()
    {
        if (!PhotonNetwork.IsConnected)
            PhotonNetwork.OfflineMode = true;

        Instance = this;
        _myPhotonView = PhotonView.Get(this);
    }

    private void Start()
    {
        AssignPlayerColors();

        PlayerMotor.Instance.StartPlayerMotor();

        StartCoroutine(BuildBoard());
    }

    IEnumerator BuildBoard()
    {
        for (int x = 0; x < _boardSize; x++)
        {
            for (int y = 0; y < _boardSize; y++)
            {
                Vector3 m_posToSpawn = new Vector3(x * 1.25f, 0, y * 1.25f);
                GameObject m_gameTile = Instantiate(baseTile, m_posToSpawn, Quaternion.identity);
                GameTiles[x, y] = m_gameTile;
                GameBoard[x, y] = TileStates.Empty;

                BaseTile m_tile = m_gameTile.GetComponent<BaseTile>();
                m_tile.defualtMaterial = ReturnMaterialForTile(x,y);
                m_tile.IsHovered(baseHoverMaterial);

                m_tile.x = x;
                m_tile.y = y;

                yield return new WaitForSeconds(0.01f);
            }
        }
        yield return null;
    }

    public void BuildOnTile(PlayerColors m_pc, int x, int y)
    {
        _myPhotonView.RPC(nameof(BuildOnTileNetworked), RpcTarget.All, m_pc, x, y);
    }
    [PunRPC]
    void BuildOnTileNetworked(PlayerColors m_pc, int x, int y)
    {
        Vector3 m_tilePosition = GameTiles[x, y].transform.position;

        GameBoard[x, y] = TileStates.Built;
        Destroy(GameTiles[x, y].gameObject);
        GameObject newBuilding = Instantiate(building, m_tilePosition, Quaternion.identity);
        newBuilding.GetComponentInChildren<MeshRenderer>().material = ReturnMaterialForBuilding(m_pc);
    }
    
    public void MoveToNextTurn()
    {
        _myPhotonView.RPC(nameof(MoveToNextTurnNetworked), RpcTarget.All);
    } 
    [PunRPC]
    void MoveToNextTurnNetworked()
    {
        playersTurnCount++;
        if (playersTurnCount >= PhotonNetwork.PlayerList.Length)
            playersTurnCount = 0;

        if (playersTurnCount == 0)
            playersTurn = PlayerColors.Red;
        if (playersTurnCount == 1)
            playersTurn = PlayerColors.Blue;
        if (playersTurnCount == 2)
            playersTurn = PlayerColors.Green;
        if (playersTurnCount == 3)
            playersTurn = PlayerColors.Yellow;
    }

    void AssignPlayerColors()
    {
        if(PhotonNetwork.IsMasterClient)
            _myPhotonView.RPC(nameof(AssignPlayerColorsNetWorked), RpcTarget.All);
    }
    [PunRPC]
    void AssignPlayerColorsNetWorked()
    {
        if (PhotonNetwork.PlayerList.Length > 0 && PhotonNetwork.PlayerList[0]?.NickName == PhotonNetwork.NickName)
        {
            PlayerMotor.Instance.SetColor(PlayerColors.Red);
        }
        if (PhotonNetwork.PlayerList.Length > 1 && PhotonNetwork.PlayerList[1]?.NickName == PhotonNetwork.NickName)
        {
            PlayerMotor.Instance.SetColor(PlayerColors.Blue);
        }
        if (PhotonNetwork.PlayerList.Length > 2 && PhotonNetwork.PlayerList[2]?.NickName == PhotonNetwork.NickName)
        {
            PlayerMotor.Instance.SetColor(PlayerColors.Green);
        }
        if (PhotonNetwork.PlayerList.Length > 3 && PhotonNetwork.PlayerList[3]?.NickName == PhotonNetwork.NickName)
        {
            PlayerMotor.Instance.SetColor(PlayerColors.Yellow);
        }
    }

    public string ReturnPlayerColorString(PlayerColors m_pc)
    {
        return m_pc.ToString();
    }
    public float ReturnRotationForColor(PlayerColors m_pc)
    {
        if (m_pc == PlayerColors.Red)
            return 0;
        if (m_pc == PlayerColors.Green)
            return 90;
        if (m_pc == PlayerColors.Blue)
            return 180;
        if (m_pc == PlayerColors.Yellow)
            return 270;

        return 0;
    }
    public Material ReturnMaterialForBuilding(PlayerColors m_pc)
    {
        if (m_pc == PlayerColors.Red)
            return buildTileMaterialRed;

        if (m_pc == PlayerColors.Green)
            return buildTileMaterialGreen;

        if (m_pc == PlayerColors.Blue)
            return buildTileMaterialBlue;

        if (m_pc == PlayerColors.Yellow)
            return buildTileMaterialYellow;

        return baseMaterial;
    }

    Material ReturnMaterialForTile(int x, int y)
    {
        //
        // God, if you are there, and watching me do this, please forgive me for this all...
        // I pray fo your mercy, I pray for a redemption, I pray for a second chance...
        // For you see my Lord, I am but a designer, not a programmer...
        // While I will never be clean of this sin...
        // I simply pray for better when judgement day comes...
        //
        if (x > 0 && x < 11 && y == 0)
            return baseTileMaterialRed;
        else if (x > 1 && x < 10 && y == 1)
            return baseTileMaterialRed;
        else if (x > 2 && x < 9 && y == 2)
            return baseTileMaterialRed;
        else if (x > 3 && x < 8 && y == 3)
            return baseTileMaterialRed;
        else if (x > 4 && x < 7 && y == 4)
            return baseTileMaterialRed;

        if (x > 0 && x < 11 && y == 11)
            return baseTileMaterialBlue;
        else if (x > 1 && x < 10 && y == 10)
            return baseTileMaterialBlue;
        else if (x > 2 && x < 9 && y == 9)
            return baseTileMaterialBlue;
        else if (x > 3 && x < 8 && y == 8)
            return baseTileMaterialBlue;
        else if (x > 4 && x < 7 && y == 7)
            return baseTileMaterialBlue;

        if (y > 0 && y < 11 && x == 0)
            return baseTileMaterialGreen;
        else if (y > 1 && y < 10 && x == 1)
            return baseTileMaterialGreen;
        else if (y > 2 && y < 9 && x == 2)
            return baseTileMaterialGreen;
        else if (y > 3 && y < 8 && x == 3)
            return baseTileMaterialGreen;
        else if (y > 4 && y < 7 && x == 4)
            return baseTileMaterialGreen;

        if (y > 0 && y < 11 && x == 11)
            return baseTileMaterialYellow;
        else if (y > 1 && y < 10 && x == 10)
            return baseTileMaterialYellow;
        else if (y > 2 && y < 9 && x == 9)
            return baseTileMaterialYellow;
        else if (y > 3 && y < 8 && x == 8)
            return baseTileMaterialYellow;
        else if (y > 4 && y < 7 && x == 7)
            return baseTileMaterialYellow;

        return baseMaterial;
    }
}