; (function (global) {
    var Toastify = function (options) { return new Toastify.lib.init(options); }, version = "0.0.1"; Toastify.lib = Toastify.prototype = {
        toastify: version, constructor: Toastify, init: function (options) {
            if (!options) { options = {}; }
            this.options = {}; this.options.text = options.text || 'Hi there!'; this.options.duration = options.duration || 3000; this.options.selector = options.selector; this.options.callback = options.callback || function () { }; return this;
        }, buildToast: function () {
            if (!this.options) { throw "Toastify is not initialized"; }
            var divElement = document.createElement("div"); divElement.className = 'toastify on'; divElement.innerHTML = this.options.text; divElement.addEventListener('click', this.options.callback); return divElement;
        }, showToast: function () {
            var toastElement = this.buildToast(); var rootElement; if (typeof this.options.selector == "undefined") { rootElement = document.body; } else { rootElement = document.getElementById(this.options.selector); }
            if (!rootElement) { throw "Root element is not defined"; }
            rootElement.insertBefore(toastElement, rootElement.firstChild); Toastify.reposition(); window.setTimeout(function () { toastElement.classList.remove("on"); window.setTimeout(function () { toastElement.remove(); Toastify.reposition(); }.bind(this), 400); }.bind(this), this.options.duration); return this;
        }
    }
    Toastify.reposition = function () {
        var topOffsetSize = 15; var allToasts = document.getElementsByClassName('toastify'); for (var i = 0; i < allToasts.length; i++) { var height = allToasts[i].offsetHeight; var offset = 15; allToasts[i].style.top = topOffsetSize + 'px'; topOffsetSize += height + offset; }
        return this;
    }
    Toastify.lib.init.prototype = Toastify.lib; global.Toastify = Toastify;
}(window));