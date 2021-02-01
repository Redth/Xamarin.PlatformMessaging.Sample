//
//  AppDelegate.swift
//  XpmSample
//
//  Created by Mike Parker on 29/01/2021.
//

import UIKit
import XpmSampleLib
import XamarinPlatformMessaging

@main
class AppDelegate: UIResponder, UIApplicationDelegate {
    let _channelId = "XpmSampleLib"
    let  _getTextMessageId = "get_text";
    let  _logTextMessageId = "log_text";
    let  _textLoggedMessageId = "text_logged";
    
    var _channel: MessageChannel?

    func application(_ application: UIApplication, didFinishLaunchingWithOptions launchOptions: [UIApplication.LaunchOptionsKey: Any]?) -> Bool {
        
        // Override point for customization after application launch.
        XpmSampleLib.start()
        
        _channel = MessageChannel(channelId: _channelId)
        
        if let channel = _channel {
            channel.setHandler(forMessageId: _textLoggedMessageId) { (parameters) in
                if let args = parameters, let arg = args.first as? NSString {
                    NSLog("Text Logged: \(arg)")
                }
             }
            
            do {
                try NSLog("\(_getTextMessageId): \(channel.sendForResponse(messageId: _getTextMessageId) as! String)")
                try channel.send(messageId: _logTextMessageId, parameters: "Hello from AppDelegate" as NSObject)
                
                try channel.clearHandler(forMessageId: _textLoggedMessageId)
            }
            catch {
                NSLog(error.localizedDescription)
            }
        }
        
        _channel = nil
        XpmSampleLib.stop()
        
        return true
    }

    // MARK: UISceneSession Lifecycle

    func application(_ application: UIApplication, configurationForConnecting connectingSceneSession: UISceneSession, options: UIScene.ConnectionOptions) -> UISceneConfiguration {
        // Called when a new scene session is being created.
        // Use this method to select a configuration to create the new scene with.
        return UISceneConfiguration(name: "Default Configuration", sessionRole: connectingSceneSession.role)
    }

    func application(_ application: UIApplication, didDiscardSceneSessions sceneSessions: Set<UISceneSession>) {
        // Called when the user discards a scene session.
        // If any sessions were discarded while the application was not running, this will be called shortly after application:didFinishLaunchingWithOptions.
        // Use this method to release any resources that were specific to the discarded scenes, as they will not return.
    }
}
