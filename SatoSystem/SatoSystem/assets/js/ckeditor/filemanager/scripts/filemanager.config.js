/*---------------------------------------------------------
  Configuration
---------------------------------------------------------*/

// Set culture to display localized messages
var culture = 'en';

// Autoload text in GUI
// If set to false, set values manually into the HTML file
var autoload = true;

// Display full path - default : false
var showFullPath = false;

// Set this to the server side language you wish to use.
var lang = 'php'; // options: php, jsp, cfm // we are looking for contributors for lasso, python connectors (partially developed)

var am = document.location.pathname.substring(1, document.location.pathname
		.lastIndexOf('/') + 1);
// Set this to the directory you wish to manage.
var fileRoot = '/app/webroot/files/';;

// Show image previews in grid views?
var showThumbs = true;

// Allowed image extensions when type is 'image'
var imagesExt = ['jpg', 'jpeg', 'gif', 'png'];
