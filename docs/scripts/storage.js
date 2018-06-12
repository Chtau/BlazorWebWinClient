Blazor.registerFunction('readStorage', (key) => {
    return localStorage.getItem(key);
});

Blazor.registerFunction('writeStorage', (key, value) => {
    localStorage.setItem(key, value);
    return true;
});

Blazor.registerFunction('removeStorage', (key) => {
    localStorage.removeItem(key);
    return true;
});




