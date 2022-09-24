using Unity.VisualScripting;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    //  the gameboard transform is at the middle of the board
    //  and the [0,0] position is at bottom left
    [Header("Slots Settings")]
    [SerializeField] private int rowCount;
    [SerializeField] private int columnCount;
    [SerializeField] private float slotWidth;
    [SerializeField] private float slotHeight;

    public LayerMask RaycastTargetsLayerMask;

    [Header("References")]
    [SerializeField] private ColumnRaycastTarget raycastTargetPrefab;

    public GameBoardSlot[,] Slots { get; private set; }

    int ConnectionsNeededForWin => GameSettings.Instance.ConnectionsNeededForWin;

    private void Awake()
    {
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        Slots = new GameBoardSlot[rowCount, columnCount];
        for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
            {
                float x = (columnIndex - ((float)columnCount / 2) + .5f) * slotWidth;
                float y = (rowIndex - ((float)rowCount / 2) + .5f) * slotHeight;
                Vector3 position = transform.position + new Vector3(x, y, 0);
                Slots[rowIndex, columnIndex] = new GameBoardSlot(position, true, null);
            }
        }
        // make target colliders only in play mode
        if (!Application.isPlaying)
            return;
        for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
        {
            float x = (columnIndex - ((float)columnCount / 2) + .5f) * slotWidth;
            Vector3 position = x * Vector3.right + transform.position;
            ColumnRaycastTarget target = Instantiate(raycastTargetPrefab, position, Quaternion.identity, transform);
            target.transform.localScale = new Vector3(
                slotWidth,
                slotHeight * rowCount,
                .3f
                );
            target.columnIndex = columnIndex;
        }
    }

    public GameBoardSlot InsertPiece(int rowIndex, int columnIndex, Player player)
    {
        GameBoardSlot slot = Slots[rowIndex, columnIndex];
        slot.IsEmpty = false;
        slot.FillingPlayer = player;
        return slot;
    }

    public int GetHighestEmptySlot(int column)
    {
        for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            GameBoardSlot slot = Slots[rowIndex, column];
            if (slot.IsEmpty)
                return rowIndex;
        }
        return -1;
    }

    public bool IsFull()
    {
        for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
        {
            if (CanDropInThisColumn(columnIndex))
                return false;
        }
        return true;
    }

    public bool CanDropInThisColumn(int columnIndex)
    {
        return Slots[rowCount - 1, columnIndex].IsEmpty;
    }

    public Vector3 GetColumnTopWorldPoistion(int columnIndex)
    {
        return Slots[rowCount - 1, columnIndex].WorldPosition + slotHeight * Vector3.up;
    }

    public bool CheckForWin(int rowIndex, int columnIndex, Player player)
    {
        if (CheckRowForWin(rowIndex, player))
            return true;
        if (CheckColumnForWin(columnIndex, player))
            return true;
        if (CheckDiagonalsForWin(rowIndex, columnIndex, player))
            return true;
        return false;
    }

    private bool CheckRowForWin(int rowIndex, Player player)
    {
        int connectedCount = 0;
        for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
        {
            if (Slots[rowIndex, columnIndex].FillingPlayer == player)
            {
                connectedCount++;
                if (connectedCount >= ConnectionsNeededForWin)
                    return true;
            }
            else
            {
                connectedCount = 0;
            }
        }
        return false;
    }
    private bool CheckColumnForWin(int columnIndex, Player player)
    {
        int connectedCount = 0;
        for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            if (Slots[rowIndex, columnIndex].FillingPlayer == player)
            {
                connectedCount++;
                if (connectedCount >= ConnectionsNeededForWin)
                    return true;
            }
            else
            {
                connectedCount = 0;
            }
        }
        return false;
    }
    private bool CheckDiagonalsForWin(int row, int column, Player player)
    {
        int connectedCount1 = 0;
        int connectedCount2 = 0;

        int difference = column - row;
        int sum = column + row;

        for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            //Check Diagonal BottomLeft to TopRight
            int columnIndex = rowIndex + difference;

            if (columnIndex >= 0 &&
                columnIndex < columnCount &&
                Slots[rowIndex, columnIndex].FillingPlayer == player)
            {
                connectedCount1++;
                if (connectedCount1 >= ConnectionsNeededForWin)
                    return true;
            }
            else
            {
                connectedCount1 = 0;
            }

            //Check Anti-Diagonal TopLeft to BottomRight
            columnIndex = sum - rowIndex;

            if (columnIndex >= 0 &&
                columnIndex < columnCount &&
                Slots[rowIndex, columnIndex].FillingPlayer == player)
            {
                connectedCount2++;
                if (connectedCount2 >= ConnectionsNeededForWin)
                    return true;
            }
            else
            {
                connectedCount2 = 0;
            }
        }
        return false;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!Application.isPlaying)
            Slots = null;
    }
    private void OnDrawGizmos()
    {
        if (Slots == null)
            InitializeBoard();
        Gizmos.color = Color.red;
        foreach (var slot in Slots)
        {
            Gizmos.DrawWireCube(slot.WorldPosition, Vector3.one * .3f);
        }
    }
#endif
}
