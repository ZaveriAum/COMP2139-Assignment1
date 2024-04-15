function updateSliderValue() {
    let slider = document.getElementById("rating");
    console.log(slider.value)
    let sliderValueSpan = document.getElementById("sliderValue");
    sliderValueSpan.textContent = slider.value + "/10";
}