// Scroll reveal animation
const scrollReveal = document.querySelectorAll('.scroll-reveal');

const observer = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            entry.target.classList.add('active');
        }
    });
}, { threshold: 0.1 });

scrollReveal.forEach((element) => {
    observer.observe(element);
});

// Emergency banner animation
const emergencyBanner = document.querySelector('.emergency-banner');
if (emergencyBanner) {
    setInterval(() => {
        emergencyBanner.style.transform = 'scale(1.02)';
        setTimeout(() => {
            emergencyBanner.style.transform = 'scale(1)';
        }, 500);
    }, 2000);
}