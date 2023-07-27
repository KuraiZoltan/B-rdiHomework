const seatOne = document.querySelector("#seat-one")
const seatTwo = document.querySelector("#seat-two")
const calendar = document.querySelector("#calendar")

let dateTime;
let reservation = {}

main()

function main() {
    data = getSeatData()
    addEventListeners()
    console.log(data)
}

function addEventListeners() {
    seatOne.addEventListener("click", () => {
        reservation['seatOne'] = dateTime
    })
    seatTwo.addEventListener("click", () => {
        reservation['seatTwo'] = dateTime
    })
}

async function ApiGet(url) {
    let response = await fetch(url);
    if (response.ok) {
        return await response.json()
    }
}

async function getSeatData() {
    let response = await ApiGet("https://localhost:7289/Seat/getSeats")
    return response
}


