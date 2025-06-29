const targetDate = new Date("2025-05-01T00:00:00").getTime();
const countdownElement = document.getElementById("countdown");
const days = document.getElementById("days");
const hours = document.getElementById("hours");
const secs = document.getElementById("secs");
const mins = document.getElementById("mins");
const videoModal = document.getElementById('video-modal');
const videoFrame = document.getElementById('video-frame');
const thumbnail = document.getElementById('video-thumbnail');
const closeModal = document.getElementById('close-modal');

thumbnail.addEventListener('click', () => {
    videoModal.style.display = 'flex';
    // autoplay və rel parametrləri ilə birlikdə video linki
    videoFrame.src = "https://www.youtube.com/embed/R6xFbxLwTmU?autoplay=0&rel=0";
});

closeModal.addEventListener('click', () => {
    videoModal.style.display = 'none';
    videoFrame.src = ""; // Modal bağlananda video dayansın
});
function updateCountdown() {
    const now = new Date().getTime();
    const timeLeft = targetDate - now;

    if (timeLeft <= 0) {
        days.textContent = `00`
        hour.textContent = `00`
        mins.textContent = `00`
        secs.textContent = `00`
        clearInterval(timer);
        return;
    }

    const day = Math.floor(timeLeft / (1000 * 60 * 60 * 24));
    const hour = Math.floor((timeLeft % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    const minute = Math.floor((timeLeft % (1000 * 60 * 60)) / (1000 * 60));
    const second = Math.floor((timeLeft % (1000 * 60)) / 1000);

    days.textContent = `${day}`;
    hours.textContent = `${hour}`;
    mins.textContent = `${minute}`;
    secs.textContent = `${second}`;
}

const timer = setInterval(updateCountdown, 1000);
updateCountdown();
const slides = document.querySelectorAll('.recognize_us-carousel .slide');
let currentIndex = 0;

function showNextSlide() {
    slides[currentIndex].classList.remove('active');
    currentIndex = (currentIndex + 1) % slides.length;
    slides[currentIndex].classList.add('active');
}

setInterval(showNextSlide, 2500);
const root = document.documentElement;
const marqueeElementsDisplayed = getComputedStyle(root).getPropertyValue("--marquee-elements-displayed");
const marqueeContent = document.querySelector("ul.marquee-content");

root.style.setProperty("--marquee-elements", marqueeContent.children.length);

for(let i=0; i<marqueeElementsDisplayed; i++) {
  marqueeContent.appendChild(marqueeContent.children[i].cloneNode(true));
}
