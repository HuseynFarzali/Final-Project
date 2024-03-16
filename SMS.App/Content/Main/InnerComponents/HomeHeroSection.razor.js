export function flickityInit() {
    var flkty = new Flickity('.flickity-carousel', {
        "pageDots": true,
        "prevNextButtons": false,
        "cellAlign": "center",
        "wrapAround": true,
        "imagesLoaded": true,
    });
}