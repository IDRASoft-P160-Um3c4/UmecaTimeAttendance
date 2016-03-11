using System;

namespace BiometricService
{
    public class EnrollEventArgs : EventArgs
    {
        public bool Result { get; }

        public int Finger { get; }

        public string FingerPrint { get; }

        public EnrollEventArgs(bool result, int finger, string fingerPrint)
        {
            Result = result;
            Finger = finger;
            FingerPrint = fingerPrint;
        }
    }
}