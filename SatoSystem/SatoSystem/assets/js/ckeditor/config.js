/*
Copyright (c) 2003-2010, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

/*CKEDITOR.editorConfig = function( config )
{
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	// config.uiColor = '#AADC6E';
};*/

CKEDITOR.editorConfig = function( config )
{
	// Define changes to default configuration here. For example:
	// config.language = 'fr';

	config.toolbar = 'MyToolbar';
 
    config.toolbar_MyToolbar =
    [
		['Source','-','Save','NewPage','DocProps','Preview','Print','-','Templates'],
        ['Cut','Copy','Paste','PasteText','PasteFromWord','-','Scayt'],
        ['Undo','Redo','-','Find','Replace','-','SelectAll','SpellChecker','RemoveFormat'],
		['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField'],
        ['MediaEmbed','Image','Flash','Table','HorizontalRule','Smiley','SpecialChar','PageBreak','Iframe'],        
        ['Styles','Format','Font','FontSize'],
		[ 'TextColor','BGColor' ],
		['Bold','Italic','Underline','Strike','Subscript','Superscript','-'],
        ['NumberedList','BulletedList','-','Outdent','Indent','Blockquote','CreateDiv','-','JustifyLeft','JustifyCenter','JustifyRight','JustifyBlock','-','BidiLtr','BidiRtl'],
        ['Link','Unlink','Anchor'],
        ['Maximize','ShowBlocks','-','About']
    ];

	config.extraPlugins = 'MediaEmbed';
	config.uiColor = 'black';
	config.width = '915';
	config.height = '500';
	config.filebrowserBrowseUrl = 'http://carwashpark.localhost/js/ckfinder/ckfinder.html';
	config.filebrowserImageBrowseUrl = 'http://carwashpark.localhost/js/ckfinder/ckfinder.html?type=Images';
	config.filebrowserFlashBrowseUrl = 'http://carwashpark.localhost/js/ckfinder/ckfinder.html?type=Flash';
	config.filebrowserUploadUrl = 'http://carwashpark.localhost/js/ckfinder/core/connector/php/connector.php?command=QuickUpload&type=Files';
	config.filebrowserImageUploadUrl = 'http://carwashpark.localhost/js/ckfinder/core/connector/php/connector.php?command=QuickUpload&type=Images';
	config.filebrowserFlashUploadUrl = 'http://carwashpark.localhost/js/ckfinder/core/connector/php/connector.php?command=QuickUpload&type=Flash';
	
};