//
//  XpmSampleLib.swift
//  XpmSampleLib
//
//  Created by Mike Parker on 29/01/2021.
//

import Foundation
import XamarinPlatformMessaging

@objc(XpmSampleLib)
public class XpmSampleLib : NSObject {
    static var _channel: MessageChannel?
    static let _channelId = "XpmSampleLib"
    static let  _getTextMessageId = "get_text";
    static let  _logTextMessageId = "log_text";
    static let  _textLoggedMessageId = "text_logged";
    
    @objc(start)
    public static func start() {
        _channel = MessageChannel(channelId: _channelId)
        
        if let channel = _channel {
            channel.setResponseHandler(forMessageId: _getTextMessageId) { (parameters) in
               return "Hello from Swift" as NSObject
            }
            
            channel.setHandler(forMessageId: _logTextMessageId) { (parameters) in
                do {
                    if let args = parameters, let arg = args.first as? NSString {
                        NSLog(arg.description)
                        try channel.send(messageId: _textLoggedMessageId, parameters: arg)
                    }
                    else {
                        NSLog("No text provided")
                        try channel.send(messageId: _textLoggedMessageId, parameters: nil)
                    }
                } catch {
                    NSLog(error.localizedDescription)
                }
            }
        }
    }
    
    @objc(stop)
    public static func stop() {
        guard let channel = _channel else {return}
        
        do {
            try channel.clearResponseHandler(forMessageId: _getTextMessageId)
            try channel.clearHandler(forMessageId: _logTextMessageId)
        }
        catch {
            NSLog(error.localizedDescription)
        }
        
        _channel = nil
    }
}
