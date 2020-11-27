using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Axis
{
    X,
    Y,
    Z
}

public class MySlider : MonoBehaviour
{

	public GameObject target;//要移动的物体
    public Slider slider;//slider组件
    public Axis axis = Axis.X;//按照此轴进行移动
    public float left_bottom_back = -1;//左边界
    public float right_up_forward = 1;//右边界
    private float D_value;//左右边界的差值

    // Start is called before the first frame update
    void Start()
    {
        D_value = right_up_forward-left_bottom_back;
        //以下是将物体的初始位置设为slider的初始位置
        switch(axis)
        {
            case Axis.X:
                target.transform.position += new Vector3(-target.transform.position.x + D_value * slider.value, 0, 0);
                break;
            case Axis.Y:
                target.transform.position += new Vector3(0, -target.transform.position.y+ D_value * slider.value, 0);
                break;
            case Axis.Z:
                target.transform.position += new Vector3(0, 0, -target.transform.position.z  + D_value * slider.value);
                break;
        }

        slider.onValueChanged.AddListener(delegate{ this.handleValuChange(); });
        if(!target)
        {
            Debug.Log("missing target!");
        }
        if (!slider)
        {
            Debug.Log("missing slider!");
        }
    }

    // Update is called once per frame
    void handleValuChange()
    {
        //当slider的value改变时，调用这个函数，并改变物体的位置
        switch(axis)
        {
            case Axis.X:
                float Xpos = D_value * slider.value;
                target.transform.position += new Vector3(-target.transform.position.x + Xpos, 0, 0);
                break;
            case Axis.Y:
                float Ypos =  D_value * slider.value;
                target.transform.position += new Vector3(0, -target.transform.position.y + Ypos, 0);
                break;
            case Axis.Z:
                float Zpos =  D_value * slider.value;
                target.transform.position += new Vector3(0, 0, -target.transform.position.z + Zpos);
                break;
        }
    }
}