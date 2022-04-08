import { MostrarAlerta } from "./Alertas.js"
const TablaCuerpo = document.getElementById("TablaCuerpo");
document.getElementById("FilterButton").addEventListener("click", function () {
    let FilterData = document.getElementById("FilterData").value

    $.ajax({
        url: `Filtrar?iduser=${idlocal}&dato=${FilterData}`,
        async: true,
        type: "post",
        success: function (data) {
            FilterData = ""
            if (data.lista.length !== 0) {
                TablaCuerpo.textContent = "";
                let lista = data.lista
                CrearFila(lista)
            } else {
                MostrarAlerta("No se logro ordernar", "danger");
            }
        },
        error: function (data) {
            console.log(data)
        }
    })
})
function CrearElemento(elemento, { list, ...props }) {
    const el = document.createElement(elemento);
    list && el.setAttribute('list', list);
    Object.assign(el, props);
    return el;
}
let asc = false
let rotacion = 0
const Ordenar = typeorder => {
    const elemento = document.getElementById(typeorder)
    const idimagen = elemento.children[1].id
    const imagen = document.getElementById(idimagen)
    rotacion = rotacion + 180
    imagen.style.transform = "rotate(" + rotacion + "deg)"
    asc = !asc
    $.ajax({
        url: `OrdenarMisArchivos?iduser=${idlocal}&ordername=${typeorder}&asc=${asc}`,
        async: true,
        type: "post",
        success: function (data) {
            if (data === null) {
                MostrarAlerta("No se logro ordernar", "danger");
            } else {
                TablaCuerpo.textContent = "";
                let lista = data.lista
                CrearFila(lista)
            }
        },
        error: function (err) {
            console.log(err)
        }
    })
}

const CrearFila = lista => {

    for (let i = 0; i < lista.length; i++) {
        const tr = document.createElement("tr")
        const tdname = document.createElement("td")
        const tddate = document.createElement("td")
        const tdsize = document.createElement("td")
        const tdbuttons = document.createElement("td")
        const spanname = document.createElement("span")
        const spandate = document.createElement("span")
        const spansize = document.createElement("span")
        const buttondelete = CrearElemento("button", {
            className: 'btn btn-danger',
        })
        buttondelete.textContent = "Borrar"
        buttondelete.addEventListener("click", function () {
            Borrar(lista[i].nameFile)
        })
        const buttondownload = CrearElemento("a", {
            className: 'btn btn-primary',
            id: 'descargar',
            href: "https://localhost:44311/User/DownloadFile?iduser=" + lista[i].idUser + "&filename=" + lista[i].nameFile,
            target: "_blank",
        })
        buttondownload.textContent = "Descargar"
        for (let index = 0; index < 4; index++) {
            spanname.textContent = lista[i].nameFile
            spandate.textContent = lista[i].dateUpload
            spansize.textContent = lista[i].sizeFile
            tdname.appendChild(spanname)
            tddate.appendChild(spandate)
            tdsize.appendChild(spansize)
            tdbuttons.classList.add("d-flex")
            tdbuttons.classList.add("gap-1")
            tdbuttons.appendChild(buttondownload)
            tdbuttons.appendChild(buttondelete)
            tr.appendChild(tdname)
            tr.appendChild(tddate)
            tr.appendChild(tdsize)
            tr.appendChild(tdbuttons)
        }

        TablaCuerpo.appendChild(tr)
    }
}