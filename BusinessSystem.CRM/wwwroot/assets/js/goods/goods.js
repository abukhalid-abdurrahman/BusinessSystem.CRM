'use strict';

const goodFormEl = document.getElementById('good-form');
const goodImageEl = document.getElementById('input-file-max-fs');
const categoriesListEl = document.getElementById('categories-list-select');
const goodNameEl = document.getElementById('good-name');
const goodDescEl = document.getElementById('good-desc');

function SetCategories()  {
    const selectEl = document.getElementById('categories-list-select');
    CreateRequest(`/Category/GetCategories/${userId}`, 'GET', function (responseData) {
        responseData.data.forEach(item => {
            const option = `<option value="${item.categoryId}">${item.categoryName}</option>`;
            selectEl.insertAdjacentHTML("beforeend", option);
        }); 
    });
}

function ValidateGoodValues() {
    return !(goodImageEl.value.trim() === '' ||
        categoriesListEl.value.trim() === '' ||
        goodNameEl.value.trim() === '' ||
        goodDescEl.value.trim() === '');
}

goodFormEl.addEventListener("submit", function (e) {
    e.preventDefault();
    
    if(!ValidateGoodValues()) {
        Snackbar.show({
            text: 'Some input field is empty, please fill it',
            actionTextColor: '#fff',
            backgroundColor: '#e7515a'
        });        
        return false;
    }
    
    const goodData = new FormData();
    goodData.append('PartnerId', userId)
    goodData.append('GoodName', goodNameEl.value);
    goodData.append('GoodDescription', goodDescEl.value);
    goodData.append('GoodImage', goodImageEl.files[0]);
    goodData.append('CategoryId', categoriesListEl.value);
    
    CreateRequest('/Good/Create/', 'POST', function (response) {
        if(response.statusCode === 200) {
            const goodsTableEl = document.getElementById('goods-table-body');
            const goodRowEl = `
                <tr>
                    <td>
                        ${response.data.goodId}
                    </td>
                    <td>
                        ${response.data.goodName}
                    </td>
                    <td>
                        ${response.data.categoryName}
                    </td>
                    <td>
                        <p class="mb-0">${response.data.goodDescription}</p>
                    </td>
                    <td> 
                        ${response.data.createDate}
                    </td>
                </tr>
            `;
            goodsTableEl.insertAdjacentHTML("beforeend", goodRowEl);

            Snackbar.show({
                text: 'Good successfully created',
                actionTextColor: '#fff',
                backgroundColor: '#8dbf42'
            });        
        } else {
            Snackbar.show({
                text: 'Something goes wrong while creating good',
                actionTextColor: '#fff',
                backgroundColor: '#e7515a'
            });          
        }
    }, goodData);
    
});

document.addEventListener("DOMContentLoaded", ev => {
    SetCategories();
});