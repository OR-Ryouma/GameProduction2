using UnityEngine;
using System;

public class ItemGenerator : MonoBehaviour
{
    public GameObject _item;
    public float s_distance;
    public float g_distance;

    int j = 0;

    float[] num = new float[3] {-2f, 0f, 2f};
    System.Random random = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        for (float i = s_distance; i < g_distance; i = i + 5)
        {
            if(i%3 == 0)
            {
                j = random.Next(num.Length);
            }

            Instantiate(_item, new Vector3(num[j], 1f, i), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
