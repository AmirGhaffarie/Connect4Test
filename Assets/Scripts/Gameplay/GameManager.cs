using Photon.Pun;
using System.Collections;
using System.Linq;
using UnityEngine;
public class GameManager : MonoBehaviourPun
{

    [Header("References")]
    [SerializeField] private GameBoard board;
    [SerializeField] private PieceController PiecePrefab;
    [SerializeField] private Transform pieceSpawnPosition;
    private Camera mainCamera;

    [Header("Events")]
    [SerializeField] private GameEvent onGameStart;
    [SerializeField] private PlayerEvent onLocalPlayerJoined;
    [SerializeField] private PlayerEvent onTurnChange;
    [SerializeField] private PlayerEvent onPlayerWin;

    //State Variables
    private Player CurrentPlayer => players[currentPlayerIndex];
    private PieceController currentPiece;
    private int currentPlayerIndex;
    private GameState gameState;
    private Player[] players;

    private void Awake()
    {
        players = new Player[GameSettings.Instance.Players.Length];
        Input.simulateMouseWithTouches = true;
        mainCamera = Camera.main;
        gameState = GameState.WaitingToStart;
    }

    private IEnumerator Start()
    {
        WaitForSeconds waitFor100ms = new(.1f);
        //wait till all players joined and assigned
        while (players.Any((p) => p == null))
            yield return waitFor100ms;
        onGameStart.Raise();
        gameState = GameState.Playing;
        currentPlayerIndex = 0;
        InstantiateNewPiece();
        onTurnChange.Raise(CurrentPlayer);
    }

    private void Update()
    {
        if (gameState != GameState.Playing || !CurrentPlayer.PhotonView.IsMine)
            return;
        CheckForInput();
    }

    public void AddPlayer(PhotonView view, int index)
    {
        Player settings = GameSettings.Instance.Players[index];
        Player player = new(settings.Name, settings.Color, view);
        players[index] = player;
        if (view.IsMine)
            onLocalPlayerJoined.Raise(player);
    }

    private void InstantiateNewPiece()
    {
        currentPiece = Instantiate(PiecePrefab, pieceSpawnPosition.position, Quaternion.identity);
        MaterialPropertyBlock mpb = new();
        mpb.SetColor("_BaseColor", CurrentPlayer.Color);
        currentPiece.GetComponentInChildren<Renderer>().SetPropertyBlock(mpb);
    }


    private void CheckForInput()
    {
        if (ClickedThisFrame())
        {
            TryDropOnMousePosition();
        }

    }

    private bool ClickedThisFrame()
    {
        return Input.GetMouseButtonDown(0);
    }
    private void TryDropOnMousePosition()
    {
        Ray mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        bool didClickonColumn = Physics.Raycast(mouseRay, out RaycastHit hitInfo,
            float.MaxValue, board.RaycastTargetsLayerMask, QueryTriggerInteraction.Collide);
        if (didClickonColumn)
        {
            int selectedColumn = hitInfo.transform.GetComponent<ColumnRaycastTarget>().columnIndex;
            if (board.CanDropInThisColumn(selectedColumn))
            {
                photonView.RPC(nameof(DropPiece), RpcTarget.All, selectedColumn);
            }
        }
    }

    [PunRPC]
    private void DropPiece(int selectedColumn)
    {
        int rowIndex = board.GetHighestEmptySlot(selectedColumn);
        GameBoardSlot slot = board.InsertPiece(rowIndex, selectedColumn, CurrentPlayer);
        currentPiece.MoveToPosition(board.GetColumnTopWorldPoistion(selectedColumn), slot.WorldPosition);
        if (board.CheckForWin(rowIndex, selectedColumn, CurrentPlayer))
        {
            gameState = GameState.GameEnded;
            onPlayerWin.Raise(CurrentPlayer);
        }
        else if(board.IsFull())
        {
            gameState = GameState.GameEnded;
            onPlayerWin.Raise(null);
        }
        else
        {
            ChangeTurn();
        }
    }

    private void ChangeTurn()
    {
        ChangePlayerIndexToNextPlayer();
        InstantiateNewPiece();
        onTurnChange.Raise(CurrentPlayer);
    }

    private void ChangePlayerIndexToNextPlayer()
    {
        int nextPlayerIndex = (currentPlayerIndex + 1) % players.Length;
        currentPlayerIndex = nextPlayerIndex;
    }
}
