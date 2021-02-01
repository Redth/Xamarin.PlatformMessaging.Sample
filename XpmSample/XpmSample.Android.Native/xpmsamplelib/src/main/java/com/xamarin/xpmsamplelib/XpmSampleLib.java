package com.xamarin.xpmsamplelib;

import android.content.Context;
import android.util.Log;

import com.xamarin.platformmessaging.MessageChannel;

public class XpmSampleLib {
    static MessageChannel _channel;
    static final String Tag = "XpmSampleLib";
    static final String ChannelId = "XpmSampleLib";
    static final String GetTextMessageId = "get_text";
    static final String LogTextMessageId = "log_text";
    static final String TextLoggedMessageId = "text_logged";

    public static void start(Context context) {
        _channel = new MessageChannel(ChannelId);

        _channel.setResponseHandler(GetTextMessageId, (parameters) -> {
            return "Hello from Java";
        });

        _channel.setHandler(LogTextMessageId, (parameters) -> {
            String arg = (parameters != null && parameters.length == 1) ? (String)parameters[0] : null;

            if (arg != null) {
                Log.i(Tag, arg);
            }
            else {
                Log.i(Tag, "No text provided");
            }

            try
            {
                _channel.send(TextLoggedMessageId, arg);
            }
            catch (Exception ex) {
                Log.e(Tag, ex.getLocalizedMessage());
            }
        });
    }

    public static void stop() {
        if (_channel == null) {
            return;
        }

        try {
            _channel.clearResponseHandler(GetTextMessageId);
            _channel.clearHandler(LogTextMessageId);
        }
        catch (Exception ex) {
            Log.e(Tag, ex.getLocalizedMessage());
        }

        _channel = null;
    }
}