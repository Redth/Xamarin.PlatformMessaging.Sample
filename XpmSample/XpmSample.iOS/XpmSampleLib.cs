using System;
using System.Linq;
using Foundation;
using Xamarin.PlatformMessaging;

namespace XpmSample.iOS
{
    public class XpmSampleLib : IXpmSampleLib, IDisposable
    {
        const string XpmSampleLibClassName = nameof(XpmSampleLib);

        MessageChannel _channel;

        public XpmSampleLib()
        {
            _channel = new MessageChannel(XpmSampleLibClassName);
            _channel.SetHandler(XpmSampleLibMessages.TextLogged, HandleTextLogged);
            Platform.Start(XpmSampleLibClassName);
        }

        public event EventHandler<string> TextLogged;

        public void Dispose()
        {
            Platform.Stop(XpmSampleLibClassName);
            _channel.ClearHandler(XpmSampleLibMessages.TextLogged);
            _channel = null;
        }

        public string GetText()
            => _channel.SendForResponse<NSString>(XpmSampleLibMessages.GetText)?.Description;

        public void LogText(string text)
            => _channel.Send(XpmSampleLibMessages.LogText, new NSString(text));

        void HandleTextLogged(NSObject[] parameters)
            => TextLogged?.Invoke(this, parameters.FirstOrDefault()?.Description);
    }
}