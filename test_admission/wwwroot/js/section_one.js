$(function () {
    $('#btnSumSerie').on("click", function () {
        let x_value = $('input[name=x_value]').val();
        let y_value = $('input[name=y_value]').val();
        fetch("/home/series", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                x_value,
                y_value,
            })
        })
        .then(response => response.json())
        .then(data => {
            console.log(data);
            $('#showResultSeries').text(data.result);
        });
    });

    $('#btnNextDatesWeek').on("click", function () {
        let date = $('input[name=date]').val();
        let day = $('input[name=day]').val();

        fetch("/home/weekdates", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                date,
                day,
            })
        })
        .then(response => response.json())
        .then(data => {
            console.log(data.dates.join(', '));
            $('#showNextDates').text(Object.values(data.dates).join(', '));
            /* Object.values(data.dates).foreach(date => {
                $('#showNextDatesWeek').append()
            });*/
        });
    });
});