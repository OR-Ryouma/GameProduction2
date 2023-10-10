using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAttachScript : MonoBehaviour
{
    //カメラ取得
    public GameObject mainCameraObject;

    // Start is called before the first frame update
    void Start()
    {
        mainCameraObject = GameObject.Find("Main Camera(AttachTarget)");
        //カメラを自分の子オブジェクトにする
        mainCameraObject.transform.SetParent(this.gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
