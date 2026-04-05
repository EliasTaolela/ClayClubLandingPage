// ─── GLOBAL VARIABLES ─────────────────────────
const nav = document.querySelector("nav");
const cols = document.querySelectorAll(".parallax-col");
const gallery = document.querySelector(".gallery-strip__inner");
const gallerySection = document.querySelector(".gallery-strip");
let isDown = false, startX = 0, scrollLeft = 0;
let ticking = false;

// ─── NAV SCROLL EFFECT ───────────────────────
window.addEventListener("scroll", () => {
    nav.classList.toggle("scrolled", window.scrollY > 50);

    if (!ticking) {
        requestAnimationFrame(scrollLoop);
        ticking = true;
    }
}, { passive: true });

// ─── UNIFIED SCROLL LOOP ─────────────────────
function scrollLoop() {
    const scrollY = window.scrollY;

    // HERO PARALLAX
    const hero = document.querySelector(".hero");
    const heroH = hero ? hero.offsetHeight : 0;
    if (scrollY <= heroH * 1.2) {
        cols.forEach(col => {
            const speed = parseFloat(col.dataset.speed) || 0.3;
            col.style.transform = `translateY(${scrollY * speed * 0.6}px)`;
        });
    }

    // GALLERY AUTO-PARALLAX
    if (gallerySection && gallery) {
        const rect = gallerySection.getBoundingClientRect();
        const center = rect.top + rect.height / 2 - window.innerHeight / 2;
        gallery.style.transform = `translateX(${-center * 0.12}px)`;
    }

    ticking = false;
}

// ─── TOGGLE NAV (HAMBURGER MENU) ─────────────
function initNavToggle() {
    const hamburger = document.querySelector(".hamburger");
    const navLinks = document.querySelector(".nav-links");
    const authButtons = document.querySelector(".auth-buttons");
    const ctaButtons = document.querySelectorAll(".btn-cta");

    if (!hamburger) return;

    hamburger.addEventListener("click", () => {
        navLinks?.classList.toggle("active");
        authButtons?.classList.toggle("active");
        ctaButtons.forEach(btn => btn.classList.toggle("active"));
    });
}
initNavToggle();

// ─── FADE-IN ON SCROLL ───────────────────────
const fadeObserver = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
        if (entry.isIntersecting) entry.target.classList.add("show");
    });
}, { threshold: 0.2 });

document.querySelectorAll(".fade-in").forEach(el => fadeObserver.observe(el));

// ─── GALLERY DRAG SCROLL ────────────────────
if (gallery) {
    gallery.addEventListener("mousedown", (e) => {
        isDown = true;
        startX = e.pageX - gallery.offsetLeft;
        scrollLeft = gallery.parentElement.scrollLeft;
        gallery.style.cursor = "grabbing";
    });

    window.addEventListener("mouseup", () => {
        isDown = false;
        gallery.style.cursor = "grab";
    });

    gallery.addEventListener("mousemove", (e) => {
        if (!isDown) return;
        e.preventDefault();
        const x = e.pageX - gallery.offsetLeft;
        const walk = (x - startX) * 1.5;
        gallery.parentElement.scrollLeft = scrollLeft - walk;
    });
}

// ─── SUBSCRIBE FORM ─────────────────────────
const subForm = document.querySelector(".subscribe-form");
const subBtn = subForm?.querySelector(".sub-btn");
const emailInput = subForm?.querySelector(".email-input");
const successMsg = document.querySelector(".success-msg");

subBtn?.addEventListener("click", () => {
    const email = emailInput.value.trim();
    if (!email || !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)) {
        emailInput.style.borderColor = "#B05C52";
        emailInput.style.background = "#FDF0EB";
        setTimeout(() => {
            emailInput.style.borderColor = "";
            emailInput.style.background = "";
        }, 1200);
        return;
    }

    fetch('/Subscribe/Add', {
        method: 'POST',
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        body: `email=${encodeURIComponent(email)}`
    })
        .then(res => res.text())
        .then(data => {
            alert(data);
            subForm.style.display = "none";
            successMsg.style.display = "block";
        })
        .catch(err => console.error(err));
});

emailInput?.addEventListener("keydown", (e) => {
    if (e.key === "Enter") subBtn.click();
});

// ─── CARD HOVER TINT ────────────────────────
document.querySelectorAll(".class-card").forEach(card => {
    card.addEventListener("mouseenter", () => card.style.filter = "brightness(0.96)");
    card.addEventListener("mouseleave", () => card.style.filter = "");
});