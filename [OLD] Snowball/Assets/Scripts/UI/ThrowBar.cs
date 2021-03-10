using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowBar : MonoBehaviour
{
    public Slider slider;                //получаем ссылку на нашу шкалу просто чтобы удобнее было вызывать ее
    public GameObject buton;             //получаем ссылку на кнопку броска
    public GameObject snowInHand;
    public GameObject snowDeploy;
    public float updateBarSpeed = 0.01f; //скорость обновления шкалы
    public int powerOfThrow = 2;         //сила броска 
    public string throwButton = "Throw"; //название кнопки которе назначаем на бросок

    private bool _isActive = true;
    private Rigidbody rb;
    private Transform endOfMapPos;

    private void Awake()
    {
        endOfMapPos = GameObject.Find("EndOfMapPos").transform;
        rb = snowDeploy.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    private void Update()
    {
        SetPower();

        //if (SimpleInput.GetButtonDown(throwButton) && _isActive == true) //Simple Input баг с Value, после нажатия кнопка не отжимается (Value остается True, это можно увидеть в инспекторе), так что пришлось сделать костыль
        //{
        //    _isActive = false;
        //    StartCoroutine(cooldown());
        //}
    }


    private void SetPower()
    {
        if (slider.value == 0)
        {
            StartCoroutine(increasing());
        } else if (slider.value == slider.maxValue)
        {
            StartCoroutine(decreasing());
        }
        
    }

    IEnumerator increasing()
    {
        for (int i = 0; i < 21; i++)
        {
            slider.value = i;
            yield return new WaitForSeconds(updateBarSpeed);
        }
    }

    IEnumerator decreasing()
    {
        for (int i = 20; i > -1; i--)
        {
            slider.value = i;
            yield return new WaitForSeconds(updateBarSpeed);
        }
    }

    IEnumerator cooldown()
    {
        buton.SetActive(false);
        snowDeploy.gameObject.transform.position = snowInHand.transform.position;
        snowInHand.SetActive(false);
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.AddForce(0f, slider.value + powerOfThrow, 0f, ForceMode.Impulse);
        yield return new WaitForSecondsRealtime(2f);
        buton.SetActive(true);
        snowInHand.SetActive(true);
        snowDeploy.gameObject.transform.position = endOfMapPos.position;
        rb.useGravity = false;
        rb.isKinematic = true;
        yield return new WaitForSeconds(0.05f);
        _isActive = true;
    }

    public void OnButtonClick()
    {
        StartCoroutine(cooldown());
    }

}
