using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpChangeTxt : MonoBehaviour
{
    public Text _valueTxt = null;//字符文本组件

    //飘动速度
    private float _speed = 80;
    //速度衰弱
    private float _speedWeak=8.0f;
    //阿尔法值，控制字体逐渐消失的效果
    private float _alpha = 1.0f;
    void Update()
    {
        transform.Translate(0, _speed * Time.deltaTime, 0);//使字体向上移动
        if (_speed > 0)
        {
            _speed -= _speedWeak;//使得向上移动的速度逐渐减少
        }
        else if (_alpha > 0)
        {
            _alpha -= 0.1f;//控制字体逐渐透明
        }

        if (_valueTxt)
        {
            _valueTxt.color = new Color(_valueTxt.color.r, _valueTxt.color.g, _valueTxt.color.b, _alpha);
        }

        if (_alpha < 0) {
            Destroy(gameObject);
        }
    }

    public void Init(int hurt)
    {
        if (_valueTxt) {
            string prefix = hurt < 0 ? "" : "+";
            _valueTxt.text = prefix + hurt.ToString();
            _valueTxt.color = hurt < 0 ? Color.red : Color.green;
        }
    }
}