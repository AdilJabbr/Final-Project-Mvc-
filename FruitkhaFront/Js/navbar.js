const navbars = document.querySelector(".navbars");
const navbar = document.querySelector(".navbar");

window.addEventListener("scroll", function () {
  if (window.innerWidth >= 992) {
    if (window.scrollY >= 150) {
      navbars.style.backgroundColor = "#051922";
      navbar.style.padding = "15px";
    } else {
      navbars.style.backgroundColor = "";
      navbar.style.padding = "25px";
    }
  } else {
    navbars.style.backgroundColor = "";
  }
});
window.addEventListener("scroll", function () {
  if (window.innerWidth < 992) {
    if (window.scrollY >= 150) {
      navbars.classList.add("hidden")
    } else {
      navbars.classList.remove("hidden")
    }
  }});
window.addEventListener("resize", function () {
  if (window.innerWidth < 992) {
    if (window.scrollY >= 150) {
      navbars.classList.add("hidden")
    }
    else {
      navbars.classList.remove("hidden")
    }
    
  }
  else{
    navbars.classList.remove("hidden")
  }
});
window.addEventListener("resize", function () {
  const navbars = document.querySelector(".navbars");
  if (window.innerWidth < 992) {
    navbars.style.backgroundColor = "";
    mobilmenu.classList.add("hidden");
    closemenu.classList.add("hidden");
    openmenu.classList.remove("hidden");
  }
});

const expand = document.getElementById("submenu-expand");
const collapse = document.getElementById("submenu-collapse");
const submenu = document.getElementById("mobil-menu");
const mobilmenu = document.getElementById("mobile-tablet-menu");
const openmenu = document.getElementById("menu-open");
const closemenu = document.getElementById("menu-close");


mobilmenu.classList.add("hidden");
collapse.classList.add("hidden");
submenu.classList.add("hidden");
closemenu.classList.add("hidden");

openmenu.addEventListener("click", () => {
  openmenu.classList.add("hidden");
  closemenu.classList.remove("hidden");
  mobilmenu.classList.remove("hidden");
});
closemenu.addEventListener("click", () => {
  closemenu.classList.add("hidden");
  openmenu.classList.remove("hidden");
  mobilmenu.classList.add("hidden");
});

expand.addEventListener("click", () => {
  expand.classList.add("hidden");
  collapse.classList.remove("hidden");
  submenu.classList.remove("hidden");
});
collapse.addEventListener("click", () => {
  collapse.classList.add("hidden");
  expand.classList.remove("hidden");
  submenu.classList.add("hidden");
});
const inputBox = document.getElementById('inputBox');
function toggleInput() {
  inputBox.classList.toggle('show');
}