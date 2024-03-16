// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//Function displays an alert message when Hello button in the Student View is clicked
function HelloStudent() {
    alert("Hello Student!!");
}

//Event handles the Hello button in the Student View
$(document).on("click", ".student-btn", function () {
    HelloStudent();
});

//Function sends two integers to the Controller to perform a sum
function Sum() {
    var firstNumber = 2;
    var secondNumber = 3;

    $.ajax({
        //url format '/{Name of Controller}/{Name of C# Function}'
        url: '/Student/Sum',
        type: 'POST',
        dataType: 'json',
        data: {
            first: firstNumber,
            second: secondNumber
        },
        //retData is the data obtained from the controller
        success: function (retData) {
            console.log(retData);
            var sum = retData.returnedSum;
            alert("The sum is: " + sum);
        },
        error: function (retData) {
            console.log(retData);
        }
    });
};

//Event handles the Sum button in the Student View
$(document).on("click", ".sum-btn", function () {
    Sum();
});

//Function sends an array to the Contoller and returns a new array with an additional property (Pretending to make a DB call)
function GetStudents() {
    $.ajax({
        url: '/Student/GetStudents',
        type: 'POST',
        dataType: 'json',
        success: function (retData) {
            console.log(retData);
            alert("Full Name: " + retData[0].firstName + " " + retData[0].lastName + " ID: " + retData[0].id);
        },
        error: function (retData) {
            console.log(retData);
        }
    });
};

//Event handles the Get Students DB button in the Student View
$(document).on("click", ".get-students-btn", function () {
    GetStudents();
});