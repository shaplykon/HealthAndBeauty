function deleteUser(userId) {
    $.ajax({
        url: '/Manage/DeleteUser/' + userId,
        type: 'POST'
    });
}

function depriveRole(userId, role) {
    $.ajax({
        url: '/Manage/DepriveRole/' + userId + '/' + role,
        type: 'POST'
    });
}

function giveRole(userId, role) {
    $.ajax({
        url: '/Manage/GiveRole/' + userId + '/' + role,
        type: 'POST'
    });
}