using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elapsed.Utility
{
    public static class PhysicsUtility
    {

        #region Units
        public enum DistanceUnits { cm, mm, m, inch };
        #endregion
        
        #region Gravity
        public static class Gravity
        {
            #region Magnitude
            private static float _gravityMagnitude;
            public static float gravityMagnitude { get { return _gravityMagnitude; } }
            public static void GravityMagnitude()
            {
                _gravityMagnitude = Physics.gravity.magnitude;
            }

            #endregion

            #region Up
            private static Vector3 _up;
            public static Vector3 up { get { return _up; } }
            public static void GravityUp()
            {
                _up = -Physics.gravity.normalized;
            }
            #endregion
        }
        #endregion

        #region Atmosphere
        public enum TemperatureUnit { Kelvin, Celsius, Fahrenheit }
        public static class Atmosphere
        {
            // https://de.wikipedia.org/wiki/Normatmosph%C3%A4re
            // https://de.wikipedia.org/wiki/Gradient#Temperaturgradient
            // https://de.wikipedia.org/wiki/Barometrische_H%C3%B6henformel#Die_H%C3%B6henstufen

            #region Atmospheric Temperature
            // Atmospheric Temperature in Kelvin(K)
            private static float _temperature = 288.15f;            // 0°C = 273.15K || 15°C = 288.15K || 20°C = 293.15K
            public static float temperature { get { return _temperature; } set { _temperature = value; } }
            #endregion

            #region Temperature Gradient
            // Temperature Gradient in Kelvin(K)/Meter(m)
            private static float _temperatureGradient = 0.0065f;    // 
            private static float _exponent = 5.255f;                // 0.03416 / 0.0065f = 5.2553846153846153846153846153846f;
            #endregion

            #region Atmospheric Pressure
            // Atmospheric Pressure in Pascal(hPa)
            private static float _pressure = 101325f;
            #endregion

            #region Gas Constant
            // Universal Gas Constant
            // https://en.wikipedia.org/wiki/Gas_constant
            private static float _universalGasConstant = 8.3145f;   // 8.31446261815324
            #endregion

            #region Molar Mass
            // Molar Mass of Air in Kg/Mol
            // https://de.wikipedia.org/wiki/Molare_Masse
            // https://de.wikipedia.org/wiki/Luft
            private static float _molarMass = 0.02996f; // Kg/Mol
            #endregion

            #region Density
            // Air density in Kg/m³
            // https://de.wikipedia.org/wiki/Luftdichte
            private static float _density = 1.225f;     // Kg/m³
            public static float density { get { return _density; } }
            #endregion

            public static void SetTemperature(float temperature)
            {
                _temperature = temperature;
            }
            public static void SetTemperature(float temperature, TemperatureUnit temperatureUnit = TemperatureUnit.Kelvin)
            {
                switch (temperatureUnit)
                {
                    case TemperatureUnit.Celsius:       // Celsius
                        _temperature = _temperature + 273.15f;
                        break;
                    case TemperatureUnit.Fahrenheit:    // Fahrenheit
                        _temperature = (temperature - 273.15f) * 1.8f + 32.00f;
                        break;
                    default:                            // Kelvin
                        _temperature = temperature;
                        break;
                }
            }
            public static float GetTemperatureAtHeight(float height)
            {
                if (height > 11000.0f)          // Stratossphere
                {
                    return 216.65f;
                }
                else                            // Tropossphere
                {
                    return _temperature - _temperatureGradient * height;
                }
            }

            public static void SetTemperatureGradient(float temperatureGradient)
            {
                _temperatureGradient = temperatureGradient;

                _exponent = 0.03416f / temperatureGradient;
            }

            public static float GetPressureAtHeight(float height)
            {
                // hPa = 1013.25 * (1.0 - (0.0065 * h / 288.15))^5.255
                return (_pressure * 0.01f) * Mathf.Pow(1.0f - (_temperatureGradient * height / _temperature), _exponent) * 100.0f;
            }

            public static float GetDensity()
            {
                // https://de.wikipedia.org/wiki/Luftdichte#Berechnung
                return (_pressure * _molarMass) / (_universalGasConstant * _temperature);
            }

            private static Vector3 _wind = new Vector3(0.0f, 0.0f, 0.0f);
            public static Vector3 wind { get { return _wind; } set { _wind = value; } }
        }
        #endregion
    }
    #region ForceTypes
    public enum ForceType
    {
        /// <summary>
        /// Force is always applied
        /// </summary>
        Active,
        /// <summary>
        /// Force is only applied if drag is disabled
        /// </summary>
        Passive
    }
    #endregion
}