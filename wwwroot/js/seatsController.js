const seatOne = document.querySelector("#seat-one")
const seatTwo = document.querySelector("#seat-two")
const calendar = document.querySelector("#calendar")
const summary = document.querySelector(".summary")
const reserveBtn = document.querySelector("#reserve-btn")

let reservation = {}

main()

async function main() {
    let response = await ApiGet("https://localhost:7289/Seat/getSeats")
    addAttributesToSeats(response)
    addEventListeners()
}

function addEventListeners() {
    seatOne.addEventListener("click", () => {
        reservation["seat-1"] = 'occupied'
        let pTag = document.createElement("p")
        pTag.innerHTML = "Seat one"
        summary.appendChild(pTag)
    })
    seatTwo.addEventListener("click", () => {
        reservation["seat-2"] = 'occupied'
        let pTag = document.createElement("p")
        pTag.innerHTML = "Seat two"
        summary.appendChild(pTag)
    })
    reserveBtn.addEventListener("click", async () => {
        await ApiPost("https://localhost:7289/Seat/reserveSeats", reservation)
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

async function ApiPost(url, payload) {
    let data = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(payload),
    })
    return await data.json()
}

