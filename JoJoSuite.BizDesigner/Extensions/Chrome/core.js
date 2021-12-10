// Unique ID for the className.
var MOUSE_VISITED_CLASSNAME = 'recorder_mouse_visited';

// Previous dom, that we want to track, so we can remove the previous styling.
var prevDOM = null;
console.log("Caught");
// Mouse listener for any move event on the current document.
if (document.addEventListener) {

    document.addEventListener('mouseover', function(e) {
        let srcElement = e.srcElement;

        srcElement.classList.add(MOUSE_VISITED_CLASSNAME);

    }, false);
    document.addEventListener('mouseout', function(e) {
        let srcElement = e.srcElement;

        srcElement.classList.add(MOUSE_VISITED_CLASSNAME);

    }, false);
}