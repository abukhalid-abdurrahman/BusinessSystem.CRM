'use strict';

const categoryFormEl = document.getElementById('category-form');
const categoryNameEl = document.getElementById('category-name');
const categoryTableListEl = document.getElementById('categories-table-body');

function ValidateValues() {
    return categoryNameEl.value !== '';
}

categoryFormEl.addEventListener("submit", function (e) {
    e.preventDefault();
    
    if(!ValidateValues()) {
        Snackbar.show({
            text: 'Category name is empty',
            actionTextColor: '#fff',
            backgroundColor: '#e7515a'
        });
        return false;
    }
    
    const categoryForm = new FormData();
    categoryForm.append("PartnerId", userId);
    categoryForm.append("CategoryName", categoryNameEl.value)
    CreateRequest('/Category/Create/', 'POST', function (response) {
        if(response.statusCode === 200) {
            const categoryRowEl = `
                <tr>
                    <td>${response.data.categoryId}</td>
                    <td><p class="mb-0">${response.data.categoryName}</p></td>
                    <td>${response.data.createDate}</td>
                </tr>
            `;
            categoryTableListEl.insertAdjacentHTML("beforeend", categoryRowEl);
            Snackbar.show({
                text: 'Category successfully created',
                actionTextColor: '#fff',
                backgroundColor: '#8dbf42'
            });
        } else {
            Snackbar.show({
                text: 'Something goes wrong while creating category',
                actionTextColor: '#fff',
                backgroundColor: '#e7515a'
            });  
        }
    }, categoryForm);
});