using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PhysBox.Constants;

namespace PhysBox {

    public static class Constants {

        public const float pi = Mathf.PI;

        /// <summary>
        /// Speed of light in a vacuum in m.s^-1
        /// </summary>
        public const float c = 2.998e8f;

        /// <summary>
        /// Permittivity of a vacuum in F.m^-1
        /// </summary>
        public const float epsilon_0 = 8.854e-12f;

        /// <summary>
        /// Permeability of a vacuum in N.A^-2
        /// </summary>
        public const float mu_0 = 4e-7f * pi;

        /// <summary>
        /// Planck constant in J.s
        /// </summary>
        public const float h = 6.626e-34f;

        /// <summary>
        /// Reduced Planck constant in J.s
        /// </summary>
        public const float hbar = h / (2 * pi);

        /// <summary>
        /// Gravitational constant in N.m^2.kg^-2
        /// </summary>
        public const float G = 6.674e-11f;

        /// <summary>
        /// Acceleration due to gravity on Earth in m.s^-2
        /// </summary>
        public const float g = 9.81f;

        /// <summary>
        /// Unified atomic mass constant in kg
        /// </summary>
        public const float m_u = 1.661e-27f;

        /// <summary>
        /// Rest mass of an electron in kg
        /// </summary>
        public const float m_e = 9.109e-31f;

        /// <summary>
        /// Rest mass of proton in kg
        /// </summary>
        public const float m_p = 1.673e-27f;

        /// <summary>
        /// Elementary charge (i.e. charge of a proton) in C
        /// </summary>
        public const float e = 1.602e-19f;

        /// <summary>
        /// Avogadro constant in mol^-1
        /// </summary>
        public const float N_A = 6.022e23f;

        /// <summary>
        /// Boltzmann constant in J.K^-1
        /// </summary>
        public const float k_B = 1.381e-23f;

        /// <summary>
        /// Molar gas constant in J.K^-1.mol^-1
        /// </summary>
        public const float R = 8.314f;

        /// <summary>
        /// Stefan-Boltzmann constant in W.m^-2.K^-4
        /// </summary>
        public const float StefanBoltzmann = 5.670e-8f;

    }

    public static class Utils {

        /// <summary>
        /// Convert value in radians to degrees
        /// </summary>
        public static float ToDeg(float value) {
            return 180 * value / pi;
        }

        /// <summary>
        /// Convert value in degrees to radians
        /// </summary>
        public static float ToRad(float value) {
            return pi * value / 180;
        }

    }
}
