

function MostrarAlerta(mensaje, tipo) {
    let div = document.createElement('div');
    let button = document.createElement('button');
    Mensaje.appendChild(div);
    div.classList.add("alert")
    div.classList.add(`alert-${tipo}`)
    div.classList.add("alert-dismissible")
    div.classList.add("fade")
    div.classList.add("show")
    div.setAttribute("role", "alert")
    div.textContent = mensaje
    //boton
    button.setAttribute("type", "button")
    button.classList.add("btn-close")
    button.setAttribute("data-bs-dismiss", "alert")
    button.setAttribute("aria-label", "Close")
    div.appendChild(button)
    document.getElementById("AlertasContent").children[0].appendChild(div)
}

export {MostrarAlerta}