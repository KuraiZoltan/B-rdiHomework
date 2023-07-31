const seatOne = document.querySelector("#seat-one")
const seatTwo = document.querySelector("#seat-two")
const calendar = document.querySelector("#calendar")
const summary = document.querySelector(".summary")
const paymentContainer = document.querySelector(".payment")
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
        reservation["seat-1"] = parseInt(seatOne.dataset.seatVersion)
        let pTag = document.createElement("p")
        pTag.innerHTML = "Seat one"
        summary.appendChild(pTag)
    })
    seatTwo.addEventListener("click", () => {
        reservation["seat-2"] = parseInt(seatTwo.dataset.seatVersion)
        let pTag = document.createElement("p")
        pTag.innerHTML = "Seat two"
        summary.appendChild(pTag)
    })
    reserveBtn.addEventListener("click", async () => {
        let response = await ApiPost("https://localhost:7289/Seat/reserveSeats", reservation)
        if (response.ok) {
            let payment = document.createElement("div")
            let paymentText = document.createElement("p")
            let paymentTitle = document.createElement("h3")
            let label = document.createElement("label")
            let input = document.createElement("input")
            let paymentButton = document.createElement("button")

            paymentButton.innerHTML = "Submit"
            paymentButton.classList.add("btn")
            paymentButton.classList.add("btn-primary")
            paymentTitle.innerHTML = "Payment"
            input.type = "email"
            label.innerHTML = "Email"
            paymentText.innerHTML = `For seat(s): ${reservation} give your email address!`

            label.appendChild(input)

            payment.appendChild(paymentTitle)
            payment.appendChild(paymentText)
            payment.appendChild(label)
            payment.appendChild(paymentButton)

            paymentContainer.appendChild(payment)

            paymentButton.addEventListener("click", async () => {
                payload = {
                    Seats: reservation,
                    Email: input.value
                }
                await ApiPost("https://localhost:7289/Seat/payForSeats", payload)
            })
        } else {
            alert("Seat(s) are not available!")
        }
        
    })
}

function addAttributesToSeats(response) {
    for (let i = 0; i < response.length; i++) {
        if (response[i].seatName == 'seat-1') {
            seatOne.classList.add(response[i].seatStatus)
            seatOne.dataset.seatVersion = `${response[i].version}`
        } else {
            seatTwo.classList.add(response[i].seatStatus)
            seatTwo.dataset.seatVersion = `${response[i].version}`
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
    let response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(payload),
    })
    return await response
}

