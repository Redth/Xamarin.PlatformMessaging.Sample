using System;
using System.Linq;
using Xamarin.PlatformMessaging;

namespace XpmSample.Droid
{
    public class XpmSampleLib : IXpmSampleLib, IDisposable
    {
        const string XpmSampleLibPackageName = "com.xamarin.xpmsamplelib";
        const string XpmSampleLibClassName = nameof(XpmSampleLib);

        MessageChannel _channel;

        public XpmSampleLib()
        {
            _channel = new MessageChannel(XpmSampleLibClassName);
            _channel.SetHandler(XpmSampleLibMessages.TextLogged, HandleTextLogged);
            Platform.Start(XpmSampleLibPackageName, XpmSampleLibClassName);
        }

        public event EventHandler<string> TextLogged;

        public void Dispose()
        {
            Platform.Stop(XpmSampleLibPackageName, XpmSampleLibClassName);
            _channel.ClearHandler(XpmSampleLibMessages.TextLogged);
            _channel = null;
        }

        public string GetText()
            => _channel.SendForResponse<Java.Lang.String>(XpmSampleLibMessages.GetText)?.ToString();

        public void LogText(string text)
            => _channel.Send(XpmSampleLibMessages.LogText, new Java.Lang.String(text));

        void HandleTextLogged(Java.Lang.Object[] parameters)
            => TextLogged?.Invoke(this, parameters.FirstOrDefault()?.ToString());
    }
}