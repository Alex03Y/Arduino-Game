using System;
using System.Collections;
using System.IO.Ports;
using UnityEngine;

public class SerialReader : MonoBehaviour
{
    private SerialPort _port = new SerialPort("COM8", 9600, Parity.None, 8, StopBits.One);
    public event Action<int> OnDataReceived;
    
    private IEnumerator Start()
    {
        while (true)
        {
            try
            {
                _port.Open();
                break;
            }
            catch (Exception e)
            {
                #if UNITY_EDITOR
                Debug.LogError("Port Exception Handled!");
                Debug.LogError("Error Message: " + e.Message);
                #endif
                if(_port.IsOpen) _port.Close();
                _port.Dispose();
                _port = new SerialPort("COM8", 9600, Parity.None, 8, StopBits.One);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private void Update()
    {
        if (!_port.IsOpen) return;
        var result = _port.ReadLine();
        if (!string.IsNullOrEmpty(result) && int.TryParse(result, out var parsedInt))
            OnDataReceived?.Invoke(parsedInt);
    }

    private void OnDestroy()
    {
        if(_port.IsOpen) _port.Close();
        _port?.Dispose();
    }

    private void OnDisable()
    {
        if(_port.IsOpen) _port.Close();
        _port?.Dispose();
    }

    private void OnApplicationQuit()
    {
        if(_port.IsOpen) _port.Close();
        _port?.Dispose();
    }
}
