const slides = document.querySelectorAll('.recognize_us-carousel .slide');
let currentIndex = 0;
function showNextSlide() {
    slides[currentIndex].classList.remove('active');
    currentIndex = (currentIndex + 1) % slides.length;
    slides[currentIndex].classList.add('active');
}

setInterval(showNextSlide, 2500);