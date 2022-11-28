

    $(document).ready(function () {
    loaddata();
        })
    function loaddata()
    {
            var person = new Object();
    person.proID = 1;
    person.cusID = 2;
    person.quantity = 3;
    $.ajax({
        url: '/getcart',
    type: 'POST',
    dataType: 'json',
    data: person,
    success: function (response) {
        $('#myTable').empty();
    $.each(response, function (index, item) {
                        var trHTML = $(`<tr>
        <td>`+ item.cartID + `</td>
        <td>`+ item.name + `</td>
        <td>`+ item.birthday + `</td>
        <td>`+ `
            <button class='iconButton' onclick="UpdateStudent(`+ item.id + `)" id="btnUpdate"><i class="fas fa-pencil-alt"></i>&nbsp;&nbsp; Sửa</button>
            <button class='iconButton' onclick="deleteStudent(`+ item.id + `)" id="btnDelete"><i class="fas fa-trash-alt"></i>&nbsp;&nbsp; Xóa </button>` + `</td>

    </tr>`);
    $('#myTable').append(trHTML);
                    })
                },
            });
         }