
// Script inspiré de plusieurs recherches sur internet
function textOnly(e) {
    var code;
    if (!e) var e = window.event;
    if (e.keyCode) code = e.keyCode;
    else if (e.which) code = e.which;
    var character = String.fromCharCode(code);
    var AllowRegex = /^[\ba-zA-ZÀ-ÿ\s-]$/;
    if (AllowRegex.test(character)) return true;
    return false;
}
