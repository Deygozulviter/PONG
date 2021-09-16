using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    // Rigidbody 2D bola
    private Rigidbody2D rigidBody2D;

    // Besarnya gaya awal yang diberikan untuk mendorong bola
    // public float xInitialForce;
    // public float yInitialForce;

    // Besarnya laju awal bola yang diinginkan dari dorongan bola
    public float initialForce;

    // Besarnya jangkauan derajat arah awal bola yang diinginkan dari dorongan bola
    // relatif terhadap x axis. ( 1 <= degree <= 89)
    [Range(1f,89f)]
    public float degreeRange;

    // Titik asal lintasan bola saat ini
    private Vector2 trajectoryOrigin;

    // Ketika bola beranjak dari sebuah tumbukan, rekam titik tumbukan tersebut
    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }

    // Untuk mengakses informasi titik asal lintasan
    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }

    void ResetBall()
    {
        // Reset posisi menjadi (0,0)
        transform.position = Vector2.zero;

        // Reset kecepatan menjadi (0,0)
        rigidBody2D.velocity = Vector2.zero;
    }
    
    void PushBall()
    {
//         // Tentukan nilai komponen y dari gaya dorong antara -yInitialForce dan yInitialForce
//         float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);

//         // Tentukan nilai acak antara 0 (inklusif) dan 2 (eksklusif)
//         float randomDirection = Random.Range(0, 2);

//         // Jika nilainya di bawah 1, bola bergerak ke kiri. 
//         // Jika tidak, bola bergerak ke kanan.
//         if (randomDirection < 1.0f)
//         {
//             // Gunakan gaya untuk menggerakkan bola ini.
//             rigidBody2D.AddForce(new Vector2(-xInitialForce, yRandomInitialForce));
//         }
//         else
//         {
//             rigidBody2D.AddForce(new Vector2(xInitialForce, yRandomInitialForce));
//         }

        // Tentukan nilai radian dari random angle
        float randomAngle = Random.Range(-degreeRange, degreeRange);
        if (randomAngle == 0) {
            randomAngle += Random.Range(1f, 10f);
        }
        float randomRadian = randomAngle * Mathf.PI / 180;
        // float randomRadian = degreeRange * Mathf.PI / 180;

        // Tentukan nilai acak antara 0 (inklusif) dan 2 (eksklusif)
        float randomDirection = Random.Range(0, 2);

        float xInitialForce = initialForce * Mathf.Cos(randomRadian);
        float yInitialForce = initialForce * Mathf.Sin(randomRadian);

        // Jika nilainya di bawah 1, bola bergerak ke kiri. 
        if (randomDirection < 1.0f) {
            xInitialForce = -xInitialForce;
        }
        
        // Gunakan gaya untuk menggerakkan bola ini.
        rigidBody2D.AddForce(new Vector2(xInitialForce, yInitialForce));
    }

    void RestartGame()
    {
        // Kembalikan bola ke posisi semula
        ResetBall();

        // Setelah 2 detik, berikan gaya ke bola
        Invoke("PushBall", 2);
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        trajectoryOrigin = transform.position;

        // Mulai game
        RestartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}