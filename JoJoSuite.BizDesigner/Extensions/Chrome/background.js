// Copyright 2018 The Chromium Authors. All rights reserved.
// Use of this source code is governed by a BSD-style license that can be
// found in the LICENSE file.

'use strict';

// chrome.runtime.onInstalled.addListener(function() {
//   chrome.storage.sync.set({color: '#3aa757'}, function() {
//     console.log("The color is green.");
//   });
// });

var active = false;
var empty = true;
var clicked = false;
var asserting = false;
var test_seq = [];

chrome.tabs.onUpdated.addListener(function(tabId, changeInfo, tab) {
    if (!(tab.url.startsWith("chrome"))) {
        // chrome.tabs.executeScript(null, { file: 'jquery-3.4.1.min.js', allFrames: true }, function () {
        //   chrome.tabs.executeScript(null, { file: "recordScript-2.js", allFrames: true });
        // });
        if (!document.WebRecorder === undefined ? 'false' : 'true') {
            //chrome.tabs.executeScript(null, { file: "Main.js", allFrames: false });
            chrome.tabs.executeScript(null, { file: "Spark.js", allFrames: true }, function() {
                chrome.tabs.executeScript(null, { file: "Main.js", allFrames: false });
            });
            chrome.tabs.insertCSS(null, {
                file: "core.css",
                allFrames: true
            });
            // chrome.tabs.executeScript(null, { file: "test.js", allFrames: true }, function() {
            //     //chrome.tabs.executeScript(null, { file: "testmain.js", allFrames: false });
            // });

        }

    }
});


chrome.runtime.onMessage.addListener((data, sender) => {
    chrome.extension.getBackgroundPage().console.log("testing");
    // First, validate the message's structure.
    if (data.jsonElement != '') {
        // Enable the page-action for the requesting tab.
        //alert(data.jsonElement);

        var xhr = new XMLHttpRequest();
        var url = "http://localhost:47581/api/Command/SaveCommand";
        xhr.open("POST", url, true);
        xhr.setRequestHeader("Content-Type", "application/json");
        xhr.onreadystatechange = function() {
            if (xhr.readyState === 4 && xhr.status === 200) {
                var json = JSON.parse(xhr.responseText);
                console.log(json);
            }
        };

        xhr.send(data.jsonElement);

        // chrome.tabs.runtime.sendMessage({
        //     from: 'fromParent',
        //     subject: 'showPageAction',
        // });
    }
});