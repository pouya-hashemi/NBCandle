document.addEventListener('click', function () {
    let mobileMenu = document.getElementById('mobileNavBar');
    let mobileMenuChilds = Array.from(mobileMenu.querySelectorAll('*'));
    let target = event.target;
    let burgerMenuButton = document.getElementById('burgerMenuButton');
    let burgerMenuButtonChilds = Array.from(burgerMenuButton.querySelectorAll('*'))

    if (target !== mobileMenu
        && mobileMenuChilds.filter(f => f === target).length == 0
        && target != burgerMenuButton
        && burgerMenuButtonChilds.filter(f => f === target).length == 0) {
        hideMobileNavbar();
    }
})



function menuButtonClicked() {
    showMobileNavbar()
}
function showMobileNavbar() {
    let navbar = document.getElementById('mobileNavBar');
    showBlackScreen();
    navbar.classList.add('d-block')
    navbar.classList.add('d-sm-none')
    navbar.classList.remove('d-none')
}
function hideMobileNavbar() {

    let navbar = document.getElementById('mobileNavBar');
    hideBlackScreen();
    navbar.classList.add('d-none')
    navbar.classList.remove('d-sm-none')
    navbar.classList.remove('d-block')
}
function showBlackScreen() {
    let blackScreen = document.getElementById('blackScreen');
    blackScreen.classList.add('d-block')
    blackScreen.classList.add('d-sm-none')
    blackScreen.classList.remove('d-none')
}
function hideBlackScreen() {
    let blackScreen = document.getElementById('blackScreen');
    blackScreen.classList.add('d-none')
    blackScreen.classList.remove('d-sm-none')
    blackScreen.classList.remove('d-block')
}
async function searchProduct() {
    let searchString = event.currentTarget.value;
    const response = await fetch("/index?handler=ProductSearch&searchstring=" + searchString, {
        method: 'GET', // *GET, POST, PUT, DELETE, etc.
        headers: {
            'Content-Type': 'application/json'
            // 'Content-Type': 'application/x-www-form-urlencoded',
        }
    });
    let list = await response.json();
    let listelem = document.getElementById("searchResult")
    listelem.innerHTML = '';
    if (list != null) {

        for (var i = 0; i < list.length; i++) {
            let btn = document.createElement('a');
            btn.classList.add('search-result-btn')
            btn.classList.add('border-light')
            btn.classList.add('border-bottom')

            let prdName = document.createElement('span')
            prdName.classList.add('x-small')
            prdName.classList.add('me-3')
            prdName.classList.add('d-inline-block')
            prdName.innerText = list[i].name;

            let categoryName = document.createElement('span')
            categoryName.classList.add('xx-small')
            categoryName.classList.add('text-light')
            categoryName.classList.add('d-inline-block')
            categoryName.innerText = list[i].category;

            btn.appendChild(prdName)
            btn.appendChild(categoryName)

            btn.href = '/productdetail?productId=' + list[i].id;


            listelem.appendChild(btn);
        }
    }

    
}
function toggleMobileCategory() {
    let mobilecategory = event.currentTarget.nextElementSibling;
    if (mobilecategory.classList.contains('d-none')) {
        mobilecategory.classList.remove('d-none')
        mobilecategory.classList.add('d-block')
    }
    else {
        mobilecategory.classList.add('d-none')
        mobilecategory.classList.remove('d-block')
    }
}
