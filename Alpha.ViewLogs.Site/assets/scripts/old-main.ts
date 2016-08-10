function slideToggle(id) {
    var obj = null;
    if (typeof (id) == 'object') {
        var rowIndex = id.rowIndex;
        var p = id.parentNode ? id.parentNode : id.parentElement;
        var table = p.parentNode ? p.parentNode : p.parentElement;
        var cell = table.rows[rowIndex + 1].cells[0];
        obj = cell.firstElementChild ? cell.firstElementChild : cell.firstChild;
    }
    else {
        obj = document.getElementById(id);
    }

    if (obj != null) {
        if (obj.style.display == 'none') {
            obj.style.display = 'block';
        }
        else {
            obj.style.display = 'none';
        }
    }
}