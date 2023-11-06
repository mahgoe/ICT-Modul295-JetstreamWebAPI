function showErrorMessage(field, errorMessageId, message) {
  document.getElementById(errorMessageId).innerText = message;
  field.classList.remove("border-gray-300", "dark:border-gray-600");
  field.classList.add("border-red-500");
}

function clearErrorMessage(field, errorMessageId) {
  document.getElementById(errorMessageId).innerText = "";
  field.classList.remove("border-red-500");
  field.classList.add("border-gray-300", "dark:border-gray-600");
}

function validateForm(e) {
  e.preventDefault();

  const firstName = document.getElementById("firstname");
  const lastName = document.getElementById("lastname");
  const email = document.getElementById("email");
  const phone = document.getElementById("phone");

  // Trim für alle Felder um keine Leerzeichen zu haben
  firstName.value = firstName.value.trim();
  lastName.value = lastName.value.trim();
  email.value = email.value.trim();
  phone.value = phone.value.trim();

  // Validation
  let isValid = true;

  if (!validateRequiredField(firstName) || !validateNameFormat(firstName)) {
    isValid = false;
    showErrorMessage(
      firstName,
      "errorMessageFirstName",
      "Bitte einen gültigen Vornamen eingeben"
    );
  } else {
    clearErrorMessage(firstName, "errorMessageFirstName");
  }

  if (!validateRequiredField(lastName) || !validateNameFormat(lastName)) {
    isValid = false;
    showErrorMessage(
      lastName,
      "errorMessageLastName",
      "Bitte einen gültigen Nachnamen eingeben."
    );
  } else {
    clearErrorMessage(lastName, "errorMessageLastName");
  }

  if (!validateRequiredField(email) || !validateEmailFormat(email)) {
    isValid = false;
    showErrorMessage(
      email,
      "errorMessageEmail",
      "Bitte eine gültige E-Mail eingeben."
    );
  } else {
    clearErrorMessage(email, "errorMessageEmail");
  }

  if (!validateRequiredField(phone) || !validatePhoneNumber(phone)) {
    isValid = false;
    showErrorMessage(
      phone,
      "errorMessagePhone",
      "Bitte eine gültige Telefonnummer eingeben"
    );
  } else {
    clearErrorMessage(phone, "errorMessagePhone");
  }

  // Formular abschicken bei erfolgreicher Validierung
  if (isValid) {
    postData(firstName.value, lastName.value, email.value);
    console.log(
      "firstName: " + firstName.value,
      "lastName: " + lastName.value,
      "email: " + email.value
    );
  }
}

// Funktionen für Validierungen und Formularverarbeitung
function validateRequiredField(field) {
  if (field.value === "") {
    field.classList.add("error");
    return false;
  } else {
    field.classList.remove("error");
    return true;
  }
}

// Funktion die nur Buchstaben und Bindestriche erlaubt
function validateNameFormat(field) {
  const namePattern = /^[a-zA-ZäöüÄÖÜ]+([- ][a-zA-ZäöüÄÖÜ]+)*$/;

  if (!namePattern.test(field.value)) {
    return false;
  }
  return true;
}

function validateEmailFormat(field) {
  const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
  if (!emailPattern.test(field.value)) {
    return false;
  }
  return true;
}

function validatePhoneNumber(field) {
  const phonePattern =
    /(\b(0041|0)|\B\+41)(\s?\(0\))?(\s)?[1-9]{2}(\s)?[0-9]{3}(\s)?[0-9]{2}(\s)?[0-9]{2}\b/;
  if (!phonePattern.test(field.value)) {
    return false;
  }
  return true;
  console.log("Telefon richtig");
}

function formatDate(dateString) {
  // Teilt das Datum in Tag, Monat und Jahr
  const parts = dateString.split("/");

  // Erstellt ein neues Date-Objekt unter Berücksichtigung des Formats "DD/MM/YYYY"
  const newDate = new Date(parts[2], parts[1] - 1, parts[0]);

  // Erstellt ein neues Date-Objekt für die aktuelle Uhrzeit
  const now = new Date();

  // Überprüft, ob das Datum gültig ist
  if (!isNaN(newDate)) {
    // Gibt das Datum und die aktuelle Uhrzeit im ISO-8601-Format zurück ohne Zeitzone
    return (
      newDate.getFullYear() +
      "-" +
      String(newDate.getMonth() + 1).padStart(2, "0") +
      "-" +
      String(newDate.getDate()).padStart(2, "0") +
      "T" +
      String(now.getHours()).padStart(2, "0") +
      ":" +
      String(now.getMinutes()).padStart(2, "0") +
      ":" +
      String(now.getSeconds()).padStart(2, "0") +
      "." +
      String(now.getMilliseconds()).padStart(3, "0")
    );
  } else {
    // Gibt einen leeren String zurück oder wirft einen Fehler, wenn das Datum ungültig ist
    return "";
  }
}

function postData(firstName, lastName, email) {
  fetch("http://localhost:5285/Registrations", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      firstName: firstName,
      lastName: lastName,
      email: email,
      phone: document.getElementById("phone").value,
      priority: document.querySelector('input[name="list-radio"]:checked')
        .value,
      service: document.getElementById("serviceDropdown").value,
      create_date: formatDate(document.getElementById("startDate").value),
      pickup_date: formatDate(document.getElementById("endDate").value),
      status: "Offen",
      service: document.getElementById("serviceDropdown").value,
      price: document.getElementById("total").value,
      comment: "Test",
    }),
  })
    .then((response) => {
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      return response.json();
    })
    .then((data) => {
      window.location.href = "formconfirm.html";
    })
    .catch((error) => {
      console.error(
        "There has been a problem with your fetch operation:",
        error
      );
      window.location.href = "formerror.html";
    });
}

const params = new URLSearchParams(window.location.search);
const dropdownValue = params.get("service");

if (dropdownValue) {
  const selectElement = document.querySelector("select");
  selectElement.value = dropdownValue;
}

function setEstimatedPickupDate(priority, startDate) {
  let start = new Date(startDate);
  let daysToAdd = 0;

  if (priority === "Tief") {
    daysToAdd = 12;
  } else if (priority === "Standard") {
    daysToAdd = 7;
  } else if (priority === "Express") {
    daysToAdd = 5;
  }

  const newDate = new Date(start);
  newDate.setDate(start.getDate() + daysToAdd);

  const formattedDate = `${String(newDate.getDate()).padStart(2, "0")}/${String(
    newDate.getMonth() + 1
  ).padStart(2, "0")}/${newDate.getFullYear()}`;

  document.getElementById("endDate").value = formattedDate;
}

function calculateTotal() {
  let serviceCost = 0;
  let priorityCost = 0;

  const serviceOption = document.querySelector("select").value;
  if (serviceOption === "Kleiner Service") {
    serviceCost = 49;
  } else if (serviceOption === "Grosser Service") {
    serviceCost = 69;
  } else if (serviceOption === "Rennski Service") {
    serviceCost = 99;
  } else if (serviceOption === "Bindungen montieren und einstellen") {
    serviceCost = 39;
  } else if (serviceOption === "Fell zuschneiden") {
    serviceCost = 25;
  } else if (serviceOption === "Heisswachsen") {
    serviceCost = 18;
  }

  const priority = document.querySelector(
    'input[name="list-radio"]:checked'
  ).value;
  if (priority === "Standard") {
    priorityCost = 5;
  } else if (priority === "Express") {
    priorityCost = 10;
  }

  const total = serviceCost + priorityCost;
  document.getElementById("total").value = `CHF ${total}.-`;
}

window.onload = function () {
  document
    .getElementById("submitForm")
    .addEventListener("submit", validateForm);

  const startDateInput = document.getElementById("startDate");
  const today = new Date();
  const formattedToday = `${String(today.getDate()).padStart(2, "0")}/${String(
    today.getMonth() + 1
  ).padStart(2, "0")}/${today.getFullYear()}`;

  startDateInput.value = formattedToday;
  setEstimatedPickupDate("Tief", today);

  document.querySelectorAll('input[name="list-radio"]').forEach((radio) => {
    radio.addEventListener("change", function () {
      console.log("radio changed to " + this.value);
      const startDateValue = startDateInput.value.split("/");
      const startDate = new Date(
        `${startDateValue[2]}-${startDateValue[1]}-${startDateValue[0]}`
      );
      setEstimatedPickupDate(this.value, startDate);
      calculateTotal();
    });
  });

  startDateInput.addEventListener("change", function () {
    const selectedDateValue = this.value.split("/");
    const selectedDate = new Date(
      `${selectedDateValue[2]}-${selectedDateValue[1]}-${selectedDateValue[0]}`
    );
    const priority = document.querySelector(
      'input[name="list-radio"]:checked'
    ).value;
    setEstimatedPickupDate(priority, selectedDate);
  });

  // Event-Listener für Änderungen an der Service-Dropdown-Auswahl
  document.querySelector("select").addEventListener("change", function () {
    calculateTotal();
  });

  // Startwert für den Gesamtbetrag setzen
  calculateTotal();
};

let lastKnownStartDate = "";

setInterval(() => {
  const startDateInput = document.getElementById("startDate");
  const currentStartDate = startDateInput.value;

  if (currentStartDate !== lastKnownStartDate) {
    lastKnownStartDate = currentStartDate;

    const startDateValue = currentStartDate.split("/");
    const startDate = new Date(
      `${startDateValue[2]}-${startDateValue[1]}-${startDateValue[0]}`
    );
    const priority = document.querySelector(
      'input[name="list-radio"]:checked'
    ).value;

    setEstimatedPickupDate(priority, startDate);
  }
}, 500); // alle 500 Millisekunden
