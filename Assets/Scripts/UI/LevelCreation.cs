using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelCreation : MonoBehaviour {

    //LevelDisplay
    [SerializeField] Sprite[] sprites;
    [SerializeField] Button[] arrows;
    [SerializeField] Sprite[] spritesLevelName;
    [SerializeField] string[] levelNames;
    [SerializeField] Image levelTitle;
    [SerializeField] GameObject _playerMenu;
    public GameObject levelDisplay;
    Button _levelDisplayButton;
    private Image _levelDisplayImage;
    int _levelDisplayImgNumberIndex;
    float _horizontalDirection;
    public EventSystem eventSystem;
    StandaloneInputModule standAloneInputModule;
   // public string level1, level2, level3;
    
    //Delay
    float _delay;
    float _inputDelayTimer;
    bool _isInputOnDelay;

    void Start ()
    {
        standAloneInputModule = eventSystem.GetComponent<StandaloneInputModule>();
       _levelDisplayButton = levelDisplay.GetComponent<Button>();
       _levelDisplayImage = levelDisplay.GetComponent<Image>();
        _levelDisplayImgNumberIndex = 0;
        levelTitle.sprite = spritesLevelName[_levelDisplayImgNumberIndex];
        _levelDisplayImage.sprite = sprites[_levelDisplayImgNumberIndex];
        _delay = standAloneInputModule.repeatDelay;
    }
	
	// Update is called once per frame
	void Update ()
    {
       
        //Bewegen van sprites Links en Rechts
        _horizontalDirection = Input.GetAxis("Horizontal_P1");
      
        //als input on delay wordt geactiveert
        if(_isInputOnDelay && _horizontalDirection > 0.4f || _horizontalDirection < -0.4f)
        {
            _inputDelayTimer += Time.deltaTime;
            if(_inputDelayTimer >= _delay)
            {
                _isInputOnDelay = false;
                _inputDelayTimer = 0f;
            }
        }
        else if(_isInputOnDelay && _horizontalDirection < 0.5f || _horizontalDirection > -0.5f)
        {
            _inputDelayTimer = 0;
            _isInputOnDelay = false;
        }


        if (_levelDisplayButton.gameObject == eventSystem.currentSelectedGameObject && !_isInputOnDelay)// && _timeSinceLastInput >= _delay
        {
            if (_horizontalDirection > 0.4f)
            {
                _levelDisplayImgNumberIndex++;
               // arrows[0]. = arrows[0].OnPointerDown;
                _isInputOnDelay = true;
            }
            else if (_horizontalDirection < -0.4f)
            {
                _levelDisplayImgNumberIndex--;

                _isInputOnDelay = true;
            }
        }

        //Looping index
        if (_levelDisplayImgNumberIndex > sprites.Length-1)
        {
            _levelDisplayImgNumberIndex = 0;
        }
        else if (_levelDisplayImgNumberIndex < 0)
        {
            _levelDisplayImgNumberIndex = sprites.Length-1;
        }

        //Afbeelding weergeven
        _levelDisplayImage.sprite = sprites[_levelDisplayImgNumberIndex];
        levelTitle.sprite = spritesLevelName[_levelDisplayImgNumberIndex];

    }


    public void ChangeScene() // make sure to attach this to event trigger
    {
        switch (_levelDisplayImgNumberIndex)
        {

            case 0:
                SceneManager.LoadScene(levelNames[_levelDisplayImgNumberIndex]);
                break;
            case 1:
                SceneManager.LoadScene(levelNames[_levelDisplayImgNumberIndex]);
                break;
            case 2:
                SceneManager.LoadScene(levelNames[_levelDisplayImgNumberIndex]);
                break;
            default:
                Debug.Log("Error");
                break;  
        }
    }
}
