using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsteroidClicker : MonoBehaviour
{
    public Animator asteroidAnimator; // Public reference to the attached Animator

    private int score = 0;
    private float clickCooldown = 3f;
    private float gameDuration = 0f;
    private float timeRemaining;
    private float nextAsteroidTime; // Time when the next asteroid becomes clickable
    private bool isAsteroidClickable = true; // Flag to track asteroid clickability

    public Text scoreText;
    public Text countdownText;
    public Text asteroidTimerText;

    public ParticleSystem asteroidParticles;

    private void Start()
    {
        gameDuration = Random.Range(60f, 120f);
        timeRemaining = gameDuration;
        SetNextAsteroidTime(); // Initialize the first asteroid availability
    }

    private void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        int secondsRemaining = Mathf.CeilToInt(timeRemaining);
        countdownText.text = "Time: " + secondsRemaining;

        // Handle asteroid countdown
        if (!isAsteroidClickable)
        {
            nextAsteroidTime -= Time.deltaTime;
            int asteroidSecondsRemaining = Mathf.CeilToInt(nextAsteroidTime);
            asteroidTimerText.text = "Next Asteroid: " + asteroidSecondsRemaining;

            if (nextAsteroidTime <= 0f)
            {
                isAsteroidClickable = true; // Enable asteroid clickability
                SetNextAsteroidTime(); // Set time for the next asteroid
            }
        }
    }

    private void OnMouseDown()
    {
        if (isAsteroidClickable)
        {
            score++;
            scoreText.text = "Score: " + score;
            asteroidAnimator.Play("AsteroidPopup", -1, 0f); // Play the attached animation from the beginning
            isAsteroidClickable = false; // Disable asteroid clickability during animation
            asteroidParticles.Play();
        }
    }

    private void SetNextAsteroidTime()
    {
        nextAsteroidTime = Random.Range(2f, 5f);
    }
}
