using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MovCam : MonoBehaviour
{
    public static MovCam Instance { get; private set; }

    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;

    private float _tiempoMovimiento;
    private float _tiempoMovimientoTotal;
    private float _intensidadInicial;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        _cinemachineBasicMultiChannelPerlin =
            _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void MoverCamara(float intensidad, float frecuencia, float tiempo)
    {
        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensidad;
        _cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frecuencia;
        _intensidadInicial = intensidad;
        _tiempoMovimientoTotal = tiempo;
        _tiempoMovimiento = tiempo;
    }

    private void Update()
    {
        if (_tiempoMovimiento > 0)
        {
            _tiempoMovimiento -= Time.deltaTime;
            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain =
                Mathf.Lerp(_intensidadInicial, 0, 1 - (_tiempoMovimiento / _tiempoMovimientoTotal));
        }
    }
}
