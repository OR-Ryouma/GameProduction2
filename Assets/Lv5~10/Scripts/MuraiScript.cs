using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuraiScript : MonoBehaviour
{
    [SerializeField] float PLSP = 1f;
    [SerializeField] float JPSP = 2f;
    [SerializeField] int JPST = 0;
    [SerializeField] float JPSPVC;
    [SerializeField] float JPSPVCDW = 0.5f;
    [SerializeField] float JPSPTM = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        JPSPVC = JPSP;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-PLSP, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(PLSP, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space) && JPST == 0)
        {
            JPSPVC = JPSP;
            JPST = 1;
            Invoke("STCHDW", JPSPTM);
        }
        if (JPST == 1)
        {
            transform.position += new Vector3(0, JPSPVC, 0);
            JPSPVC = JPSPVC * JPSPVCDW;
        }
        if (JPST == 2)
        {
            transform.position += new Vector3(0, -JPSPVC, 0);
            JPSPVC = JPSPVC / JPSPVCDW;
        }
    }
    void STCHDW()
    {
        JPST = 2;
        Invoke("STCHST", JPSPTM);
    }
    void STCHST()
    {
        JPSPVC = JPSP;
        transform.position = new Vector3(this.transform.position.x, -3, this.transform.position.z);
        JPST = 0;
    }
}