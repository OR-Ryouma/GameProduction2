using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAttachScript : MonoBehaviour
{
    //�J�����擾
    public GameObject mainCameraObject;

    // Start is called before the first frame update
    void Start()
    {
        mainCameraObject = GameObject.Find("Main Camera(AttachTarget)");
        //�J�����������̎q�I�u�W�F�N�g�ɂ���
        mainCameraObject.transform.SetParent(this.gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
