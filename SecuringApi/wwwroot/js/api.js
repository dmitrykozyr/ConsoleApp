// Проект нужно запускать вместе с проектов WebApiNetCore

var url = "https://localhost:44345/Products";

var productsList = document.getElementById("products-list");
if (productsList) {
    fetch(url)
        .then(response => response.json())
        .then(data => showProducts(data))
        .catch(ex => {
            alert("Something went wrong...");
            console.log(ex);
        });
}

function showProducts(products) {
    products.forEach(product => {
        let listItem = document.createElement("li");
        let text = `${product.name} ($${product.price})`;
        listItem.appendChild(document.createTextNode(text));
        productsList.appendChild(listItem);
    });
}
