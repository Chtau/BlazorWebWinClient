Blazor.registerFunction('scrollIntoView', (element) => {
    document.getElementById(element.id).scrollIntoView();
    return true;
});

Blazor.registerFunction('readFile', (inputElementId) => {
    inputElementId = inputElementId.replace('"', '').replace('"', '');
    var filesElement = document.getElementById(inputElementId);
    var file = filesElement.files[0];
    return new Promise((resolve, reject) => {       
        var reader = new FileReader();
        reader.readAsArrayBuffer(file);
        reader.onload = function () {
            var arrayBuffer = reader.result
            var bytes = new Uint8Array(arrayBuffer);
            resolve(Array.from(bytes));
        }
    });
});