const seatOne = document.querySelector("#seat-one")
const seatTwo = document.querySelector("#seat-two")
const calendar = document.querySelector("#calendar")

let dateTime;
let reservation = {}

calendar.addEventListener("change", e => {
    dateTime = e.currentTarget.value
    checkForReservation();
})

seatOne.addEventListener("click", () => {
    reservation['seatOne'] = dateTime
})

seatTwo.addEventListener("click", () => {
    reservation['seatTwo'] = dateTime
})

function checkForReservation() {
    fetch('https://localhost:7289/Reservation/checkReservation')
        .then(response => response.text())
        .then(text => console.log(text))
}