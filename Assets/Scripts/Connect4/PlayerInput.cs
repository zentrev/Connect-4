using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [System.Serializable]
    public struct DropNode
    {
        [SerializeField] public GameObject node;
        [SerializeField] public int collum;
    }

    [SerializeField] public DropNode[] m_dropNodes = new DropNode[7];
    [SerializeField] GameBoard m_board = null;

    bool m_playerTurn = true;

    void Update()
    {
        if(m_playerTurn)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                Debug.Log(hit == true);
                if (hit)
                {
                    foreach(DropNode node in m_dropNodes)
                    {
                        if(node.node == hit.transform.gameObject)
                        {
                            m_board.PlaceChip(node.collum);
                            m_board.DebugBoard();
                            continue;
                        }
                    }
                }
            }
        }
    }
}
