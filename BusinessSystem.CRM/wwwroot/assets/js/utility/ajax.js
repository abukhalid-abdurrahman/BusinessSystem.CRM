'use strict';

function CreateRequest(url, method, callback, formData) {
    const xhr = new XMLHttpRequest();
    
    xhr.open(method, url);
    if(formData === null || formData === undefined) {
        xhr.send();
    } else {
        xhr.send(formData);
    }
    
    xhr.onload = () => {
        if (xhr.responseText !== '') {
            const responseData = JSON.parse(xhr.responseText);
            callback(responseData);
        }
    };
}