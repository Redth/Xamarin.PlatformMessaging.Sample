using System;

namespace XpmSample
{
    public interface IXpmSampleLib
    {
        event EventHandler<string> TextLogged;
        void LogText(string text);
        string GetText();
    }
}