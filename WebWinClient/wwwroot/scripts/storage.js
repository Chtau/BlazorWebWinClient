window.storage = {
    readStorage: function (key) {
        return localStorage.getItem(key);
    },
    writeStorage: function (key, value) {
        localStorage.setItem(key, value);
        return true;
    },
    removeStorage: function (key) {
        localStorage.removeItem(key);
        return true;
    }
}


