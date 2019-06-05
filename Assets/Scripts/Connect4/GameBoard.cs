using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameBoard : MonoBehaviour
{
    [SerializeField] Transform m_bottomLeftSlot = null;
    [SerializeField] GameObject m_doritoPrefab = null;
    [SerializeField] GameObject m_iluminatiPrefab = null;
    [SerializeField] GameObject m_doritoPrefabDrop = null;
    [SerializeField] GameObject m_iluminatiPrefabDrop = null;
    [SerializeField] GameObject m_board = null;
    [SerializeField] GameObject[] m_doritioDisplay = null;
    [SerializeField] GameObject[] m_iluminatieDisplay = null;
    [SerializeField] GameObject m_winCanvas = null;

    [SerializeField] AudioClip m_chipSound = null;
    [SerializeField] AudioClip m_eyeSound = null;
    [SerializeField] PlayerInput m_playerInput = null;


    public static int Width = 7;
    public static int Height = 6;
    public static int WinLength = 4;

    public enum ePlaceHolder
    {
        NONE,
        DORITO,
        ILUMINATI,
    }

    public ePlaceHolder[,] board = new ePlaceHolder[Height,Width];

    bool m_doritosTurn = true;

    public void ClearBoard()
    {
        for(int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                board[i,j] = ePlaceHolder.NONE;
            }
        }
    }

    public void Start()
    {
        ClearBoard();
    }

    public void PlaceChip(int collum)
    {
        if (CheckCollum(collum)) throw new System.Exception("Collum already full");

        for(int i = 0; i < Height; i++)
        {
            if(board[i,collum] == ePlaceHolder.NONE)
            {
                //Place Chip
                ePlaceHolder player = (m_doritosTurn) ? ePlaceHolder.DORITO : ePlaceHolder.ILUMINATI;
                board[i, collum] = player;
                Debug.Log("chip placed at " + (i) + " : " + collum);

                Vector3 offset = new Vector3((Width - collum-1) * 3, 0 ,0);
                Instantiate(((m_doritosTurn) ? m_doritoPrefabDrop : m_iluminatiPrefabDrop), m_bottomLeftSlot.position + offset, Quaternion.identity, m_board.transform);
                GetComponent<AudioSource>().clip = (m_doritosTurn) ? m_chipSound : m_eyeSound;
                GetComponent<AudioSource>().Play();
                //Check for win
                if (CheckForWin(i,collum))
                {
                    StartCoroutine("Win");
                }
                m_doritosTurn = !m_doritosTurn;

                //Display Turn
                for (int f = 0; f < Width; f++)
                {

                    if (board[Height - 1, f] == ePlaceHolder.NONE)
                    {
                        if (m_doritosTurn)
                        {
                            m_doritioDisplay[f].SetActive(true);
                            m_iluminatieDisplay[f].SetActive(false);
                        }
                        else
                        {
                            m_doritioDisplay[f].SetActive(false);
                            m_iluminatieDisplay[f].SetActive(true);
                        }
                    }
                    else
                    {
                        m_doritioDisplay[f].SetActive(false);
                        m_iluminatieDisplay[f].SetActive(false);
                    }
                }

                return;
            }
        }
    }


    IEnumerator Win()
    {
        m_playerInput.m_playerTurn = false;
        yield return new WaitForSeconds(3);
        m_winCanvas.GetComponent<Canvas>().enabled = true;
        yield return new WaitForSeconds(5);
        m_winCanvas.GetComponent<Canvas>().enabled = false;
        GameManager.Instance.LoadLevel("MainMenu");
    }

    public void DebugBoard()
    {
        return;

        Debug.Log("Debug Board-----------------------------------------");
        for (int i = Height-1; i >= 0; i--)
        {
            string row = "";
            for (int j = Width-1; j >= 0; j--)
            {
                row += board[i, j].ToString() + " : ";
            }
            Debug.Log(row);
        }
    }

    public bool CheckCollum(int collum)
    {
        if (board[Height-1, collum] != ePlaceHolder.NONE) return true;
        return false;
    }

    bool CheckForWin(int row, int collum)
    {
        ePlaceHolder player = (m_doritosTurn) ? ePlaceHolder.DORITO : ePlaceHolder.ILUMINATI;
        int c = (collum - 3);
        for (int f = 0; f < 2; f++)
        {
            if (f == 1) c = collum + 3;
            int inARowCount = 0;
            int inACollumCount = 0;
            int inADiagonalCount = 0;
            for (int r = row - 3; r <= (((row + 3) < Height) ? (row + 3) : (Height - 1)); r++)
            {
                
                //check diagonal
                if (c >= 0 && c < Height && r >= 0 && r < Width)
                {
                    if (board[r, c] == player) inADiagonalCount++;
                    else inADiagonalCount = 0;
                }
                //check row
                if (c >= 0 && c < Width)
                {
                    if (board[row, c] == player) inARowCount++;
                    else inARowCount = 0;
                }
                //check colum
                if (r >= 0 && r < Height)
                {
                    if (board[r, collum] == player) inACollumCount++;
                    else inACollumCount = 0;
                }

                if (inARowCount >= WinLength || inACollumCount >= WinLength || inADiagonalCount >= WinLength)
                {
                    return true;
                }

                if (f == 0) c++;
                else c--;
            }
        }

        return false;
    }
}
