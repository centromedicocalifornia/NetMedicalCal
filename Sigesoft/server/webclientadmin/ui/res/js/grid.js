

function renderGender(value, params) {
    return value == 1 ? 'Male' : 'Female';
}


function renderMajor(value, params) {
    var url = 'http://gsa.ustc.edu.cn/search?q=' + F.urlEncode(value);
    return F.formatString('<a href="{0}" data-qtip="{1}" target="_blank">{1}</a>', url, F.htmlEncode(value));
}


function renderGroup(value, params) {
    var imageUrl = F.baseUrl + 'res/images/16/' + value + '.png';
    return F.formatString('<img class="f-grid-imagefield" src="{0}"/>', imageUrl);
}


function renderExpander(value, params) {
    return '<div class="expander">' +
        '<p><strong>User name: </strong>' + params.rowData.values.Name + '</p>' +
        '<p><strong>Description: </strong>' + value + '</p>' +
        '</div>';
}


function showNotify(content) {

    F.notify({
        message: content,
        target: '_top',
        header: false,
        messageIcon: '',
        positionX: 'center',
        positionY: 'top'
    });
}


function notifySelectedRows(gridId) {
    var grid = F(gridId);

    if (!grid.hasSelection()) {
        F.alert('No items selected!');
        return;
    }

    var genderColumn = grid.getColumn('Gender');
    var majorColumn = grid.getColumn('Major');

    var result = ['<table class="result">'];
    result.push('<tr>');
    if (grid.idField) {
        result.push('<th>ID</th>');
    }
    if (grid.textField) {
        result.push('<th>Text</th>');
    }
    if (genderColumn) {
        result.push('<th>Gender</th>');
    }
    if (majorColumn) {
        result.push('<th>Major</th>');
    }

    result.push('</tr>');

    $.each(grid.getSelectedRows(true), function (index, row) {
        result.push('<tr>');
        if (grid.idField) {
            result.push('<td>' + row.id + '</td>');
        }
        if (grid.textField) {
            result.push('<td>' + row.text + '</td>');
        }
        if (genderColumn) {
            result.push('<td>' + (row.values['Gender'] == 1 ? 'Male' : 'Female') + '</td>');
        }
        if (majorColumn) {
            result.push('<td>' + row.values['Major'] + '</td>');
        }

        result.push('</tr>');
    });

    result.push('</table>');

    showNotify(result.join(''));
}

