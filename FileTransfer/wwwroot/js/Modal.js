
const myModal = new bootstrap.Modal(document.getElementById('staticBackdrop'), {
    keyboard: false
})

const ModalText = (titulo,mensaje) => {
    const modaltitle = document.getElementById("modal-title")
    const modalbody = document.getElementById("modal-body")
    modaltitle.textContent = titulo
    modalbody.textContent = mensaje
}


export { myModal, ModalText }