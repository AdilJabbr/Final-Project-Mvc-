const accordions = document.querySelectorAll('.accordion');
accordions.forEach(btn => {
    btn.addEventListener('click', function () {
        this.classList.toggle('active');
        const panel = this.nextElementSibling;
        panel.classList.toggle('show');
    });
});

const shopsubmenu = document.querySelector(".shop-submenu");
const shopsubmenubtn = document.getElementById("shopsubmenubtn")
const closeshopsubmenubtn = document.getElementById("closeshopsubmenubtn")
shopsubmenu.classList.add("hidden");
shopsubmenubtn.addEventListener("click", () => {
    shopsubmenu.classList.remove("hidden");

});
closeshopsubmenubtn.addEventListener("click", () => {
    shopsubmenu.classList.add("hidden");
});
