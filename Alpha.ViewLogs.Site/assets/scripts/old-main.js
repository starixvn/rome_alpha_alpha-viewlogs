function slideToggle(id) {
    var obj = null;
    if (typeof (id) == 'object') {
        var rowIndex = id.rowIndex;
        var p = id.parentNode ? id.parentNode : id.parentElement;
        var table = p.parentNode ? p.parentNode : p.parentElement;
        obj = table.rows[rowIndex + 1];
    }
    else {
        obj = document.getElementById(id);
    }
    if (obj != null) {
        if (obj.style.display == 'none') {
            obj.style.display = '';
        }
        else {
            obj.style.display = 'none';
        }
    }
}
//# sourceMappingURL=old-main.js.map