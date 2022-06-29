using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using System.Threading.Tasks;
public class ElevatorBehaviour : MonoBehaviour
{
    //----------------------------------------------------------------------------------------------------------------------------------------------------
    //��������� ����������� ���� ������ ��� ����������� ������ � ����
    //----------------------------------------------------------------------------------------------------------------------------------------------------
    public int ElevatorPosition = 1;
    public string Floor1 = "Null", Floor2 = "Null", Floor3 = "Null", Floor4 = "Null", ElevatorCondition = "InActive", ElevatorDirection = "--";
    bool quit, stop = false;
    //----------------------------------------------------------------------------------------------------------------------------------------------------
    //����� �������������� ������, ��������� �����, � ����� �������� ������ ��� ������ ������ ����� � ����������� �����
    //----------------------------------------------------------------------------------------------------------------------------------------------------
    public Button Up1, Up2, Up3, Down2, Down3, Down4, FloorButton1, FloorButton2, FloorButton3, FloorButton4, Stop, Pause;
    public Image State1, State2, State3, State4;
    public Text ElevatorPanel, ElevatorDirect, Floor1Text, Floor2Text, Floor3Text, Floor4Text;


    //----------------------------------------------------------------------------------------------------------------------------------------------------
    //������� Update ����������� ������ �����(frame), ������� ��� �������� �� ����������� ��������� ��������� ����� � ��� ����������� �� ��������� �������
    //----------------------------------------------------------------------------------------------------------------------------------------------------
    void Update()
    {
        ElevatorPanel.text = ElevatorPosition.ToString();
        ElevatorDirect.text = ElevatorDirection.ToString();
        Floor1Text.text = ElevatorPosition.ToString();
        Floor2Text.text = ElevatorPosition.ToString();
        Floor3Text.text = ElevatorPosition.ToString();
        Floor4Text.text = ElevatorPosition.ToString();
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------
    /*IEnumerator - ������� ��������� ��� ���� ��������������, ����� ����������� �������� ���������� ���� � ����� �������� ��������� ������������
     ���� ���������� �����, ������������ �� ������� ������ ���������� ��������� �������� */
    //----------------------------------------------------------------------------------------------------------------------------------------------------
    IEnumerator wait(float waitTime)
    {
        float counter = 0;

        while (counter < waitTime)
        {
            counter += Time.deltaTime;
            if (quit)
            {
                yield break;
            }
            yield return null;
        }
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------
    //������ ������� �������� �� ������ ����� �� ������������ ����, ����� �������� ���������� � ���������� x
    //----------------------------------------------------------------------------------------------------------------------------------------------------
    IEnumerator GoUp(int x)
    {
        int a = ElevatorPosition;
        if (a == x) //���� ���� ��� ��������� �� ����� � �������, �� ���������� �������� ������ � ������� ����������
        {
            if (a == 1) { Floor1 = "Null"; }
            else if (a == 2) { Floor2 = "Null"; }
            else if (a == 3) { Floor3 = "Null"; }
            else if (a == 4) { Floor4 = "Null"; }
            yield return wait(3);
        }
        for (int i = ElevatorPosition; i != x; i++) // ���� ����������� �� ��� ���, ���� ���� �� ������� �� ������ ����
        {
            ElevatorDirection = "Up";
            switch (ElevatorPosition)
            {
                case 1:
                    yield return wait(1);
                    //gameObject.setActive �������� �� ��������� � ����������� �������� ����� � ����������
                    State1.gameObject.SetActive(false);
                    State2.gameObject.SetActive(true);
                    Floor1 = "Null";
                    ElevatorPosition = 2;
                    break;
                case 2:
                    yield return wait(1);
                    if (Floor2 == "Up") //�� ����, ���� ����������� ����� ��������� � ������� ������, �� ���� �����������
                    {
                        yield return wait(3);
                        Floor2 = "Null";//����� �������� ������ ������ ������ �� ������ ����� ���������� 
                    }
                    State2.gameObject.SetActive(false);
                    State3.gameObject.SetActive(true);
                    ElevatorPosition = 3;
                    break;
                case 3:
                    yield return wait(1);
                    if (Floor3 == "Up")
                    {
                        yield return wait(3);
                        Floor3 = "Null";
                    }
                    State3.gameObject.SetActive(false);
                    State4.gameObject.SetActive(true);
                    ElevatorPosition = 4;
                    yield return wait(3);
                    break;
            }
            if (stop == true) //������ ������� �����������, ����� ���������� ������ STOP
            {
                stop = false;
                break;
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------
        //����� ����������� �������� �� ��, ������ �� ������ ������ �� ������ ��� ���
        //���� ������ ������ 1 ��� 4 �����, �� ������� ���� ������ �� ���, ��������� �� ���� �� ����� ������������ � ��������� ����� �� ������ ������ 
        //----------------------------------------------------------------------------------------------------------------------------------------------------
        if (Floor1 == "Up" && ElevatorPosition >= 1)
        {
            StartCoroutine(GoDown(1));
        }
        else if (Floor4 == "Down" && ElevatorPosition <= 4)
        {
            StartCoroutine(GoUp(4));
        }
        else if (Floor3 != "Null" && ElevatorPosition <= 3)
        {
            StartCoroutine(GoUp(3));
        }
        else if (Floor3 != "Null" && ElevatorPosition >= 3)
        {
            StartCoroutine(GoDown(3));
        }
        else if (Floor2 != "Null" && ElevatorPosition <= 2)
        {
            StartCoroutine(GoUp(2));
        }
        else if (Floor2 != "Null" && ElevatorPosition >= 2)
        {
            StartCoroutine(GoDown(2));
        }
        //���� ������� ������ ���, �� ���� ���������� ����������
        else { ElevatorDirection = "--"; ElevatorCondition = "InActive"; }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------
    //������ ������� �������� �� ����� ����� �� ������������ ���� � �� ������ ������ �� ������� �������
    //----------------------------------------------------------------------------------------------------------------------------------------------------
    IEnumerator GoDown(int x)
    {
        int a = ElevatorPosition;
        if (a == x)
        {
            if (a == 1) { Floor1 = "Null"; }
            else if (a == 2) { Floor2 = "Null"; }
            else if (a == 3) { Floor3 = "Null"; }
            else if (a == 4) { Floor4 = "Null"; }
            yield return wait(3);//���� ������� ��� �������� ������, ������� ��� ������� ���������� � ��� ������� ��� �������� ������
        }
        for (int i = ElevatorPosition; i != x; i--)
        {
            ElevatorDirection = "Down";
            switch (ElevatorPosition)
            {
                case 2:
                    yield return wait(1); //���� ������� ��� ��������� �������� �������� ����� � ������ ����� �� ������
                    if (Floor2 == "Down")
                    {
                        yield return wait(3);
                        Floor2 = "Null";
                    }
                    State2.gameObject.SetActive(false);
                    State1.gameObject.SetActive(true);
                    ElevatorPosition = 1;
                    yield return wait(3);
                    break;
                case 3:
                    yield return wait(1);
                    if (Floor3 == "Down")
                    {
                        yield return wait(3);
                        Floor3 = "Null";
                    }
                    State3.gameObject.SetActive(false);
                    State2.gameObject.SetActive(true);
                    ElevatorPosition = 2;
                    break;
                case 4:
                    yield return wait(1);
                    State4.gameObject.SetActive(false);
                    State3.gameObject.SetActive(true);
                    Floor4 = "Null";
                    ElevatorPosition = 3;
                    break;
            }
            if (stop == true)
            {
                stop = false;
                break;
            }
        }
        if (Floor1 == "Up" && ElevatorPosition > 1)
        {
            StartCoroutine(GoDown(1));
        }
        else if (Floor4 == "Down" && ElevatorPosition <= 4)
        {
            StartCoroutine(GoUp(4));
        }
        else if (Floor3 != "Null" && ElevatorPosition <= 3)
        {
            StartCoroutine(GoUp(3));
        }
        else if (Floor3 != "Null" && ElevatorPosition >= 3)
        {
            StartCoroutine(GoDown(3));
        }
        else if (Floor2 != "Null" && ElevatorPosition <= 2)
        {
            StartCoroutine(GoUp(2));
        }
        else if (Floor2 != "Null" && ElevatorPosition >= 2)
        {
            StartCoroutine(GoDown(2));
        }
        else { ElevatorDirection = "--"; ElevatorCondition = "InActive"; } 
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------
    //����� ���� �������, ����������� � ������������ ������� � ���������� � ����������� �� ���� ������� �� ���� ��� ���
    //----------------------------------------------------------------------------------------------------------------------------------------------------
    public void FourthFloorButton()
    {
        if (ElevatorCondition == "InActive") //���� ���� ���������, �� �������� ��� �� ����
        {
            ElevatorCondition = "Active";
            StartCoroutine(GoUp(4));
        }
        else if (ElevatorCondition == "Active") //���� ���� �������, �� ���������� ������ ������ � ��������
        {
            Floor4 = "Down";
        }
    }
    public void ThirdFloorButtonUp()
    {
        if (ElevatorCondition == "InActive" && ElevatorPosition > 3)
        {
            ElevatorCondition = "Active";
            StartCoroutine(GoDown(3));
        }
        else if (ElevatorCondition == "InActive" && ElevatorPosition < 3)
        {
            ElevatorCondition = "Active";
            StartCoroutine(GoUp(3));
        }
        else if (ElevatorCondition == "Active")
        {
            Floor3 = "Up";
        }
    }
    public void ThirdFloorButtonDown()
    {
        if (ElevatorCondition == "InActive" && ElevatorPosition > 3)
        {
            ElevatorCondition = "Active";
            StartCoroutine(GoDown(3));
        }
        else if (ElevatorCondition == "InActive" && ElevatorPosition < 3)
        {
            ElevatorCondition = "Active";
            StartCoroutine(GoUp(3));
        }
        else if (ElevatorCondition == "Active")
        {
            Floor3 = "Down";
        }
    }
    public void SecondFloorButtonUp()
    {
        if (ElevatorCondition == "InActive" && ElevatorPosition > 2)
        {
            ElevatorCondition = "Active";
            StartCoroutine(GoDown(2));
        }
        else if (ElevatorCondition == "InActive" && ElevatorPosition < 2)
        {
            ElevatorCondition = "Active";
            StartCoroutine(GoUp(2));
        }
        else if (ElevatorCondition == "Active")
        {
            Floor2 = "Up";
        }
    }
    public void SecondFloorButtonDown()
    {
        if (ElevatorCondition == "InActive" && ElevatorPosition > 2)
        {
            ElevatorCondition = "Active";
            StartCoroutine(GoDown(2));
        }
        else if (ElevatorCondition == "InActive" && ElevatorPosition < 2)
        {
            ElevatorCondition = "Active";
            StartCoroutine(GoUp(2));
        }
        else if(ElevatorCondition == "Active")
        {
            Floor2 = "Down";
        }

    }
    public void FirstFloorButton()
    {
        if (ElevatorCondition == "InActive")
        {
            ElevatorCondition = "Active";
            StartCoroutine(GoDown(1));
        }
        else if (ElevatorCondition == "Active")
        {
            Floor1 = "Up";
        }
    }
    public void Elevator4Floor()
    {
        if (ElevatorCondition == "InActive")
        {
            ElevatorCondition = "Active";
            StartCoroutine(GoUp(4));
        }
        else if (ElevatorCondition == "Active")
        {
            Floor4 = "Down";
        }
    }
    public void Elevator3Floor()
    {
        if (ElevatorCondition == "InActive" && ElevatorPosition > 3)
        {
            ElevatorCondition = "Active";
            StartCoroutine(GoDown(3));
        }
        else if (ElevatorCondition == "InActive" && ElevatorPosition < 3)
        {
            ElevatorCondition = "Active";
            StartCoroutine(GoUp(3));
        }
        else if (ElevatorCondition == "Active" && ElevatorPosition <= 3)
        {
            Floor3 = "Up";
        }
        else if (ElevatorCondition == "Active" && ElevatorPosition >= 3)
        {
            Floor3 = "Down";
        }

    }
    public void Elevator2Floor()
    {
        if (ElevatorCondition == "InActive" && ElevatorPosition > 2)
        {
            ElevatorCondition = "Active";
            StartCoroutine(GoDown(2));
        }
        else if (ElevatorCondition == "InActive" && ElevatorPosition < 2)
        {
            ElevatorCondition = "Active";
            StartCoroutine(GoUp(2));
        }
        else if (ElevatorCondition == "Active" && ElevatorPosition <= 2)
        {
            Floor2 = "Up";
        }
        else if (ElevatorCondition == "Active" && ElevatorPosition >= 2)
        {
            Floor2 = "Down";
        }
    }
    public void Elevator1Floor()
    {
        if (ElevatorCondition == "InActive")
        {
            ElevatorCondition = "Active";
            StartCoroutine(GoDown(1));
        }
        else if (ElevatorCondition == "Active")
        {
            Floor1 = "Up";
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------
    //������ STOP ������������� ���� �� ��������� ����� (�������� ��������) � �������� ��� ������ �����
    //----------------------------------------------------------------------------------------------------------------------------------------------------
    public void STOP()
    {
        stop = true;
       Floor1 = "Null"; Floor2 = "Null"; Floor3 = "Null"; Floor4 = "Null"; ElevatorCondition = "InActive";
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------
    //������ PAUSE ��������� ����������������� �������� ������ �����, � ��� �� ���������� ��������� ����� �� �����
    //����� ���� ���������� ���������� �������
    //----------------------------------------------------------------------------------------------------------------------------------------------------
    public void PAUSE()
    {
        Thread.Sleep(2000);
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------------
    //������ ������ � UNITY ��������� ����� �� ����������
    //----------------------------------------------------------------------------------------------------------------------------------------------------
    public void Exit()
    {
        Application.Quit();
    }

}
