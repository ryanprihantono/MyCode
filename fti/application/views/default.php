<?php 
	$timezone = "Asia/Jakarta";
	if(function_exists('date_default_timezone_set')) date_default_timezone_set($timezone);
?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Sistem Inventory FTI UKSW - <?php echo $web_title; ?></title>
<link type="text/css" rel="stylesheet" href="<?php echo base_url(); ?>asset/css/base.css" />
<link type="text/css" rel="stylesheet" href="<?php echo base_url(); ?>assets/grocery_crud/css/ui/simple/jquery-ui-1.8.10.custom.css" /> 
<link rel="stylesheet" type="text/css" href="<?php echo base_url(); ?>flexi/css/flexigrid.css" />
<?php 
if(isset($css_files)){
foreach($css_files as $file): ?>
	<link type="text/css" rel="stylesheet" href="<?php echo $file; ?>" />
<?php endforeach; }?>
<?php 
if(isset($js_files)){
	foreach($js_files as $file): ?>
	<script src="<?php echo $file; ?>"></script>
<?php
	endforeach;
}else{
?>
<script type="text/javascript" src="<?php echo base_url('asset/js/jquery.js'); ?>"></script>
<script type="text/javascript" src="<?php echo base_url('asset/js/jquery-ui-1.8.18.custom.min.js'); ?>"></script>
<script type="text/javascript" src="<?php echo base_url('flexi/flexigrid.js'); ?>"></script>
<script type="text/javascript" src="<?php echo base_url('flexi/flexibuild.js'); ?>"></script>
<?php
}

?>
<script type="text/javascript" src="<?php echo base_url(); ?>asset/js/accordion.js"></script>
<script type="text/javascript">
	$(document).ready(function(e) {
		$('.accordion').sayAccordion();
		$('#contract_date_from_field_box').hide();
		$('#contract_date_to_field_box').hide();
		$('#id_employee_replacement_field_box').hide();
		$('#date_terminated_field_box').hide();
		$('#field-status_vacancies').change(function(){
			if($('#field-status_vacancies').val() == 'Temporary'){
				$('#contract_date_from_field_box').show();
				$('#contract_date_to_field_box').show();
			}else{
				$('#contract_date_from_field_box').hide();
				$('#contract_date_to_field_box').hide();
			}
		});
		$('#field-status_recruitment').change(function(){
			if($('#field-status_recruitment').val() == 'Replacement'){
				$('#id_employee_replacement_field_box').show();
				$('#date_terminated_field_box').show();
			}else{
				$('#id_employee_replacement_field_box').hide();
				$('#date_terminated_field_box').hide();
			}
		});
    });
</script>
</head>
<body>
<div id="jj-layout">
  <div id="jj-info">
    <ul>
    	<li>
        	Welcome, <?php echo $this->session->userdata('username');?> :: <?php echo $this->session->userdata('role');?>
        </li>
    	<li>
        	<a href="<?php echo site_url('login/changePassword/edit/'.$this->session->userdata('id'))?>" class="span">Change Password</a> 
        </li>
        <li style="border-right: none;">
        	<a href="<?php echo site_url('login/doLogout')?>" class="span">Logout</a> 
        </li>
  	</ul>
  </div>
  <div class="clear"></div>
  <div id="jj-navigation">
	<div id="jj-logo" class="round-top">
        <font size="+2" color="#FF0000">Sistem </font><font size="+2">Inventory </font> <font size="+3" color="#0000FF">FTI </font><font size="+2">UKSW</font>
    </div>
    <div id="jj-mainmenu">
      <?php //include "navigasi/menu/$navigasi.php"; ?>
    </div>
  </div>
  <div class="clear"></div>
  <div id="jj-leftbar">
      <?php include "navigasi/sidebar/$sidebar.php"; ?>
  </div>
  <div id="jj-container2">
    <div id="jj-title"><?php echo $page_title; ?></div>
    <div id="jj-content" class="round-bottom">
      <?php include "content/$page_name.php"; ?>
    </div>
    <div id="jj-footer">
    	&copy; Copyright 2012. Sistem Inventory FTI UKSW
    </div>
  </div>
</div>
<div class="clear"></div>
</body>
</html>
