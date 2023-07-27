const seatOne = document.querySelector("#seat-one")
const seatTwo = document.querySelector("#seat-two")
const calendar = document.querySelector("#calendar")

let reservation = {}

main()

async function main() {
    let response = await ApiGet("https://localhost:7289/Seat/getSeats")
    addAttributesToSeats(response)
    addEventListeners()
}

function addEventListeners() {
    seatOne.addEventListener("click", () => {
        reservation['seatOne'] = dateTime
    })
    seatTwo.addEventListener("click", () => {
        reservation['seatTwo'] = dateTime
    })
}

function addAttributesToSeats(response) {
    for (let i = 0; i < response.length; i++) {
        if (response[i].seatName == 'seat-1') {
            seatOne.classList.add(response[i].seatStatus)
        } else {
            seatTwo.classList.add(response[i].seatStatus)
        }
    }
    
}

async function ApiGet(url) {
    let response = await fetch(url);
    if (response.ok) {
        return await response.json()
    }
}



