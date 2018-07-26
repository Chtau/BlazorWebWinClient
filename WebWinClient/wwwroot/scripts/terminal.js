window.terminal = {
    readFile: function (inputElementId, dotnetHelper) {
        inputElementId = inputElementId.replace('"', '').replace('"', '');
        var filesElement = document.getElementById(inputElementId);
        var file = filesElement.files[0];
        var reader = new FileReader();
        reader.readAsArrayBuffer(file);
        reader.onload = function () {
            var arrayBuffer = reader.result
            var bytes = new Uint8Array(arrayBuffer);
            dotnetHelper.invokeMethodAsync('ReadFileReturn', Array.from(bytes));
        }
        return true;
    },
    scrollIntoView: function (element) {
        document.getElementById(element.id).scrollIntoView();
        return true;
    }
}