document
  .getElementById("loginForm")
  .addEventListener("submit", function (event) {
    event.preventDefault();

    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;

    fetch("http://localhost:5285/employees/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ username: username, password: password }),
    })
      .then((response) => {
        if (response.ok) {
          return response.json();
        } else {
          throw new Error("Failed to login");
        }
      })
      .then((data) => {
        localStorage.setItem("token", data.token); // stores the Token in the local storage
        window.location.href = "/adminconfirm.html";
      })
      .catch((error) => {
        console.error("Login Error:", error);
      });
  });
