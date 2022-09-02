using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PhysBox.Constants;

namespace PhysBox {

    public static class Constants {

        public const double pi = Math.PI;

        /// <summary>
        /// Speed of light in a vacuum in m.s^-1
        /// </summary>
        public const double c = 2.998e8;

        /// <summary>
        /// Permittivity of a vacuum in F.m^-1
        /// </summary>
        public const double epsilon_0 = 8.854e-12;

        /// <summary>
        /// Permeability of a vacuum in N.A^-2
        /// </summary>
        public const double mu_0 = 4e-7 * pi;

        /// <summary>
        /// Planck constant in J.s
        /// </summary>
        public const double h = 6.626e-34;

        /// <summary>
        /// Reduced Planck constant in J.s
        /// </summary>
        public const double hbar = h / (2 * pi);

        /// <summary>
        /// Gravitational constant in N.m^2.kg^-2
        /// </summary>
        public const double G = 6.674e-11;

        /// <summary>
        /// Acceleration due to gravity on Earth in m.s^-2
        /// </summary>
        public const double g = 9.81;

        /// <summary>
        /// Unified atomic mass constant in kg
        /// </summary>
        public const double m_u = 1.661e-27;

        /// <summary>
        /// Rest mass of an electron in kg
        /// </summary>
        public const double m_e = 9.109e-31;

        /// <summary>
        /// Rest mass of proton in kg
        /// </summary>
        public const double m_p = 1.673e-27;

        /// <summary>
        /// Elementary charge (i.e. charge of a proton) in C
        /// </summary>
        public const double e = 1.602e-19;

        /// <summary>
        /// Avogadro constant in mol^-1
        /// </summary>
        public const double N_A = 6.022e23;

        /// <summary>
        /// Boltzmann constant in J.K^-1
        /// </summary>
        public const double k_B = 1.381e-23;

        /// <summary>
        /// Molar gas constant in J.K^-1.mol^-1
        /// </summary>
        public const double R = 8.314;

        /// <summary>
        /// Stefan-Boltzmann constant in W.m^-2.K^-4
        /// </summary>
        public const double StefanBoltzmann = 5.670e-8;

    }

    public static class Utils {

        /// <summary>
        /// Convert value in radians to degrees
        /// </summary>
        public static double ToDeg(double value) {
            return 180 * value / pi;
        }

        /// <summary>
        /// Convert value in degrees to radians
        /// </summary>
        public static double ToRad(double value) {
            return pi * value / 180;
        }

    }
}
