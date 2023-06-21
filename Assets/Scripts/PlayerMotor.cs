using UnityEngine;

using System.Collections;

using Photon.Pun;

public class PlayerMotor : MonoBehaviour
{
    PlayerColors myColor;

    [SerializeField] Transform _Base;

    PhotonView _myPhotonView;

    //
    // Later make private & allocate with GameManager
    //
    Camera _myCamera;

    Vector2 _mousePosition;
    Ray _rayOrigin;
    RaycastHit _hitInfo;


    public static PlayerMotor Instance;
    private void Awake()
    {
        Instance = this;
        _myPhotonView = GetComponent<PhotonView>();
        _myCamera = GetComponentInChildren<Camera>();
    }

    public void StartPlayerMotor()
    {
        _Base.Rotate(0, GameManager.Instance.ReturnRotationForColor(myColor), 0);
    }

    private void Update()
    {
        _mousePosition = Input.mousePosition;
        _rayOrigin = _myCamera.ScreenPointToRay(_mousePosition);

        Hover();
        Click();
    }

    void Click()
    {
        if (!Input.GetMouseButtonDown(0) || myColor != GameManager.Instance.playersTurn)
            return;

        if (Physics.Raycast(_rayOrigin, out _hitInfo))
        {
            var mm_objectHit = _hitInfo.collider;

            if (mm_objectHit.GetComponentInParent<BaseTile>())
            {
                BaseTile mm_ClickedTile = mm_objectHit.GetComponentInParent<BaseTile>();

                if (GameManager.Instance.GameBoard[mm_ClickedTile.x, mm_ClickedTile.y] == TileStates.Empty &&
                    !GameManager.Instance.isBusy)
                {
                    _myPhotonView.RPC(nameof(Building), RpcTarget.All, mm_ClickedTile.x, mm_ClickedTile.y);
                }
            }
        }
    }
    void Hover()
    {
        if (GameManager.Instance.isBusy)
            return;

        if(Physics.Raycast(_rayOrigin, out _hitInfo))
        {
            var mm_objectHit = _hitInfo.collider;

            if (mm_objectHit.GetComponentInParent<BaseTile>())
            {
                mm_objectHit.GetComponentInParent<BaseTile>().IsHovered(GameManager.Instance.baseHoverMaterial);
            }
        }
    }


    [PunRPC]
    void Building(int x, int y)
    {
        StartCoroutine(BuildingEnum(x,y));
    }
    IEnumerator BuildingEnum(int x, int y)
    {
        CameraMotor.Instance.SwitchCamera(1);
        GameManager.Instance.isBusy = true;

        GameManager.Instance._pointOfInterest.position = new Vector3(x * 1.25f, 0, y * 1.25f);

        yield return new WaitForSeconds(1f);

        if(myColor == GameManager.Instance.playersTurn)
            GameManager.Instance.BuildOnTile(myColor, x, y);

        yield return new WaitForSeconds(1.5f);

        if (myColor == GameManager.Instance.playersTurn)
            EffectManager.Instance.gameEffects[GameManager.Instance.currentEffect].RunEffect(myColor);

        yield return new WaitForSeconds(2f);

        CameraMotor.Instance.SwitchCamera(0);

        yield return new WaitForSeconds(0.5f);

        GameManager.Instance.isBusy = false;
        if (myColor == GameManager.Instance.playersTurn)
            GameManager.Instance.MoveToNextTurn();

        yield return null;
    }

    public void SetColor(PlayerColors m_pc)
    {
        myColor = m_pc;
    }
    public PlayerColors GetColor()
    {
        return myColor;
    }
}