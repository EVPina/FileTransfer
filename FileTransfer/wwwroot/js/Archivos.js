import { MostrarAlerta } from "./Alertas.js"
import { BlockPage } from "./BlockPage.js"
class FileUser {
    constructor(numeroFile, nameFile, sizeFile, dateUpload, iduser) {
        this.NumeroFile = numeroFile,
        this.IdFileUser = "",
        this.NameFile = nameFile,
        this.SizeFile = sizeFile,
        this.DateUpload = dateUpload
        this.IdUser = iduser
    }
}
const iduser = localStorage.getItem('iduser')
const ContentFiles = document.getElementById("ContentFiles")
const uploadbutton = document.getElementById("Uploadbutton")
const uploadarchivos = document.getElementById("uploadarchivos")
const ListArchivos = document.getElementById("ListArchivos")
const sendbutton = document.getElementById("Sendbutton")
const lista = ListArchivos.children[1]
let file = new FileUser()
let dataToSend = []
//Cargar Archivos
uploadbutton.addEventListener("change", function () {
    ListArchivos.style.display = "block";
    const allarchivos = uploadarchivos.files
    let cantidadarchivos = allarchivos.length
    if (cantidadarchivos + dataToSend.length <= 10) {
        for (let i = 0; i < cantidadarchivos; i++) {
            let actualfile = uploadarchivos.files[i];
            CrearItem(i, actualfile.name, actualfile.size);
        }
    }
    else {
        MostrarAlerta("Se excedio en el límite", "danger");
    }
})

 function SizeSuffix(bytes)
{
     const sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB']
     if (bytes === 0) return 'n/a'
     const i = parseInt(Math.floor(Math.log(bytes) / Math.log(1024)), 10)
     if (i === 0) return `${bytes} ${sizes[i]})`
     return `${(bytes / (1024 ** i)).toFixed(1)} ${sizes[i]}`
}


function CrearItem(i, nombre_archivo, size_archivo) {
    let li = document.createElement('li');
    let img = document.createElement("img");
    let spannamefile = document.createElement("span");
    let spansize = document.createElement("span");
    let today = new Date();
    const sizetotal = SizeSuffix(size_archivo);
    lista.appendChild(li)
    img.src = document.URL.replace("Home/Archivos", "images/carpeta.png")
    li.appendChild(img)
    li.appendChild(spannamefile)
    spannamefile.textContent = nombre_archivo
    li.appendChild(spansize)
    spansize.textContent = sizetotal
    file = new FileUser(i, nombre_archivo, sizetotal, today.toLocaleString("en-US", { day: "2-digit", month: "2-digit", year: "2-digit" }), iduser)
    dataToSend.push(file)
    let button = document.createElement('button')
    button.classList.add("btn")
    button.classList.add("btn-danger")
    button.setAttribute("id", "DeleteButton")
    button.textContent = "Borrar"
    li.appendChild(button)
    button.addEventListener('click', function () {
        dataToSend.splice(dataToSend.indexOf(file), 1)
        button.parentElement.remove()
    })
}

//Enviar Archivos
sendbutton.addEventListener("click", function (evt) {
    if (iduser === null | iduser === undefined) {
        window.location.href = "Login/Account";
    } else {
        if (lista.children.length !== 0) {
            const blockpage = BlockPage(ContentFiles);
            $.ajax({
                type: 'post',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(dataToSend),
                url: 'Archivos',
                async: true,
                success: function (data) {
                    lista.textContent = "";
                    if (data.success) {
                        MostrarAlerta("Se guardo exitosamente", "success");
                    } else {
                        MostrarAlerta("Hubo un error", "danger");
                    }
                    dataToSend = [];
                    if (data.length != 0) {
                        for (let indice = 0; indice < data.files.length; indice++) {
                            CrearItem(indice, data.files[indice].nameFile, data.files[indice].sizeFile);
                        }
                    }
                    blockpage.remove();
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } else {
            console.log("No hay archivos")
        }
    }
})