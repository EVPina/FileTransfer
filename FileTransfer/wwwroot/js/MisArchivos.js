   import { MostrarAlerta } from "./Alertas.js"
    const Borrar = namefile => {
        let iduser = localStorage.getItem("iduser");
        $.ajax({
            url: `DeleteFile?iduser=${iduser}&filename=${namefile}`,
            async: true,
            type: "post",
            success: function (res) {
                if (res) {
                    window.location.reload()
                } else {
                    MostrarAlerta("No se logro eliminar el archivo", "danger");
                }
            },
            error: function (err) {
                MostrarAlerta("No se logro eliminar el archivo", "danger");
            }
        })
}

export default Borrar