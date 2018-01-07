<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
<title>Yaris - <?php echo $title; ?></title>
<link rel="shortcut icon" href="<?php echo base_url(); ?>assets/main/images/logo/icon.png" />
<link type="text/css" rel="stylesheet" href="<?php echo base_url(); ?>assets/main/css/style.css" />
<link type="text/css" rel="stylesheet" href="<?php echo base_url(); ?>assets/main/css/skins/tango/skin.css" />
</head>
<body>

<div id="container">
	<div id="header">
    	<img src="<?php echo base_url(); ?>assets/main/images/logo/banner_yaris.jpg" width="550"/>
		<div class="header-contacts">
            <pre>
                Customer Care:
<?php foreach($contactus->result() as $row){ echo "		$row->type";?>		<?php echo " : $row->contact \n"; } ?>
            </pre>
        </div>
        <div class="header-contacts">
            <pre>
                OFFICE HOURS:
                Mon-Sat		08:00 - 17:00
                Sun & Public Holiday OFF    
            </pre>
        </div>
    </div>
    <div class="clear"></div>
    <div id="main-menu" class="round5">
        <ul>
            <li>
                <a href="<?php echo base_url().'main'; ?>">Home</a>
            </li>
            <li>
                <a href="<?php echo base_url().'main/product'; ?>">Products</a>
            </li>
            <li>
                <a href="<?php echo base_url().'main/how_to_order'; ?>">How To Order</a>
            </li>
            <?php
			if($this->session->userdata("username")!=FALSE){
			?>
            <li>
                <a href="<?php echo base_url().'main/payment_confirmation'; ?>">Payment Confirmation</a>
            </li>
            <?php
			}
			?>
            <li>
                <a href="<?php echo base_url().'main/contactus'; ?>">Contact Us</a>
            </li>
        </ul>
        <?php include "content/search.php"; ?>
    </div>
    <div id="content">
    	<div id="mainbar">
            <?php include "content/$mainbar.php"; ?>
        </div>
        <!-- end main-bar -->
        <div id="sidebar">
        	<?php include "sidebar/sidebar.php"; ?>
            <!-- end sidebar-box -->
        </div>
    </div>
    <div class="clear"></div>
</div>
<div id="footer">
	<div id="footer-contaner">
        <div id="cp">
            Copyright &copy; 2012 PT. . All Rights Reserved.
        </div>
		<div class="footer-contacts">
            <pre>
                Customer Care: 
<?php foreach($contactus->result() as $row){ echo "		$row->type";?>		<?php echo " : $row->contact \n"; } ?>    
            </pre>
        </div>
        <div class="footer-contacts">
            <pre>
                OFFICE HOURS:
                Mon-Sat		08:00 - 17:00
                Sun & Public Holiday OFF    
            </pre>
        </div>
    </div>
</div>
<script type="text/javascript" src="<?php echo base_url(); ?>assets/main/js/jquery-1.8.2.min.js"></script>
<script type="text/javascript" src="<?php echo base_url(); ?>assets/main/js/jquery.slider.js"></script>
<!--<script type="text/javascript" src="<?php //echo base_url(); ?>assets/main/js/jquery.gallery.js"></script>-->
<script type="text/javascript" src="<?php echo base_url(); ?>assets/main/js/jquery.cycle.js"></script>
<script type="text/javascript" src="<?php echo base_url(); ?>assets/main/js/jquery-ui-1.8.24.custom.min.js"></script>
<script type="text/javascript" src="<?php echo base_url(); ?>assets/main/js/jquery.uniform.min.js"></script>
<script type="text/javascript" src="<?php echo base_url(); ?>assets/main/js/jNotify.jquery.min.js"></script>
<script type="text/javascript" src="<?php echo base_url(); ?>assets/main/js/jquery.jqzoom-core-pack.js"></script>
<script type="text/javascript" src="<?php echo base_url(); ?>assets/main/js/jquery.jcarousel.min.js"></script>

<script type="text/javascript" src="<?php echo base_url(); ?>assets/main/js/jquery.ui.core.js"></script>
<script type="text/javascript" src="<?php echo base_url(); ?>assets/main/js/jquery.ui.widget.js"></script>
<script type="text/javascript" src="<?php echo base_url(); ?>assets/main/js/jquery.ui.tabs.js"></script>
<script type="text/javascript" src="<?php echo base_url(); ?>assets/main/js/jquery.ui.accordion.js"></script>

<?php

	foreach($css_files as $file): ?>
		<link type="text/css" rel="stylesheet" href="<?php echo $file; ?>" />
	<?php endforeach; ?>
	<?php foreach($js_files as $file): ?>
		<script src="<?php echo $file; ?>"></script>
	<?php endforeach;

?>

<script type="text/javascript">
	var base_url= '<?php echo base_url(); ?>';
</script>
<?php
	include "js/cart_js.php";
	include "js/login_js.php";
	include "js/category_js.php";
	include "js/search_js.php";
	if($mainbar=='register'){
		include "js/register_js.php";
	}
	if($mainbar=='profile' || $mainbar== "profile/transactions"){
		include "js/profile_js.php";
	}
?>

<script type="text/javascript">

	$(function(){
		var now = new Date()
		$("select.uniform, input.uniform, button.uniform").uniform();
		$( ".datepicker" ).datepicker({
			changeMonth:true,
			changeYear:true,
			yearRange:"c-60:c+0",
			dateFormat:"yy-mm-dd"
		});
	});

	//jqzoom
	$(document).ready(function(){  
        $('.jqzoom').jqzoom();
		$('.zoomPad').css('z-index','auto');
	}); 
	
	/* highslide */
/*
	hs.graphicsDir = '<?php //echo base_url(); ?>assets/main/images/js/gallery/';

	hs.align = 'center';
	hs.transitions = ['expand', 'crossfade'];
	hs.outlineType = 'rounded-white';
	hs.wrapperClassName = 'controls-in-heading';
	hs.fadeInOut = true;
	//hs.dimmingOpacity = 0.75;

	// Add the controlbar
	if (hs.addSlideshow) hs.addSlideshow({
		//slideshowGroup: 'group1',
		interval: 5000,
		repeat: false,
		useControls: true,
		fixedControls: false,
		overlayOptions: {
			opacity: 1,
			position: 'top right',
			hideOnMouseOut: false
		}
	});
*/
	/* Cycle */

	$('.sidebar-cycle').cycle({
		fx:     'scrollUp',
		speed:   300,
		timeout: 3000,
		//next:   '.sidebar-cycle',
		pause:   true
	});

	var anim = 'scrollLeft';
	var act = 1;
	var clicked=0;
	$('.product').before('<div id="nav">').cycle({
		fx:     'fade',
		speed:   300,
		timeout: 0,
		//next:   '.sidebar-cycle',
		pager:  '#nav',
		//pause:   1
		//before:  onBefore,
    	//after:   onAfter
	});
	/*function onBefore() {

		$('.product').cycle({fx:'ScrollLeft'});
		if(act<clicked){
			$('.product').cycle({fx:'ScrollLeft'});
		}
		else if(act>clicked){
			$('.product').cycle({fx:'scrollRight'});
		}
		act = clicked;
	}
	function onAfter() {

	}
	$('#nav a').click(function(){
		clicked = this.text;
	});*/
	$('.product').cycle({
		fx: 'fade',
		speed:   300,
		timeout: 0,
		next:   '.next',
    	prev:   '.prev'
	});
	
	jQuery(document).ready(function() {
		jQuery('#mycarousel').jcarousel({ scroll :1});
	});
	//slider

</script>
</body>
</html>