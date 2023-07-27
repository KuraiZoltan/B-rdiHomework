const seatOne = document.querySelector("#seat-one")
const seatTwo = document.querySelector("#seat-two")
const calendar = document.querySelector("#calendar")

let dateTime;
let reservation = {}

function main() {
    
}

calendar.addEventListener("change", async e => {
    dateTime = e.currentTarget.value
    let response = await ApiGet("https://localhost:7289/Reservation/checkReservation")
    console.log(response)
})

seatOne.addEventListener("click", () => {
    reservation['seatOne'] = dateTime
})

seatTwo.addEventListener("click", () => {
    reservation['seatTwo'] = dateTime
})

async function ApiGet(url) {
    let response = await fetch(url);
    if (response.ok) {
        return await response.json()
    }
}