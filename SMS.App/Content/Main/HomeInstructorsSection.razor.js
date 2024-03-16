/*import flickity from 'https://cdn.jsdelivr.net/npm/flickity@3.0.0/+esm'*/

export function flickityInit3() {
    var flickity = new Flickity('.flickity-carousel-instructors', {
        "pageDots": false,
        "cellAlign": "left",
        "wrapAround": true,
        "imagesLoaded": true
    });
}