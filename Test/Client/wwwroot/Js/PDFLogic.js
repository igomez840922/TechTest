
function exportToPDF(productos) {



    var doc = new jsPDF();

    //doc.text('Hola, ' + Loginuser + ', estos son tus productos', 10, 10);

    var y = 30;
    productos.forEach(function (prod) {
        doc.text(20, y, 'Nombre: ' + prod.nombre);
        doc.text(20, y + 10, 'Modelo: ' + prod.modelo);
        doc.text(20, y + 20, 'Precio: ' + prod.precio);
        y += 40;
    });

    doc.save('productos.pdf');
}
