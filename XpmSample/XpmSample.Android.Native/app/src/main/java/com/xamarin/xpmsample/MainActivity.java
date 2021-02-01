package com.xamarin.xpmsample;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.util.Log;

import com.xamarin.platformmessaging.MessageChannel;
import com.xamarin.xpmsamplelib.XpmSampleLib;

public class MainActivity extends AppCompatActivity {

    final String Tag = "XpmSample";
    static final String ChannelId = "XpmSampleLib";
    final String GetTextMessageId = "get_text";
    final String LogTextMessageId = "log_text";
    final String TextLoggedMessageId = "text_logged";

    MessageChannel _channel;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        XpmSampleLib.start(this);

        _channel = new MessageChannel(ChannelId);

        _channel.setHandler(TextLoggedMessageId, (parameters) -> {
            String arg = (parameters != null && parameters.length == 1) ? (String)parameters[0] : null;
            Log.i(Tag, String.format("Text Logged: %s", arg));
        });

        try {
            Log.i(Tag, String.format("%s: %s", GetTextMessageId, (String)_channel.sendForResponse(GetTextMessageId)));
            _channel.send(LogTextMessageId, "Hello from MainActivity");

            _channel.clearHandler(TextLoggedMessageId);
        }
        catch (Exception ex) {
            Log.e(Tag, ex.getLocalizedMessage());
        }

        _channel = null;
        XpmSampleLib.stop();
    }
}