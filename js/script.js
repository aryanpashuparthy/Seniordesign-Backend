// Select the button with the ID 'btn' and the sidebar with the class 'sidebar'
let btn = document.querySelector('#btn');
let sidebar = document.querySelector('.sidebar');

// Toggle the 'active' class on the sidebar when the button is clicked
btn.onclick = function () {
  sidebar.classList.toggle('active');
};
