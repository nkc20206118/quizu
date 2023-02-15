using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTest : MonoBehaviour
{
    [SerializeField] private QS QData; // データを格納
    [SerializeField] private Text nameText; // 表示するモンスターの名前のテキスト

    [SerializeField] public GM mondai;
    private void Update()
    {
        // 表示のテキストをモンスターの名前（リストの1番はじめの）に変える
        nameText.text = QData.Q[mondai.i].Q;
    }
}
