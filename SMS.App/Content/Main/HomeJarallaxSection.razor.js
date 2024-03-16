import { jarallax, jarallaxVideo } from "https://cdn.jsdelivr.net/npm/jarallax@2/+esm"
export function jarallaxInit() {

    jarallax(document.querySelector('.jarallax'), {
        speed: 0.8
    });
}