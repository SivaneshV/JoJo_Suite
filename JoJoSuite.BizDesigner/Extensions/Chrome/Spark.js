if (document.body != null) {
    WebRecorder = (function () {

    });

    document.WebRecorder = new WebRecorder();
}

const syncWait = ms => {
    const end = Date.now() + ms
    while (Date.now() < end) continue
}

var MOUSE_VISITED_CLASSNAME = 'recorder_mouse_visited';
var x, i;
useraction = 1;
Action = 0;

var typevalue = new Array();

var ELEMENT_NODE = 1;

//window.parent.parent.parent.parent.parent.document.isRecordingPaused = false;
document.isRecordingPaused = false;


function IsRecording() {
    var xhr = new XMLHttpRequest();
    var url = "http://localhost:47581/api/Command/IsRecording";
    xhr.open("GET", url, true);
    //xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4 && xhr.status === 200) {
            var json = JSON.parse(xhr.responseText);
            try {
                document.getElementById("content").innerHTML = json;
            }
            catch { }
            if (json.toLowerCase() == "recording") {
                window.parent.parent.parent.parent.parent.document.isRecordingPaused = false;
                try {
                    document.getElementById("circle").style = "visibility: visible;";
                }
                catch { }
            }
            else {
                window.parent.parent.parent.parent.parent.document.isRecordingPaused = true;
                try {
                    document.getElementById("circle").style = "visibility: hidden;";
                }
                catch { }
            }
        }
    };
    xhr.send();
}

if (document.body != null) {
    IsRecording();
}

var inputSendKeys = ['email', 'month', 'number', 'password', 'search', 'tel', 'text', 'time', 'url', 'week', 'date', 'datetime-local', 'textarea'];

var clickCotnrols = ['button', 'submit', 'checkbox', 'color', 'file', 'radio', 'range', 'reset', 'img'];

var inputTypes = [
    'text',
    'password',
    'file',
    'datetime',
    'datetime-local',
    'date',
    'month',
    'time',
    'week',
    'number',
    'range',
    'email',
    'url',
    'search',
    'tel',
    'color',
]

var selectOptionTypes = [
    'select'
]


/**
 * MouseOver action for all elements on the page:
 */
function inspectorMouseOver(e) {
    if (!window.parent.parent.parent.parent.parent.document.isRecordingPaused) {
        // NB: this doesn't work in IE (needs fix):
        var element = e.target;
        //console.log(element.id)
        //console.log(element.closest('div'))
        //console.log(element.closest('span'))
        if (element.id != "recordLoadingClose" && element.id != "recordLoading" && element.id != "recording" &&
            element.id != "ide-img" && element.id != "circle" && element.id != "content") {
            if (element.closest('div') != null) {
                //console.log('DIV')
                if (!element.closest('div').id.includes('WebDrivePopUp') &&
                    !element.closest('div').className.includes('WebDrivePopUp')) {
                    //console.log('Not POPUP')
                    // Set outline:
                    // element.style.setProperty("outline", "3px solid rgba(250, 0, 0, 0.7)", "important");
                    // element.style.setProperty("background", "rgba(73, 229, 255, 0.4)", "important");
                    // element.style.setProperty("cursor", "pointer", "important");
                    try {
                        if (element.id != null) {
                            if (element.id.includes('WebDrivePopUp')) {
                                return false;
                            }
                        }
                    } catch { }
                    //console.log(element)
                    element.classList.add(MOUSE_VISITED_CLASSNAME);

                    // Set last selected element so it can be 'deselected' on cancel.
                    last = element;
                } else {
                    return false;
                }
            } else if (element.closest('span') != null) {
                //console.log('SPAN')
                if (!element.closest('span').id.includes('WebDrivePopUp') &&
                    !element.closest('span').className.includes('WebDrivePopUp')) {
                    // Set outline:
                    // element.style.setProperty("outline", "3px solid rgba(250, 0, 0, 0.7)", "important");
                    // element.style.setProperty("background", "rgba(73, 229, 255, 0.4)", "important");
                    // element.style.setProperty("cursor", "pointer", "important");
                    try {
                        if (element.id != null) {
                            if (element.id.includes('WebDrivePopUp')) {
                                return false;
                            }
                        }
                    } catch { }
                    element.classList.add(MOUSE_VISITED_CLASSNAME);

                    // Set last selected element so it can be 'deselected' on cancel.
                    last = element;
                } else {
                    return false;
                }
            } else {
                console.log('ELSE')
                element.classList.add(MOUSE_VISITED_CLASSNAME);
            }
        }
        //else if (element.id != "recordLoadingClose") {
        //    element.style.outline = '2px solid #f00';
        //    last = element;
        //}
    }
}

addStyle = function (css) {
    var head, style;
    head = document.getElementsByTagName('head')[0];
    style = document.createElement('style');
    style.type = 'text/css';
    if (style.styleSheet) {
        style.styleSheet.cssText = css;
    } else {
        style.appendChild(document.createTextNode(css));
    }
    head.appendChild(style);
};

/**
 * MouseOut event action for all elements
 */
function inspectorMouseOut(e) {
    // Remove outline from element:
    // e.target.style.outline = '';
    // e.target.style.background = '';
    // element.style.cursor = '';
    e.target.classList.remove(MOUSE_VISITED_CLASSNAME);
}

function inspectorCancel(e) {
    // Unbind inspector mouse and click events:
    if (e === null && event.keyCode === 27) { // IE (won't work yet):
        document.detachEvent("mouseover", inspectorMouseOver);
        document.detachEvent("mouseout", inspectorMouseOut);

        document.detachEvent("keydown", inspectorCancel);
        last.style.outlineStyle = 'none';
    } else if (e.which === 27) { // Better browsers:
        document.removeEventListener("mouseover", inspectorMouseOver, true);
        document.removeEventListener("mouseout", inspectorMouseOut, true);

        document.removeEventListener("keydown", inspectorCancel, true);

        // Remove outline on last-selected element:
        last.style.outline = 'none';
    }
    /* else if (e.which==9)
     {
       blurHandler(e);
     }*/
}

function inspectorKeyDown(e) {
    var srcElement = e.srcElement;
    try {
        if (e.which === 13) {
            if (srcElement.type != undefined) {
                if (srcElement.type != null) {
                    if (srcElement.type.toLowerCase() == 'password' || srcElement.type.toLowerCase() == 'text' || srcElement.type.toLowerCase() == 'search' || srcElement.type.toLowerCase() == 'email') {
                        blurHandler(e);
                        //keyHandler(e);
                    }
                }
            }
        }
    } catch { }
}

function setCurrentFrame(event) {
    var frames = window.parent.parent.parent.document.frameDocuments;
    for (var key in frames) {
        if (frames.hasOwnProperty(key)) {
            if (event.ownerDocument == frames[key][0]) {
                window.parent.parent.parent.parent.parent.document.currentFrame = key;
            }
        }
    }
}


/**
 * Add event listeners for DOM-inspectorey actions
 */
if (document.body != null) {
    if (document.addEventListener) {
        document.addEventListener("mouseover", inspectorMouseOver, true);
        document.addEventListener("mouseout", inspectorMouseOut, true);


        //document.addEventListener("mousemove", removeStopEvent, true);

        //document.addEventListener("keydown", inspectorCancel, true);
        document.addEventListener("keydown", inspectorKeyDown, true);

        document.addEventListener("click", function (e) {
            try {
                if (e.target.type == undefined) {
                    if (selectOptionTypes.indexOf(e.target.tagName.toLowerCase()) < 0) {
                        clickHandler(e.target);
                    }
                }
                else {
                    if (inputTypes.indexOf(e.target.type.toLowerCase()) < 0 && selectOptionTypes.indexOf(e.target.tagName.toLowerCase()) < 0) {
                        clickHandler(e.target);
                    }
                }
            } catch { }
        }, true);

        document.addEventListener("change", function (e) {
            try {
                if (inputTypes.indexOf(e.target.type.toLowerCase()) >= 0) {
                    blurHandler(e.target);
                }
            } catch { }
        }, true);

        // document.addEventListener("focus", function (e) {
        //     try {
        //         if (inputTypes.indexOf(e.target.type.toLowerCase()) >= 0) {
        //             blurHandler(e.target);
        //         }
        //     } catch { }
        // }, true);

        document.addEventListener("select", function (e) {
            try {
                if (inputTypes.indexOf(e.target.type.toLowerCase()) >= 0) {
                    blurHandler(e.target);
                }
            } catch { }
        }, true);


        x = document.querySelectorAll("input");
        for (i = 0; i < x.length; i++) {
            try {
                if (inputTypes.indexOf(e.target.type.toLowerCase()) >= 0) {
                    x[i].addEventListener('change', blurHandler);
                }
            } catch (error) {

            }
        }

        x = document.querySelectorAll("textarea");
        for (i = 0; i < x.length; i++) {
            try {
                if (inputTypes.indexOf(e.target.type.toLowerCase()) >= 0) {
                    x[i].addEventListener('change', blurHandler);
                }
            } catch (error) {

            }
        }

        inputTypes.forEach(elType => {
            x = document.querySelectorAll(elType);
            for (i = 0; i < x.length; i++) {
                //if (x[i].closest('div').id != 'WebDrivePopUp') {
                x[i].addEventListener('change', blurHandler);
                //}
            }
        });

        x = document.querySelectorAll("select");
        for (i = 0; i < x.length; i++) {
            //if (x[i].closest('div').id != 'WebDrivePopUp') {
            x[i].addEventListener('change', blurHandler);
            //}
        }

        x = document.querySelectorAll("iframe");
        for (i = 0; i < x.length; i++) {
            x[i].addEventListener('mouseout', function (eve) {
                eve.fromElement.contentDocument.getElementById("WebDrivePopUp").style.display = "none";
            });
        }

        // document.addEventListener("change", function(e) {
        //     try {
        //         if (selectOptionTypes.indexOf(e.target.tagName) >= 0) {
        //             blurHandler(e.target);
        //         }
        //     } catch {}
        // }, true);

        //x = document.querySelectorAll("input");
        //for (i = 0; i < x.length; i++) {
        //    if (x[i].closest('div') != null) {
        //        if (x[i].closest('div').id != 'WebDrivePopUp') {
        //            //if (x[i].type.toLowerCase() == "text" || x[i].type.toLowerCase() == "password") {
        //            if (inputSendKeys.some(el => x[i].type.toLowerCase().includes(el))) {
        //                //x[i].addEventListener('blur', blurHandler);
        //                x[i].addEventListener('change', blurHandler);
        //            }
        //            //else if (x[i].type.toLowerCase() == "button" || x[i].type.toLowerCase() == "submit") {
        //            else if (clickCotnrols.some(el => x[i].type.toLowerCase().includes(el))) {
        //                x[i].addEventListener('click', blurHandler);
        //            }
        //        }
        //    }
        //    else {
        //        //if (x[i].type.toLowerCase() == "text" || x[i].type.toLowerCase() == "password") {
        //        if (inputSendKeys.some(el => x[i].type.toLowerCase().includes(el))) {
        //            x[i].addEventListener('change', blurHandler);
        //        }
        //        //else if (x[i].type.toLowerCase() == "button" || x[i].type.toLowerCase() == "submit") {
        //        else if (clickCotnrols.some(el => x[i].type.toLowerCase().includes(el))) {
        //            x[i].addEventListener('click', blurHandler);
        //        }
        //    }
        //}

        // document.addEventListener('keyup', function(event) {
        //     var key = event.key || event.keyCode;
        //     if (key === 119) {

        //     }
        // });


        // x = document.querySelectorAll("input");
        // for (i = 0; i < x.length; i++) {
        //     //if (x[i].type.toLowerCase() == "text" || x[i].type.toLowerCase() == "password") {
        //     if (inputSendKeys.some(el => x[i].type.toLowerCase().includes(el))) {
        //         x[i].addEventListener('change', blurHandler);
        //     }
        //     //else if (x[i].type.toLowerCase() == "button" || x[i].type.toLowerCase() == "submit") {
        //     // else if (clickCotnrols.some(el => x[i].type.toLowerCase().includes(el))) {
        //     //     x[i].addEventListener('click', blurHandler);
        //     // }
        // }

        //x = document.querySelectorAll("span");
        //for (i = 0; i < x.length; i++) {

        //    //else if (x[i].type.toLowerCase() == "button" || x[i].type.toLowerCase() == "submit") {
        //    //if (clickCotnrols.some(el => x[i].type.toLowerCase().includes(el))) {
        //    x[i].addEventListener('click', blurHandler);
        //    //}
        //}

        // x = document.querySelectorAll("span");
        // for (i = 0; i < x.length; i++) {
        //     x[i].addEventListener('click', blurHandler);
        // }


        // x = document.querySelectorAll("a");
        // for (i = 0; i < x.length; i++) {

        //     //else if (x[i].type.toLowerCase() == "button" || x[i].type.toLowerCase() == "submit") {
        //     //if (clickCotnrols.some(el => x[i].type.toLowerCase().includes(el))) {
        //     x[i].addEventListener('click', blurHandler);
        //     //}
        // }

        //x = document.querySelectorAll("div");
        //for (i = 0; i < x.length; i++) {

        //    //else if (x[i].type.toLowerCase() == "button" || x[i].type.toLowerCase() == "submit") {
        //    //if (clickCotnrols.some(el => x[i].type.toLowerCase().includes(el))) {
        //    //x[i].addEventListener('click', blurHandler);
        //    x[i].setAttribute('onclick', blurHandler);
        //    //}
        //}

        //jQuery(document).on('click', 'div', function (e) { blurHandler(e); });

        //x = document.querySelectorAll("div");
        //for (i = 0; i < x.length; i++) {
        //    if (x[i].closest('div') != null) {
        //        if (x[i].closest('div').id != 'WebDrivePopUp') {
        //            //else if (x[i].type.toLowerCase() == "button" || x[i].type.toLowerCase() == "submit") {
        //            //if (clickCotnrols.some(el => x[i].type.toLowerCase().includes(el))) {
        //            x[i].addEventListener('click', blurHandler);
        //            //}
        //        }
        //    }
        //    else {
        //        x[i].addEventListener('click', blurHandler);
        //    }
        //}




    } else if (document.attachEvent) {
        document.attachEvent("mouseover", inspectorMouseOver);
        document.attachEvent("mouseout", inspectorMouseOut);

        //document.attachEvent("mousemove", setCurrentFrame);

        document.attachEvent("keydown", inspectorCancel);
    }


    document.onclick = function (e) {
        if (!window.parent.parent.parent.parent.parent.document.isRecordingPaused) {
            if (e.srcElement.tagName != null) {
                if (e.srcElement.tagName.toLowerCase() != "input" && e.srcElement.tagName.toLowerCase() != "select"
                    //&& e.srcElement.tagName.toLowerCase() != "div"
                    //&& e.srcElement.closest('div').id != 'WebDrivePopUp'
                    &&
                    e.srcElement.id != "recordLoadingClose") {
                    var x = e.pageX;
                    var y = e.pageY;
                    //alert("User clicked at position (" + x + "," + y + ")")

                    //try {
                    //    if (e.srcElement.parentElement != undefined) {
                    //        if (e.srcElement.parentElement.tagName.toLowerCase() == 'li') {
                    //            if (e.srcElement.parentElement.parentElement.tagName.toLowerCase() == 'ul') {
                    //                if (e.srcElement.parentElement.parentElement.parentElement.tagName.toLowerCase() == 'li') {

                    //                    var li = e.srcElement.parentElement.parentElement.parentElement.getElementsByTagName('a');
                    //                    if (li.length > 0) {
                    //                        blurHandler(li[0]);
                    //                    }

                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    //catch { }

                    //blurHandler(e);
                }
            }
        }
        //if (e.srcElement.closest('div') != null)
        //    if (e.srcElement.closest('div').id != 'WebDrivePopUp')
        //        closeForm();
    };

    document.onchange = function (e) {
        //if (e.srcElement.closest('div') != null) {
        //    if (e.srcElement.closest('div').id != 'WebDrivePopUp') {
        if (!window.parent.parent.parent.parent.parent.document.isRecordingPaused) {
            if (e.srcElement.tagName != null) {
                if (inputSendKeys.some(el => e.srcElement.type.toLowerCase().includes(el))) {
                    //if (e.srcElement.tagName.toLowerCase() == "select") {
                    var x = e.pageX;
                    var y = e.pageY;
                    //alert("User clicked at position (" + x + "," + y + ")")
                    //blurHandler(e);
                    //}
                }
            }
        }
        //}
        //}
        //else {
        //    if (!window.parent.parent.parent.parent.parent.document.isRecordingPaused) {
        //        if (e.srcElement.tagName != null) {
        //            if (!inputSendKeys.some(el => x[i].type.toLowerCase().includes(el))) {
        //                //if (e.srcElement.tagName.toLowerCase() == "select") {
        //                var x = e.pageX;
        //                var y = e.pageY;
        //                //alert("User clicked at position (" + x + "," + y + ")")
        //                blurHandler(e);
        //                //}
        //            }
        //        }
        //    }
        //}
    };

    //document.onkeypress = function (e) {
    //    if (e.which == 13) {
    //        return false;
    //    }
    //};
}

function webDriveActionChange () {
    if (document.getElementById("WebDrivePopUp_Actions").value.toLowerCase() == "click") {
        document.getElementById("WebDrivePopUp_TextValue").style = "display: none";
        document.getElementById("WebDrivePopUp_SelectValue").style = "display: none";
    } else if (document.getElementById("WebDrivePopUp_Actions").value.toLowerCase() == "type") {
        document.getElementById("WebDrivePopUp_TextValue").style = "display: block";
        document.getElementById("WebDrivePopUp_SelectValue").style = "display: none";
    } else {
        document.getElementById("WebDrivePopUp_TextValue").style = "display: none";
        document.getElementById("WebDrivePopUp_SelectValue").style = "display: block";
    }
};

closeForm = function () {
    return document.getElementById('WebDrivePopUp').style.display = 'none';
}

function createElementForm() {
    var closeClickHandler, element;
    element = document.createElement("div");
    element.id = 'WebDrivePopUp';
    if (document.body != null) {
        document.body.appendChild(element);
    } else {

    }
    closeClickHandler = "";
    element.innerHTML = '\
        <table id="WebDrivePopUpTable">\
            <tr>\
              <td>Target</td>\
              <td colspan="2">\
              </td>\
            </tr>\
            <tr>\
                <td>Actions</td>\
                <td><select id="WebDrivePopUp_Actions" onchange="webDriveActionChange()"><option>click</option><option>type</option><option>selectoption</option><option>mouseover</option><option>read</option></select></td>\
                <td> <input type="text"  id="WebDrivePopUp_TextValue">\
                     <select id="WebDrivePopUp_SelectValue" ></select></td>\
            </tr>\
            <tr>\
              <td>Id:</td>\
              <td colspan="2"><span id="WebDrivePopUp_ElementId">Element</span></td>\
            </tr>\
            <tr>\
              <td>XPath:</td>\
              <td colspan="2" style="word-break: break-all"><span id="WebDrivePopUp_XPathLocator">Element</span></td>\
            </tr>\
            <tr style="display:none">\
              <td>Css:</td>\
              <td colspan="2"><span id="WebDrivePopUp_CssSelector">Element</span></td>\
            </tr>\
            <tr style="display:none">\
              <td>Text:</td>\
              <td colspan="2"><span id="WebDrivePopUp_ElementText">Element</span><input type="hidden" id="hdnType" value="" /></td>\
            </tr>\
            <tr style="display:none">\
              <td>Element</td>\
              <td colspan="2"><span id="WebDrivePopUp_ElementName">Element</span><input type="hidden" id="hdnFrameId" value="" /></td>\
            </tr>\
            <tr>\
                <td colspan="3">\
                <input type="button" value="CAPTURE" class="WebDrivePopUp_Button" onclick="addElement()">\
                <input type="button" value="CANCEL" class="WebDrivePopUp_Cancel_Button" onclick="parent.document.getElementById(\'WebDrivePopUp\').style.display = \'none\'; window.parent.parent.parent.parent.document.getElementById(\'WebDrivePopUp\').style.display = \'none\';">\
                </td>\
            </tr>\
            </table>\
        ';

    element = document.createElement("script");
    element.innerHTML = '\
    pseudoGuid = function () {\
        var result;\
        result = "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx";\
        result = result.replace(/[xy]/g, function (re_match) {\
            var random_value, replacement;\
            random_value = Math.random() * 16 | 0;\
            replacement = re_match === "x" ? random_value : random_value & 0x3 | 0x8;\
            return replacement.toString(16);\
        });\
        return result;\
    };\
    addElement = function() {\
        var JsonData, XPathLocatorElement, codeIDTextElement, htmlIdElement, ActionType, ControlValue;\
        codeIDTextElement = document.getElementById("WebDrivePopUp_CodeIDText");\
        htmlIdElement = document.getElementById("WebDrivePopUp_ElementId");\
        CssSelectorElement = document.getElementById("WebDrivePopUp_CssSelector");\
        XPathLocatorElement = document.getElementById("WebDrivePopUp_XPathLocator");\
        ActionType = document.getElementById("WebDrivePopUp_Actions");\
        ControlValue = document.getElementById("WebDrivePopUp_TextValue");\
        FrameId = document.getElementById("hdnFrameId");\
        Type = document.getElementById("hdnType");\
        JsonData = {\
            "Command": "AddElement",\
            "Caller": "addElement",\
            "CommandId": pseudoGuid(),\
            "ElementCodeName": codeIDTextElement.value,\
            "ElementId": (htmlIdElement.hasChildNodes()) ? htmlIdElement.firstChild.nodeValue : "",\
            "ElementCssSelector": CssSelectorElement.firstChild.nodeValue,\
            "ElementXPath": XPathLocatorElement.firstChild.nodeValue,\
            "ElementAction": ActionType.value,\
            "ElementValue": ControlValue.value,\
            "FrameId": FrameId.value, \
            "Type": Type.value\
        };\
        window.parent.parent.parent.parent.parent.postMessage({"type": "AddElement", "jsonData": JsonData}, "*");\
    };';
    if (document.body != null) {

        //if (typeof addElement != "function") {
        document.body.appendChild(element);
        //}
    }

    return '';
}

window.addEventListener('message', function (event) {
    if (event.data != undefined) {
        if (event.data.type != undefined) {
            if (event.data.type == 'AddElement') {
                console.log('received response:  ', event.data.jsonData);

                createCommand(event.data.jsonData);

                var el = document.getElementById('WebDrivePopUp');
                el.style.display = "none";
                try {
                    if (event.data.jsonData.ElementAction == 'type') {
                        currentHoverElement.value = event.data.jsonData.ElementValue;
                    }
                } catch { }
            } else if (event.data.type == 'CloseAddElement') {
                var el = document.getElementById('WebDrivePopUp');
                el.style.display = "none";
                try {
                    if (event.data.jsonData.ElementAction == 'type') {
                        currentHoverElement.value = event.data.jsonData.ElementValue;
                    }
                } catch { }
            } else if (event.data.type == 'isRecordingPaused') {
                window.parent.parent.parent.parent.parent.document.isRecordingPaused = event.data.jsonData;
            }
        }
    }


}, false);

getMainWinElement = function () {
    return document.getElementById('WebDrivePopUp');
};

displayWebDriverForm = function (x, y) {
    var el;
    el = this.getMainWinElement();
    el.style.background = "white";
    el.style.position = "absolute";
    el.style.left = x + "px";
    el.style.top = y + "px";
    el.style.display = "block";
    el.style.border = "3px solid black";
    el.style.padding = "5px 5px 5px 5px";
    el.style.zIndex = 2147483647;
    return '';
};

showPos = function (event, xpath, css_selector, id, TextName) {
    var x, y;
    var innerDiv = document.getElementById('WebDrivePopUp');
    innerDiv.style.display = "block";
    var outerDiv = document.getElementsByTagName("body")[0];

    if (outerDiv.right == undefined) {
        outerDiv.right = (outerDiv.offsetLeft + outerDiv.offsetWidth);
        outerDiv.bottom = (outerDiv.offsetTop + outerDiv.offsetHeight);
    }

    var x = (event.clientX) + 15;
    var y = (event.clientY) + 15;

    var x_allowed = x >= outerDiv.offsetLeft && x <= (outerDiv.right - innerDiv.offsetWidth);
    var y_allowed = y >= outerDiv.offsetTop && y <= (outerDiv.bottom - (innerDiv.offsetHeight));
    if (y_allowed) {
        innerDiv.style.top = y + 'px';
    } else {
        if (y >= outerDiv.offsetTop) {
            innerDiv.style.top = (((outerDiv.bottom - innerDiv.offsetHeight) - 50) + 'px');
        }
        if (y <= (outerDiv.bottom - innerDiv.offsetHeight)) {
            console.log('beyond')
            innerDiv.style.bottom = outerDiv.offsetTop + 'px';
        }
    }

    if (x_allowed) {
        innerDiv.style.left = x + 'px';
    } else {
        if (x >= outerDiv.offsetLeft) {
            innerDiv.style.left = outerDiv.right - innerDiv.offsetWidth + 'px';
        }
        if (x <= (outerDiv.right - innerDiv.offsetWidth)) {
            console.log('beyond')
            innerDiv.style.right = outerDiv.offsetLeft + 'px';
        }
    }



    //if (window.event) {
    //    x = window.event.clientX + document.documentElement.scrollLeft + document.body.scrollLeft;
    //    y = window.event.clientY + document.documentElement.scrollTop + document.body.scrollTop;
    //} else {
    //    x = event.clientX + window.scrollX;
    //    y = event.clientY + window.scrollY;
    //}
    //x -= 2;
    //y -= 2;
    //y = y + 15;
    //this.displayWebDriverForm(x, y);
    document.getElementById("WebDrivePopUp_XPathLocator").innerHTML = xpath;
    document.getElementById("WebDrivePopUp_CssSelector").innerHTML = css_selector;
    document.getElementById("WebDrivePopUp_ElementId").innerHTML = id;
    document.getElementById("WebDrivePopUp_ElementText").innerHTML = pseudoGuid();
    document.getElementById("WebDrivePopUp_CodeIDText").value = TextName;
    //say(x + ";" + y);
    return '';
};


webDriveActionChange = function () {
    if (document.getElementById("WebDrivePopUp_Actions").value.toLowerCase() == "click") {
        document.getElementById("WebDrivePopUp_TextValue").style = "display: none";
        document.getElementById("WebDrivePopUp_SelectValue").style = "display: none";
    } else if (document.getElementById("WebDrivePopUp_Actions").value.toLowerCase() == "type") {
        document.getElementById("WebDrivePopUp_TextValue").style = "display: block";
        document.getElementById("WebDrivePopUp_SelectValue").style = "display: none";
    } else {
        document.getElementById("WebDrivePopUp_TextValue").style = "display: none";
        document.getElementById("WebDrivePopUp_SelectValue").style = "display: block";
    }
};

pseudoGuid = function () {
    var result;
    result = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx';
    result = result.replace(/[xy]/g, function (re_match) {
        var random_value, replacement;
        random_value = Math.random() * 16 | 0;
        replacement = re_match === 'x' ? random_value : random_value & 0x3 | 0x8;
        return replacement.toString(16);
    });
    return result;
};

getCssSelectorOF = function (element) {
    if (!(element instanceof Element))
        return;
    var path = [];
    while (element.nodeType === ELEMENT_NODE) {
        var selector = element.nodeName.toLowerCase();
        if (element.id) {
            if (element.id.indexOf('-') > -1) {
                selector += '[id = "' + element.id + '"]';
            } else {
                selector += '#' + element.id;
            }
            path.unshift(selector);
            break;
        } else {
            var element_sibling = element;
            var sibling_cnt = 1;
            while (element_sibling = element_sibling.previousElementSibling) {
                if (element_sibling.nodeName.toLowerCase() == selector)
                    sibling_cnt++;
            }
            if (sibling_cnt != 1)
                selector += ':nth-of-type(' + sibling_cnt + ')';
        }
        path.unshift(selector);
        element = element.parentNode;
    }
    return path.join(' > ');
};

getElementId = function (element) {
    var selector = '';

    if (element instanceof Element && element.nodeType === ELEMENT_NODE && element.id) {
        selector = element.id;
    }
    return selector;
};

getElementXPath = function (element) {
    if (!element) return null

    if (element.id) {
        return `//*[@id=${element.id}]`
    } else if (element.tagName === 'BODY') {
        return '/html/body'
    } else {
        const sameTagSiblings = Array.from(element.parentNode.childNodes)
            .filter(e => e.nodeName === element.nodeName)
        const idx = sameTagSiblings.indexOf(element)

        return getElementXPath(element.parentNode) +
            '/' +
            element.tagName.toLowerCase() +
            (sameTagSiblings.length > 1 ? `[${idx + 1}]` : '')
    }
}

//function getXPath(element) {
//    var path = [];

//    do {
//        if (element.id) {
//            path.unshift('id("' + element.id + '")');
//            break;
//        }
//        else if (element.parentNode) {
//            var nodeName = element.nodeName;
//            var hasNamedSiblings = Boolean(element.previousElementSibling || element.nextElementSibling);
//            var index = 1;
//            var sibling = element;

//            if (hasNamedSiblings) {
//                while ((sibling = sibling.previousElementSibling)) {
//                    if (sibling.nodeName === nodeName) {
//                        ++index;
//                    }
//                }

//                path.unshift(nodeName + '[' + index + ']');
//            }
//            else {
//                path.unshift(nodeName);
//            }
//        }
//        else {
//            path.unshift('');
//        }
//    } while ((element = element.parentNode));

//    return path.join('/');

//}


function getRelXpath(element) {
    var results = makeQueryForElement(element);
    return results;
    //console.log(results);
}

function getelemid(el) {
    var query = '';

    var index = getElementIndex(el);
    if (el.id) {
        query = '' + el.id + '';
    } else {
        return '';
    }

    return query;
}

function getActionsPerformed(element) {
    var ctrls = element.tagName.toLowerCase();
    ctrls.addEventListener("click", function () {
        //console.log('clicked');
        return "click";
    });
    ctrls.dblclick(function () {
        return "db-click";
    });
    ctrls.contextmenu(function () {
        return "right-click";
    });
    return "";
}


function makeQueryForElement(el) {
    var query = '';
    for (; el && el.nodeType === Node.ELEMENT_NODE; el = el.parentNode) {
        var component = el.tagName.toLowerCase();
        var index = getElementIndex(el);
        if (el.id) {
            component += '[@id=\'' + el.id + '\']';
        } else if (el.className) {
            component += '[@class=\'' + el.className + '\']';
        }
        if (index >= 1) {
            component += '[' + index + ']';
        }
        // If the last tag is an img, the user probably wants img/@src.
        if (query === '' && el.tagName.toLowerCase() === 'img') {
            component += '/@src';
        }
        query = '/' + component + query;
    }
    return query;
};

function returntypename(el) {
    var component = el.tagName.toLowerCase();
    return component;
}

function getElementIndex(el) {
    var className = el.className;
    var id = el.id;

    var index = 1; // XPath is one-indexed
    var sib;
    for (sib = el.previousSibling; sib; sib = sib.previousSibling) {
        if (sib.nodeType === Node.ELEMENT_NODE && elementsShareFamily(el, sib)) {
            index++;
        }
    }
    if (index > 1) {
        return index;
    }
    for (sib = el.nextSibling; sib; sib = sib.nextSibling) {
        if (sib.nodeType === Node.ELEMENT_NODE && elementsShareFamily(el, sib)) {
            return 1;
        }
    }
    return 0;
};

function elementsShareFamily(primaryEl, siblingEl) {
    if (primaryEl.tagName === siblingEl.tagName &&
        (!primaryEl.className || primaryEl.className === siblingEl.className) &&
        (!primaryEl.id || primaryEl.id === siblingEl.id)) {
        return true;
    }
    return false;
};


function getFrameElementXpath(element) {
    var iFramePath = "",
        previousControl = window,
        currentControl = "";
    var loop = 20;
    var ctrl = window;

    //console.log("------------IFramePath---------------");
    if (window.parent.last.tagName.toLowerCase() == "iframe") {
        //console.log("Yes IFrame");
        var PreviousCtrlID = "none";
        var i = 0;
        while (i <= loop) {
            currentControl = ctrl;
            if (i == 0) {
                previousControl = ctrl;
            }
            var currentCtrlID = currentControl.parent.last.id != "" ? currentControl.parent.last.id : currentControl.parent.last.name;
            //console.log(currentCtrlID + "_Cur");
            PreviousCtrlID = previousControl.parent.last.id != "" ? previousControl.parent.last.id : previousControl.parent.last.name;
            //console.log(PreviousCtrlID + "_Prev");
            if (i != 0) {
                if (currentCtrlID == PreviousCtrlID) {
                    return iFramePath.substring(0, iFramePath.length - 1);
                } else if (currentCtrlID != PreviousCtrlID) {
                    previousControl = currentControl;
                }
            }
            iFramePath += currentCtrlID + "/";
            ctrl = ctrl.parent;
            i++;
        }
    }
    return iFramePath.substring(0, iFramePath.length - 1);
}

getXPath = function (element) {

    var path = [];

    do {
        if (element.id) {
            if (element.type && !element.type.includes('select')) {
                if (element.type != null) {
                    if (element.id.includes('[')) {
                        var rowCol = '[' + element.id.split('[')[1];
                        path.unshift('//' + element.tagName + '[contains(@id,"' + rowCol + '")]');
                    } else {
                        path.unshift('//*[@id="' + element.id + '"][@type="' + element.type + '"]');
                    }
                } else {
                    path.unshift('//*[@id="' + element.id + '"]');
                }
            } else {
                path.unshift('//*[@id="' + element.id + '"]');
            }
            break;
        } else if (element.parentNode) {
            var nodeName = element.nodeName;
            var hasNamedSiblings = Boolean(element.previousElementSibling || element.nextElementSibling);
            var index = 1;
            var sibling = element;

            if (hasNamedSiblings) {
                while ((sibling = sibling.previousElementSibling)) {
                    if (sibling.nodeName === nodeName) {
                        ++index;
                    }
                }

                path.unshift(nodeName + '[' + index + ']');
            } else {
                path.unshift(nodeName);
            }
        } else {
            path.unshift('');
        }
    } while ((element = element.parentNode));

    return path.join('/');
};

if (document.body != null) {
    if (window.top.document == window.document) {
        window.parent.parent.parent.parent.parent.document.frameDocuments = {};

        window.parent.parent.parent.parent.parent.document.currentFrame = '';

        window.parent.parent.parent.parent.parent.document.isRecording = true;

        function storeDocuments(doc) {
            try {
                if (!window.parent.parent.parent.parent.parent.document.frameDocuments['DefaultContent']) {
                    window.parent.parent.parent.parent.parent.document.frameDocuments['DefaultContent'] = [];
                }

                window.parent.parent.parent.parent.parent.document.frameDocuments['DefaultContent'].push(window.top.document);

                for (var i = 0; i < doc.getElementsByTagName('iframe').length; ++i) {
                    if (doc.getElementsByTagName('iframe')[i].contentWindow) {
                        var curDoc = doc.getElementsByTagName('iframe')[i].contentWindow;
                        var frameId = doc.getElementsByTagName('iframe')[i].id;
                        if (frameId == null)
                            frameId = doc.getElementsByTagName('iframe')[i].name;
                        if (frameId == '')
                            frameId = doc.getElementsByTagName('iframe')[i].name;

                        if (!window.parent.parent.parent.parent.parent.document.frameDocuments[frameId]) {
                            window.parent.parent.parent.parent.parent.document.frameDocuments[frameId] = [];
                        }

                        window.parent.parent.parent.parent.parent.document.frameDocuments[frameId].push(curDoc.document);

                        storeDocuments(curDoc.document);
                    }
                }
            } catch { }
        }

        var timerRecordingDot;


        // addStyle("#recording{ background-color: white; font-family: 'HP Simplified', 'Helvetica Neue', Arial, sans-serif; font-size: 18px; font-weight: 400; display: flex; align-items: center; flex-direction: row; margin: 10px; width: auto; border: 1px #004d8f solid; padding-right: 25px; margin-right: -22px; position: -webkit-sticky;position: absolute; position: fixed; bottom: 0; right: 0; border-radius: 30px; animation-iteration-count: 1; animation-fill-mode: forwards; visibility: hidden; z-index: 2147483647; }");
        // addStyle("#recording:hover{  } #circle{ height: 10px; width: 10px; background: #E80600; border-radius: 50%; margin: 0 5px; animation: fadeIn 1s infinite alternate; } #content{ color: #004d8f; text-align: center } @keyframes fadeInAnimation{ 0 % { visibility: visible; opacity: 0; margin-right: -200px } 100 % { opacity: 1; margin-right: -22px } } @keyframes fadeIn { from { opacity: 0; } }");

        // addStyle(".highlightElement { background: yellow !important; border: 5px solid brown !important; color: black !important; }");

        // addStyle("#recordLoading { width: 100%; height: 100%; background: rgba(51, 51, 51, 0.5); position: fixed; z-index: 99999; left: 0; top: 0; text-align: center; padding-top: 20%; font-family: 'Calibri'; font-size: xx-large; font-weight: bold; color: white; display: none; }");
        // addStyle("#recordLoadingClose { position: fixed; right: 2%; top: 2%; cursor: pointer; border: 1px solid white; font-size: x-large; width: 25px; line-height: 25px; }");
        //addStyle("#recordLoading {width: 100%; height: 100%; background: rgba(51,51,51,0.5); position: fixed; z-index: 99999; left: 0; top: 0; text-align: center; padding-top: 20%; font-family: 'Calibri'; font-size: xx-large; font-weight: bold; color: white; }");

        // var elmnt = document.createElement("div");
        // elmnt.id = "recordLoading";
        // elmnt.innerHTML = "Processing...<span id='recordLoadingClose'>X</span>";
        // if (window.parent.parent.parent.parent.parent.document.body != null) {
        //     var avlElement = document.getElementById("recordLoading");
        //     if (typeof(avlElement) == 'undefined' || avlElement == null) {
        //         window.parent.parent.parent.parent.parent.document.body.appendChild(elmnt);
        //     }
        // }
        try {
            document.getElementById('recordLoadingClose').addEventListener('click', function () {
                document.getElementById('recordLoading').style.display = "none !important";
            });
        } catch { }

        var isTop = false;

        // function startRecording() {
        //     element = document.createElement("div");
        //     element.innerHTML = 'Processing...';
        //     element.id = 'recordLoading';

        //     if (window.parent.parent.parent.parent.parent.document.body != null)
        //         window.parent.parent.parent.parent.parent.document.body.appendChild(element);

        //     element = document.createElement("div");
        //     element.innerHTML = '\
        //        <div style="display: flex;align-items: center;flex-direction: row;margin: 10px;" id="recording">\
        //        <img id = "ide-img" style = "width: 28px;margin-right: 5px;" src = "data:image/png;base64,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" >\
        //        <div id="circle"></div>\
        //            <span id="content">\
        //                Recording...\
        //            </span>\
        //        </div >\
        //     ';

        //     if (window.parent.parent.parent.parent.parent.document.body != null)
        //         window.parent.parent.parent.parent.parent.document.body.appendChild(element);

        //     document.getElementById("recording").style = "animation: fadeInAnimation ease 1s;";
        //     document.getElementById("recording").style = "visibility: visible;";
        //     refreshRecordingDiv();

        //     document.getElementById("recording").addEventListener("mouseover", function(e) {
        //         if (!isTop) {
        //             document.getElementById("recording").style.position = "fixed";
        //             document.getElementById("recording").style.top = "0";
        //             document.getElementById("recording").style.right = "0";
        //             document.getElementById("recording").style.bottom = "unset";
        //             isTop = true;
        //         } else {
        //             document.getElementById("recording").style.position = "fixed";
        //             document.getElementById("recording").style.bottom = "0";
        //             document.getElementById("recording").style.right = "0";
        //             document.getElementById("recording").style.top = "94.6%";
        //             isTop = false;
        //         }
        //     });
        //     document.getElementById("recordLoading").style = "visibility: visible;";
        // }

        function refreshRecordingDiv() {
            if (window.parent.parent.parent.parent.parent.document.isRecordingPaused) {
                document.getElementById("content").innerText = "Paused";
                document.getElementById("circle").style = "visibility: hidden;";
            } else {
                document.getElementById("content").innerText = "Recording...";
                document.getElementById("circle").style = "visibility: visible;";
            }
        }

        // function stopRecording() {
        //     document.getElementById("recording").style = "visibility: hidden;";
        // }

        storeDocuments(document);

        // startRecording();


        //window.onbeforeunload = confirmExit;
        function confirmExit() {
            return false;
        }
    }
}

createCommand = function (jsonData) {

    var xhr = new XMLHttpRequest();
    var url = "http://localhost:47581/api/Command/SaveCommand";
    xhr.open("POST", url, true);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4 && xhr.status === 200) {
            var json = JSON.parse(xhr.responseText);
            console.log(json);
        }
    };
    var data = JSON.stringify(jsonData);

    //if (e.which == 13) {
    //    window.parent.parent.parent.parent.parent.document.getElementById('recordLoading').style.display = "block";
    //    setTimeout(function () { xhr.send(data); }, 700);
    //}
    //else {
    window.parent.parent.parent.parent.parent.document.getElementById('recordLoading').style.display = "block";
    xhr.send(data);
    //}

    //myJSONText = JSON.stringify(jsonData, null, 2);
    //window.parent.parent.parent.parent.parent.parent.document.webdriver_command = myJSONText;
    //localStorage.setItem("recordedJson", myJSONText);
};


getPathTo = function (element) {
    var element_sibling, siblingTagName, siblings, cnt, sibling_count;

    var elementTagName = element.tagName.toLowerCase();
    if (element.id != '') {
        return 'id("' + element.id + '")';
        // alternative : 
        // return '*[@id="' + element.id + '"]';
    } else if (element.name && document.getElementsByName(element.name).length === 1) {
        return '//' + elementTagName + '[@name="' + element.name + '"]';
    }
    if (element === document.body) {
        return '/html/' + elementTagName;
    }
    sibling_count = 0;
    siblings = element.parentNode.childNodes;
    siblings_length = siblings.length;
    for (cnt = 0; cnt < siblings_length; cnt++) {
        var element_sibling = siblings[cnt];
        if (element_sibling.nodeType !== ELEMENT_NODE) { // not ELEMENT_NODE
            continue;
        }
        if (element_sibling === element) {
            return getPathTo(element.parentNode) + '/' + elementTagName + '[' + (sibling_count + 1) + ']';
        }
        if (element_sibling.nodeType === 1 && element_sibling.tagName.toLowerCase() === elementTagName) {
            sibling_count++;
        }
    }
    return '';
};

getPageXY = function (element) {
    var x, y;
    x = 0;
    y = 0;
    while (element) {
        x += element.offsetLeft;
        y += element.offsetTop;
        element = element.offsetParent;
    }
    return [x, y];
};

getElementText = function (element) {
    var selector = '';
    // if (element.value != undefined) {
    //     selector = element.value.replace(/\s/g, "_").replace('\'', '');
    // } else {
    //     selector = element.id != undefined && element.id != '' ? element.id.replace(/\s/g, "_").replace('\'', '') : '';
    // }

    if (selector == "") {
        try {
            selector = element.tagName;
        } catch { }
    }

    return selector;
};

preventEvent = function (event) {
    if (event.preventDefault) {
        event.preventDefault();
    }
    event.returnValue = false;
    if (event.stopPropagation) {
        event.stopPropagation();
    } else {
        event.cancelBubble = true;
    }
    return false;
};

mouseCtrlOverHandler = function (event) {
    var JsonData, body, eventPreventingResult, mxy, path, root, target, txy, xpath, css_selector, id, TextName;

    if (document.WebRecorder == null) {
        return;
    }

    if (event == null) {
        event = window.event;
    }

    if (event.ctrlKey) {
        target = 'target' in event ? event.target : event.srcElement;
        if (target.id != "WebDrivePopUp") {
            var isShow = window.parent.parent.parent.parent.parent.document.getElementById("recordLoading").style.display;
            if (isShow == "block") {
                return false;
            }
            root = document.compatMode === 'CSS1Compat' ? document.documentElement : document.body;
            mxy = [event.clientX + root.scrollLeft, event.clientY + root.scrollTop];
            path = getXPath(target), //getPathTo(target);
                txy = getPageXY(target);
            css_selector = getCssSelectorOF(target);
            id = getElementId(target);
            body = document.getElementsByTagName('body')[0];
            TextName = getElementText(target);
            xpath = path; //getXPathByElement(target);
            currentHoverElement = target;
            JsonData = {
                "Command": "GetXPathFromElement",
                "Caller": "EventListener : mousedown",
                "CommandId": pseudoGuid(),
                "CssSelector": css_selector,
                "ElementId": id,
                "XPathValue": xpath
            };

            if (document.getElementById("WebDrivePopUp") != undefined)
                document.getElementById("WebDrivePopUp").style.display = "none";
            if (parent.document.getElementById("WebDrivePopUp") != undefined)
                parent.document.getElementById("WebDrivePopUp").style.display = "none";

            if (target.fromElement != null)
                if (target.fromElement.contentDocument.getElementById("WebDrivePopUp") != undefined)
                    target.fromElement.contentDocument.getElementById("WebDrivePopUp").style.display = "none";

            if (target.type == 'password') {
                document.getElementById("WebDrivePopUp_TextValue").type = 'password';
            } else {
                document.getElementById("WebDrivePopUp_TextValue").type = 'text';
            }

            document.getElementById("hdnType").value = target.type;

            var frameId = '';

            if (window.parent.last != undefined)
                if (window.parent.last != null)
                    if (window.parent.last.tagName.toLowerCase() == 'iframe')
                        frameId = window.parent.last.id;

            document.getElementById("hdnFrameId").value = frameId;

            //createCommand(JsonData);
            showPos(event, xpath, css_selector, id, TextName);
            eventPreventingResult = preventEvent(event);

            document.getElementById("WebDrivePopUp_TextValue").value = "";
            document.getElementById("WebDrivePopUp_TextValue").style = "display: none";
            document.getElementById("WebDrivePopUp_SelectValue").style = "display: none";
            document.getElementById("WebDrivePopUp_Actions").selectedIndex = 0;

            if (target.type != undefined) {
                if (target.tagName.toLowerCase() == "input") {
                    if (inputSendKeys.some(el => target.type.toLowerCase().includes(el))) {
                        document.getElementById("WebDrivePopUp_Actions").selectedIndex = 1;
                        document.getElementById("WebDrivePopUp_TextValue").style = "display: block";
                        document.getElementById("WebDrivePopUp_SelectValue").style = "display: none";
                    } else {
                        document.getElementById("WebDrivePopUp_Actions").selectedIndex = 0;
                        document.getElementById("WebDrivePopUp_TextValue").style = "display: none";
                        document.getElementById("WebDrivePopUp_SelectValue").style = "display: none";
                    }
                } else if (target.tagName.toLowerCase() == 'select') {
                    document.getElementById("WebDrivePopUp_Actions").selectedIndex = 2;
                    document.getElementById("WebDrivePopUp_TextValue").style = "display: none";
                    document.getElementById("WebDrivePopUp_SelectValue").style = "display: block";
                    removeOptions(document.getElementById("WebDrivePopUp_SelectValue"));

                    document.getElementById("WebDrivePopUp_SelectValue").innerHTML = target.innerHTML;
                }
            }

            // IFrame Handling Begin
            if (target.tagName == "IFRAME") {
                if (document.getElementById("WebDrivePopUp") != undefined)
                    document.getElementById("WebDrivePopUp").style.display = "none";

                if (parent.document.getElementById("WebDrivePopUp") != undefined)
                    parent.document.getElementById("WebDrivePopUp").style.display = "none";

                if (target.fromElement != null)
                    if (target.fromElement.contentDocument.getElementById("WebDrivePopUp") != undefined)
                        target.fromElement.contentDocument.getElementById("WebDrivePopUp").style.display = "none";

                target.onmouseout = function (event) {
                    if (event.fromElement.contentDocument.getElementById("WebDrivePopUp") != undefined)
                        event.fromElement.contentDocument.getElementById("WebDrivePopUp").style.display = "none";
                }
                //JsonFrameData = {
                //    "ElementId": id,
                //    "XPathValue": ''
                //};
                //createFrameCommand(JsonFrameData);
            }
            // IFrame Handling End

            return eventPreventingResult;
        } else {
            if (document.getElementById("WebDrivePopUp") != undefined)
                document.getElementById("WebDrivePopUp").style.display = "none";
        }
    } else {
        target = 'target' in event ? event.target : event.srcElement;

        //IFrame Handling Begin
        if (target.tagName == "IFRAME") {
            id = getElementId(target);
            path = getPathTo(target);
            xpath = path;

            if (document.getElementById("WebDrivePopUp") != undefined)
                document.getElementById("WebDrivePopUp").style.display = "none";

            if (parent.document.getElementById("WebDrivePopUp") != undefined)
                parent.document.getElementById("WebDrivePopUp").style.display = "none";

            if (target.onmouseout == null) {
                target.onmouseout = function (event) {
                    try {
                        if (event.fromElement.contentDocument.getElementById("WebDrivePopUp") != undefined)
                            event.fromElement.contentDocument.getElementById("WebDrivePopUp").style.display = "none";
                    } catch { }
                }
            }
        }
        //IFrame Handling End
    }
};

if (document.body != null) {
    if (document.addEventListener) {
        document.addEventListener('mousemove', mouseCtrlOverHandler, false);
    }

    createElementForm();
}

document.onkeydown = function (evt) {
    evt = evt || window.event;
    var isEscape = false;
    if ("key" in evt) {
        isEscape = (evt.key === "Escape" || evt.key === "Esc");
    } else {
        isEscape = (evt.keyCode === 27);
    }
    if (isEscape) {
        closeForm();
    }
};


var isSelectOptionChanged = false;

function clickHandler(e) {
    if (!window.parent.parent.parent.parent.parent.document.isRecordingPaused) {
        console.log("click handler");
        var srcElement = e;
        console.log(srcElement);
        if (e.srcElement != undefined) {
            srcElement = e.srcElement;
        }bcvnjgmk9yyp9jpk

        var isPopup = false;

        if (srcElement.id.includes('WebDrivePopUp'))
            isPopup = true;

        if (!isPopup)
            if (srcElement.closest('div') != null)
                if (srcElement.closest('div').id != null)
                    if (srcElement.closest('div').id.includes('WebDrivePopUp'))
                        isPopup = true;

        try {
            if (srcElement.id == "recordLoadingClose" && element.id != "recordLoading" && element.id != "recording" &&
                element.id != "ide-img" && element.id != "circle" && element.id != "content")
                isPopup = true;
        } catch { }

        if (!isPopup) {

            var elementName = srcElement.type;

            setCurrentFrame(srcElement);
            var frameId = '';

            if (window.parent.last != undefined)
                if (window.parent.last != null)
                    if (window.parent.last.tagName.toLowerCase() == 'iframe')
                        frameId = window.parent.last.id;

            if (srcElement.placeholder != null && srcElement.placeholder != '') {
                elementName = srcElement.placeholder.split(' ')[0].replace('-', '').replace('_', '');
            }
            if (srcElement.tagName.toLowerCase() == 'select') {
                elementName = srcElement.tagName.toLowerCase();
            }

            action = 'click';

            if (srcElement.type != undefined) {
                if (srcElement.tagName.toLowerCase() == "input") {
                    if (inputSendKeys.some(el => srcElement.type.toLowerCase().includes(el))) {
                        action = 'type';
                    }
                } else if (srcElement.tagName.toLowerCase() == 'select') {
                    action = 'selectoption';
                } else if (srcElement.tagName.toLowerCase() == 'textarea') {
                    action = 'type';
                }
            }


            if (e.which == 13 && srcElement.type.toLowerCase() != 'password') {
                action = action + " and enter";
            }

            var value = srcElement.value == undefined ? '' : srcElement.value;

            if (srcElement.tagName.toLowerCase() == 'select') {
                value = srcElement.options[srcElement.selectedIndex].text == undefined ? '' : srcElement.options[srcElement.selectedIndex].text;
            }

            if (value == '') {
                value = srcElement.text;
            }

            if (elementName == undefined)
                elementName = '';

            if (srcElement.tagName.toLowerCase() == 'a') {
                if (srcElement.text != undefined && srcElement.text != '') {
                    var srcText = srcElement.text.split(' ');
                    if (srcText.length > 0)
                        elementName = srcText[srcText.length - 1];
                    else
                        elementName = srcElement.text;
                }
            }

            if (srcElement.tagName.toLowerCase() == 'input') {
                if (srcElement.type.toLowerCase() == 'button' || srcElement.type.toLowerCase() == 'submit') {
                    elementName = srcElement.text == undefined ? '' : srcElement.text;
                }
            }

            if (srcElement.tagName.toLowerCase() == 'button') {
                elementName = srcElement.text == undefined ? '' : srcElement.text;
            }

            if (elementName == '') {
                elementName = srcElement.name == undefined ? '' : srcElement.name;
                if (elementName == '') {
                    if (srcElement.tagName.split(' ').length <= 2) {
                        elementName = srcElement.tagName.replace(' ', '');
                    }
                    elementName = elementName == '' ? (value == '' ? srcElement.tagName : value) : elementName;
                }
            }


            if (srcElement.tagName != undefined) {
                if (srcElement.tagName.toLowerCase() == 'input') {
                    if (srcElement.type.toLowerCase() == 'button' || srcElement.type.toLowerCase() == 'submit') {
                        elementName = srcElement.text == undefined ? '' : srcElement.text;
                    }
                } else if (srcElement.tagName.toLowerCase() == 'button') {
                    elementName = srcElement.text == undefined ? '' : srcElement.text;
                } else {

                }
            }

            elementName = elementName.replace(' ', '');

            var empId = getElementId(srcElement);
            debugger;
            var css_code = getCssSelectorOF(srcElement);
            var x_path_code = getXPath(srcElement);

            JsonData = {
                "Command": "AddElement",
                "Caller": "addElement",
                "CommandId": pseudoGuid(),
                "ElementCodeName": elementName == '' ? 'element' : elementName,
                "ElementId": empId == null ? '' : empId,
                "ElementCssSelector": css_code,
                "ElementXPath": x_path_code,
                "ElementAction": action,
                "ElementValue": value == undefined ? "" : value,
                "FrameId": frameId, //window.parent.parent.parent.parent.parent.document.currentFrame,
                "Type": srcElement.type,
                "WindowTitle": decodeURIComponent(document.title)
            };

            var xhr = new XMLHttpRequest();
            var url = "http://localhost:47581/api/Command/SaveCommand";
            xhr.open("POST", url, true);
            xhr.setRequestHeader("Content-Type", "application/json");
            xhr.onreadystatechange = function () {
                if (xhr.readyState === 4 && xhr.status === 200) {
                    var json = JSON.parse(xhr.responseText);
                    //console.log(json);
                }
            };
            var data = JSON.stringify(JsonData);
            closeForm();
            window.parent.parent.parent.parent.parent.document.getElementById('recordLoading').style.display = "block";

            if (action == 'click') {
                
                syncWait(1000)

                chrome.runtime.sendMessage({
                    jsonElement: data
                });

                // setTimeout(function () {
                //     //xhr.send(data);

                //     chrome.runtime.sendMessage({
                //         jsonElement: data
                //     });
                // }, 1000);
            } else {

                syncWait(1000)

                chrome.runtime.sendMessage({
                    jsonElement: data
                });

                // setTimeout(function () {
                //     //xhr.send(data);

                //     chrome.runtime.sendMessage({
                //         jsonElement: data
                //     });
                // }, 1000);
                //xhr.send(data);
            }

            if (e.which == 13 && srcElement.type.toLowerCase() == 'password') {
                return false;
                //keyHandler(e);
            }

        }
    }
}


function blurHandler(e) {
    if (!window.parent.parent.parent.parent.parent.document.isRecordingPaused) {
        console.log("blur handler");
        var srcElement = e;
        console.log(srcElement);
        if (e.srcElement != undefined) {
            srcElement = e.srcElement;
        }

        var isPopup = false;

        if (srcElement.id.includes('WebDrivePopUp'))
            isPopup = true;

        if (!isPopup)
            if (srcElement.closest('div') != null)
                if (srcElement.closest('div').id != null)
                    if (srcElement.closest('div').id.includes('WebDrivePopUp'))
                        isPopup = true;

        try {
            if (srcElement.id == "recordLoadingClose" && element.id != "recordLoading" && element.id != "recording" &&
                element.id != "ide-img" && element.id != "circle" && element.id != "content")
                isPopup = true;
        } catch { }

        if (!isPopup) {

            var elementName = srcElement.type;

            setCurrentFrame(srcElement);
            var frameId = '';

            if (window.parent.last != undefined)
                if (window.parent.last != null)
                    if (window.parent.last.tagName.toLowerCase() == 'iframe')
                        frameId = window.parent.last.id;

            if (srcElement.placeholder != null && srcElement.placeholder != '') {
                elementName = srcElement.placeholder.split(' ')[0].replace('-', '').replace('_', '');
            }
            if (srcElement.tagName.toLowerCase() == 'select') {
                elementName = srcElement.tagName.toLowerCase();
            }

            action = 'click';

            if (srcElement.type != undefined) {
                if (srcElement.tagName.toLowerCase() == "input") {
                    if (inputSendKeys.some(el => srcElement.type.toLowerCase().includes(el))) {
                        action = 'type';
                    }
                } else if (srcElement.tagName.toLowerCase() == 'select') {
                    action = 'selectoption';
                } else if (srcElement.tagName.toLowerCase() == 'textarea') {
                    action = 'type';
                }
            }


            if (e.which == 13 && srcElement.type.toLowerCase() != 'password') {
                action = action + " and enter";
            }

            var value = srcElement.value == undefined ? '' : srcElement.value;

            if (srcElement.tagName.toLowerCase() == 'select') {
                value = srcElement.options[srcElement.selectedIndex].text == undefined ? '' : srcElement.options[srcElement.selectedIndex].text;
            }

            if (value == '') {
                value = srcElement.text;
            }

            if (elementName == undefined)
                elementName = '';

            if (srcElement.tagName.toLowerCase() == 'a') {
                if (srcElement.text != undefined && srcElement.text != '') {
                    var srcText = srcElement.text.split(' ');
                    if (srcText.length > 0)
                        elementName = srcText[srcText.length - 1];
                    else
                        elementName = srcElement.text;
                }
            }

            if (srcElement.tagName.toLowerCase() == 'input') {
                if (srcElement.type.toLowerCase() == 'button' || srcElement.type.toLowerCase() == 'submit') {
                    elementName = srcElement.text == undefined ? '' : srcElement.text;
                }
            }

            if (srcElement.tagName.toLowerCase() == 'button') {
                elementName = srcElement.text == undefined ? '' : srcElement.text;
            }

            if (elementName == '') {
                elementName = srcElement.name == undefined ? '' : srcElement.name;
                if (elementName == '') {
                    if (srcElement.tagName.split(' ').length <= 2) {
                        elementName = srcElement.tagName.replace(' ', '');
                    }
                    elementName = elementName == '' ? (value == '' ? srcElement.tagName : value) : elementName;
                }
            }


            if (srcElement.tagName != undefined) {
                if (srcElement.tagName.toLowerCase() == 'input') {
                    if (srcElement.type.toLowerCase() == 'button' || srcElement.type.toLowerCase() == 'submit') {
                        elementName = srcElement.text == undefined ? '' : srcElement.text;
                    }
                } else if (srcElement.tagName.toLowerCase() == 'button') {
                    elementName = srcElement.text == undefined ? '' : srcElement.text;
                } else {

                }
            }

            elementName = elementName.replace(' ', '');

            var empId = getElementId(srcElement);

            JsonData = {
                "Command": "AddElement",
                "Caller": "addElement",
                "CommandId": pseudoGuid(),
                "ElementCodeName": elementName == '' ? 'element' : elementName,
                "ElementId": empId == null ? '' : empId,
                "ElementCssSelector": getCssSelectorOF(srcElement),
                "ElementXPath": getXPath(srcElement),
                "ElementAction": action,
                "ElementValue": value == undefined ? "" : value,
                "FrameId": frameId, //window.parent.parent.parent.parent.parent.document.currentFrame,
                "Type": srcElement.type,
                "WindowTitle": decodeURIComponent(document.title)
            };

            var xhr = new XMLHttpRequest();
            var url = "http://localhost:47581/api/Command/SaveCommand";
            xhr.open("POST", url, true);
            xhr.setRequestHeader("Content-Type", "application/json");
            xhr.onreadystatechange = function () {
                if (xhr.readyState === 4 && xhr.status === 200) {
                    var json = JSON.parse(xhr.responseText);
                    //console.log(json);
                }
            };
            var data = JSON.stringify(JsonData);
            closeForm();
            if (action == 'click') {
                window.parent.parent.parent.parent.parent.document.getElementById('recordLoading').style.display = "block";
                chrome.runtime.sendMessage({
                    jsonElement: data
                });
            } else {
                window.parent.parent.parent.parent.parent.document.getElementById('recordLoading').style.display = "block";
                chrome.runtime.sendMessage({
                    jsonElement: data
                });
                //xhr.send(data);
            }

            var isNew = true;

            if (e.which == 13 && srcElement.type.toLowerCase() == 'password') {
                return false;
                //keyHandler(e);
            }

        }
    }
}
