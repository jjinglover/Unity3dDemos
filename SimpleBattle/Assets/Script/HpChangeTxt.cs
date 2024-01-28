using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpChangeTxt : MonoBehaviour
{
    public Text _valueTxt = null;//�ַ��ı����

    //Ʈ���ٶ�
    private float _speed = 80;
    //�ٶ�˥��
    private float _speedWeak=8.0f;
    //������ֵ��������������ʧ��Ч��
    private float _alpha = 1.0f;
    void Update()
    {
        transform.Translate(0, _speed * Time.deltaTime, 0);//ʹ���������ƶ�
        if (_speed > 0)
        {
            _speed -= _speedWeak;//ʹ�������ƶ����ٶ��𽥼���
        }
        else if (_alpha > 0)
        {
            _alpha -= 0.1f;//����������͸��
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