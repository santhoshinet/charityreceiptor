$(function () {
    $('input[type="file"]').change(function () {
        var filePath = $(this)[0].value;
        if (filePath.indexOf('.') == -1)
            return false;
        var validExtensions = new Array();
        var ext = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();
        validExtensions[0] = 'xls';
        validExtensions[1] = 'xlsx';
        for (var i = 0; i < validExtensions.length; i++) {
            if (ext == validExtensions[i])
                return true;
        }
        alert('The file extension ' + ext.toUpperCase() + ' is not allowed!');
        return false;
    });
});