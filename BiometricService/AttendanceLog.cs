using System;

namespace BiometricService
{
    public class AttendanceLog
    {
        public string EnrollNumber { get; set; }
        public int VerifyMode { get; set; }
        public int InOutMode { get; set; }
        public DateTime Date { get; set; }
        public int WorkCode { get; set; }
    }
}