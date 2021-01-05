using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JackSParrot.Utils;
using UnityEngine.SceneManagement;
using JackSParrot.Services.Localization;

public class TutorialClassicController : MonoBehaviour
{

    [SerializeField] GameObject redObstacle;
    [SerializeField] GameObject greenObstacle;
    [SerializeField] GameObject blueObstacle;

    [SerializeField] GameObject goldenObstacle;
    [SerializeField] GameObject miniGolden1;
    [SerializeField] GameObject miniGolden2;
    [SerializeField] GameObject miniGolden3;

    [SerializeField] TextMeshProUGUI tutorialText;
    [SerializeField] TextMeshProUGUI tutorialChangeText;

    private ILocalizationService service;


    private bool tutorialStarted;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitializeLocalizationService());
    }

    // Update is called once per frame
    void Update()
    {
        if(!tutorialStarted && GameController.instance.GameStarted)
        {
            StartCoroutine(StartTutorial());
        }
    }


    IEnumerator StartTutorial()
    {
        tutorialStarted = true;
        UpdateText("UI_CLASSIC_TUTORIAL_0");

        tutorialText.gameObject.SetActive(false);
        tutorialChangeText.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(5f);
        tutorialChangeText.text = "";

        yield return new WaitForSeconds(0.5f);
        UpdateText("UI_CLASSIC_TUTORIAL_1");

        yield return new WaitForSeconds(4f);
        tutorialChangeText.text = "";
        
        yield return new WaitForSeconds(0.5f);
        UpdateText("UI_CLASSIC_TUTORIAL_2");

        yield return new WaitForSeconds(1.5f);
        StartCoroutine(StartPooling());
        tutorialChangeText.gameObject.SetActive(false);


    }

    IEnumerator StartPooling()
    {
        yield return new WaitForSeconds(1f);
        redObstacle.SetActive(true); 
        redObstacle.GetComponent<TutorialClassicObstacle>().CanMove = true;

        yield return new WaitForSeconds(1.5f);
        greenObstacle.SetActive(true);
        greenObstacle.GetComponent<TutorialClassicObstacle>().CanMove = true;

        yield return new WaitForSeconds(1.5f);
        blueObstacle.SetActive(true);
        blueObstacle.GetComponent<TutorialClassicObstacle>().CanMove = true;

    }

    public IEnumerator ColorOK()
    {
        StartCoroutine(redObstacle.GetComponent<TutorialClassicObstacle>().ResetObstacle());
        StartCoroutine(greenObstacle.GetComponent<TutorialClassicObstacle>().ResetObstacle());
        StartCoroutine(blueObstacle.GetComponent<TutorialClassicObstacle>().ResetObstacle());

        redObstacle.GetComponent<TutorialClassicObstacle>().CanMove = false;
        greenObstacle.GetComponent<TutorialClassicObstacle>().CanMove = false;
        blueObstacle.GetComponent<TutorialClassicObstacle>().CanMove = false;

        tutorialChangeText.gameObject.SetActive(true);
        UpdateText("UI_CLASSIC_TUTORIAL_3");

        yield return new WaitForSeconds(2f);
        tutorialChangeText.text = "";

        yield return new WaitForSeconds(0.5f);
        UpdateText("UI_CLASSIC_TUTORIAL_4");

        yield return new WaitForSeconds(4f);
        tutorialChangeText.text = "";

        yield return new WaitForSeconds(0.5f);
        UpdateText("UI_CLASSIC_TUTORIAL_5");

        yield return new WaitForSeconds(1f);
        goldenObstacle.SetActive(true);
        tutorialChangeText.gameObject.SetActive(false);
    }

    public IEnumerator GoldenOK()
    {
        Debug.Log("GoldenOK");
        if (!miniGolden1.activeInHierarchy)
        {
            miniGolden1.SetActive(true);
            StartCoroutine(goldenObstacle.GetComponent<TutorialClassicObstacle>().ResetObstacle());
        }
        else if (!miniGolden2.activeInHierarchy)
        {
            miniGolden2.SetActive(true);
            StartCoroutine(goldenObstacle.GetComponent<TutorialClassicObstacle>().ResetObstacle());
        }
        else if (!miniGolden3.activeInHierarchy)
        {
            miniGolden3.SetActive(true);

            StartCoroutine(goldenObstacle.GetComponent<TutorialClassicObstacle>().ResetObstacle());
            goldenObstacle.GetComponent<TutorialClassicObstacle>().CanMove = false;

            yield return new WaitForSeconds(0.2f);
            tutorialChangeText.gameObject.SetActive(true);
            UpdateText("UI_CLASSIC_TUTORIAL_6");

            yield return new WaitForSeconds(3f);
            tutorialChangeText.text = "";

            yield return new WaitForSeconds(0.5f);
            UpdateText("UI_CLASSIC_TUTORIAL_7");

            yield return new WaitForSeconds(3f);
            PlayerPrefs.SetInt("classicTutorial", 1);
            SceneManager.LoadScene("GameMode_Classic");
        }

    }

    public IEnumerator ColorKO()
    {
        StartCoroutine(redObstacle.GetComponent<TutorialClassicObstacle>().ResetObstacle());
        StartCoroutine(greenObstacle.GetComponent<TutorialClassicObstacle>().ResetObstacle());
        StartCoroutine(blueObstacle.GetComponent<TutorialClassicObstacle>().ResetObstacle());

        redObstacle.GetComponent<TutorialClassicObstacle>().CanMove = false;
        greenObstacle.GetComponent<TutorialClassicObstacle>().CanMove = false;
        blueObstacle.GetComponent<TutorialClassicObstacle>().CanMove = false;

        yield return new WaitForSeconds(1f);
        tutorialChangeText.gameObject.SetActive(true);
        UpdateText("UI_CLASSIC_TUTORIAL_1");

        yield return new WaitForSeconds(4f);
        tutorialChangeText.text = "";

        yield return new WaitForSeconds(1f);
        UpdateText("UI_CLASSIC_TUTORIAL_2");

        yield return new WaitForSeconds(1f);
        StartCoroutine(StartPooling());

        yield return new WaitForSeconds(1f);
        tutorialChangeText.gameObject.SetActive(false);

    }

    IEnumerator InitializeLocalizationService()
    {
        service = SharedServices.GetService<ILocalizationService>();
        if (service == null)
        {
            service = new LocalLocalizationService();
            service.Initialize(() => Debug.Log("LocalizationService Initialized"));
            SharedServices.RegisterService<ILocalizationService>(service);
        }
        while (!service.Initialized)
        {
            yield return new WaitForSeconds(1.0f);
        }
    }

    void UpdateText(string _key)
    {
        string label = service.GetLocalizedString(_key);
        label = label.Replace("[LB]", "\r\n");
        tutorialChangeText.text = label;
    }
}
