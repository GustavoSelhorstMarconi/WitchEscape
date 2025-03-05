using UnityEngine;
using Unity.Cinemachine;

public class CinemachineExtensionControl : MonoBehaviour
{
    [Header("Configurações de Sensibilidade")]
    [SerializeField] private float horizontalSpeed = 100f; // Sensibilidade do movimento horizontal
    [SerializeField] private float verticalSpeed = 100f;   // Sensibilidade do movimento vertical

    [Header("Limites de Rotação Vertical")]
    [SerializeField] private float minVerticalAngle = -80f; // Ângulo mínimo para olhar para baixo
    [SerializeField] private float maxVerticalAngle = 80f;  // Ângulo máximo para olhar para cima

    private CinemachineCamera cinemachineCamera; // Referência para a câmera do Cinemachine
    private float rotationX = 0f; // Rotação acumulada no eixo X (vertical)
    private float rotationY = 0f; // Rotação acumulada no eixo Y (horizontal)

    private void Start()
    {
        // Obtém a referência para o componente CinemachineCamera
        cinemachineCamera = GetComponent<CinemachineCamera>();

        // Trava o cursor no centro da tela e o torna invisível
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Captura o delta do mouse usando o método GameInput.Instance.GetMouseDelta()
        Vector2 mouseDelta = GameInput.Instance.GetMouseDelta();

        // Calcula o movimento com base no delta do mouse e na sensibilidade
        float mouseX = mouseDelta.x * horizontalSpeed * Time.deltaTime;
        float mouseY = mouseDelta.y * verticalSpeed * Time.deltaTime;

        // Atualiza a rotação acumulada
        rotationY += mouseX; // Rotação horizontal (eixo Y)
        rotationX -= mouseY; // Rotação vertical (eixo X)
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle); // Limita a rotação vertical

        // Aplica a rotação à câmera do Cinemachine
        if (cinemachineCamera != null)
        {
            // Atualiza a rotação da câmera
            cinemachineCamera.transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
        }
    }

    private void OnDestroy()
    {
        // Restaura o comportamento padrão do cursor ao destruir o script
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}