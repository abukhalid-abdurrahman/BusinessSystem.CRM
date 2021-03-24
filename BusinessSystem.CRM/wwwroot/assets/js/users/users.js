const deactivateUserBtnEl = document.getElementById('deactivate-user-button');

function CreateFormData()
{
    const formData = new FormData();
    formData.append("Id", selectedUserId);
    formData.append("Status", currentUserStatus);
    return formData;
}

function SetDeactivateButtonText()
{
    if(currentUserStatus) {
        deactivateUserBtnEl.innerText = "Disable";
    } else {
        deactivateUserBtnEl.innerText = "Enable";
    }
}

deactivateUserBtnEl.addEventListener("click", ev => {
    ev.preventDefault();

    const formData = CreateFormData();
    CreateRequest(`/Users/RemoveUser`, 'POST', function (responseData) {
        if(responseData.statusCode === 200) {
            if(responseData.data.removed) {
                deactivateUserBtnEl.innerText = "Enable";
            } else {
                deactivateUserBtnEl.innerText = "Disable";
            }
            currentUserStatus = !currentUserStatus;
            
            Snackbar.show({
                text: 'Successfully changed',
                actionTextColor: '#fff',
                backgroundColor: '#8dbf42'
            });
        } else {
            Snackbar.show({
                text: 'Something goes wrong',
                actionTextColor: '#fff',
                backgroundColor: '#e7515a'
            });
        }
    }, formData);
});

document.addEventListener("DOMContentLoaded", ev => {
    SetDeactivateButtonText();
});