function CheckFin() {

    let fin = document.getElementById("FINInput").value;
    console.log(fin);
    fetch('/admin/user/GetFin?fin=' + fin)
        .then((response) => {
            return response.json();
            if (response.ok) {
                return response.json();
            } else {
                console.log("bla sef var");
            }

        })
        .then((data) => {
            //const myObj = JSON.parse(data);
            let text = "<table border=1 >";
            text += `<tr><td><img height="150px" src="data:image/png;base64, ${data['photo']}"/></td></tr>`
            for (let x in data) {
                if (x != "photo") {
                    text += "<tr><td>" + x.toLowerCase() + ": " + data[x] + "</td></tr>";
                }

            }
            var image = new Image();

            image.src = data['photo'];

            text += "</table>";
            document.getElementById('table').appendChild(image);
            document.getElementById("table").innerHTML = text;
        });
}