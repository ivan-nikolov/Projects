// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const fBtn = $("#FBtn");
const tBtn = $("#TBtn");
const currentPageBtn = $("#SBtn");
const prevContainer = $("#prevContainer");
const nextContainer = $("#nextContainer");
const usersTable = $("#usersTable");
const usersRadio = $("#usersRadio");
const projectsRadio = $("#projectsRadio");
const startDateElement = $("#startDate");
const lastDateElement = $("#endDate");
const filterButton = $("#filterButton");

const topUsersBaseURL = "/Users/GetTop10Users";
const topProjectsBaseURL = "/Projects/GetTop10Projects";
const totalHoursForUser = "/Users/GetTotalHoursForUser";
const topProjectHoursForUser = "/Users/GetTopProjectHoursByUserId";

$(document).ready(function () {
    if (fBtn.text() == 0) {
        fBtn.hide();
    }

    getTop10(topUsersBaseURL, "Users");


    $(function () {
        let radios = $('input:radio[name=inlineRadioOptions]');
        if (radios.is(':checked') === false) {
            radios.filter('[value=user]').prop('checked', true);
        }
    });

    $(function () {
        let radios = $('input:radio[name=sortInlineRadioOptions]');
        if (radios.is(':checked') === false) {
            radios.filter('[value=FirstName]').prop('checked', true);
        }
    });

    $('input:radio[name=sortInlineRadioOptions]').change(function () {
        var value = $(this).val();
        sort(value);
    })

    $("#clearButton").click(function () {
        startDateElement.val("");
        lastDateElement.val("");
        if ($(usersRadio).prop("checked")) {
            getTop10(topUsersBaseURL, "Users");
        }
        else if ($(projectsRadio).prop("checked")) {
            getTop10(topProjectsBaseURL, "Projets");
        }

        getUsersByPage(1);
    })

    $('input:radio[name=inlineRadioOptions]').change(function () {
        loadTop10();
    })

    $("#filterButton").click(function () {
        loadTop10();
        getUsersByPage(1);
    })

    $("#PrevBtn").click(function () {
        const prevPage = parseInt(currentPageBtn.text()) - 1;
        getUsersByPage(prevPage);
    })

    $("#NextBtn").click(function () {
        const nextPage = tBtn.text();
        getUsersByPage(nextPage);
    })

    $("#FBtn").click(function () {
        const value = $(this).text();
        getUsersByPage(value);
    })

    $("#TBtn").click(function () {
        const value = $(this).text();
        getUsersByPage(value);
    })

    $(".compareBtn").click(compare);
})

function getTop10(url, title, compareElement) {

    let arr = []
    
    $.ajax({
        url: url,
        type: 'GET',
        success: function (res) {
            google.charts.load('current', { packages: ['corechart', 'bar'] });
            google.charts.setOnLoadCallback(drawBarColors);
            function drawBarColors() {
                
                $(res).each(function (i, item) {
                    let innerArr = ['', item.hours, 'blue', item.fullName];
                    arr.push(innerArr)
                })

                var googleArray = [
                    ['Element', 'Hours', { role: 'style' }, { role: 'annotation' }],
                    ['', populateEmptyFields(arr[0], "number"), 'green', populateEmptyFields(arr[0], "string")],
                    ['', populateEmptyFields(arr[1], "number"), 'silver', populateEmptyFields(arr[1], "string")],
                    ['', populateEmptyFields(arr[2], "number"), 'blue', populateEmptyFields(arr[2], "string")],
                    ['', populateEmptyFields(arr[3], "number"), 'orangered', populateEmptyFields(arr[3], "string")],
                    ['', populateEmptyFields(arr[4], "number"), 'turquoise', populateEmptyFields(arr[4], "string")],
                    ['', populateEmptyFields(arr[5], "number"), 'deeppink', populateEmptyFields(arr[5], "string")],
                    ['', populateEmptyFields(arr[6], "number"), 'darkorange', populateEmptyFields(arr[6], "string")],
                    ['', populateEmptyFields(arr[7], "number"), 'coral', populateEmptyFields(arr[7], "string")],
                    ['', populateEmptyFields(arr[8], "number"), 'blanchedalmond', populateEmptyFields(arr[8], "string")],
                    ['', populateEmptyFields(arr[9], "number"), 'aquamarine', populateEmptyFields(arr[9], "string")],
                ];

                if (compareElement != undefined) {
                    var compareElementArray = ['', compareElement.hours, 'blue', compareElement.fullName];
                    googleArray.push(['', populateEmptyFields(compareElementArray, "number"), 'red', populateEmptyFields(compareElementArray, "string")])
                }

                var data = google.visualization.arrayToDataTable(googleArray);
                var options = {
                    title: 'Top 10 ' + title,
                    chartArea: { width: '100%' },
                    colors: ['#b0120a', '#ffab91'],
                    hAxis: {
                        title: 'Total Hours',
                        minValue: 0
                    },

                };
                var chart = new google.visualization.BarChart(document.getElementById('chart_div'));
                chart.draw(data, options);
            }
        }
    });
}

function loadTop10(compareElement) {
    let startDate = startDateElement.val();
    let lastDate = lastDateElement.val();
    const minDate = '0001-01-01';
    let postUrl = "";
    if (startDate == "" && lastDate != "") {
        postUrl = `?dateFrom=${minDate}&dateTo=${lastDate}`;
    }
    else if (startDate != "" && lastDate == "") {
        postUrl = `?dateFrom=${startDate}`;
    }
    else if (startDate != "" && lastDate != "") {
        postUrl = `?dateFrom=${startDate}&dateTo=${lastDate}`;
    }

    let url = "";
    if ($(usersRadio).prop("checked")) {
        url = topUsersBaseURL + postUrl;
        getTop10(url, "Users", compareElement);
    }
    else if ($(projectsRadio).prop("checked")) {
        url = topProjectsBaseURL + postUrl;
        getTop10(url, "Projets", compareElement);
    }
}

function getUsersByPage(pageNumber) {

    let sortOption;
    $('input:radio[name=sortInlineRadioOptions]').each(function () {
        if ($(this).is(":checked")) {
            sortOption = $(this).attr('value');
        }
    });

    let sortParam = '&sortBy=' + sortOption;

    let startDate = startDateElement.val();
    let lastDate = lastDateElement.val();
    const minDate = '0001-01-01';
    let postUrl = "";
    if (startDate == "" && lastDate != "") {
        postUrl = `&dateFrom=${minDate}&dateTo=${lastDate}`;
    }
    else if (startDate != "" && lastDate == "") {
        postUrl = `&dateFrom=${startDate}`;
    }
    else if (startDate != "" && lastDate != "") {
        postUrl = `&dateFrom=${startDate}&dateTo=${lastDate}`;
    }
    $.ajax({
        url: "/Users/GetAllUsers?pageIndex=" + pageNumber + postUrl + sortParam,
        type: 'GET',
        success: function (res) {
            movePage(res);
            populateTable(res);
        }
    });
}

function populateTable(res) {
    usersTable.html("");
    const users = res.users;
    $(users).each(function () {
        const row = document.createElement("tr");
        const fNameElement = document.createElement("td");
        const surnameElement = document.createElement("td");
        const emailElement = document.createElement("td");
        const btnElementContainer = document.createElement("td");
        const btn = document.createElement("input");
        $(btn).addClass("btn btn-primary btn-block hoursBtn");
        $(btn).attr("type", "button");
        $(btn).attr("userid", this.id);
        $(btn).val("Compare");
        $(btn).click(compare);
        $(btnElementContainer).html(btn);
        $(fNameElement).text(this.firstName);
        $(surnameElement).text(this.lastName);
        $(emailElement).text(this.email);
        $(row).append(fNameElement);
        $(row).append(surnameElement);
        $(row).append(emailElement);
        $(row).append(btnElementContainer);
        $(usersTable).append(row);
    });
}

function compare() {
    let idElement = $(this).attr("userid");
    console.log(idElement);

    let startDate = startDateElement.val();
    let lastDate = lastDateElement.val();
    const minDate = '0001-01-01';
    let postUrl = "";
    if (startDate == "" && lastDate != "") {
        postUrl = `&dateFrom=${minDate}&dateTo=${lastDate}`;
    }
    else if (startDate != "" && lastDate == "") {
        postUrl = `&dateFrom=${startDate}`;
    }
    else if (startDate != "" && lastDate != "") {
        postUrl = `&dateFrom=${startDate}&dateTo=${lastDate}`;
    }

    let url = "";
    let userIdParam = "?userId=" + idElement;
    if ($(usersRadio).prop("checked")) {
        url = totalHoursForUser + postUrl + userIdParam;
    }
    else if ($(projectsRadio).prop("checked")) {
        url = topProjectHoursForUser + postUrl + userIdParam;
    }

    $.ajax({
        url: url,
        type: 'GET',
        success: function (res) {

            loadTop10(res);
        }
    });

}

function movePage(res) {

    if (!res.hasPreviousPage) {
        prevContainer.addClass("disabled");
        fBtn.hide();
    }
    else {
        prevContainer.removeClass("disabled");
        fBtn.show();
    }

    if (!res.hasNextPage) {
        nextContainer.addClass("disabled");
        tBtn.hide();
    }
    else {
        nextContainer.removeClass("disabled");
        tBtn.show();
    }

    currentPageBtn.text(res.pageIndex);
    fBtn.text(res.previousPage);
    tBtn.text(res.nextPage);
}

function populateEmptyFields(value, type) {
    if (value == undefined && type == "number") {
        return 0;
    }
    else if (value == undefined && type == "string") {
        return "";
    }
    else if (value != undefined && type == "number") {
        return value[1];
    }
    else if (value != undefined && type == "string") {
        return value[3];
    }
}

function sort(sortOptoin) {
    let sortParam = '&sortBy=' + sortOptoin;
    let startDate = startDateElement.val();
    let lastDate = lastDateElement.val();
    const minDate = '0001-01-01';
    let postUrl = "";
    if (startDate == "" && lastDate != "") {
        postUrl = `&dateFrom=${minDate}&dateTo=${lastDate}`;
    }
    else if (startDate != "" && lastDate == "") {
        postUrl = `&dateFrom=${startDate}`;
    }
    else if (startDate != "" && lastDate != "") {
        postUrl = `&dateFrom=${startDate}&dateTo=${lastDate}`;
    }
    $.ajax({
        url: "/Users/GetAllUsers?pageIndex=" + currentPageBtn.text + postUrl + sortParam,
        type: 'GET',
        success: function (res) {
            movePage(res);
            populateTable(res);
        }
    });
}