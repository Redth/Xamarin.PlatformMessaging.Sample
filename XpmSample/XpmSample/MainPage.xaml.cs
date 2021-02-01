using System.Diagnostics;
using Xamarin.Forms;

namespace XpmSample
{
    public partial class MainPage : ContentPage
    {
        readonly IXpmSampleLib _xpmSampleLib;

        public MainPage()
        {
            InitializeComponent();
            _xpmSampleLib = XpmSampleContainer.XpmSampleLib;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _xpmSampleLib.TextLogged += TextLogged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _xpmSampleLib.TextLogged -= TextLogged;
        }

        void LogText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return;

            _xpmSampleLib.LogText(text);
        }

        void Entry_Completed(System.Object sender, System.EventArgs e)
            => LogText(TextToLogEntry.Text);

        void Log_Button_Clicked(System.Object sender, System.EventArgs e)
            => LogText(TextToLogEntry.Text);

        void Hello_Button_Clicked(System.Object sender, System.EventArgs e)
            => ShowAlert(_xpmSampleLib.GetText());

        void TextLogged(object sender, string e)
            => ShowAlert($"Logged: {e}");

        void ShowAlert(string text)
            => DisplayAlert(nameof(XpmSample), text, "OK").ContinueWith((task)
                => { if (task.IsFaulted) Trace.TraceError(task.Exception?.Message); });
    }
}