using System;

namespace TestWebApi_DAL.Logging
{
    public interface ILog
    {
        void Information(string message);
        void Warning(string message);
        void Debug(string message);
        void Error(Exception ex);
    }
}
