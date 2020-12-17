// Javascript for Loading page
setTimeout(function () { $('.loading-screen').fadeToggle(900); }, 1500); // Làm hiển thị (display: block;) hoặc biến mất (display: none;) thành phần kèm với hiệu ứng làm mờ (opacity).


// Jquery for Button
$(function () 
{
    $(".btn").on('mouseenter', function (e) 
    {
        x = e.pageX - $(this).offset().left;
        y = e.pageY - $(this).offset().top;
        $(this).find('span').css({ top: y, left: x });
    });

    $(".btn").on('mouseout', function (e) 
    {
        x = e.pageX - $(this).offset().left;
        y = e.pageY - $(this).offset().top;
        $(this).find('span').css({ top: y, left: x });
    });
});



// Jquery for Modal
var modalbtn = document.querySelector('.modal-btn');
var modalBg = document.querySelector('.modal-bg');
var modalClose = document.querySelector('.modal-close');
modalbtn.addEventListener('click', function()
{
    modalBg.classList.add('bg-active');
});
modalClose.addEventListener('click', function()
{
    modalBg.classList.remove('bg-active');
});


// // Jquery for Search
// var SearchBar = document.querySelector(".search");
// function Slide() 
// {
//     if (SearchBar.classList.contains("hide")) 
//     {
//         SearchBar.classList.remove("hide");
//     }
//     else 
//     {
//         SearchBar.classList.add("hide");
//     }
// }



// Javascript for to top
const Totop = document.querySelector(".to-top");
window.addEventListener("scroll", () => 
{
    if (window.pageYOffset > 200) 
    {
        Totop.classList.add("active");
    }
    else 
    {
        Totop.classList.remove("active");
    }
});


// Javascript for toggle menu 
var MenuItems = document.getElementById("MenuItems");
MenuItems.style.maxHeight = "0px";
function menutoggle() 
{
    if (MenuItems.style.maxHeight == "0px") 
    {
        MenuItems.style.maxHeight = "200px";
    }
    else 
    {
        MenuItems.style.maxHeight = "0px";
    }
}



// Javascript for product gallery 
var productimg = document.getElementById("productimg");
var small_img = document.getElementsByClassName("small-img");
small_img[0].onclick = function () 
{
    productimg.src = small_img[0].src;
}
small_img[1].onclick = function () 
{
    productimg.src = small_img[1].src;
}
small_img[2].onclick = function () 
{
    productimg.src = small_img[2].src;
}
small_img[3].onclick = function () 
{
    productimg.src = small_img[3].src;
}
small_img[4].onclick = function () 
{
    productimg.src = small_img[4].src;
}
small_img[5].onclick = function () 
{
    productimg.src = small_img[5].src;
}

// Add to cart
// let products = 
// [
//     {
//         name: 'Xoài Cát Hòa Lộc', 
//         tag: 'Xoài',
//         price: 50000,
//         inCart: 0
//     },

//     {
//         name: 'Bòn Bon', 
//         tag: 'Bòn bon',
//         price: 50000,
//         inCart: 0
//     },

//     {
//         name: 'Bưởi Da Xanh', 
//         tag: 'Bưởi',
//         price: 50000,
//         inCart: 0
//     },

//     {
//         name: 'Cam Sành', 
//         tag: 'Cam sành',
//         price: 50000,
//         inCart: 0
//     },
// ];

// let carts = document.querySelectorAll('.add-cart');
// for(let i = 0; i < carts.length; i++)
// {
//     carts[i].addEventListener('click', () => 
//     {
//         CartNumbers(products[i]);
//         TotalPrice(products[i]);  
//     })
// }

// function CartNumbers(product)
// {
//     // console.log("Sản phẩm: ", product);
//     let productNumbers = localStorage.getItem('CartNumbers');
//     productNumbers = parseInt(productNumbers);

//     if(productNumbers)
//     {
//         localStorage.setItem('CartNumbers', productNumbers + 1);
//         document.querySelector('.icon span').textContent = productNumbers + 1;
//     }
//     else
//     {
//         localStorage.setItem('CartNumbers', 1);
//         document.querySelector('.icon span').textContent = 1;
//     }
//     SetItems(product);
// }

// function SetItems(product)
// {
//     let cartItems = localStorage.getItem('productsIncart');
//     cartItems = JSON.parse(cartItems);

//     if(cartItems != null)
//     {
//         if(cartItems[product.tag] == undefined)
//         {
//             cartItems =
//             {
//                 ...cartItems,
//                 [product.tag]: product
//             }
//         }
//         cartItems[product.tag].inCart += 1;
//     }
//     else
//     {
//         product.inCart = 1;
//         cartItems = 
//         {
//             [product.tag]: product
//         }
//     }

//     localStorage.setItem('productsIncart', JSON.stringify(cartItems));
// }

// function TotalPrice(product)
// {
//     // console.log("Price: ", product.price);
//     let cartCost = localStorage.getItem('TotalPrice');
//     if(cartCost != null)
//     {
//         cartCost = parseInt(cartCost);
//         localStorage.setItem("TotalPrice", cartCost + product.price);
//     }
//     else
//     {
//         localStorage.setItem("TotalPrice", product.price);
//     }
// }

// function DisplayCart()
// {
//     let cartItems = localStorage.getItem('productsIncart');
//     cartItems = JSON.parse(cartItems);
//     let productcontainer = document.querySelector(".products-container");
//     if(cartItems && productcontainer)
//     {
//         productcontainer.innerHTML = '';
//         Object.values(cartItems).map(item => 
//             {
//                 productcontainer.innerHTML += 
//                 `
//                     <div class="product">
//                         <i class="fa fa-times-circle"></i>
//                         <img src="../Image/Products/${item.tag}/${item.tag}.jpg">
//                         <span><h3>${item.name}</h3></span>                       
//                     </div>
//                     <div class="price">${item.price}</div>
//                     <div class="quantity">${item.inCart}</div>                   
//                 `
//             });
//     }
// }


// function OnLoadCartNumbers()
// {
//     let productNumbers = localStorage.getItem('CartNumbers');
//     if(productNumbers)
//     {
//         document.querySelector('.icon span').textContent = productNumbers;
//     }
// }

// window.onload = function()
// {
//     OnLoadCartNumbers();
//     DisplayCart();
// }























