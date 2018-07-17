$('head').append('<style id="app-height-css" type="text/css">.app-height {height:100%;}</style>');
$('head').append('<style id="app-half-height-css" type="text/css">.app-half-height {height:50%;}</style>');


function updateAppHeight() {
  var height = window.innerHeight;
  var menuHeight = $("#menu-container").outerHeight(true);
  var appHeight = height - menuHeight - 30;
  var appHalfHeight = height / 2;

  $('#app-height-css').text('.app-height {height:' + appHeight + 'px;}');
  $('#app-half-height-css').text('.app-half-height {height:' + appHalfHeight+ 'px;}');
  
}

$(document).ready(function () {
  updateAppHeight();
});

$(window).resize(function () {
  updateAppHeight();
});
