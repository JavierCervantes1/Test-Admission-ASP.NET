function fillTableUsers(){
    fetch('/user/allusers')
    .then(response => response.json())
    .then(users => {
        console.log(users);
        $('#tbodyUsers').empty();
        users.forEach(user => {
            $('#tbodyUsers').append(`<tr id="trUser${user.Id}">` +
                `<td>${user.Firstname}</td>` +
                `<td>${user.Surname}</td>` +
                `<td>${user.Identification}</td>` +
                `<td>${user.Birthday.slice(0, 11)}</td>` +
                `<td>${user.Salary}</td>` +
                `<td>${user.Name}</td>` +
                `<td>
                    <div class="d-flex justify-content-around font-20" data-user_id="${user.Id}">
                        <a class="editUser fas fa-edit text-warning" href="javascript:void(0)"></a>
                        <a class="deleteUser fas fa-trash text-danger" href="javascript:void(0)"></a>
                    </div>
                </td>` +
            '</tr>');
        });

    });
}

$(function () {
    fillTableUsers();

    $('#btnSaveUser').on("click", function() {
        let Firstname = $('input[name=Firstname]').val();
        let Surname = $('input[name=Surname]').val();
        let Identification = $('input[name=Identification]').val();
        let Birthday = $('input[name=Birthday]').val();
        let Salary = $('input[name=Salary]').val();
        let Cod_position = $('select[name=Cod_position]').val();

        fetch('/user/store', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                Firstname, Surname,
                Identification, Birthday,
                Salary, Cod_position
            })
        })
        .then(response => response.json())
        .then(data => {
            console.log(data);
            Swal.fire({
                icon: "success",
                title: data.message,
                showConfirmButton: false,
                timer: 3000,
            });
            $('input.form-control, select.form-control').val("");
            fillTableUsers();
        })
    });

    $('body').on("click", '.deleteUser', function () {
        Swal.fire({
            icon: 'warning',
            title: '¿Desea borrar este Usuario?',
            showDenyButton: true,
            confirmButtonText: 'Sí',
            denyButtonText: `No`,
        }).then((result) => {
            if (result.isConfirmed) {
                let user_id = $(this).parent().data("user_id");
                console.log(user_id)
                fetch(`/user/delete/${user_id}`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({
                        Id: user_id
                    })
                })
                .then(response => response.json())
                .then(data => {
                    console.log(data);
                    Swal.fire({
                        icon: "success",
                        title: data.message,
                        showConfirmButton: false,
                        timer: 3000,
                    });
                    fillTableUsers();
                })
            }
        })
    });
});