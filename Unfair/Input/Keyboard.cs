using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Unfair.Input
{
    /// <summary>
    /// Represents the keyboard and its keys states.
    /// </summary>
    public class Keyboard
    {
        private Hashtable keys, prevKeys;
        private short[] allKeys;

        public const int KEY_PRESSED = 0x8000;

        public static bool GetKeyDown(VirtualKeyShort key)
        {
            return GetKeyDown((int)key);
        }

        public static bool GetKeyDown(int key)
        {
            return Convert.ToBoolean(GetKeyState(key) & KEY_PRESSED);
        }

        public static bool GetKeyDownAsync(int key)
        {
            return GetKeyDownAsync((VirtualKeyShort)key);
        }

        public static bool GetKeyDownAsync(VirtualKeyShort key)
        {
            return Convert.ToBoolean(GetAsyncKeyState(key) & KEY_PRESSED);
        }

        public Keyboard()
        {
            keys = new Hashtable();
            prevKeys = new Hashtable();
            VirtualKeyShort[] _keys = (VirtualKeyShort[])Enum.GetValues(typeof(VirtualKeyShort));
            allKeys = new short[_keys.Length];
            for (int i = 0; i < allKeys.Length; i++)
                allKeys[i] = (short)_keys[i];

            Init();
        }

        ~Keyboard()
        {
            keys.Clear();
            prevKeys.Clear();
        }

        /// <summary>
        /// Initializes and fills the hashtables
        /// </summary>
        private void Init()
        {
            foreach (int key in allKeys)
            {
                if (!prevKeys.ContainsKey(key))
                {
                    prevKeys.Add(key, false);
                    keys.Add(key, false);
                }
            }
        }

        /// <summary>
        /// Updates the key-states
        /// </summary>
        public void Update()
        {
            prevKeys = (Hashtable)keys.Clone();
            foreach (int key in allKeys)
            {
                keys[key] = GetKeyDown(key);
            }
        }

        /// <summary>
        /// Returns an array of all keys that went up since the last Update-call
        /// </summary>
        /// <returns></returns>
        public VirtualKeyShort[] KeysThatWentUp()
        {
            List<VirtualKeyShort> keys = new List<VirtualKeyShort>();

            foreach (VirtualKeyShort key in allKeys)
            {
                if (KeyWentUp(key))
                    keys.Add(key);
            }

            return keys.ToArray();
        }

        /// <summary>
        /// Returns an array of all keys that went down since the last Update-call
        /// </summary>
        /// <returns></returns>
        public VirtualKeyShort[] KeysThatWentDown()
        {
            List<VirtualKeyShort> keys = new List<VirtualKeyShort>();

            foreach (VirtualKeyShort key in allKeys)
            {
                if (KeyWentDown(key))
                    keys.Add(key);
            }

            return keys.ToArray();
        }

        /// <summary>
        /// Returns an array of all keys that went are down since the last Update-call
        /// </summary>
        /// <returns></returns>
        public VirtualKeyShort[] KeysThatAreDown()
        {
            List<VirtualKeyShort> keys = new List<VirtualKeyShort>();
            foreach (VirtualKeyShort key in allKeys)
            {
                if (KeyIsDown(key))
                    keys.Add(key);
            }
            return keys.ToArray();
        }

        /// <summary>
        /// Returns whether the given key went up since the last Update-call
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns></returns>
        public bool KeyWentUp(VirtualKeyShort key)
        {
            return KeyWentUp((int)key);
        }

        /// <summary>
        /// Returns whether the given key went up since the last Update-call
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns></returns>
        public bool KeyWentUp(int key)
        {
            if (!KeyExists(key))
                return false;

            return (bool)prevKeys[key] && !(bool)keys[key];
        }

        /// <summary>
        /// Returns whether the given key went down since the last Update-call
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns></returns>
        public bool KeyWentDown(VirtualKeyShort key)
        {
            return KeyWentDown((int)key);
        }

        /// <summary>
        /// Returns whether the given key went down since the last Update-call
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns></returns>
        public bool KeyWentDown(int key)
        {
            if (!KeyExists(key))
                return false;

            return !(bool)prevKeys[key] && (bool)keys[key];
        }

        /// <summary>
        /// Returns whether the given key was down at time of the last Update-call
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns></returns>
        public bool KeyIsDown(VirtualKeyShort key)
        {
            return KeyIsDown((int)key);
        }

        /// <summary>
        /// Returns whether the given key was down at time of the last Update-call
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns></returns>
        public bool KeyIsDown(int key)
        {
            if (!KeyExists(key))
                return false;

            return (bool)prevKeys[key] || (bool)keys[key];
        }

        /// <summary>
        /// Returns whether the given key is contained in the used hashtables
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns></returns>
        private bool KeyExists(int key)
        {
            return (prevKeys.ContainsKey(key) && keys.ContainsKey(key));
        }

        [DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(Keys vKey);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern uint MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(VirtualKeyShort vKey);

        [DllImport("user32.dll")]
        public static extern int GetKeyState(Int32 vKey);
    }
}