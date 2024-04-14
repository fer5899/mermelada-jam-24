using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossAttackInfo : MonoBehaviour
{

    public GameManagerSO gameManager;
    [SerializeField] private GameObject bossAttackInfoBanner;
    [SerializeField] private TextMeshProUGUI bossAttackInfoText;
    [SerializeField] private float bannerDuration = 4f;

    public void OnEnable()
    {
        gameManager.onBossAttack.AddListener(ShowBossAttackInfo);
    }

    public void OnDisable()
    {
        gameManager.onBossAttack.RemoveListener(ShowBossAttackInfo);
    }

    public void ShowBossAttackInfo(string attackInfo)
    {
        bossAttackInfoBanner.SetActive(true);
        bossAttackInfoText.text = attackInfo;
        StopAllCoroutines();
        StartCoroutine(DisableBanner());
    }

    private IEnumerator DisableBanner()
    {
        yield return new WaitForSeconds(bannerDuration);
        bossAttackInfoBanner.SetActive(false);
    }
}
