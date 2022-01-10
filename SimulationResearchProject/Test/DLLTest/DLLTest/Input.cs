﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLLTest
{
    class Input
    {
        public static bool GetKeyDown(KeyCode keycode)
        {
            return RenderProgramDLL.GetKeyDown((int)keycode);
        }

        public static bool GetKeyUp(KeyCode keycode)
        {
            return RenderProgramDLL.GetKeyUp((int)keycode);
        }

        public static bool GetKey(KeyCode keycode)
        {
            return RenderProgramDLL.GetKey((int)keycode);
        }

        public static bool GetMouseKeyDown(int key)
        {
            return RenderProgramDLL.GetMouseKeyDown(key);
        }

        public static bool GetMouseKeyUp(int key)
        {
            return RenderProgramDLL.GetMouseKeyUp(key);
        }

        public static bool GetMouseKey(int key)
        {
            return RenderProgramDLL.GetMouseKey(key);
        }
    }

    public enum KeyCode
    {
        KeyPad0 = 48,
        KeyPad1 = 49,
        KeyPad2 = 50,
        KeyPad3 = 51,
        KeyPad4 = 52,
        KeyPad5 = 53,
        KeyPad6 = 54,
        KeyPad7 = 55,
        KeyPad8 = 56,
        KeyPad9 = 57,
        A = 65,
 	    B = 66,
 	    C = 67,
 	    D = 68,
 	    E = 69,
 	    F = 70,
 	    G = 71,
 	    H = 72,
 	    I = 73,
 	    J = 74,
 	    K = 75,
 	    L = 76,
 	    M = 77,
 	    N = 78,
 	    O = 79,
 	    P = 80,
 	    Q = 81,
 	    R = 82, 
 	    S = 83,
 	    T = 84,
 	    U = 85,
 	    V = 86,
 	    W = 87,
 	    X = 88,
 	    Y = 89,
 	    Z = 90,
    }
}