using UnityEngine;
using Unity.Cinemachine;

public class CinemachineExtensionControl : MonoBehaviour
{
    [Header("Configura��es de Sensibilidade")]
    [SerializeField] private float horizontalSpeed = 100f; // Sensibilidade do movimento horizontal
    [SerializeField] private float verticalSpeed = 100f;   // Sensibilidade do movimento vertical

    [Header("Limites de Rota��o Vertical")]
    [SerializeField] private float minVerticalAngle = -80f; // �ngulo m�nimo para olhar para baixo
    [SerializeField] private float maxVerticalAngle = 80f;  // �ngulo m�ximo para olhar para cima

    private CinemachineCamera cinemachineCamera; // Refer�ncia para a c�mera do Cinemachine
    private float rotationX = 0f; // Rota��o acumulada no eixo X (vertical)
    private float rotationY = 0f; // Rota��o acumulada no eixo Y (horizontal)

    private void Start()
    {
        // Obt�m a refer�ncia para o componente CinemachineCamera
        cinemachineCamera = GetComponent<CinemachineCamera>();

        // Trava o cursor no centro da tela e o torna invis�vel
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Captura o delta do mouse usando o m�todo GameInput.Instance.GetMouseDelta()
        Vector2 mouseDelta = GameInput.Instance.GetMouseDelta();

        // Calcula o movimento com base no delta do mouse e na sensibilidade
        float mouseX = mouseDelta.x * horizontalSpeed * Time.deltaTime;
        float mouseY = mouseDelta.y * verticalSpeed * Time.deltaTime;

        // Atualiza a rota��o acumulada
        rotationY += mouseX; // Rota��o horizontal (eixo Y)
        rotationX -= mouseY; // Rota��o vertical (eixo X)
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle); // Limita a rota��o vertical

        // Aplica a rota��o � c�mera do Cinemachine
        if (cinemachineCamera != null)
        {
            // Atualiza a rota��o da c�mera
            cinemachineCamera.transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
        }
    }

    private void OnDestroy()
    {
        // Restaura o comportamento padr�o do cursor ao destruir o script
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}